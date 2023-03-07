using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine8 : CrcEngine
    {
        #region Properties

        private readonly int _checksumStringLength;
        private readonly byte _init;
        private readonly int _move;
        private readonly byte _poly;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly byte[] _table;
        private readonly byte _xorout;
        private byte _crc;

        #endregion Properties

        #region Construction

        internal CrcEngine8(string algorithmName,  int checksumSize, bool refin, bool refout, byte poly, byte init, byte xorout)
            : base(algorithmName,  checksumSize, false)
        {
            if (checksumSize <= 0 || checksumSize > 8)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 8.", nameof(checksumSize));
            }
            _checksumStringLength = (int)Math.Ceiling(checksumSize / 4d);
            _move = 8 - checksumSize;
            _refin = refin;
            _refout = refout;
            _poly = Parse(poly, _move, _refin);
            _init = Parse(init, _move, _refin);
            _xorout = xorout;
            _crc = _init;
        }

        internal CrcEngine8(string algorithmName, int checksumSize, bool refin, bool refout, byte[] table, byte init, byte xorout)
            : base(algorithmName,  checksumSize, true)
        {
            if (checksumSize <= 0 || checksumSize > 8)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 8.", nameof(checksumSize));
            }
            _checksumStringLength = (int)Math.Ceiling(checksumSize / 4d);
            _move = 8 - checksumSize;
            _refin = refin;
            _refout = refout;
            _table = table;
            _init = init;
            _xorout = xorout;
            _crc = _init;
        }

        #endregion Construction

        internal static byte[] GenerateReversedTable(byte reversedPoly)
        {
            byte[] table = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                byte data = (byte)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 1) == 1)
                    {
                        data = (byte)((data >> 1) ^ reversedPoly);
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

        internal static byte[] GenerateTable(byte poly)
        {
            byte[] table = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                byte data = (byte)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 0x80) == 0x80)
                    {
                        data = (byte)((data << 1) ^ poly);
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

        internal static byte Parse(byte input, int move, bool reverse)
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

        internal override string DoFinal()
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
            string result = Convert.ToString(_crc, 16).PadLeft(2, '0');
            _crc = _init;
            if (result.Length > _checksumStringLength)
            {
                return result.Substring(result.Length - _checksumStringLength, _checksumStringLength).ToUpperInvariant();
            }
            else
            {
                return result.ToUpperInvariant();
            }
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
            byte[] result = new byte[] { _crc };
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
                        _crc = (byte)((_crc >> 1) ^ _poly);
                    }
                    else
                    {
                        _crc >>= 1;
                    }
                }
            }
            else
            {
                _crc ^= input;
                for (int j = 0; j < 8; j++)
                {
                    if ((_crc & 0x80) == 0x80)
                    {
                        _crc = (byte)((_crc << 1) ^ _poly);
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
                _crc = (byte)((_crc >> 8) ^ _table[(_crc & 0xFF) ^ input]);
            }
            else
            {
                _crc = (byte)((_crc << 8) ^ _table[(_crc & 0xFF) ^ input]);
            }
        }

        private static byte Reverse(byte input)
        {
            input = (byte)((input & 0x55) << 1 | (input >> 1) & 0x55);
            input = (byte)((input & 0x33) << 2 | (input >> 2) & 0x33);
            input = (byte)((input & 0x0F) << 4 | (input >> 4) & 0x0F);
            return input;
        }
    }
}