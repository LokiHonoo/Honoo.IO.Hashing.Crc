using System;

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
        private readonly CrcTableInfo _tableInfo = CrcTableInfo.None;
        private readonly int _width;
        private readonly byte[] _xoroutParsed;
        private byte[] _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override CrcTableInfo TableInfo => _tableInfo;
        internal override int Width => _width;

        #endregion Members

        #region Construction

        internal CrcEngineSharding8(int width, byte[] poly, byte[] init, byte[] xorout, bool refin, bool refout)
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
            _crc = (byte[])_initParsed.Clone();
        }

        #endregion Construction

        #region Table

        internal override CrcTable CloneTable()
        {
            return new CrcTable(_tableInfo, _core, null);
        }

        #endregion Table

        #region ComputeFinal

        internal override CrcValue ComputeFinal()
        {
            Finish();
            CrcValue result;
            if (_width > 64)
            {
                result = new CrcHexValue(CrcConverter.GetHex(_crc, _width, CrcCaseSensitivity.Lower), _width);
            }
            else if (_width > 56)
            {
                ulong value = _crc[_crc.Length - 1] & 0xFFUL;
                value |= (_crc[_crc.Length - 2] & 0xFFUL) << 8;
                value |= (_crc[_crc.Length - 3] & 0xFFUL) << 16;
                value |= (_crc[_crc.Length - 4] & 0xFFUL) << 24;
                value |= (_crc[_crc.Length - 5] & 0xFFUL) << 32;
                value |= (_crc[_crc.Length - 6] & 0xFFUL) << 40;
                value |= (_crc[_crc.Length - 7] & 0xFFUL) << 48;
                value |= (_crc[_crc.Length - 8] & 0xFFUL) << 56;
                result = new CrcUInt64Value(value, _width);
            }
            else if (_width > 48)
            {
                ulong value = _crc[_crc.Length - 1] & 0xFFUL;
                value |= (_crc[_crc.Length - 2] & 0xFFUL) << 8;
                value |= (_crc[_crc.Length - 3] & 0xFFUL) << 16;
                value |= (_crc[_crc.Length - 4] & 0xFFUL) << 24;
                value |= (_crc[_crc.Length - 5] & 0xFFUL) << 32;
                value |= (_crc[_crc.Length - 6] & 0xFFUL) << 40;
                value |= (_crc[_crc.Length - 7] & 0xFFUL) << 48;
                result = new CrcUInt64Value(value, _width);
            }
            else if (_width > 40)
            {
                ulong value = _crc[_crc.Length - 1] & 0xFFUL;
                value |= (_crc[_crc.Length - 2] & 0xFFUL) << 8;
                value |= (_crc[_crc.Length - 3] & 0xFFUL) << 16;
                value |= (_crc[_crc.Length - 4] & 0xFFUL) << 24;
                value |= (_crc[_crc.Length - 5] & 0xFFUL) << 32;
                value |= (_crc[_crc.Length - 6] & 0xFFUL) << 40;
                result = new CrcUInt64Value(value, _width);
            }
            else if (_width > 32)
            {
                ulong value = _crc[_crc.Length - 1] & 0xFFUL;
                value |= (_crc[_crc.Length - 2] & 0xFFUL) << 8;
                value |= (_crc[_crc.Length - 3] & 0xFFUL) << 16;
                value |= (_crc[_crc.Length - 4] & 0xFFUL) << 24;
                value |= (_crc[_crc.Length - 5] & 0xFFUL) << 32;
                result = new CrcUInt64Value(value, _width);
            }
            else if (_width > 24)
            {
                uint value = _crc[_crc.Length - 1] & 0xFFU;
                value |= (_crc[_crc.Length - 2] & 0xFFU) << 8;
                value |= (_crc[_crc.Length - 3] & 0xFFU) << 16;
                value |= (_crc[_crc.Length - 4] & 0xFFU) << 24;
                result = new CrcUInt32Value(value, _width);
            }
            else if (_width > 16)
            {
                uint value = _crc[_crc.Length - 1] & 0xFFU;
                value |= (_crc[_crc.Length - 2] & 0xFFU) << 8;
                value |= (_crc[_crc.Length - 3] & 0xFFU) << 16;
                result = new CrcUInt32Value(value, _width);
            }
            else if (_width > 8)
            {
                ushort value = _crc[_crc.Length - 1];
                value |= (ushort)(_crc[_crc.Length - 2] << 8);
                result = new CrcUInt16Value(value, _width);
            }
            else if (_width > 0)
            {
                result = new CrcUInt8Value((byte)_crc[_crc.Length - 1], _width);
            }
            else
            {
                return null;
            }
            _crc = (byte[])_initParsed.Clone();
            return result;
        }

        #endregion ComputeFinal

        #region Update byte

        internal override void Update(byte input)
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

        #endregion Update byte

        #region Update bytes

        internal override unsafe void Update(byte[] inputBuffer, int offset, int length)
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

        #endregion Update bytes

        internal override void Reset()
        {
            _crc = (byte[])_initParsed.Clone();
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