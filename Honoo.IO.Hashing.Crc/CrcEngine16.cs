using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine16 : CrcEngine
    {
        #region Properties

        private readonly int _checksumByteLength;
        private readonly int _checksumSize;
        private readonly int _checksumStringLength;
        private readonly ushort _init;
        private readonly int _move;
        private readonly ushort _poly;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly ushort[] _table;
        private readonly ushort _xorout;
        private ushort _crc;

        #endregion Properties

        #region Construction

        internal CrcEngine16(string algorithmName, int checksumSize, bool refin, bool refout, ushort poly, ushort init, ushort xorout)
            : base(algorithmName, checksumSize, false)
        {
            if (checksumSize <= 0 || checksumSize > 16)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 16.", nameof(checksumSize));
            }
            _checksumSize = checksumSize;
            _checksumByteLength = (int)Math.Ceiling(checksumSize / 8d);
            _checksumStringLength = (int)Math.Ceiling(checksumSize / 4d);
            _move = 16 - checksumSize;
            _refin = refin;
            _refout = refout;
            _poly = Parse(poly, _move, _refin);
            _init = Parse(init, _move, _refin);
            _xorout = xorout;
            _crc = _init;
        }

        internal CrcEngine16(string algorithmName, int checksumSize, bool refin, bool refout, ushort[] table, ushort init, ushort xorout)
            : base(algorithmName, checksumSize, true)
        {
            if (checksumSize <= 0 || checksumSize > 16)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 16.", nameof(checksumSize));
            }
            _checksumSize = checksumSize;
            _checksumByteLength = (int)Math.Ceiling(checksumSize / 8d);
            _checksumStringLength = (int)Math.Ceiling(checksumSize / 4d);
            _move = 16 - checksumSize;
            _refin = refin;
            _refout = refout;
            _table = table;
            _init = init;
            _xorout = xorout;
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

        internal static ushort Parse(ushort input, int move, bool reverse)
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
            Finish();
            string result = Convert.ToString(_crc, 16).PadLeft(4, '0');
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
            checksum = _crc;
            _crc = _init;
            return false;
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

        private static ushort Reverse(ushort input)
        {
            input = (ushort)((input & 0x5555) << 1 | (input >> 1) & 0x5555);
            input = (ushort)((input & 0x3333) << 2 | (input >> 2) & 0x3333);
            input = (ushort)((input & 0x0F0F) << 4 | (input >> 4) & 0x0F0F);
            input = (ushort)((input & 0x00FF) << 8 | (input >> 8) & 0x00FF);
            return input;
        }

        private void Finish()
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
        }
    }
}