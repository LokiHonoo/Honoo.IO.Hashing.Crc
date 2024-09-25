﻿using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine16 : CrcEngine
    {
        #region Members

        private readonly int _checksumByteLength;
        private readonly int _checksumHexLength;
        private readonly CrcCore _core = CrcCore.UInt16;
        private readonly ushort _initParsed;
        private readonly int _moves;
        private readonly ushort _polyParsed;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly CrcTableInfo _tableInfo;
        private readonly int _width;
        private readonly ushort _xoroutParsed;
        private ushort _crc;
        private ushort[] _table;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override CrcTableInfo TableInfo => _tableInfo;
        internal override int Width => _width;

        #endregion Members

        #region Construction

        internal CrcEngine16(int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, CrcTableInfo tableInfo)
        {
            if (width <= 0 || width > 16)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
            }
            _width = width;
            _refin = refin;
            _refout = refout;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            _moves = 16 - width;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            switch (tableInfo)
            {
                case CrcTableInfo.None: _tableInfo = tableInfo; break;
                default: _table = _refin ? GenerateTableRef(_polyParsed) : GenerateTable(_polyParsed); _tableInfo = CrcTableInfo.Standard; break;
            }
            _crc = _initParsed;
        }

        internal CrcEngine16(int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, ushort[] table)
        {
            if (width <= 0 || width > 16)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
            }
            _width = width;
            _refin = refin;
            _refout = refout;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            _moves = 16 - width;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _table = table;
            _tableInfo = table == null ? CrcTableInfo.None : CrcTableInfo.Standard;
            _crc = _initParsed;
        }

        protected override void Dispose(bool disposing)
        {
            _table = null;
        }

        #endregion Construction

        #region Table

        internal static ushort[] GenerateTable(ushort polyParsed)
        {
            ushort[] table = new ushort[256];
            for (int i = 0; i < 256; i++)
            {
                ushort data = (ushort)(i << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 0x8000) == 0x8000)
                    {
                        data = (ushort)((data << 1) ^ polyParsed);
                    }
                    else
                    {
                        data <<= 1;
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal static ushort[] GenerateTableRef(ushort polyParsed)
        {
            ushort[] table = new ushort[256];
            for (int i = 0; i < 256; i++)
            {
                ushort data = (ushort)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 1) == 1)
                    {
                        data = (ushort)((data >> 1) ^ polyParsed);
                    }
                    else
                    {
                        data >>= 1;
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal override CrcTableData CloneTable()
        {
            return new CrcTableData(_core, _tableInfo, _table?.Clone());

        }

        #endregion Table

        #region ComputeFinal

        internal override string ComputeFinal(CrcStringFormat outputFormat)
        {
            Finish();
            string result;
            switch (outputFormat)
            {
                case CrcStringFormat.Binary: result = GetBinaryString(_crc, _width); break;
                case CrcStringFormat.Hex: result = GetHexString(_crc, _checksumHexLength); break;
                default: throw new ArgumentException("Invalid crc string format.", nameof(outputFormat));
            }
            _crc = _initParsed;
            return result;
        }

        internal override int ComputeFinal(CrcEndian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            if (outputEndian == CrcEndian.LittleEndian)
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    outputBuffer[i + outputOffset] = (byte)(_crc >> (i * 8));
                }
            }
            else
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    outputBuffer[_checksumByteLength - 1 - i + outputOffset] = (byte)(_crc >> (i * 8));
                }
            }
            _crc = _initParsed;
            return _checksumByteLength;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = (byte)_crc;
            _crc = _initParsed;
            return _width > 8;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _initParsed;
            return false;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _initParsed;
            return false;
        }

        internal override bool ComputeFinal(out ulong checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _initParsed;
            return false;
        }

        #endregion ComputeFinal

        #region Update byte

        internal override void Update(byte input)
        {
            if (_tableInfo == CrcTableInfo.None)
            {
                if (_refin)
                {
                    UpdateWithoutTableRef(input);
                }
                else
                {
                    UpdateWithoutTable(input);
                }
            }
            else
            {
                if (_refin)
                {
                    UpdateWithTableRef(input);
                }
                else
                {
                    UpdateWithTable(input);
                }
            }
        }

        private void UpdateWithoutTable(byte input)
        {
            _crc ^= (ushort)(input << 8);
            for (int j = 0; j < 8; j++)
            {
                if ((_crc & 0x8000) == 0x8000)
                {
                    _crc = (ushort)((_crc << 1) ^ _polyParsed);
                }
                else
                {
                    _crc <<= 1;
                }
            }
        }

        private void UpdateWithoutTableRef(byte input)
        {
            _crc ^= input;
            for (int j = 0; j < 8; j++)
            {
                if ((_crc & 1) == 1)
                {
                    _crc = (ushort)((_crc >> 1) ^ _polyParsed);
                }
                else
                {
                    _crc >>= 1;
                }
            }
        }

        private void UpdateWithTable(byte input)
        {
            _crc = (ushort)((_crc << 8) ^ _table[(_crc >> 8) ^ input]);
        }

        private void UpdateWithTableRef(byte input)
        {
            _crc = (ushort)((_crc >> 8) ^ _table[(_crc ^ input) & 0xFF]);
        }

        #endregion Update byte

        #region Update bytes

        internal override unsafe void Update(byte[] inputBuffer, int offset, int length)
        {
            if (_tableInfo == CrcTableInfo.None)
            {
                if (_refin)
                {
                    UpdateWithoutTableRef(inputBuffer, offset, length);
                }
                else
                {
                    UpdateWithoutTable(inputBuffer, offset, length);
                }
            }
            else
            {
                fixed (byte* inputP = inputBuffer)
                {
                    if (_refin)
                    {
                        UpdateWithTableRef(inputP, length);
                    }
                    else
                    {
                        UpdateWithTable(inputP, length);
                    }
                }
            }
        }

        private void UpdateWithoutTable(byte[] inputBuffer, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                UpdateWithoutTable(inputBuffer[i]);
            }
        }

        private void UpdateWithoutTableRef(byte[] inputBuffer, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                UpdateWithoutTableRef(inputBuffer[i]);
            }
        }

        private unsafe void UpdateWithTable(byte* inputP, int length)
        {
            fixed (ushort* tableP = _table)
            {
                while (length >= 32)
                {
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[0]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[1]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[2]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[3]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[4]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[5]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[6]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[7]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[8]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[9]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[10]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[11]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[12]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[13]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[14]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[15]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[16]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[17]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[18]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[19]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[20]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[21]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[22]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[23]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[24]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[25]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[26]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[27]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[28]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[29]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[30]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[31]]);
                    inputP += 32;
                    length -= 32;
                }
                if (length >= 16)
                {
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[0]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[1]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[2]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[3]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[4]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[5]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[6]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[7]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[8]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[9]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[10]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[11]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[12]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[13]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[14]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[15]]);
                    inputP += 16;
                    length -= 16;
                }
                if (length >= 8)
                {
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[0]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[1]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[2]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[3]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[4]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[5]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[6]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[7]]);
                    inputP += 8;
                    length -= 8;
                }
                if (length >= 4)
                {
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[0]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[1]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[2]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[3]]);
                    inputP += 4;
                    length -= 4;
                }
                if (length >= 2)
                {
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[0]]);
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[1]]);
                    inputP += 2;
                    length -= 2;
                }
                if (length > 0)
                {
                    _crc = (ushort)((_crc << 8) ^ tableP[(_crc >> 8) ^ inputP[0]]);
                }
            }
        }

        private unsafe void UpdateWithTableRef(byte* inputP, int length)
        {
            fixed (ushort* tableP = _table)
            {
                while (length >= 32)
                {
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[0]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[1]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[2]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[3]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[4]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[5]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[6]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[7]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[8]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[9]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[10]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[11]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[12]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[13]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[14]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[15]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[16]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[17]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[18]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[19]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[20]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[21]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[22]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[23]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[24]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[25]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[26]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[27]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[28]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[29]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[30]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[31]) & 0xFF]);
                    inputP += 32;
                    length -= 32;
                }
                if (length >= 16)
                {
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[0]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[1]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[2]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[3]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[4]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[5]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[6]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[7]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[8]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[9]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[10]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[11]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[12]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[13]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[14]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[15]) & 0xFF]);
                    inputP += 16;
                    length -= 16;
                }
                if (length >= 8)
                {
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[0]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[1]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[2]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[3]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[4]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[5]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[6]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[7]) & 0xFF]);
                    inputP += 8;
                    length -= 8;
                }
                if (length >= 4)
                {
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[0]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[1]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[2]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[3]) & 0xFF]);
                    inputP += 4;
                    length -= 4;
                }
                if (length >= 2)
                {
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[0]) & 0xFF]);
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[1]) & 0xFF]);
                    inputP += 2;
                    length -= 2;
                }
                if (length > 0)
                {
                    _crc = (ushort)((_crc >> 8) ^ tableP[(_crc ^ inputP[0]) & 0xFF]);
                }
            }
        }

        #endregion Update bytes

        internal override void Reset()
        {
            _crc = _initParsed;
        }

        private static string GetBinaryString(ushort input, int width)
        {
            string result = Convert.ToString(input, 2).PadLeft(16, '0');
            if (result.Length > width)
            {
                result = result.Substring(result.Length - width, width);
            }
            return result;
        }

        private static string GetHexString(ushort input, int hexLength)
        {
            string result = Convert.ToString(input, 16).PadLeft(4, '0');
            if (result.Length > hexLength)
            {
                result = result.Substring(result.Length - hexLength, hexLength);
            }
            return result;
        }

        private static ushort Parse(ushort input, int moves, bool reverse)
        {
            if (moves > 0)
            {
                input <<= moves;
            }
            else
            {
            }
            if (reverse)
            {
                input = Reverse(input);
            }
            return input;
        }

        private static ushort Reverse(ushort input)
        {
            input = (ushort)((input & 0x5555) << 1 | (input >> 1) & 0x5555);
            input = (ushort)((input & 0x3333) << 2 | (input >> 2) & 0x3333);
            input = (ushort)((input & 0x0F0F) << 4 | (input >> 4) & 0x0F0F);
            input = (ushort)((input & 0x00FF) << 8 | (input >> 8) & 0x00FF);
            return input;
        }

        private static ushort TruncateLeft(ushort input, int bits)
        {
            if (bits > 0)
            {
                input <<= bits;
                input >>= bits;
            }
            return input;
        }

        private void Finish()
        {
            if (_refout ^ _refin)
            {
                _crc = Reverse(_crc);
            }
            if (_moves > 0 && !_refout)
            {
                _crc >>= _moves;
            }
            _crc ^= _xoroutParsed;
        }
    }
}