using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineSharding32 : CrcEngine
    {
        #region Members

        private readonly uint[] _initParsed;
        private readonly int _moves;
        private readonly uint[] _polyParsed;
        private readonly uint[][] _table;
        private readonly uint[] _xoroutParsed;
        private uint[] _crc;

        #endregion Members

        #region Construction

        internal CrcEngineSharding32(int width, bool refin, bool refout, uint[] poly, uint[] init, uint[] xorout, bool generateTable)
            : base(width, refin, refout, generateTable)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(width));
            }
            int rem = width % 32;
            _moves = rem > 0 ? 32 - rem : 0;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _table = generateTable ? _refin ? GenerateReversedTable(_polyParsed) : GenerateTable(_polyParsed) : null;
            _crc = (uint[])_initParsed.Clone();
        }

        internal CrcEngineSharding32(int width, bool refin, bool refout, uint[] poly, uint[] init, uint[] xorout, uint[][] table)
            : base(width, refin, refout, true)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(width));
            }
            int rem = width % 32;
            _moves = rem > 0 ? 32 - rem : 0;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _table = table;
            _crc = (uint[])_initParsed.Clone();
        }

        #endregion Construction

        internal static uint[][] GenerateReversedTable(uint[] polyParsed)
        {
            uint[][] table = new uint[256][];
            for (int i = 0; i < 256; i++)
            {
                uint[] data = new uint[polyParsed.Length];
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
                table[i] = data;
            }
            return table;
        }

        internal static uint[][] GenerateTable(uint[] polyParsed)
        {
            uint[][] table = new uint[256][];
            for (int i = 0; i < 256; i++)
            {
                uint[] data = new uint[polyParsed.Length];
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
            _crc = (uint[])_initParsed.Clone();
            return result;
        }

        internal override int ComputeFinal(Endian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            if (outputEndian == Endian.LittleEndian)
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

        internal override void Reset()
        {
            _crc = (uint[])_initParsed.Clone();
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
                        _crc = ShiftRight(_crc, 1);
                        _crc = Xor(_crc, _polyParsed);
                    }
                    else
                    {
                        _crc = ShiftRight(_crc, 1);
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
                        _crc = ShiftLeft(_crc, 1);
                        _crc = Xor(_crc, _polyParsed);
                    }
                    else
                    {
                        _crc = ShiftLeft(_crc, 1);
                    }
                }
            }
        }

        protected override void UpdateWithTable(byte input)
        {
            if (_refin)
            {
                uint[] match = _table[(_crc[_crc.Length - 1] & 0xFF) ^ input];
                _crc = ShiftRight(_crc, 8);
                _crc = Xor(_crc, match);
            }
            else
            {
                uint[] match = _table[((_crc[0] >> 24) & 0xFF) ^ input];
                _crc = ShiftLeft(_crc, 8);
                _crc = Xor(_crc, match);
            }
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