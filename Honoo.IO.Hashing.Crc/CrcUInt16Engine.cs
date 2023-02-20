using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcUInt16Engine : CrcEngine
    {
        #region Properties

        private readonly int _checksumLength;
        private readonly ushort _init;
        private readonly int _move;
        private readonly ushort _poly;
        private readonly bool _reverse;
        private readonly ushort _xorout;
        private ushort _crc;

        #endregion Properties

        #region Construction

        internal CrcUInt16Engine(string algorithmName, int checksumSize, bool reverse, ushort poly, ushort init, ushort xorout, bool handled)
            : base(algorithmName, checksumSize)
        {
            if (checksumSize <= 8 || checksumSize > 16)
            {
                throw new ArgumentException("Invalid checkcum size.", nameof(checksumSize));
            }
            _checksumLength = Math.DivRem(checksumSize, 8, out _move);
            if (_move > 0)
            {
                _checksumLength += 1;
            }
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
            _crc = (ushort)(_crc << 8 | _crc >> 8);
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
                            _crc = (ushort)((_crc >> 1) ^ _poly);
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
                    _crc ^= (ushort)(buffer[i] << 8);
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
        }

        private static ushort Parse(int move, bool reverse, ushort input)
        {
            if (reverse)
            {
                input = (ushort)((input & 0x5555) << 1 | (input >> 1) & 0x5555);
                input = (ushort)((input & 0x3333) << 2 | (input >> 2) & 0x3333);
                input = (ushort)((input & 0x0F0F) << 4 | (input >> 4) & 0x0F0F);
                input = (ushort)((input & 0x00FF) << 8 | (input >> 8) & 0x00FF);
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