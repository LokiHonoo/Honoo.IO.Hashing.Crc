using System;

namespace Honoo.IO.HashingOld
{
    internal sealed class CrcEngine32 : CrcEngine
    {
        #region Properties

        private readonly int _checksumLength;
        private readonly uint _init;
        private readonly int _move;
        private readonly uint _poly;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly uint[] _table;
        private readonly uint _xorout;
        private uint _crc;

        #endregion Properties

        #region Construction

        internal CrcEngine32(string algorithmName, int checksumSize, bool refin, bool refout, uint poly, uint init, uint xorout)
            : base(algorithmName, checksumSize, false)
        {
            if (checksumSize <= 0 || checksumSize > 32)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 32.", nameof(checksumSize));
            }
            _checksumLength = (int)Math.Ceiling(checksumSize / 8d);
            _move = 32 - checksumSize;
            _refin = refin;
            _refout = refout;
                _poly = Parse(poly, _move, _refin);
                _init = Parse(init, _move, _refin);
            _xorout = xorout;
            _crc = _init;
        }

        internal CrcEngine32(string algorithmName, int checksumSize, bool refin, bool refout, uint[] table, uint init, uint xorout)
            : base(algorithmName, checksumSize, true)
        {
            if (checksumSize <= 0 || checksumSize > 32)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 32.", nameof(checksumSize));
            }
            _checksumLength = (int)Math.Ceiling(checksumSize / 8d);
            _move = 32 - checksumSize;
            _refin = refin;
            _refout = refout;
            _table = table;
            _init = init;
            _xorout = xorout;
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

        internal static uint Parse(uint input, int move, bool reverse)
        {
            if (move > 0)
            {
                input <<= move;
            }
            if (reverse)
            {
                input = Reverse(input);
            }
            return input;
        }

        internal override object DoFinal()
        {
            if (_refout ^ _refin)
            {
                _crc = Reverse(_crc);
            }
            if (_move > 0 && !_refout)
            {
                _crc >>= _move;
            }
            _crc ^= _xorout;
            object result = _crc;
            _crc = _init;
            return result;
        }

        internal override byte[] DoFinal(bool littleEndian)
        {
            if (_refout ^ _refin)
            {
                _crc = Reverse(_crc);
            }
            if (_move > 0 && !_refout)
            {
                _crc >>= _move;
            }
            _crc ^= _xorout;
            byte[] result = new byte[_checksumLength];
            if (littleEndian)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = (byte)(_crc >> (i * 8));
                }
            }
            else
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[result.Length - 1 - i] = (byte)(_crc >> (i * 8));
                }
            }
            _crc = _init;
            return result;
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

        private static uint Reverse(uint input)
        {
            input = (input & 0x55555555) << 1 | (input >> 1) & 0x55555555;
            input = (input & 0x33333333) << 2 | (input >> 2) & 0x33333333;
            input = (input & 0x0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F;
            input = (input & 0x00FF00FF) << 8 | (input >> 8) & 0x00FF00FF;
            input = (input & 0x0000FFFF) << 16 | (input >> 16) & 0x0000FFFF;
            return input;
        }
    }
}