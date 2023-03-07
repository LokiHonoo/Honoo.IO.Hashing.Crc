using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineX2 : CrcEngine
    {
        #region Properties

        private readonly int _checksumByteLength;
        private readonly int _checksumStringLength;
        private readonly uint[] _init;
        private readonly int _move;
        private readonly uint[] _poly;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly uint[][] _table;
        private readonly uint[] _xorout;
        private uint[] _crc;

        #endregion Properties

        #region Construction

        internal CrcEngineX2(string algorithmName, int checksumSize, bool refin, bool refout, uint[] poly, uint[] init, uint[] xorout)
            : base(algorithmName, checksumSize, false)
        {
            if (checksumSize <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(checksumSize));
            }
            _checksumByteLength = (int)Math.Ceiling(checksumSize / 8d);
            _checksumStringLength = (int)Math.Ceiling(checksumSize / 4d);
            int rem = checksumSize % 32;
            _move = rem > 0 ? 32 - rem : 0;
            _refin = refin;
            _refout = refout;
            _poly = ParseS2(poly, _move, _refin);
            _init = ParseS2(init, _move, _refin);
            _xorout = xorout;
            _crc = (uint[])_init.Clone();
        }

        internal CrcEngineX2(string algorithmName, int checksumSize, bool refin, bool refout, uint[][] table, uint[] init, uint[] xorout)
            : base(algorithmName, checksumSize, true)
        {
            if (checksumSize <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(checksumSize));
            }
            _checksumByteLength = (int)Math.Ceiling(checksumSize / 8d);
            _checksumStringLength = (int)Math.Ceiling(checksumSize / 4d);
            int rem = checksumSize % 32;
            _move = rem > 0 ? 32 - rem : 0;
            _refin = refin;
            _refout = refout;
            _table = table;
            _init = init;
            _xorout = xorout;
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

        internal static uint[] ParseS1(string input, int checksumLength)
        {
            uint[] result = new uint[(int)Math.Ceiling(checksumLength / 4d)];
            int stringLength = result.Length * 4 * 2;
            if (input.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase) || input.StartsWith("&h", StringComparison.InvariantCultureIgnoreCase))
            {
                input = input.Substring(2, input.Length - 2).Replace("_", null);
            }
            else
            {
                input = input.Replace("_", null).Replace("-", null);
            }
            if (input.Length > stringLength)
            {
                input = input.Substring(input.Length - stringLength, stringLength);
            }
            else if (input.Length < stringLength)
            {
                input = input.PadLeft(stringLength, '0');
            }
            for (int i = 0; i < result.Length; i++)
            {
                int offset = i * 8;
                int m = 24;
                while (offset < input.Length)
                {
                    result[i] |= Convert.ToUInt32(input.Substring(offset, 2), 16) << m;
                    offset += 2;
                    m -= 8;
                    if (offset % 8 == 0)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        internal static uint[] ParseS2(uint[] input, int move, bool reverse)
        {
            if (move > 0)
            {
                ShiftLeft(input, move);
            }
            if (reverse)
            {
                Reverse(input);
            }
            return input;
        }

        internal override string DoFinal()
        {
            if (_refout ^ _refin)
            {
                Reverse(_crc);
            }
            if (_move > 0 && !_refout)
            {
                ShiftRight(_crc, _move);
            }
            Xor(_crc, _xorout);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < _crc.Length; i++)
            {
                result.Append(Convert.ToString(_crc[i], 16).PadLeft(8, '0'));
            }
            _crc = (uint[])_init.Clone();
            if (result.Length > _checksumStringLength)
            {
                return result.ToString(result.Length - _checksumStringLength, _checksumStringLength).ToUpperInvariant();
            }
            else
            {
                return result.ToString().ToUpperInvariant();
            }

            // byte[] result = DoFinal(false);
            // return BitConverter.ToString(result).Replace("-", null);
        }

        internal override byte[] DoFinal(bool littleEndian)
        {
            if (_refout ^ _refin)
            {
                Reverse(_crc);
            }
            if (_move > 0 && !_refout)
            {
                ShiftRight(_crc, _move);
            }
            Xor(_crc, _xorout);
            byte[] result = new byte[_checksumByteLength];
            if (littleEndian)
            {
                int j = -1;
                int m = 3;
                for (int i = 0; i < result.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        j++;
                        m = 3;
                    }
                    result[i] = (byte)(_crc[j] << (m * 8));
                    m--;
                }
            }
            else
            {
                int j = _crc.Length;
                int m = 0;
                for (int i = 0; i < result.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        j--;
                        m = 0;
                    }
                    result[result.Length - 1 - i] = (byte)(_crc[j] >> (m * 8));
                    m++;
                }
            }
            _crc = (uint[])_init.Clone();
            return result;
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

        private static void Xor(uint[] inputModified, uint[] input2)
        {
            for (int i = 0; i < inputModified.Length; i++)
            {
                inputModified[i] ^= input2[i];
            }
        }
    }
}