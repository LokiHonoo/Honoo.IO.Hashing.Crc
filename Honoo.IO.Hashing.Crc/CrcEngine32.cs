using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine32 : CrcEngine
    {
        #region Members

        private readonly uint _initParsed;
        private readonly int _moves;
        private readonly uint _polyParsed;
        private readonly uint[] _table;
        private readonly uint _xoroutParsed;
        private uint _crc;

        #endregion Members

        #region Construction

        internal CrcEngine32(int width, bool refin, bool refout, uint poly, uint init, uint xorout, bool generateTable)
            : base(width, refin, refout, generateTable)
        {
            if (width <= 0 || width > 32)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 32.", nameof(width));
            }
            _moves = 32 - width;
            _polyParsed = TruncateLeft(poly, _moves);
            _initParsed = TruncateLeft(init, _moves);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _polyParsed = Parse(_polyParsed, _moves, _refin);
            _initParsed = Parse(_initParsed, _moves, _refin);
            _table = generateTable ? _refin ? GenerateReversedTable(_polyParsed) : GenerateTable(_polyParsed) : null;
            _crc = _initParsed;
        }

        internal CrcEngine32(int width, bool refin, bool refout, uint poly, uint init, uint xorout, uint[] table)
            : base(width, refin, refout, true)
        {
            if (width <= 0 || width > 32)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 32.", nameof(width));
            }
            _moves = 32 - width;
            _polyParsed = TruncateLeft(poly, _moves);
            _initParsed = TruncateLeft(init, _moves);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _polyParsed = Parse(_polyParsed, _moves, _refin);
            _initParsed = Parse(_initParsed, _moves, _refin);
            _table = table;
            _crc = _initParsed;
        }

        #endregion Construction

        internal static uint[] GenerateReversedTable(uint reversedPolyParsed)
        {
            uint[] table = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                uint data = (uint)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 1) == 1)
                    {
                        data = (data >> 1) ^ reversedPolyParsed;
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

        internal static uint[] GenerateTable(uint polyParsed)
        {
            uint[] table = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                uint data = (uint)(i << 24);
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 0x80000000) == 0x80000000)
                    {
                        data = (data << 1) ^ polyParsed;
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
            _crc = _initParsed;
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
            _crc = _initParsed;
            return _checksumByteLength;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = (byte)_crc;
            _crc = _initParsed;
            return _width > 8;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = (ushort)_crc;
            _crc = _initParsed;
            return _width > 16;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _initParsed;
            return false;
        }

        internal override bool ComputeFinal(out ulong checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _initParsed;
            return false;
        }

        internal override void Reset()
        {
            _crc = _initParsed;
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
                        _crc = (_crc >> 1) ^ _polyParsed;
                    }
                    else
                    {
                        _crc >>= 1;
                    }
                }
            }
            else
            {
                _crc ^= (uint)(input << 24);
                for (int j = 0; j < 8; j++)
                {
                    if ((_crc & 0x80000000) == 0x80000000)
                    {
                        _crc = (_crc << 1) ^ _polyParsed;
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
                _crc = (_crc << 8) ^ _table[((_crc >> 24) & 0xFF) ^ input];
            }
        }

        private static string GetBinaryString(uint input, int width)
        {
            string result = Convert.ToString(input, 2).PadLeft(32, '0');
            if (result.Length > width)
            {
                result = result.Substring(result.Length - width, width);
            }
            return result;
        }

        private static string GetHexString(uint input, int hexLength)
        {
            string result = Convert.ToString(input, 16).PadLeft(8, '0');
            if (result.Length > hexLength)
            {
                result = result.Substring(result.Length - hexLength, hexLength);
            }
            return result;
        }

        private static uint Parse(uint input, int moves, bool reverse)
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

        private static uint Reverse(uint input)
        {
            input = (input & 0x55555555) << 1 | (input >> 1) & 0x55555555;
            input = (input & 0x33333333) << 2 | (input >> 2) & 0x33333333;
            input = (input & 0x0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F;
            input = (input & 0x00FF00FF) << 8 | (input >> 8) & 0x00FF00FF;
            input = (input & 0x0000FFFF) << 16 | (input >> 16) & 0x0000FFFF;
            return input;
        }

        private static uint TruncateLeft(uint input, int bits)
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
            _crc ^= _xoroutParsed;
        }
    }
}