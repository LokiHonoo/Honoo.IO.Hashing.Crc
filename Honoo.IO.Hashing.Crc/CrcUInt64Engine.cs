using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcUInt64Engine : CrcEngine
    {
        #region Properties

        private readonly ulong _init;
        private readonly int _move;
        private readonly ulong _poly;
        private readonly bool _reverse;
        private readonly ulong _xorout;
        private ulong _crc;

        #endregion Properties

        #region Construction

        internal CrcUInt64Engine(string algorithmName, int checksumSize, bool reverse, ulong poly, ulong init, ulong xorout, bool handled)
            : base(algorithmName, checksumSize)
        {
            if (checksumSize <= 32 || checksumSize > 64)
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
            _crc = _crc << 56
                | (_crc & 0xFF00) << 40
                | (_crc & 0xFF0000) << 24
                | (_crc & 0xFF000000) << 8
                | (_crc >> 8) & 0xFF000000
                | (_crc >> 24) & 0xFF0000
                | (_crc >> 40) & 0xFF00
                | _crc >> 56;
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
                    _crc ^= (ulong)(buffer[i] << 56);
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
        }

        private static ulong Parse(int move, bool reverse, ulong input)
        {
            if (reverse)
            {
                input = (input & 0x5555555555555555) << 1 | (input >> 1) & 0x5555555555555555;
                input = (input & 0x3333333333333333) << 2 | (input >> 2) & 0x3333333333333333;
                input = (input & 0x0F0F0F0F0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F0F0F0F0F;
                input = (input & 0x00FF00FF00FF00FF) << 8 | (input >> 8) & 0x00FF00FF00FF00FF;
                input = (input & 0x0000FFFF0000FFFF) << 16 | (input >> 16) & 0x0000FFFF0000FFFF;
                input = (input & 0x00000000FFFFFFFF) << 32 | (input >> 32) & 0x00000000FFFFFFFF;
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