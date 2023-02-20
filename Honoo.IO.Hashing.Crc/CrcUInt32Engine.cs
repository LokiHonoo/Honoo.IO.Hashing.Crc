using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcUInt32Engine : CrcEngine
    {
        #region Properties

        private readonly uint _init;
        private readonly int _move;
        private readonly uint _poly;
        private readonly bool _reverse;
        private readonly uint _xorout;
        private uint _crc;

        #endregion Properties

        #region Construction

        internal CrcUInt32Engine(string algorithmName, int checksumSize, bool reverse, uint poly, uint init, uint xorout, bool handled)
            : base(algorithmName, checksumSize)
        {
            if (checksumSize <= 16 || checksumSize > 32)
            {
                throw new ArgumentException("Invalid checkcum size.", nameof(checksumSize));
            }
            _move = 32 - checksumSize;
            _reverse = reverse;
            if (handled)
            {
                _poly = poly;
                _init = init;
                _xorout = xorout;
            }
            else
            {
                _poly = Parse(_move, _reverse, poly);
                _init = Parse(_move, _reverse, init);
                _xorout = Parse(_move, _reverse, xorout);
            }
            _crc = _init;
        }

        #endregion Construction

        internal override byte[] DoFinal()
        {
            _crc ^= _xorout;
            _crc = _crc << 24 | (_crc & 0xFF00) << 8 | (_crc >> 8) & 0xFF00 | _crc >> 24;
            if (_move > 0 && !_reverse)
            {
                _crc >>= _move;
            }
            byte[] result = BitConverter.GetBytes(_crc);
            _crc = _init;
            return result;
        }

        internal override void Reset()
        {
            _crc = _init;
        }

        internal override void Update(byte[] buffer, int offset, int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (_reverse)
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= buffer[i];
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
            }
            else
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= (uint)(buffer[i] << 24);
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
        }

        private static uint Parse(int move, bool reverse, uint input)
        {
            if (reverse)
            {
                input = (input & 0x55555555) << 1 | (input >> 1) & 0x55555555;
                input = (input & 0x33333333) << 2 | (input >> 2) & 0x33333333;
                input = (input & 0x0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F;
                input = (input & 0x00FF00FF) << 8 | (input >> 8) & 0x00FF00FF;
                input = (input & 0x0000FFFF) << 16 | (input >> 16) & 0x0000FFFF;
                if (move > 0)
                {
                    input >>= move;
                }
            }
            else
            {
                if (move > 0)
                {
                    input <<= move;
                }
            }
            return input;
        }
    }
}