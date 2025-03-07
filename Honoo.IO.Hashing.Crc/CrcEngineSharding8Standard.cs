using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineSharding8Standard : CrcEngine
    {
        #region Members

        private readonly int _calculationLength;
        private readonly int _checksumByteLength;
        private readonly int _checksumHexLength;
        private readonly CrcCore _core = CrcCore.Sharding8;
        private readonly byte[] _initParsed;
        private readonly int _moves;
        private readonly byte[] _polyParsed;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly byte[] _table;
        private readonly CrcTableInfo _tableInfo = CrcTableInfo.Standard;
        private readonly int _width;
        private readonly byte[] _xoroutParsed;
        private byte[] _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override CrcTableInfo TableInfo => _tableInfo;
        internal override int Width => _width;

        #endregion Members

        #region Construction

        internal CrcEngineSharding8Standard(int width, byte[] poly, byte[] init, byte[] xorout, bool refin, bool refout, byte[] table)
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
            _calculationLength = _polyParsed.Length;
            _table = table ?? (_refin ? GenerateTableRef(_polyParsed) : GenerateTable(_polyParsed));
            _crc = (byte[])_initParsed.Clone();
        }

        #endregion Construction

        #region Table

        internal static byte[] GenerateTable(int width, byte[] poly, bool refin)
        {
            int rem = width % 8;
            int moves = rem > 0 ? 8 - rem : 0;
            byte[] polyParsed = Parse(poly, moves, refin);
            return refin ? GenerateTableRef(polyParsed) : GenerateTable(polyParsed);
        }

        internal override CrcTable CloneTable()
        {
            return new CrcTable(_tableInfo, _core, _table);
        }

        private static byte[] GenerateTable(byte[] polyParsed)
        {
            int l = polyParsed.Length;
            byte[] table = new byte[256 * l];
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
                for (int m = 0; m < data.Length; m++)
                {
                    table[i * l + m] = data[m];
                }
            }
            return table;
        }

        private static byte[] GenerateTableRef(byte[] polyParsed)
        {
            int l = polyParsed.Length;
            byte[] table = new byte[256 * l];
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
                for (int m = 0; m < data.Length; m++)
                {
                    table[i * l + m] = data[m];
                }
            }
            return table;
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
                UpdateWithTableRef(input);
            }
            else
            {
                UpdateWithTable(input);
            }
        }

        private void UpdateWithTable(byte input)
        {
            int index = (_crc[0] ^ input) * _calculationLength;
            _crc = ShiftLeft(_crc, 8);
            _crc = Xor(_crc, _table, index);
        }

        private void UpdateWithTableRef(byte input)
        {
            int index = (_crc[_crc.Length - 1] ^ input) * _calculationLength;
            _crc = ShiftRight(_crc, 8);
            _crc = Xor(_crc, _table, index);
        }

        #endregion Update byte

        #region Update bytes

        internal override unsafe void Update(byte[] inputBuffer, int offset, int length)
        {
            fixed (byte* inputP = inputBuffer)
            {
                if (_refin)
                {
                    UpdateWithTableRef(inputP, offset, length);
                }
                else
                {
                    UpdateWithTable(inputP, offset, length);
                }
            }
        }

        private unsafe void UpdateWithTable(byte* inputP, int offset, int length)
        {
            inputP += offset;
            fixed (byte* tableP = _table)
            fixed (byte* crc = _crc)
            {
                int index;
                while (length >= 16)
                {
                    index = (crc[0] ^ inputP[0]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[1]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[2]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[3]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[4]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[5]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[6]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[7]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[8]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[9]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[10]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[11]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[12]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[13]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[14]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (crc[0] ^ inputP[15]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    inputP += 16;
                    length -= 16;
                }
                while (length > 0)
                {
                    index = (crc[0] ^ inputP[0]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    inputP++;
                    length--;
                }
            }
        }

        private unsafe void UpdateWithTableRef(byte* inputP, int offset, int length)
        {
            inputP += offset;
            fixed (byte* tableP = _table)
            fixed (byte* crc = _crc)
            {
                int index;
                while (length >= 16)
                {
                    index = (_crc[_crc.Length - 1] ^ inputP[0]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[1]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[2]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[3]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[4]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[5]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[6]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[7]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[8]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[9]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[10]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[11]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[12]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[13]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[14]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = (_crc[_crc.Length - 1] ^ inputP[15]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    inputP += 16;
                    length -= 16;
                }
                while (length > 0)
                {
                    index = (_crc[_crc.Length - 1] ^ inputP[0]) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    inputP++;
                    length--;
                }
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

        private static unsafe byte* ShiftLeft(byte* input, int bits, int calculationLength)
        {
            if (bits > 0)
            {
                for (int i = 0; i < calculationLength - 1; i++)
                {
                    input[i] = (byte)((input[i] << bits) | (input[i + 1] >> (8 - bits)));
                }
                input[calculationLength - 1] <<= bits;
            }
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

        private static unsafe byte* ShiftRight(byte* input, int bits, int calculationLength)
        {
            if (bits > 0)
            {
                for (int i = calculationLength - 1; i >= 1; i--)
                {
                    input[i] = (byte)((input[i] >> bits) | (input[i - 1] << (8 - bits)));
                }
                input[0] >>= bits;
            }
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

        private static byte[] Xor(byte[] input, byte[] input2, int input2Offset)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] ^= input2[input2Offset + i];
            }
            return input;
        }

        private static unsafe byte* Xor(byte* input, byte* input2, int input2Offset, int calculationLength)
        {
            for (int i = 0; i < calculationLength; i++)
            {
                input[i] ^= input2[input2Offset + i];
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