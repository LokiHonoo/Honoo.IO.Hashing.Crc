using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine32 : CrcEngine
    {
        #region Properties

        private readonly uint _init;
        private readonly string _initHex;
        private readonly int _moves;
        private readonly uint _poly;
        private readonly string _polyHex;
        private readonly uint[] _table;
        private readonly uint _xorout;
        private readonly string _xoroutHex;
        private uint _crc;
        internal override string InitHex => _initHex;

        internal override string PolyHex => _polyHex;

        internal override string XoroutHex => _xoroutHex;

        #endregion Properties

        #region Construction

        internal CrcEngine32(string algorithmName, int checksumSize, bool refin, bool refout, uint poly, uint init, uint xorout, bool generateTable)
            : base(algorithmName, checksumSize, refin, refout, generateTable)
        {
            if (checksumSize <= 0 || checksumSize > 32)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 32.", nameof(checksumSize));
            }
            _moves = 32 - checksumSize;
            _poly = TruncateLeft(poly, _moves);
            _init = TruncateLeft(init, _moves);
            _xorout = TruncateLeft(xorout, _moves);
            _polyHex = GetString(_poly, _checksumHexLength);
            _initHex = GetString(_init, _checksumHexLength);
            _xoroutHex = GetString(_xorout, _checksumHexLength);
            _poly = Parse(_poly, _moves, _refin);
            _init = Parse(_init, _moves, _refin);
            _table = generateTable ? _refin ? GenerateReversedTable(_poly) : GenerateTable(_poly) : null;
            _crc = _init;
        }

        internal CrcEngine32(string algorithmName, int checksumSize, bool refin, bool refout, uint poly, uint init, uint xorout, uint[] table)
            : base(algorithmName, checksumSize, refin, refout, true)
        {
            if (checksumSize <= 0 || checksumSize > 32)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 32.", nameof(checksumSize));
            }
            _moves = 32 - checksumSize;
            _poly = TruncateLeft(poly, _moves);
            _init = TruncateLeft(init, _moves);
            _xorout = TruncateLeft(xorout, _moves);
            _polyHex = GetString(_poly, _checksumHexLength);
            _initHex = GetString(_init, _checksumHexLength);
            _xoroutHex = GetString(_xorout, _checksumHexLength);
            _poly = Parse(_poly, _moves, _refin);
            _init = Parse(_init, _moves, _refin);
            _table = table;
            _crc = _init;
        }

        #endregion Construction

        internal static uint[] GenerateReversedTable(uint reversedPoly)
        {
            uint[] table = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                uint data = (uint)i;
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

        internal static uint[] GenerateTable(uint poly)
        {
            uint[] table = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                uint data = (uint)(i << 24);
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 0x80000000) == 0x80000000)
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
            return _checksumSize > 8;
        }

        internal override bool DoFinal(out ushort checksum)
        {
            Finish();
            checksum = (ushort)_crc;
            _crc = _init;
            return _checksumSize > 16;
        }

        internal override bool DoFinal(out uint checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _init;
            return false;
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
                _crc ^= (uint)(input << 24);
                for (int j = 0; j < 8; j++)
                {
                    if ((_crc & 0x80000000) == 0x80000000)
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
                _crc = (_crc << 8) ^ _table[((_crc >> 24) & 0xFF) ^ input];
            }
        }

        private static string GetString(uint input, int hexLength)
        {
            string result = Convert.ToString(input, 16).PadLeft(8, '0');
            if (result.Length > hexLength)
            {
                return result.Substring(result.Length - hexLength, hexLength).ToUpperInvariant();
            }
            else
            {
                return result.ToUpperInvariant();
            }
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
            _crc ^= _xorout;
        }
    }
}