﻿using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine32Standard : CrcEngine
    {
        #region Members

        private readonly int _checksumByteLength;
        private readonly int _checksumHexLength;
        private readonly CrcCore _core = CrcCore.UInt32;
        private readonly uint _initParsed;
        private readonly int _moves;
        private readonly uint _polyParsed;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly uint[] _table;
        private readonly CrcTableInfo _tableInfo = CrcTableInfo.Standard;
        private readonly int _width;
        private readonly uint _xoroutParsed;
        private uint _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override CrcTableInfo TableInfo => _tableInfo;
        internal override int Width => _width;

        #endregion Members

        #region Construction

        internal CrcEngine32Standard(int width, uint poly, uint init, uint xorout, bool refin, bool refout, uint[] table)
        {
            if (width <= 0 || width > 32)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 32.", nameof(width));
            }
            _width = width;
            _refin = refin;
            _refout = refout;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            _moves = 32 - width;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _table = table ?? (_refin ? GenerateTableRef(_polyParsed) : GenerateTable(_polyParsed));
            _crc = _initParsed;
        }

        #endregion Construction

        #region Table

        internal static uint[] GenerateTable(int width, uint poly, bool refin)
        {
            uint polyParsed = Parse(poly, 32 - width, refin);
            return refin ? GenerateTableRef(polyParsed) : GenerateTable(polyParsed);
        }

        internal override CrcTable CloneTable()
        {
            return new CrcTable(_tableInfo, _core, _table);
        }

        private static uint[] GenerateTable(uint polyParsed)
        {
            uint[] table = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                uint data = (uint)(i << 24);
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 0x80000000) == 0x80000000)
                    {
                        data = (data << 1) ^ polyParsed;
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

        private static uint[] GenerateTableRef(uint polyParsed)
        {
            uint[] table = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                uint data = (uint)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data & 1) == 1)
                    {
                        data = (data >> 1) ^ polyParsed;
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

        #endregion Table

        #region ComputeFinal

        internal override CrcValue ComputeFinal()
        {
            Finish();
            var checksum = new CrcUInt32Value(_crc, _width);
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
            _crc = (_crc << 8) ^ _table[(_crc >> 24) ^ input];
        }

        private void UpdateWithTableRef(byte input)
        {
            _crc = (_crc >> 8) ^ _table[(_crc ^ input) & 0xFF];
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
            fixed (uint* tableP = _table)
            {
                while (length >= 16)
                {
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[0]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[1]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[2]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[3]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[4]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[5]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[6]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[7]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[8]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[9]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[10]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[11]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[12]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[13]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[14]];
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[15]];
                    inputP += 16;
                    length -= 16;
                }
                while (length > 0)
                {
                    _crc = (_crc << 8) ^ tableP[(_crc >> 24) ^ inputP[0]];
                    inputP++;
                    length--;
                }
            }
        }

        private unsafe void UpdateWithTableRef(byte* inputP, int offset, int length)
        {
            inputP += offset;
            fixed (uint* tableP = _table)
            {
                while (length >= 16)
                {
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[0]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[1]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[2]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[3]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[4]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[5]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[6]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[7]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[8]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[9]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[10]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[11]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[12]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[13]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[14]) & 0xFF];
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[15]) & 0xFF];
                    inputP += 16;
                    length -= 16;
                }
                while (length > 0)
                {
                    _crc = (_crc >> 8) ^ tableP[(_crc ^ inputP[0]) & 0xFF];
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

        private static uint Parse(uint input, int moves, bool reverse)
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

        private static uint Reverse(uint input)
        {
            input = (input & 0x55555555) << 1 | (input >> 1) & 0x55555555;
            input = (input & 0x33333333) << 2 | (input >> 2) & 0x33333333;
            input = (input & 0x0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F;
            input = (input & 0x00FF00FF) << 8 | (input >> 8) & 0x00FF00FF;
            input = (input & 0x0000FFFF) << 16 | (input >> 16) & 0x0000FFFF;
            return input;
        }

        private static uint TruncateLeft(uint input, int bits)
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