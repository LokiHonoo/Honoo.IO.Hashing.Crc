//using System;
//using System.Numerics;
//using System.Text;

//namespace Honoo.IO.Hashing
//{
//    internal sealed class CrcBigIntegerEngine : CrcEngine
//    {
//        #region Properties

//        private readonly byte[] _init;
//        private readonly int _move;
//        private readonly byte[] _poly;
//        private readonly bool _reverse;
//        private readonly byte[] _xorout;
//        private byte[] _crc;

//        #endregion Properties

//        #region Construction

//        internal CrcBigIntegerEngine(string algorithmName, int checksumSize, bool reverse, byte[] poly, byte[] init, byte[] xorout, bool handled)
//            : base(algorithmName, checksumSize)
//        {
//            if (checksumSize <= 0)
//            {
//                throw new ArgumentException("Invalid checkcum size.", nameof(checksumSize));
//            }
//            _ = Math.DivRem(checksumSize, 8, out _move);
//            _reverse = reverse;
//            if (handled)
//            {
//                _poly = poly;
//                _init = init;
//                _xorout = xorout;
//            }
//            else
//            {
//                _poly = Parse(_move, _reverse, poly);
//                _init = Parse(_move, _reverse, init);
//                _xorout = Parse(_move, _reverse, xorout);
//            }
//            _crc = _init;
//        }

//        #endregion Construction

//        internal override byte[] DoFinal()
//        {
//            _crc ^= _xorout;
//            byte[] tmp = _crc.ToByteArray();
//            byte t;
//            for (int i = 0; i < tmp.Length / 2; i++)
//            {
//                t = tmp[i];
//                tmp[i] = tmp[tmp.Length - 1 - i];
//                tmp[tmp.Length - 1 - i] = t;
//            }
//            _crc = new BigInteger(tmp);
//            if (_move > 0 && !_reverse)
//            {
//                _crc >>= _move;
//            }
//            byte[] result = _crc.ToByteArray();
//            _crc = _init;
//            return result;
//        }

//        internal override void Reset()
//        {
//            _crc = _init;
//        }

//        internal override void Update(byte[] buffer, int offset, int length)
//        {
//            if (buffer == null)
//            {
//                throw new ArgumentNullException(nameof(buffer));
//            }
//            if (_reverse)
//            {
//                for (int i = 0; i < offset + length; i++)
//                {
//                    _crc ^= buffer[i];
//                    for (int j = 0; j < 8; j++)
//                    {
//                        if ((_crc & 1) == 1)
//                        {
//                            _crc = (_crc >> 1) ^ _poly;
//                        }
//                        else
//                        {
//                            _crc >>= 1;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                for (int i = 0; i < offset + length; i++)
//                {
//                    _crc ^= (ulong)(buffer[i] << 56);
//                    for (int j = 0; j < 8; j++)
//                    {
//                        if ((_crc & 0x8000000000000000) == 0x8000000000000000)
//                        {
//                            _crc = (_crc << 1) ^ _poly;
//                        }
//                        else
//                        {
//                            _crc <<= 1;
//                        }
//                    }
//                }
//            }
//        }

//        private static byte[] Parse(int move, bool reverse, byte[] input)
//        {
//            if (reverse)
//            {
//                var tmp = input.ToByteArray();
//                var sb = new StringBuilder();
//                if (BitConverter.IsLittleEndian)
//                {
//                    for (int i = tmp.Length - 1; i >= 0; i--)
//                    {
//                        sb.Append(Convert.ToString(tmp[i], 2).PadLeft(8, '0'));
//                    }
//                }
//                else
//                {
//                    for (int i = 0; i < tmp.Length; i++)
//                    {
//                        sb.Append(Convert.ToString(tmp[i], 2).PadLeft(8, '0'));
//                    }
//                }
//                for (int i = 0; i < tmp.Length; i++)
//                {
//                    tmp[i] = Convert.ToByte(sb.ToString(i * 8, 8), 2);
//                }
//                input = new BigInteger(tmp);
//                if (move > 0)
//                {
//                    input >>= move;
//                }
//            }
//            else
//            {
//                if (move > 0)
//                {
//                    input <<= move;
//                }
//            }
//            return input;
//        }
//    }
//}