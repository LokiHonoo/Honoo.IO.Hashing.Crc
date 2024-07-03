using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineX : CrcEngine
    {
        #region Properties

        private readonly byte[] _init;
        private readonly int _moves;
        private readonly byte[] _poly;
        private readonly byte[][] _table;
        private readonly byte[] _xorout;
        private byte[] _crc;

        #endregion Properties

        #region Construction

        internal CrcEngineX(int width, bool refin, bool refout, string poly, string init, string xorout, bool generateTable)
            : base(width, refin, refout, generateTable)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(width));
            }
            int rem = width % 8;
            _moves = rem > 0 ? 8 - rem : 0;
            _poly = Convent(poly, width, _moves);
            _init = Convent(init, width, _moves);
            _xorout = Convent(xorout, width, _moves);
            Parse(_poly, _moves, _refin);
            Parse(_init, _moves, _refin);
            _table = generateTable ? _refin ? GenerateReversedTable(_poly) : GenerateTable(_poly) : null;
            _crc = (byte[])_init.Clone();
        }

        internal CrcEngineX(int width, bool refin, bool refout, string poly, string init, string xorout, byte[][] table)
            : base(width, refin, refout, true)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(width));
            }
            int rem = width % 8;
            _moves = rem > 0 ? 8 - rem : 0;
            _poly = Convent(poly, width, _moves);
            _init = Convent(init, width, _moves);
            _xorout = Convent(xorout, width, _moves);
            Parse(_poly, _moves, _refin);
            Parse(_init, _moves, _refin);
            _table = table;
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

        internal override string ComputeFinal()
        {
            Finish();
            string result = GetString(_crc, _checksumHexLength);
            _crc = (byte[])_init.Clone();
            return result;
        }

        internal override int ComputeFinal(Endian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            if (outputEndian == Endian.LittleEndian)
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
            _crc = (byte[])_init.Clone();
            return _checksumByteLength;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            _crc = (byte[])_init.Clone();
            return _width > 8;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            if (_crc.Length > 1) checksum |= (ushort)((_crc[_crc.Length - 1 - 1] & 0xFF) << 8);
            _crc = (byte[])_init.Clone();
            return _width > 16;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            if (_crc.Length > 1) checksum |= (_crc[_crc.Length - 1 - 1] & 0xFFU) << 8;
            if (_crc.Length > 2) checksum |= (_crc[_crc.Length - 1 - 2] & 0xFFU) << 16;
            if (_crc.Length > 3) checksum |= (_crc[_crc.Length - 1 - 3] & 0xFFU) << 24;
            _crc = (byte[])_init.Clone();
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
            _crc = (byte[])_init.Clone();
            return _width > 64;
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

        private static byte[] Convent(string input, int width, int truncates)
        {
            byte[] result = new byte[(int)Math.Ceiling(width / 8d)];
            int hexLength = result.Length * 2;
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
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(input.Substring(i * 2, 2), 16);
            }
            if (truncates > 0)
            {
                result[0] <<= truncates;
                result[0] >>= truncates;
            }
            return result;
        }

        private static string GetString(byte[] input, int hexLength)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 16).PadLeft(2, '0'));
            }
            return result.Length > hexLength ? result.ToString(result.Length - hexLength, hexLength) : result.ToString();
        }

        private static void Parse(byte[] input, int moves, bool reverse)
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

        private static void Xor(byte[] input, byte[] input2)
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