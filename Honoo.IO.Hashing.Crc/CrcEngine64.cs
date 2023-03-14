using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine64 : CrcEngine
    {
        #region Properties

        private readonly ulong _init;
        private readonly int _moves;
        private readonly ulong _poly;
        private readonly ulong[] _table;
        private readonly ulong _xorout;
        private ulong _crc;


        #endregion Properties

        #region Construction

        internal CrcEngine64(int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool generateTable)
            : base(width, refin, refout, generateTable)
        {
            if (width <= 0 || width > 64)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 64.", nameof(width));
            }
            _moves = 64 - width;
            _poly = TruncateLeft(poly, _moves);
            _init = TruncateLeft(init, _moves);
            _xorout = TruncateLeft(xorout, _moves);
            _poly = Parse(_poly, _moves, _refin);
            _init = Parse(_init, _moves, _refin);
            _table = generateTable ? _refin ? GenerateReversedTable(_poly) : GenerateTable(_poly) : null;
            _crc = _init;
        }

        internal CrcEngine64(int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, ulong[] table)
            : base(width, refin, refout, true)
        {
            if (width <= 0 || width > 64)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 64.", nameof(width));
            }
            _moves = 64 - width;
            _poly = TruncateLeft(poly, _moves);
            _init = TruncateLeft(init, _moves);
            _xorout = TruncateLeft(xorout, _moves);
            _poly = Parse(_poly, _moves, _refin);
            _init = Parse(_init, _moves, _refin);
            _table = table;
            _crc = _init;
        }

        #endregion Construction

        internal static ulong[] GenerateReversedTable(ulong reversedPoly)
        {
            ulong[] table = new ulong[256];
            for (int i = 0; i < 256; i++)
            {
                ulong data = (ulong)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 1) == 1)
                    {
                        data = (data >> 1) ^ reversedPoly;
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

        internal static ulong[] GenerateTable(ulong poly)
        {
            ulong[] table = new ulong[256];
            for (int i = 0; i < 256; i++)
            {
                ulong data = (ulong)i << 56;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 0x8000000000000000) == 0x8000000000000000)
                    {
                        data = (data << 1) ^ poly;
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

        internal override string DoFinal()
        {
            Finish();
            string result = GetString(_crc, _checksumHexLength);
            _crc = _init;
            return result;
        }

        internal override byte[] DoFinal(bool littleEndian)
        {
            byte[] result = new byte[_checksumByteLength];
            DoFinal(littleEndian, result, 0);
            return result;
        }

        internal override int DoFinal(bool littleEndian, byte[] output, int offset)
        {
            Finish();
            if (littleEndian)
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    output[i] = (byte)(_crc >> (i * 8));
                }
            }
            else
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    output[_checksumByteLength - 1 - i] = (byte)(_crc >> (i * 8));
                }
            }
            _crc = _init;
            return _checksumByteLength;
        }

        internal override bool DoFinal(out byte checksum)
        {
            Finish();
            checksum = (byte)_crc;
            _crc = _init;
            return _width > 8;
        }

        internal override bool DoFinal(out ushort checksum)
        {
            Finish();
            checksum = (ushort)_crc;
            _crc = _init;
            return _width > 16;
        }

        internal override bool DoFinal(out uint checksum)
        {
            Finish();
            checksum = (uint)_crc;
            _crc = _init;
            return _width > 32;
        }

        internal override bool DoFinal(out ulong checksum)
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
                        _crc = (_crc >> 1) ^ _poly;
                    }
                    else
                    {
                        _crc >>= 1;
                    }
                }
            }
            else
            {
                _crc ^= (ulong)input << 56;
                for (int j = 0; j < 8; j++)
                {
                    if ((_crc & 0x8000000000000000) == 0x8000000000000000)
                    {
                        _crc = (_crc << 1) ^ _poly;
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
                _crc = (_crc >> 8) ^ _table[(_crc & 0xFF) ^ input];
            }
            else
            {
                _crc = (_crc << 8) ^ _table[((_crc >> 56) & 0xFF) ^ input];
            }
        }

        private static string GetString(ulong input, int hexLength)
        {
            string result = Convert.ToString((long)input, 16).PadLeft(16, '0');
            if (result.Length > hexLength)
            {
                return result.Substring(result.Length - hexLength, hexLength).ToUpperInvariant();
            }
            else
            {
                return result.ToUpperInvariant();
            }
        }

        private static ulong Parse(ulong input, int moves, bool reverse)
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

        private static ulong Reverse(ulong input)
        {
            input = (input & 0x5555555555555555) << 1 | (input >> 1) & 0x5555555555555555;
            input = (input & 0x3333333333333333) << 2 | (input >> 2) & 0x3333333333333333;
            input = (input & 0x0F0F0F0F0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F0F0F0F0F;
            input = (input & 0x00FF00FF00FF00FF) << 8 | (input >> 8) & 0x00FF00FF00FF00FF;
            input = (input & 0x0000FFFF0000FFFF) << 16 | (input >> 16) & 0x0000FFFF0000FFFF;
            input = (input & 0x00000000FFFFFFFF) << 32 | (input >> 32) & 0x00000000FFFFFFFF;
            return input;
        }

        private static ulong TruncateLeft(ulong input, int bits)
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