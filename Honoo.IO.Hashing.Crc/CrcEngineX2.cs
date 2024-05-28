using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineX2 : CrcEngine
    {
        #region Properties

        private readonly uint[] _init;
        private readonly int _moves;
        private readonly uint[] _poly;
        private readonly uint[][] _table;
        private readonly uint[] _xorout;
        private uint[] _crc;

        #endregion Properties

        #region Construction

        internal CrcEngineX2(int width, bool refin, bool refout, string poly, string init, string xorout, bool generateTable)
            : base(width, refin, refout, generateTable)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(width));
            }
            int rem = width % 32;
            _moves = rem > 0 ? 32 - rem : 0;
            _poly = Convent(poly, width, _moves);
            _init = Convent(init, width, _moves);
            _xorout = Convent(xorout, width, _moves);
            Parse(_poly, _moves, _refin);
            Parse(_init, _moves, _refin);
            _table = generateTable ? _refin ? GenerateReversedTable(_poly) : GenerateTable(_poly) : null;
            _crc = (uint[])_init.Clone();
        }

        internal CrcEngineX2(int width, bool refin, bool refout, string poly, string init, string xorout, uint[][] table)
            : base(width, refin, refout, true)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(width));
            }
            int rem = width % 32;
            _moves = rem > 0 ? 32 - rem : 0;
            _poly = Convent(poly, width, _moves);
            _init = Convent(init, width, _moves);
            _xorout = Convent(xorout, width, _moves);
            Parse(_poly, _moves, _refin);
            Parse(_init, _moves, _refin);
            _table = table;
            _crc = (uint[])_init.Clone();
        }

        #endregion Construction

        internal static uint[][] GenerateReversedTable(uint[] reversedPoly)
        {
            uint[][] table = new uint[256][];
            for (int i = 0; i < 256; i++)
            {
                uint[] data = new uint[reversedPoly.Length];
                data[data.Length - 1] = (uint)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data[data.Length - 1] & 1) == 1)
                    {
                        ShiftRight(data, 1);
                        Xor(data, reversedPoly);
                    }
                    else
                    {
                        ShiftRight(data, 1);
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal static uint[][] GenerateTable(uint[] poly)
        {
            uint[][] table = new uint[256][];
            for (int i = 0; i < 256; i++)
            {
                uint[] data = new uint[poly.Length];
                data[0] = (uint)i << 24;
                for (int j = 0; j < 8; j++)
                {
                    if ((data[0] & 0x80000000) == 0x80000000)
                    {
                        ShiftLeft(data, 1);
                        Xor(data, poly);
                    }
                    else
                    {
                        ShiftLeft(data, 1);
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal override string ComputeFinal()
        {
            Finish();
            string result = GetString(_crc, _checksumHexLength);
            _crc = (uint[])_init.Clone();
            return result;
        }

        internal override int ComputeFinal(bool littleEndian, byte[] output, int offset)
        {
            Finish();
            if (littleEndian)
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
                    output[i + offset] = (byte)(_crc[j] << m);
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
                    output[_checksumByteLength - 1 - i + offset] = (byte)(_crc[j] >> m);
                    m += 8;
                }
            }
            _crc = (uint[])_init.Clone();
            return _checksumByteLength;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = (byte)_crc[_crc.Length - 1];
            _crc = (uint[])_init.Clone();
            return _width > 8;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = (ushort)_crc[_crc.Length - 1];
            _crc = (uint[])_init.Clone();
            return _width > 16;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            _crc = (uint[])_init.Clone();
            return _width > 32;
        }

        internal override bool ComputeFinal(out ulong checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            if (_crc.Length > 1) checksum |= (_crc[_crc.Length - 1 - 1] & 0xFFFFFFFFUL) << 32;
            _crc = (uint[])_init.Clone();
            return _width > 64;
        }

        internal override void Reset()
        {
            _crc = (uint[])_init.Clone();
        }

        protected override void UpdateWithoutTable(byte input)
        {
            if (_refin)
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
                        ShiftRight(_crc, 1);
                        Xor(_crc, _poly);
                    }
                    else
                    {
                        ShiftRight(_crc, 1);
                    }
                }
            }
            else
            {
                for (int i = _crc.Length - 1; i >= 1; i--)
                {
                    _crc[i] ^= 0;
                }
                _crc[0] ^= (uint)input << 24;
                for (int j = 0; j < 8; j++)
                {
                    if ((_crc[0] & 0x80000000) == 0x80000000)
                    {
                        ShiftLeft(_crc, 1);
                        Xor(_crc, _poly);
                    }
                    else
                    {
                        ShiftLeft(_crc, 1);
                    }
                }
            }
        }

        protected override void UpdateWithTable(byte input)
        {
            if (_refin)
            {
                uint[] match = _table[(_crc[_crc.Length - 1] & 0xFF) ^ input];
                ShiftRight(_crc, 8);
                Xor(_crc, match);
            }
            else
            {
                uint[] match = _table[((_crc[0] >> 24) & 0xFF) ^ input];
                ShiftLeft(_crc, 8);
                Xor(_crc, match);
            }
        }

        private static uint[] Convent(string input, int width, int truncates)
        {
            uint[] result = new uint[(int)Math.Ceiling(width / 32d)];
            int hexLength = result.Length * 8;
            if (input.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase) || input.StartsWith("&h", StringComparison.InvariantCultureIgnoreCase))
            {
                input = input.Substring(2, input.Length - 2).Replace("_", null).Replace("-", null);
            }
            else
            {
                input = input.Replace("_", null).Replace("-", null);
            }
            if (input.Length > hexLength)
            {
                input = input.Substring(input.Length - hexLength, hexLength);
            }
            else if (input.Length < hexLength)
            {
                input = input.PadLeft(hexLength, '0');
            }
            int j = -1;
            int m = 24;
            for (int i = 0; i < input.Length; i += 2)
            {
                if (i % 8 == 0)
                {
                    j++;
                    m = 24;
                }
                result[j] |= Convert.ToUInt32(input.Substring(i, 2), 16) << m;
                m -= 8;
            }
            if (truncates > 0)
            {
                result[0] <<= truncates;
                result[0] >>= truncates;
            }
            return result;
        }

        private static string GetString(uint[] input, int hexLength)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 16).PadLeft(8, '0'));
            }
            return result.Length > hexLength ? result.ToString(result.Length - hexLength, hexLength) : result.ToString();
        }

        private static void Parse(uint[] input, int moves, bool reverse)
        {
            if (moves > 0)
            {
                ShiftLeft(input, moves);
            }
            if (reverse)
            {
                Reverse(input);
            }
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

        private static void Reverse(uint[] input)
        {
            uint tmp;
            for (int i = 0; i < (int)Math.Ceiling(input.Length / 2d); i++)
            {
                tmp = Reverse(input[input.Length - 1 - i]);
                input[input.Length - 1 - i] = Reverse(input[i]);
                input[i] = tmp;
            }
        }

        private static void ShiftLeft(uint[] input, int bits)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                input[i] = (input[i] << bits) | (input[i + 1] >> (32 - bits));
            }
            input[input.Length - 1] <<= bits;
        }

        private static void ShiftRight(uint[] input, int bits)
        {
            for (int i = input.Length - 1; i >= 1; i--)
            {
                input[i] = (input[i] >> bits) | (input[i - 1] << (32 - bits));
            }
            input[0] >>= bits;
        }

        private static void Xor(uint[] input, uint[] input2)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] ^= input2[i];
            }
        }

        private void Finish()
        {
            if (_refout ^ _refin)
            {
                Reverse(_crc);
            }
            if (_moves > 0 && !_refout)
            {
                ShiftRight(_crc, _moves);
            }
            Xor(_crc, _xorout);
        }
    }
}