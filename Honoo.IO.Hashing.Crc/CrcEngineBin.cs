using System;
using System.Collections.Generic;
using System.Text;

namespace Honoo.IO.HashingOld
{
    internal sealed class CrcEngineBin : CrcEngine
    {
        #region Properties

        private readonly int _checksumLength;
        private readonly List<byte> _crc = new List<byte>();
        private readonly byte[] _init;
        private readonly int _move;
        private readonly byte[] _poly;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly byte[] _xorout;

        #endregion Properties

        #region Construction

        internal CrcEngineBin(string algorithmName, int checksumSize, bool refin, bool refout, string polyHex, string initHex, string xoroutHex)
            : base(algorithmName, checksumSize, false)
        {
            if (checksumSize <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(checksumSize));
            }
            _checksumLength = (int)Math.Ceiling(checksumSize / 8d);
            _move = _checksumLength * 8 - checksumSize;
            _refin = refin;
            _refout = refout;
            _poly = Parse(polyHex, _checksumLength, _move, _refin);
            _init = Parse(initHex, _checksumLength, _move, _refin);
            _xorout = Parse(xoroutHex, _checksumLength, 0, false);
            _crc.AddRange(_init);
        }

        #endregion Construction

        internal override object DoFinal()
        {
            byte[] result = DoFinal(false);
            return BitConverter.ToString(result).Replace("-", null);
        }

        internal override byte[] DoFinal(bool littleEndian)
        {
            if (_refout ^ _refin)
            {
                _crc.Reverse();
            }
            if (_move > 0 && !_refout)
            {
                _crc.RemoveRange(_crc.Count - _move, _move);
                _crc.InsertRange(0, new byte[_move]);
            }
            for (int i = 0; i < _crc.Count; i++)
            {
                _crc[i] ^= _xorout[i];
            }
            byte[] result = new byte[_checksumLength];
            StringBuilder sb = new StringBuilder();
            if (littleEndian)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Clear();
                    for (int j = 0; j < 8; j++)
                    {
                        sb.Append(_crc[i * 8 + j]);
                    }
                    result[result.Length - 1 - i] = Convert.ToByte(sb.ToString(), 2);
                }
            }
            else
            {
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Clear();
                    for (int j = 0; j < 8; j++)
                    {
                        sb.Append(_crc[i * 8 + j]);
                    }
                    result[i] = Convert.ToByte(sb.ToString(), 2);
                }
            }
            _crc.Clear();
            _crc.AddRange(_init);
            return result;
        }

        internal override void Reset()
        {
            _crc.Clear();
            _crc.AddRange(_init);
        }

        protected override void UpdateWithoutTable(byte input)
        {
            if (_refin)
            {
                for (int i = 0; i < _crc.Count - 8; i++)
                {
                    _crc[i] ^= 0;
                }
                _crc[_crc.Count - 1 - 7] ^= (byte)((input & 0b10000000) >> 7);
                _crc[_crc.Count - 1 - 6] ^= (byte)((input & 0b01000000) >> 6);
                _crc[_crc.Count - 1 - 5] ^= (byte)((input & 0b00100000) >> 5);
                _crc[_crc.Count - 1 - 4] ^= (byte)((input & 0b00010000) >> 4);
                _crc[_crc.Count - 1 - 3] ^= (byte)((input & 0b00001000) >> 3);
                _crc[_crc.Count - 1 - 2] ^= (byte)((input & 0b00000100) >> 2);
                _crc[_crc.Count - 1 - 1] ^= (byte)((input & 0b00000010) >> 1);
                _crc[_crc.Count - 1 - 0] ^= (byte)((input & 0b00000001) >> 0);
                for (int j = 0; j < 8; j++)
                {
                    if (_crc[_crc.Count - 1] == 1)
                    {
                        _crc.RemoveAt(_crc.Count - 1);
                        _crc.Insert(0, 0);
                        for (int i = 0; i < _crc.Count; i++)
                        {
                            _crc[i] ^= _poly[i];
                        }
                    }
                    else
                    {
                        _crc.RemoveAt(_crc.Count - 1);
                        _crc.Insert(0, 0);
                    }
                }
            }
            else
            {
                for (int i = _crc.Count - 1; i >= 8; i--)
                {
                    _crc[i] ^= 0;
                }
                _crc[0] ^= (byte)((input & 0b10000000) >> 7);
                _crc[1] ^= (byte)((input & 0b01000000) >> 6);
                _crc[2] ^= (byte)((input & 0b00100000) >> 5);
                _crc[3] ^= (byte)((input & 0b00010000) >> 4);
                _crc[4] ^= (byte)((input & 0b00001000) >> 3);
                _crc[5] ^= (byte)((input & 0b00000100) >> 2);
                _crc[6] ^= (byte)((input & 0b00000010) >> 1);
                _crc[7] ^= (byte)((input & 0b00000001) >> 0);
                for (int j = 0; j < 8; j++)
                {
                    if (_crc[0] == 1)
                    {
                        _crc.RemoveAt(0);
                        _crc.Add(0);
                        for (int i = 0; i < _crc.Count; i++)
                        {
                            _crc[i] ^= _poly[i];
                        }
                    }
                    else
                    {
                        _crc.RemoveAt(0);
                        _crc.Add(0);
                    }
                }
            }
        }

        protected override void UpdateWithTable(byte input)
        {
            throw new NotImplementedException();
        }

        private static byte[] Parse(string input, int checksumLength, int move, bool reverse)
        {
            List<byte> result = new List<byte>();
            if (input.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase) || input.StartsWith("&h", StringComparison.InvariantCultureIgnoreCase))
            {
                input = input.Substring(2, input.Length - 2).Replace("_", null);
            }
            else
            {
                input = input.Replace("_", null);
            }
            for (int i = 0; i < input.Length; i++)
            {
                string tmp = Convert.ToString(Convert.ToByte(input.Substring(i, 1), 16), 2).PadLeft(4, '0');
                for (int j = 0; j < tmp.Length; j++)
                {
                    result.Add(Convert.ToByte(tmp.Substring(j, 1), 2));
                }
            }
            int size = checksumLength * 8;
            if (result.Count < size)
            {
                result.InsertRange(0, new byte[size - result.Count]);
            }
            else if (result.Count > size)
            {
                result.RemoveRange(0, result.Count - size);
            }
            if (move > 0)
            {
                result.RemoveRange(0, move);
                result.AddRange(new byte[move]);
            }
            if (reverse)
            {
                result.Reverse();
            }
            return result.ToArray();
        }
    }
}