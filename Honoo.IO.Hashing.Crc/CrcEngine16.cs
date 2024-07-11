using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine16 : CrcEngine
    {
        #region Properties

        private readonly ushort _init;
        private readonly int _moves;
        private readonly ushort _poly;
        private readonly ushort[] _table;
        private readonly ushort _xorout;
        private ushort _crc;

        #endregion Properties

        #region Construction

        internal CrcEngine16(int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool generateTable)
            : base(width, refin, refout, generateTable)
        {
            if (width <= 0 || width > 16)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
            }
            _moves = 16 - width;
            _poly = TruncateLeft(poly, _moves);
            _init = TruncateLeft(init, _moves);
            _xorout = TruncateLeft(xorout, _moves);
            _poly = Parse(_poly, _moves, _refin);
            _init = Parse(_init, _moves, _refin);
            _table = generateTable ? _refin ? GenerateReversedTable(_poly) : GenerateTable(_poly) : null;
            _crc = _init;
        }

        internal CrcEngine16(int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, ushort[] table)
            : base(width, refin, refout, true)
        {
            if (width <= 0 || width > 16)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
            }
            _moves = 16 - width;
            _poly = TruncateLeft(poly, _moves);
            _init = TruncateLeft(init, _moves);
            _xorout = TruncateLeft(xorout, _moves);
            _poly = Parse(_poly, _moves, _refin);
            _init = Parse(_init, _moves, _refin);
            _table = table;
            _crc = _init;
        }

        #endregion Construction

        internal static ushort[] GenerateReversedTable(ushort reversedPoly)
        {
            ushort[] table = new ushort[256];
            for (int i = 0; i < 256; i++)
            {
                ushort data = (ushort)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 1) == 1)
                    {
                        data = (ushort)((data >> 1) ^ reversedPoly);
                    }
                    else
                    {
                        data >>= 1;
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal static ushort[] GenerateTable(ushort poly)
        {
            ushort[] table = new ushort[256];
            for (int i = 0; i < 256; i++)
            {
                ushort data = (ushort)(i << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 0x8000) == 0x8000)
                    {
                        data = (ushort)((data << 1) ^ poly);
                    }
                    else
                    {
                        data <<= 1;
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal override string ComputeFinal(StringFormat outputFormat)
        {
            Finish();
            string result = outputFormat == StringFormat.Hex ? GetHexString(_crc, _checksumHexLength) : GetBinaryString(_crc, _width);
            _crc = _init;
            return result;
        }

        internal override int ComputeFinal(Endian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            if (outputEndian == Endian.LittleEndian)
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    outputBuffer[i + outputOffset] = (byte)(_crc >> (i * 8));
                }
            }
            else
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    outputBuffer[_checksumByteLength - 1 - i + outputOffset] = (byte)(_crc >> (i * 8));
                }
            }
            _crc = _init;
            return _checksumByteLength;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = (byte)_crc;
            _crc = _init;
            return _width > 8;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _init;
            return false;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _init;
            return false;
        }

        internal override bool ComputeFinal(out ulong checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _init;
            return false;
        }

        internal override void Reset()
        {
            _crc = _init;
        }

        protected override void UpdateWithoutTable(byte input)
        {
            if (_refin)
            {
                _crc ^= input;
                for (int j = 0; j < 8; j++)
                {
                    if ((_crc & 1) == 1)
                    {
                        _crc = (ushort)((_crc >> 1) ^ _poly);
                    }
                    else
                    {
                        _crc >>= 1;
                    }
                }
            }
            else
            {
                _crc ^= (ushort)(input << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((_crc & 0x8000) == 0x8000)
                    {
                        _crc = (ushort)((_crc << 1) ^ _poly);
                    }
                    else
                    {
                        _crc <<= 1;
                    }
                }
            }
        }

        protected override void UpdateWithTable(byte input)
        {
            if (_refin)
            {
                _crc = (ushort)((_crc >> 8) ^ _table[(_crc & 0xFF) ^ input]);
            }
            else
            {
                _crc = (ushort)((_crc << 8) ^ _table[((_crc >> 8) & 0xFF) ^ input]);
            }
        }

        private static string GetBinaryString(ushort input, int width)
        {
            string result = Convert.ToString(input, 2).PadLeft(16, '0');
            return result.Length > width ? result.Substring(result.Length - width, width) : result;
        }

        private static string GetHexString(ushort input, int hexLength)
        {
            string result = Convert.ToString(input, 16).PadLeft(4, '0');
            return result.Length > hexLength ? result.Substring(result.Length - hexLength, hexLength) : result;
        }

        private static ushort Parse(ushort input, int moves, bool reverse)
        {
            if (moves > 0)
            {
                input <<= moves;
            }
            if (reverse)
            {
                input = Reverse(input);
            }
            return input;
        }

        private static ushort Reverse(ushort input)
        {
            input = (ushort)((input & 0x5555) << 1 | (input >> 1) & 0x5555);
            input = (ushort)((input & 0x3333) << 2 | (input >> 2) & 0x3333);
            input = (ushort)((input & 0x0F0F) << 4 | (input >> 4) & 0x0F0F);
            input = (ushort)((input & 0x00FF) << 8 | (input >> 8) & 0x00FF);
            return input;
        }

        private static ushort TruncateLeft(ushort input, int bits)
        {
            input <<= bits;
            input >>= bits;
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
                _crc >>= _moves;
            }
            _crc ^= _xorout;
        }
    }
}