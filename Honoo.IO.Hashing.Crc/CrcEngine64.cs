﻿using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine64 : CrcEngine
    {
        #region Members

        private readonly int _checksumByteLength;
        private readonly int _checksumHexLength;
        private readonly CrcCore _core = CrcCore.UInt64;
        private readonly ulong _initParsed;
        private readonly int _moves;
        private readonly ulong _polyParsed;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly ulong[] _table;
        private readonly int _width;
        private readonly bool _withTable;
        private readonly ulong _xoroutParsed;
        private ulong _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override int Width => _width;

        internal override bool WithTable => _withTable;

        #endregion Members

        #region Construction

        internal CrcEngine64(int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool generateTable)
        {
            if (width <= 0 || width > 64)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 64.", nameof(width));
            }
            _width = width;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            _refin = refin;
            _refout = refout;
            _withTable = generateTable;
            //
            _moves = 64 - width;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _table = generateTable ? _refin ? GenerateReversedTable(_polyParsed) : GenerateTable(_polyParsed) : null;
            _crc = _initParsed;
        }

        internal CrcEngine64(int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, ulong[] table)
        {
            if (width <= 0 || width > 64)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 64.", nameof(width));
            }
            _width = width;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            _refin = refin;
            _refout = refout;
            _withTable = table != null;
            //
            _moves = 64 - width;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _table = table;
            _crc = _initParsed;
        }

        #endregion Construction

        #region Table

        internal static ulong[] GenerateReversedTable(ulong polyParsed)
        {
            ulong[] table = new ulong[256];
            for (int i = 0; i < 256; i++)
            {
                ulong data = (ulong)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 1) == 1)
                    {
                        data = (data >> 1) ^ polyParsed;
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

        internal static ulong[] GenerateTable(ulong polyParsed)
        {
            ulong[] table = new ulong[256];
            for (int i = 0; i < 256; i++)
            {
                ulong data = (ulong)i << 56;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 0x8000000000000000) == 0x8000000000000000)
                    {
                        data = (data << 1) ^ polyParsed;
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

        internal override object CloneTable()
        {
            return _table?.Clone();
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

        internal override int ComputeFinal(Endian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            if (outputEndian == Endian.LittleEndian)
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
            checksum = (ushort)_crc;
            _crc = _initParsed;
            return _width > 16;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = (uint)_crc;
            _crc = _initParsed;
            return _width > 32;
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
            if (_withTable)
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
            else
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
        }

        private void UpdateWithoutTable(byte input)
        {
            _crc ^= (ulong)input << 56;
            for (int j = 0; j < 8; j++)
            {
                if ((_crc & 0x8000000000000000) == 0x8000000000000000)
                {
                    _crc = (_crc << 1) ^ _polyParsed;
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
                    _crc = (_crc >> 1) ^ _polyParsed;
                }
                else
                {
                    _crc >>= 1;
                }
            }
        }

        private void UpdateWithTable(byte input)
        {
            _crc = (_crc << 8) ^ _table[((_crc >> 56) & 0xFF) ^ input];
        }

        private void UpdateWithTableRef(byte input)
        {
            _crc = (_crc >> 8) ^ _table[(_crc & 0xFF) ^ input];
        }

        #endregion Update byte

        #region Update bytes

        internal override unsafe void Update(byte[] inputBuffer, int offset, int length)
        {
            if (_withTable)
            {
                if (_refin)
                {
                    fixed (byte* inputPointer = inputBuffer)
                    {
                        UpdateWithTableRef(inputPointer, length);
                    }
                }
                else
                {
                    fixed (byte* inputPointer = inputBuffer)
                    {
                        UpdateWithTable(inputPointer, length);
                    }
                }
            }
            else
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

        private unsafe void UpdateWithTable(byte* inputPointer, int length)
        {
            fixed (ulong* tablePointer = _table)
            {
                while (length >= 32)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[7]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[8]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[9]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[10]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[11]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[12]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[13]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[14]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[15]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[16]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[17]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[18]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[19]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[20]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[21]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[22]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[23]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[24]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[25]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[26]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[27]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[28]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[29]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[30]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[31]];
                    inputPointer += 32;
                    length -= 32;
                }
                if (length >= 16)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[7]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[8]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[9]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[10]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[11]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[12]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[13]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[14]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[15]];
                    inputPointer += 16;
                    length -= 16;
                }
                if (length >= 8)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[7]];
                    inputPointer += 8;
                    length -= 8;
                }
                if (length >= 4)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[3]];
                    inputPointer += 4;
                    length -= 4;
                }
                if (length >= 2)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[1]];
                    inputPointer += 2;
                    length -= 2;
                }
                if (length > 0)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 56) & 0xFF) ^ inputPointer[0]];
                }
            }
        }

        private unsafe void UpdateWithTableRef(byte* inputPointer, int length)
        {
            fixed (ulong* tablePointer = _table)
            {
                while (length >= 32)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[7]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[8]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[9]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[10]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[11]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[12]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[13]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[14]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[15]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[16]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[17]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[18]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[19]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[20]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[21]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[22]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[23]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[24]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[25]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[26]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[27]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[28]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[29]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[30]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[31]];
                    inputPointer += 32;
                    length -= 32;
                }
                if (length >= 16)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[7]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[8]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[9]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[10]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[11]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[12]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[13]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[14]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[15]];
                    inputPointer += 16;
                    length -= 16;
                }
                if (length >= 8)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[7]];
                    inputPointer += 8;
                    length -= 8;
                }
                if (length >= 4)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[3]];
                    inputPointer += 4;
                    length -= 4;
                }
                if (length >= 2)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    inputPointer += 2;
                    length -= 2;
                }
                if (length > 0)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                }
            }
        }

        #endregion Update bytes

        internal override void Reset()
        {
            _crc = _initParsed;
        }

        private static string GetBinaryString(ulong input, int width)
        {
            string result = Convert.ToString((long)input, 2).PadLeft(64, '0');
            if (result.Length > width)
            {
                result = result.Substring(result.Length - width, width);
            }
            return result;
        }

        private static string GetHexString(ulong input, int hexLength)
        {
            string result = Convert.ToString((long)input, 16).PadLeft(16, '0');
            if (result.Length > hexLength)
            {
                result = result.Substring(result.Length - hexLength, hexLength);
            }
            return result;
        }

        private static ulong Parse(ulong input, int moves, bool reverse)
        {
            if (moves > 0)
            {
                input <<= moves;
            }
            if (reverse)
            {
                input = Reverse(input);
            }
            return input;
        }

        private static ulong Reverse(ulong input)
        {
            input = (input & 0x5555555555555555) << 1 | (input >> 1) & 0x5555555555555555;
            input = (input & 0x3333333333333333) << 2 | (input >> 2) & 0x3333333333333333;
            input = (input & 0x0F0F0F0F0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F0F0F0F0F;
            input = (input & 0x00FF00FF00FF00FF) << 8 | (input >> 8) & 0x00FF00FF00FF00FF;
            input = (input & 0x0000FFFF0000FFFF) << 16 | (input >> 16) & 0x0000FFFF0000FFFF;
            input = (input & 0x00000000FFFFFFFF) << 32 | (input >> 32) & 0x00000000FFFFFFFF;
            return input;
        }

        private static ulong TruncateLeft(ulong input, int bits)
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