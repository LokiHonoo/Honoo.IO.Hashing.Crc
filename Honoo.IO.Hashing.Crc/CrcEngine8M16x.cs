﻿using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine8M16x : CrcEngine
    {
        #region Members

        private readonly int _checksumByteLength;
        private readonly int _checksumHexLength;
        private readonly CrcCore _core = CrcCore.UInt8;
        private readonly byte _initParsed;
        private readonly int _moves;
        private readonly byte _polyParsed;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly byte[] _table;
        private readonly CrcTableInfo _tableInfo = CrcTableInfo.M16x;
        private readonly int _width;
        private readonly byte _xoroutParsed;
        private byte _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override CrcTableInfo TableInfo => _tableInfo;
        internal override int Width => _width;

        #endregion Members

        #region Construction

        internal CrcEngine8M16x(int width, byte poly, byte init, byte xorout, bool refin, bool refout, byte[] table)
        {
            if (width <= 0 || width > 8)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 8.", nameof(width));
            }
            _width = width;
            _refin = refin;
            _refout = refout;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            _moves = 8 - width;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _table = table ?? (_refin ? GenerateTableRef(_polyParsed) : GenerateTable(_polyParsed));
            _crc = _initParsed;
        }

        #endregion Construction

        #region Table

        internal static byte[] GenerateTable(int width, byte poly, bool refin)
        {
            byte polyParsed = Parse(poly, 8 - width, refin);
            return refin ? GenerateTableRef(polyParsed) : GenerateTable(polyParsed);
        }

        internal override CrcTable CloneTable()
        {
            return new CrcTable(_tableInfo, _core, _table);
        }

        private static byte[] GenerateTable(byte polyParsed)
        {
            byte[] table = new byte[256 * 16];
            for (int i = 0; i < 256; i++)
            {
                byte data = (byte)i;
                for (int k = 0; k < 16; k++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if ((data & 0x80) == 0x80)
                        {
                            data = (byte)((data << 1) ^ polyParsed);
                        }
                        else
                        {
                            data <<= 1;
                        }
                    }
                    table[k * 256 + i] = data;
                }
            }
            return table;
        }

        private static byte[] GenerateTableRef(byte polyParsed)
        {
            byte[] table = new byte[256 * 16];
            for (int i = 0; i < 256; i++)
            {
                byte data = (byte)i;
                for (int k = 0; k < 16; k++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if ((data & 1) == 1)
                        {
                            data = (byte)((data >> 1) ^ polyParsed);
                        }
                        else
                        {
                            data >>= 1;
                        }
                    }
                    table[k * 256 + i] = data;
                }
            }
            return table;
        }

        #endregion Table

        #region ComputeFinal

        internal override CrcValue ComputeFinal()
        {
            Finish();
            var checksum = new CrcUInt8Value(_crc, _width);
            _crc = _initParsed;
            return checksum;
        }

        #endregion ComputeFinal

        #region Update byte

        internal override void Update(byte input)
        {
            if (_refin)
            {
                UpdateWithTableRef(input);
            }
            else
            {
                UpdateWithTable(input);
            }
        }

        private void UpdateWithTable(byte input)
        {
            _crc = (byte)((_crc << 8) ^ _table[_crc ^ input]);
        }

        private void UpdateWithTableRef(byte input)
        {
            _crc = (byte)((_crc >> 8) ^ _table[_crc ^ input]);
        }

        #endregion Update byte

        #region Update bytes

        internal override unsafe void Update(byte[] inputBuffer, int offset, int length)
        {
            fixed (byte* inputP = inputBuffer)
            {
                if (_refin)
                {
                    UpdateWithTableRef(inputP, offset, length);
                }
                else
                {
                    UpdateWithTable(inputP, offset, length);
                }
            }
        }

        private unsafe void UpdateWithTable(byte* inputP, int offset, int length)
        {
            inputP += offset;
            fixed (byte* tableP = _table)
            {
                while (length >= 16)
                {
                    var a = tableP[(15 * 256) + ((_crc & 0xFF) ^ inputP[0])]
                        ^ tableP[(14 * 256) + inputP[1]]
                        ^ tableP[(13 * 256) + inputP[2]]
                        ^ tableP[(12 * 256) + inputP[3]];
                    var b = tableP[(11 * 256) + inputP[4]]
                        ^ tableP[(10 * 256) + inputP[5]]
                        ^ tableP[(9 * 256) + inputP[6]]
                        ^ tableP[(8 * 256) + inputP[7]];
                    var c = tableP[(7 * 256) + inputP[8]]
                        ^ tableP[(6 * 256) + inputP[9]]
                        ^ tableP[(5 * 256) + inputP[10]]
                        ^ tableP[(4 * 256) + inputP[11]];
                    var d = tableP[(3 * 256) + inputP[12]]
                        ^ tableP[(2 * 256) + inputP[13]]
                        ^ tableP[(1 * 256) + inputP[14]]
                        ^ tableP[(0 * 256) + inputP[15]];
                    _crc = (byte)(a ^ b ^ c ^ d);
                    inputP += 16;
                    length -= 16;
                }
                while (length > 0)
                {
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[0]]);
                    inputP++;
                    length--;
                }
            }
        }

        private unsafe void UpdateWithTableRef(byte* inputP, int offset, int length)
        {
            inputP += offset;
            fixed (byte* tableP = _table)
            {
                while (length >= 16)
                {
                    var a = tableP[(15 * 256) + ((_crc & 0xFF) ^ inputP[0])]
                        ^ tableP[(14 * 256) + inputP[1]]
                        ^ tableP[(13 * 256) + inputP[2]]
                        ^ tableP[(12 * 256) + inputP[3]];
                    var b = tableP[(11 * 256) + inputP[4]]
                        ^ tableP[(10 * 256) + inputP[5]]
                        ^ tableP[(9 * 256) + inputP[6]]
                        ^ tableP[(8 * 256) + inputP[7]];
                    var c = tableP[(7 * 256) + inputP[8]]
                        ^ tableP[(6 * 256) + inputP[9]]
                        ^ tableP[(5 * 256) + inputP[10]]
                        ^ tableP[(4 * 256) + inputP[11]];
                    var d = tableP[(3 * 256) + inputP[12]]
                        ^ tableP[(2 * 256) + inputP[13]]
                        ^ tableP[(1 * 256) + inputP[14]]
                        ^ tableP[(0 * 256) + inputP[15]];
                    _crc = (byte)(a ^ b ^ c ^ d);
                    inputP += 16;
                    length -= 16;
                }
                while (length > 0)
                {
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[0]]);
                    inputP++;
                    length--;
                }
            }
        }

        #endregion Update bytes

        internal override void Reset()
        {
            _crc = _initParsed;
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
            if (bits > 0)
            {
                input <<= bits;
                input >>= bits;
            }
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
            _crc ^= _xoroutParsed;
        }
    }
}