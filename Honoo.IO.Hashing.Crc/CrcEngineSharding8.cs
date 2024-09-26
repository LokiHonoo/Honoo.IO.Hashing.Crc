using System;
using System.Collections.Generic;
using System.Text;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineSharding8 : CrcEngine
    {
        #region Members

        private readonly int _checksumByteLength;
        private readonly int _checksumHexLength;
        private readonly CrcCore _core = CrcCore.Sharding8;
        private readonly byte[] _initParsed;
        private readonly int _moves;
        private readonly byte[] _polyParsed;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly CrcTableInfo _tableInfo;
        private readonly int _width;
        private readonly byte[] _xoroutParsed;
        private byte[] _crc;
        private byte[][] _table;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override CrcTableInfo TableInfo => _tableInfo;
        internal override int Width => _width;

        #endregion Members

        #region Construction

        internal CrcEngineSharding8(int width, bool refin, bool refout, byte[] poly, byte[] init, byte[] xorout, CrcTableInfo tableInfo)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0.", nameof(width));
            }
            _width = width;
            _refin = refin;
            _refout = refout;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            int rem = width % 8;
            _moves = rem > 0 ? 8 - rem : 0;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            switch (tableInfo)
            {
                case CrcTableInfo.None: _tableInfo = tableInfo; break;
                default: _table = _refin ? GenerateTableRef(_polyParsed) : GenerateTable(_polyParsed); _tableInfo = CrcTableInfo.Standard; break;
            }
            _crc = (byte[])_initParsed.Clone();
        }

        internal CrcEngineSharding8(int width, bool refin, bool refout, byte[] poly, byte[] init, byte[] xorout, byte[][] table)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0.", nameof(width));
            }
            _width = width;
            _refin = refin;
            _refout = refout;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            int rem = width % 8;
            _moves = rem > 0 ? 8 - rem : 0;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _table = table;
            _tableInfo = table == null ? CrcTableInfo.None : CrcTableInfo.Standard;
            _crc = (byte[])_initParsed.Clone();
        }

        protected override void Dispose(bool disposing)
        {
            _table = null;
        }

        #endregion Construction

        #region Table

        internal static byte[][] GenerateTable(byte[] polyParsed)
        {
            byte[][] table = new byte[256][];
            for (int i = 0; i < 256; i++)
            {
                byte[] data = new byte[polyParsed.Length];
                data[0] = (byte)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data[0] & 0x80) == 0x80)
                    {
                        data = ShiftLeft(data, 1);
                        data = Xor(data, polyParsed);
                    }
                    else
                    {
                        data = ShiftLeft(data, 1);
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal static byte[][] GenerateTableRef(byte[] polyParsed)
        {
            byte[][] table = new byte[256][];
            for (int i = 0; i < 256; i++)
            {
                byte[] data = new byte[polyParsed.Length];
                data[data.Length - 1] = (byte)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data[data.Length - 1] & 1) == 1)
                    {
                        data = ShiftRight(data, 1);
                        data = Xor(data, polyParsed);
                    }
                    else
                    {
                        data = ShiftRight(data, 1);
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal override CrcTableData CloneTable()
        {
            if (_table != null)
            {
                var table = new List<byte[]>();
                foreach (byte[] item in _table)
                {
                    table.Add((byte[])item.Clone());
                }
                return new CrcTableData(_core, _tableInfo, table.ToArray());
            }
            return new CrcTableData(_core, _tableInfo, null);
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
            _crc = (byte[])_initParsed.Clone();
            return result;
        }

        internal override int ComputeFinal(CrcEndian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            if (outputEndian == CrcEndian.LittleEndian)
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    outputBuffer[_checksumByteLength - 1 - i + outputOffset] = _crc[i];
                }
            }
            else
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    outputBuffer[i + outputOffset] = _crc[i];
                }
            }
            _crc = (byte[])_initParsed.Clone();
            return _checksumByteLength;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            _crc = (byte[])_initParsed.Clone();
            return _width > 8;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            if (_crc.Length > 1) checksum |= (ushort)((_crc[_crc.Length - 1 - 1] & 0xFF) << 8);
            _crc = (byte[])_initParsed.Clone();
            return _width > 16;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            if (_crc.Length > 1) checksum |= (_crc[_crc.Length - 1 - 1] & 0xFFU) << 8;
            if (_crc.Length > 2) checksum |= (_crc[_crc.Length - 1 - 2] & 0xFFU) << 16;
            if (_crc.Length > 3) checksum |= (_crc[_crc.Length - 1 - 3] & 0xFFU) << 24;
            _crc = (byte[])_initParsed.Clone();
            return _width > 32;
        }

        internal override bool ComputeFinal(out ulong checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            if (_crc.Length > 1) checksum |= (_crc[_crc.Length - 1 - 1] & 0xFFUL) << 8;
            if (_crc.Length > 2) checksum |= (_crc[_crc.Length - 1 - 2] & 0xFFUL) << 16;
            if (_crc.Length > 3) checksum |= (_crc[_crc.Length - 1 - 3] & 0xFFUL) << 24;
            if (_crc.Length > 4) checksum |= (_crc[_crc.Length - 1 - 4] & 0xFFUL) << 32;
            if (_crc.Length > 5) checksum |= (_crc[_crc.Length - 1 - 5] & 0xFFUL) << 40;
            if (_crc.Length > 6) checksum |= (_crc[_crc.Length - 1 - 6] & 0xFFUL) << 48;
            if (_crc.Length > 7) checksum |= (_crc[_crc.Length - 1 - 7] & 0xFFUL) << 56;
            _crc = (byte[])_initParsed.Clone();
            return _width > 64;
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
            for (int i = _crc.Length - 1; i >= 1; i--)
            {
                _crc[i] ^= 0;
            }
            _crc[0] ^= input;
            for (int j = 0; j < 8; j++)
            {
                if ((_crc[0] & 0x80) == 0x80)
                {
                    _crc = ShiftLeft(_crc, 1);
                    _crc = Xor(_crc, _polyParsed);
                }
                else
                {
                    _crc = ShiftLeft(_crc, 1);
                }
            }
        }

        private void UpdateWithoutTableRef(byte input)
        {
            for (int i = 0; i < _crc.Length - 1; i++)
            {
                _crc[i] ^= 0;
            }
            _crc[_crc.Length - 1] ^= input;
            for (int j = 0; j < 8; j++)
            {
                if ((_crc[_crc.Length - 1] & 1) == 1)
                {
                    _crc = ShiftRight(_crc, 1);
                    _crc = Xor(_crc, _polyParsed);
                }
                else
                {
                    _crc = ShiftRight(_crc, 1);
                }
            }
        }

        private void UpdateWithTable(byte input)
        {
            byte[] match = _table[_crc[0] ^ input];
            _crc = ShiftLeft(_crc, 8);
            _crc = Xor(_crc, match);
        }

        private void UpdateWithTableRef(byte input)
        {
            byte[] match = _table[_crc[_crc.Length - 1] ^ input];
            _crc = ShiftRight(_crc, 8);
            _crc = Xor(_crc, match);
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
                        UpdateWithTableRef(inputBuffer, offset, length);
                    }
                    else
                    {
                        UpdateWithTable(inputBuffer, offset, length);
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

        private void UpdateWithTable(byte[] inputBuffer, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                UpdateWithTable(inputBuffer[i]);
            }
        }

        private void UpdateWithTableRef(byte[] inputBuffer, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                UpdateWithTableRef(inputBuffer[i]);
            }
        }

        #endregion Update bytes

        internal override void Reset()
        {
            _crc = (byte[])_initParsed.Clone();
        }

        private static string GetBinaryString(byte[] input, int width)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 2).PadLeft(8, '0'));
            }
            if (result.Length > width)
            {
                result.Remove(0, result.Length - width);
            }
            return result.ToString();
        }

        private static string GetHexString(byte[] input, int hexLength)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 16).PadLeft(2, '0'));
            }
            if (result.Length > hexLength)
            {
                result.Remove(0, result.Length - hexLength);
            }
            return result.ToString();
        }

        private static byte[] Parse(byte[] input, int moves, bool reverse)
        {
            if (moves > 0)
            {
                input = ShiftLeft(input, moves);
            }
            if (reverse)
            {
                input = Reverse(input);
            }
            return input;
        }

        private static byte Reverse(byte input)
        {
            input = (byte)((input & 0x55) << 1 | (input >> 1) & 0x55);
            input = (byte)((input & 0x33) << 2 | (input >> 2) & 0x33);
            input = (byte)((input & 0x0F) << 4 | (input >> 4) & 0x0F);
            return input;
        }

        private static byte[] Reverse(byte[] input)
        {
            byte tmp;
            for (int i = 0; i < (int)Math.Ceiling(input.Length / 2d); i++)
            {
                tmp = Reverse(input[input.Length - 1 - i]);
                input[input.Length - 1 - i] = Reverse(input[i]);
                input[i] = tmp;
            }
            return input;
        }

        private static byte[] ShiftLeft(byte[] input, int bits)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                input[i] = (byte)((input[i] << bits) | (input[i + 1] >> (8 - bits)));
            }
            input[input.Length - 1] <<= bits;
            return input;
        }

        private static byte[] ShiftRight(byte[] input, int bits)
        {
            for (int i = input.Length - 1; i >= 1; i--)
            {
                input[i] = (byte)((input[i] >> bits) | (input[i - 1] << (8 - bits)));
            }
            input[0] >>= bits;
            return input;
        }

        private static byte[] TruncateLeft(byte[] input, int bits)
        {
            if (bits > 0)
            {
                input = ShiftLeft(input, bits);
                input = ShiftRight(input, bits);
            }
            return input;
        }

        private static byte[] Xor(byte[] input, byte[] input2)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] ^= input2[i];
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
                _crc = ShiftRight(_crc, _moves);
            }
            _crc = Xor(_crc, _xoroutParsed);
        }
    }
}