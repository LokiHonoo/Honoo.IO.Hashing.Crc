﻿using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine8 : CrcEngine
    {
        #region Properties

        private readonly byte _init;
        private readonly string _initHex;
        private readonly int _moves;
        private readonly byte _poly;
        private readonly string _polyHex;
        private readonly byte[] _table;
        private readonly byte _xorout;
        private readonly string _xoroutHex;
        private byte _crc;
        internal override string InitHex => _initHex;

        internal override string PolyHex => _polyHex;

        internal override string XoroutHex => _xoroutHex;

        #endregion Properties

        #region Construction

        internal CrcEngine8(string algorithmName, int checksumSize, bool refin, bool refout, byte poly, byte init, byte xorout, bool generateTable)
            : base(algorithmName, checksumSize, refin, refout, generateTable)
        {
            if (checksumSize <= 0 || checksumSize > 8)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 8.", nameof(checksumSize));
            }
            _moves = 8 - checksumSize;
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

        internal CrcEngine8(string algorithmName, int checksumSize, bool refin, bool refout, byte poly, byte init, byte xorout, byte[] table)
            : base(algorithmName, checksumSize, refin, refout, true)
        {
            if (checksumSize <= 0 || checksumSize > 8)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 8.", nameof(checksumSize));
            }
            _moves = 8 - checksumSize;
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

        internal override string DoFinal()
        {
            Finish();
            string result = GetString(_crc, _checksumHexLength);
            _crc = _init;
            return result;
        }

        internal override byte[] DoFinal(bool littleEndian)
        {
            byte[] result = new byte[1];
            DoFinal(littleEndian, result, 0);
            return result;
        }

        internal override int DoFinal(bool littleEndian, byte[] output, int offset)
        {
            Finish();
            output[offset] = _crc;
            _crc = _init;
            return 1;
        }

        internal override bool DoFinal(out byte checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _init;
            return false;
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

        private static string GetString(byte input, int hexLength)
        {
            string result = Convert.ToString(input, 16).PadLeft(2, '0');
            if (result.Length > hexLength)
            {
                return result.Substring(result.Length - hexLength, hexLength).ToUpperInvariant();
            }
            else
            {
                return result.ToUpperInvariant();
            }
        }

        private static byte Parse(byte input, int moves, bool reverse)
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

        private static byte Reverse(byte input)
        {
            input = (byte)((input & 0x55) << 1 | (input >> 1) & 0x55);
            input = (byte)((input & 0x33) << 2 | (input >> 2) & 0x33);
            input = (byte)((input & 0x0F) << 4 | (input >> 4) & 0x0F);
            return input;
        }

        private static byte TruncateLeft(byte input, int bits)
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