using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineSharding32Standard : CrcEngine
    {
        #region Members

        private readonly int _calculationLength;
        private readonly int _checksumByteLength;
        private readonly int _checksumHexLength;
        private readonly CrcCore _core = CrcCore.Sharding32;
        private readonly uint[] _initParsed;
        private readonly int _moves;
        private readonly uint[] _polyParsed;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly uint[] _table;
        private readonly CrcTableInfo _tableInfo = CrcTableInfo.Standard;
        private readonly int _width;
        private readonly uint[] _xoroutParsed;
        private uint[] _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override CrcTableInfo TableInfo => _tableInfo;
        internal override int Width => _width;

        #endregion Members

        #region Construction

        internal CrcEngineSharding32Standard(int width, uint[] poly, uint[] init, uint[] xorout, bool refin, bool refout, uint[] table)
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
            int rem = width % 32;
            _moves = rem > 0 ? 32 - rem : 0;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _calculationLength = _polyParsed.Length;
            _table = table ?? (_refin ? GenerateTableRef(_polyParsed) : GenerateTable(_polyParsed));
            _crc = (uint[])_initParsed.Clone();
        }

        #endregion Construction

        #region Table

        internal static uint[] GenerateTable(int width, uint[] poly, bool refin)
        {
            int rem = width % 32;
            int moves = rem > 0 ? 32 - rem : 0;
            uint[] polyParsed = Parse(poly, moves, refin);
            return refin ? GenerateTableRef(polyParsed) : GenerateTable(polyParsed);
        }

        internal override CrcTable CloneTable()
        {
            return new CrcTable(_tableInfo, _core, _table);
        }

        private static uint[] GenerateTable(uint[] polyParsed)
        {
            int l = polyParsed.Length;
            uint[] table = new uint[256 * l];
            for (int i = 0; i < 256; i++)
            {
                uint[] data = new uint[l];
                data[0] = (uint)i << 24;
                for (int j = 0; j < 8; j++)
                {
                    if ((data[0] & 0x80000000) == 0x80000000)
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

        private static uint[] GenerateTableRef(uint[] polyParsed)
        {
            int l = polyParsed.Length;
            uint[] table = new uint[256 * l];
            for (int i = 0; i < 256; i++)
            {
                uint[] data = new uint[l];
                data[data.Length - 1] = (uint)i;
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
            _crc = (uint[])_initParsed.Clone();
            return result;
        }

        internal override int ComputeFinal(CrcEndian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            if (outputEndian == CrcEndian.LittleEndian)
            {
                int j = -1;
                int m = 24;
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    if (i % 4 == 0)
                    {
                        j++;
                        m = 24;
                    }
                    outputBuffer[i + outputOffset] = (byte)(_crc[j] << m);
                    m -= 8;
                }
            }
            else
            {
                int j = _crc.Length;
                int m = 0;
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    if (i % 4 == 0)
                    {
                        j--;
                        m = 0;
                    }
                    outputBuffer[_checksumByteLength - 1 - i + outputOffset] = (byte)(_crc[j] >> m);
                    m += 8;
                }
            }
            _crc = (uint[])_initParsed.Clone();
            return _checksumByteLength;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = (byte)_crc[_crc.Length - 1];
            _crc = (uint[])_initParsed.Clone();
            return _width > 8;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = (ushort)_crc[_crc.Length - 1];
            _crc = (uint[])_initParsed.Clone();
            return _width > 16;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            _crc = (uint[])_initParsed.Clone();
            return _width > 32;
        }

        internal override bool ComputeFinal(out ulong checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            if (_crc.Length > 1) checksum |= (_crc[_crc.Length - 1 - 1] & 0xFFFFFFFFUL) << 32;
            _crc = (uint[])_initParsed.Clone();
            return _width > 64;
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
            int index = ((int)(_crc[0] >> 24) ^ input) * _calculationLength;
            _crc = ShiftLeft(_crc, 8);
            _crc = Xor(_crc, _table, index);
        }

        private void UpdateWithTableRef(byte input)
        {
            int index = ((int)((_crc[_crc.Length - 1] ^ input) & 0xFF)) * _calculationLength;
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
            fixed (uint* tableP = _table)
            fixed (uint* crc = _crc)
            {
                int index;
                while (length >= 16)
                {
                    index = ((int)(crc[0] >> 24) ^ inputP[0]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[1]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[2]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[3]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[4]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[5]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[6]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[7]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[8]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[9]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[10]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[11]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[12]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[13]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[14]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)(crc[0] >> 24) ^ inputP[15]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    inputP += 16;
                    length -= 16;
                }
                while (length > 0)
                {
                    index = ((int)(crc[0] >> 24) ^ inputP[0]) * _calculationLength; Xor(ShiftLeft(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    inputP++;
                    length--;
                }
            }
        }

        private unsafe void UpdateWithTableRef(byte* inputP, int offset, int length)
        {
            inputP += offset;
            fixed (uint* tableP = _table)
            fixed (uint* crc = _crc)
            {
                int index;
                while (length >= 16)
                {
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[0]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[1]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[2]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[3]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[4]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[5]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[6]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[7]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[8]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[9]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[10]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[11]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[12]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[13]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[14]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[15]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    inputP += 16;
                    length -= 16;
                }
                while (length > 0)
                {
                    index = ((int)((crc[_calculationLength - 1] ^ inputP[0]) & 0xFF)) * _calculationLength; Xor(ShiftRight(crc, 8, _calculationLength), tableP, index, _calculationLength);
                    inputP++;
                    length--;
                }
            }
        }

        #endregion Update bytes

        internal override void Reset()
        {
            _crc = (uint[])_initParsed.Clone();
        }

        private static string GetBinaryString(uint[] input, int width)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 2).PadLeft(32, '0'));
            }
            if (result.Length > width)
            {
                result.Remove(0, result.Length - width);
            }
            return result.ToString();
        }

        private static string GetHexString(uint[] input, int hexLength)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 16).PadLeft(8, '0'));
            }
            if (result.Length > hexLength)
            {
                result.Remove(0, result.Length - hexLength);
            }
            return result.ToString();
        }

        private static uint[] Parse(uint[] input, int moves, bool reverse)
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

        private static uint Reverse(uint input)
        {
            input = (input & 0x55555555) << 1 | (input >> 1) & 0x55555555;
            input = (input & 0x33333333) << 2 | (input >> 2) & 0x33333333;
            input = (input & 0x0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F;
            input = (input & 0x00FF00FF) << 8 | (input >> 8) & 0x00FF00FF;
            input = (input & 0x0000FFFF) << 16 | (input >> 16) & 0x0000FFFF;
            return input;
        }

        private static uint[] Reverse(uint[] input)
        {
            uint tmp;
            for (int i = 0; i < (int)Math.Ceiling(input.Length / 2d); i++)
            {
                tmp = Reverse(input[input.Length - 1 - i]);
                input[input.Length - 1 - i] = Reverse(input[i]);
                input[i] = tmp;
            }
            return input;
        }

        private static uint[] ShiftLeft(uint[] input, int bits)
        {
            if (bits > 0)
            {
                for (int i = 0; i < input.Length - 1; i++)
                {
                    input[i] = (input[i] << bits) | (input[i + 1] >> (32 - bits));
                }
                input[input.Length - 1] <<= bits;
            }
            return input;
        }

        private static unsafe uint* ShiftLeft(uint* input, int bits, int calculationLength)
        {
            if (bits > 0)
            {
                for (int i = 0; i < calculationLength - 1; i++)
                {
                    input[i] = (input[i] << bits) | (input[i + 1] >> (32 - bits));
                }
                input[calculationLength - 1] <<= bits;
            }
            return input;
        }

        private static uint[] ShiftRight(uint[] input, int bits)
        {
            if (bits > 0)
            {
                for (int i = input.Length - 1; i >= 1; i--)
                {
                    input[i] = (input[i] >> bits) | (input[i - 1] << (32 - bits));
                }
                input[0] >>= bits;
            }
            return input;
        }

        private static unsafe uint* ShiftRight(uint* input, int bits, int calculationLength)
        {
            if (bits > 0)
            {
                for (int i = calculationLength - 1; i >= 1; i--)
                {
                    input[i] = (input[i] >> bits) | (input[i - 1] << (32 - bits));
                }
                input[0] >>= bits;
            }
            return input;
        }

        private static uint[] TruncateLeft(uint[] input, int bits)
        {
            if (bits > 0)
            {
                input = ShiftLeft(input, bits);
                input = ShiftRight(input, bits);
            }
            return input;
        }

        private static uint[] Xor(uint[] input, uint[] input2)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] ^= input2[i];
            }
            return input;
        }

        private static uint[] Xor(uint[] input, uint[] input2, int input2Offset)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] ^= input2[input2Offset + i];
            }
            return input;
        }

        private static unsafe uint* Xor(uint* input, uint* input2, int input2Offset, int calculationLength)
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