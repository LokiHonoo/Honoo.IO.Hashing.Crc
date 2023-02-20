using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcUInt8Engine : CrcEngine
    {
        #region Properties

        private readonly byte _init;
        private readonly int _move;
        private readonly byte _poly;
        private readonly bool _reverse;
        private readonly byte _xorout;
        private byte _crc;

        #endregion Properties

        #region Construction

        internal CrcUInt8Engine(string algorithmName, int checksumSize, bool reverse, byte poly, byte init, byte xorout, bool handled)
            : base(algorithmName, checksumSize)
        {
            if (checksumSize <= 0 || checksumSize > 8)
            {
                throw new ArgumentException("Invalid checkcum size.", nameof(checksumSize));
            }
            _move = 8 - checksumSize;
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
            if (_move > 0 && !_reverse)
            {
                _crc >>= _move;
            }
            byte[] result = new byte[] { _crc };
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
                            _crc = (byte)((_crc >> 1) ^ _poly);
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
                    _crc ^= buffer[i];
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
        }

        private static byte Parse(int move, bool reverse, byte input)
        {
            if (reverse)
            {
                input = (byte)((input & 0x55) << 1 | (input >> 1) & 0x55);
                input = (byte)((input & 0x33) << 2 | (input >> 2) & 0x33);
                input = (byte)((input & 0x0F) << 4 | (input >> 4) & 0x0F);
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