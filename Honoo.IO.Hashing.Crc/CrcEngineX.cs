using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineX : CrcEngine
    {
        #region Properties

        private readonly int _checksumByteLength;
        private readonly int _checksumStringLength;
        private readonly byte[] _init;
        private readonly int _move;
        private readonly byte[] _poly;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly byte[][] _table;
        private readonly byte[] _xorout;
        private byte[] _crc;

        #endregion Properties

        #region Construction

        internal CrcEngineX(string algorithmName, int checksumSize, bool refin, bool refout, byte[] poly, byte[] init, byte[] xorout)
            : base(algorithmName, checksumSize, false)
        {
            if (checksumSize <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(checksumSize));
            }
            _checksumByteLength = (int)Math.Ceiling(checksumSize / 8d);
            _checksumStringLength = (int)Math.Ceiling(checksumSize / 4d);
            int rem = checksumSize % 8;
            _move = rem > 0 ? 8 - rem : 0;
            _refin = refin;
            _refout = refout;
            _poly = ParseS2(poly, _move, _refin);
            _init = ParseS2(init, _move, _refin);
            _xorout = xorout;
            _crc = (byte[])_init.Clone();
        }

        internal CrcEngineX(string algorithmName, int checksumSize, bool refin, bool refout, byte[][] table, byte[] init, byte[] xorout)
            : base(algorithmName, checksumSize, true)
        {
            if (checksumSize <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(checksumSize));
            }
            _checksumByteLength = (int)Math.Ceiling(checksumSize / 8d);
            _checksumStringLength = (int)Math.Ceiling(checksumSize / 4d);
            int rem = checksumSize % 8;
            _move = rem > 0 ? 8 - rem : 0;
            _refin = refin;
            _refout = refout;
            _table = table;
            _init = init;
            _xorout = xorout;
            _crc = (byte[])_init.Clone();
        }

        #endregion Construction

        internal static byte[][] GenerateReversedTable(byte[] reversedPoly)
        {
            byte[][] table = new byte[256][];
            for (int i = 0; i < 256; i++)
            {
                byte[] data = new byte[reversedPoly.Length];
                data[data.Length - 1] = (byte)i;
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

        internal static byte[][] GenerateTable(byte[] poly)
        {
            byte[][] table = new byte[256][];
            for (int i = 0; i < 256; i++)
            {
                byte[] data = new byte[poly.Length];
                data[0] = (byte)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data[0] & 0x80) == 0x80)
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

        internal static byte[] ParseS1(string input, int checksumLength)
        {
            byte[] result = new byte[checksumLength];
            int stringLength = checksumLength * 2;
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
                result[i] = Convert.ToByte(input.Substring(i * 2, 2), 16);
            }
            return result;
        }

        internal static byte[] ParseS2(byte[] input, int move, bool reverse)
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
                result.Append(Convert.ToString(_crc[i], 16).PadLeft(2, '0'));
            }
            _crc = (byte[])_init.Clone();
            if (result.Length > _checksumStringLength)
            {
                return result.ToString(result.Length - _checksumStringLength, _checksumStringLength).ToUpperInvariant();
            }
            else
            {
                return result.ToString().ToUpperInvariant();
            }
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
                for (int i = 0; i < result.Length; i++)
                {
                    result[result.Length - 1 - i] = _crc[i];
                }
            }
            else
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = _crc[i];
                }
            }
            _crc = (byte[])_init.Clone();
            return result;
        }

        internal override void Reset()
        {
            _crc = (byte[])_init.Clone();
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
                _crc[0] ^= input;
                for (int j = 0; j < 8; j++)
                {
                    if ((_crc[0] & 0x80) == 0x80)
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
                byte[] match = _table[_crc[_crc.Length - 1] ^ input];
                ShiftRight(_crc, 8);
                Xor(_crc, match);
            }
            else
            {
                byte[] match = _table[_crc[0] ^ input];
                ShiftLeft(_crc, 8);
                Xor(_crc, match);
            }
        }

        private static byte Reverse(byte input)
        {
            input = (byte)((input & 0x55) << 1 | (input >> 1) & 0x55);
            input = (byte)((input & 0x33) << 2 | (input >> 2) & 0x33);
            input = (byte)((input & 0x0F) << 4 | (input >> 4) & 0x0F);
            return input;
        }

        private static void Reverse(byte[] input)
        {
            byte tmp;
            for (int i = 0; i < (int)Math.Ceiling(input.Length / 2d); i++)
            {
                tmp = Reverse(input[input.Length - 1 - i]);
                input[input.Length - 1 - i] = Reverse(input[i]);
                input[i] = tmp;
            }
        }

        private static void ShiftLeft(byte[] input, int bits)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                input[i] = (byte)((input[i] << bits) | (input[i + 1] >> (8 - bits)));
            }
            input[input.Length - 1] <<= bits;
        }

        private static void ShiftRight(byte[] input, int bits)
        {
            for (int i = input.Length - 1; i >= 1; i--)
            {
                input[i] = (byte)((input[i] >> bits) | (input[i - 1] << (8 - bits)));
            }
            input[0] >>= bits;
        }

        private static void Xor(byte[] inputModified, byte[] input2)
        {
            for (int i = 0; i < inputModified.Length; i++)
            {
                inputModified[i] ^= input2[i];
            }
        }
    }
}