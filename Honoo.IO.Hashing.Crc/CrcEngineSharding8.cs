using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineSharding8 : CrcEngine
    {
        #region Members

        private readonly byte[] _initParsed;
        private readonly int _moves;
        private readonly byte[] _polyParsed;
        private readonly byte[][] _table;
        private readonly byte[] _xoroutParsed;
        private byte[] _crc;

        #endregion Members

        #region Construction

        internal CrcEngineSharding8(int width, bool refin, bool refout, byte[] poly, byte[] init, byte[] xorout, bool generateTable)
            : base(width, refin, refout, generateTable)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(width));
            }
            int rem = width % 8;
            _moves = rem > 0 ? 8 - rem : 0;
            _polyParsed = poly;
            _initParsed = init;
            _xoroutParsed = xorout;
            Parse(_polyParsed, _moves, _refin);
            Parse(_initParsed, _moves, _refin);
            _table = generateTable ? _refin ? GenerateReversedTable(_polyParsed) : GenerateTable(_polyParsed) : null;
            _crc = (byte[])_initParsed.Clone();
        }

        internal CrcEngineSharding8(int width, bool refin, bool refout, byte[] poly, byte[] init, byte[] xorout, byte[][] table)
            : base(width, refin, refout, true)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(width));
            }
            int rem = width % 8;
            _moves = rem > 0 ? 8 - rem : 0;
            _polyParsed = poly;
            _initParsed = init;
            _xoroutParsed = xorout;
            Parse(_polyParsed, _moves, _refin);
            Parse(_initParsed, _moves, _refin);
            _table = table;
            _crc = (byte[])_initParsed.Clone();
        }

        #endregion Construction

        internal static byte[][] GenerateReversedTable(byte[] reversedPolyParsed)
        {
            byte[][] table = new byte[256][];
            for (int i = 0; i < 256; i++)
            {
                byte[] data = new byte[reversedPolyParsed.Length];
                data[data.Length - 1] = (byte)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data[data.Length - 1] & 1) == 1)
                    {
                        ShiftRight(data, 1);
                        Xor(data, reversedPolyParsed);
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
                        ShiftLeft(data, 1);
                        Xor(data, polyParsed);
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

        internal override string ComputeFinal(NumericsStringFormat outputFormat)
        {
            Finish();
            string result;
            switch (outputFormat)
            {
                case NumericsStringFormat.Binary: result = GetBinaryString(_crc, _width); break;
                case NumericsStringFormat.Hex: result = GetHexString(_crc, _checksumHexLength); break;
                default: throw new ArgumentException("Invalid NumericsStringFormat value.", nameof(outputFormat));
            }
            _crc = (byte[])_initParsed.Clone();
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

        internal override void Reset()
        {
            _crc = (byte[])_initParsed.Clone();
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
                        Xor(_crc, _polyParsed);
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
                        Xor(_crc, _polyParsed);
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
            Xor(_crc, _xoroutParsed);
        }
    }
}