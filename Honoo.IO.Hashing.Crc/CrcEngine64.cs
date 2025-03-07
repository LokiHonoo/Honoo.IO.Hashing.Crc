using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine64 : CrcEngine
    {
        #region Members

        private readonly int _checksumByteLength;
        private readonly int _checksumHexLength;
        private readonly CrcCore _core = CrcCore.UInt64;
        private readonly ulong _initParsed;
        private readonly int _moves;
        private readonly ulong _polyParsed;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly CrcTableInfo _tableInfo = CrcTableInfo.None;
        private readonly int _width;
        private readonly ulong _xoroutParsed;
        private ulong _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override CrcTableInfo TableInfo => _tableInfo;
        internal override int Width => _width;

        #endregion Members

        #region Construction

        internal CrcEngine64(int width, ulong poly, ulong init, ulong xorout, bool refin, bool refout)
        {
            if (width <= 0 || width > 64)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64.", nameof(width));
            }
            _width = width;
            _refin = refin;
            _refout = refout;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            _moves = 64 - width;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _crc = _initParsed;
        }

        #endregion Construction

        #region Table

        internal override CrcTable CloneTable()
        {
            return new CrcTable(_tableInfo, _core, null);
        }

        #endregion Table

        #region ComputeFinal

        internal override CrcValue ComputeFinal()
        {
            Finish();
            var checksum = new CrcUInt64Value(_crc, _width);
            _crc = _initParsed;
            return checksum;
        }

        #endregion ComputeFinal

        #region Update byte

        internal override void Update(byte input)
        {
            if (_refin)
            {
                UpdateWithoutTableRef(input);
            }
            else
            {
                UpdateWithoutTable(input);
            }
        }

        private void UpdateWithoutTable(byte input)
        {
            _crc ^= (ulong)input << 56;
            for (int j = 0; j < 8; j++)
            {
                if ((_crc & 0x8000000000000000) == 0x8000000000000000)
                {
                    _crc = (_crc << 1) ^ _polyParsed;
                }
                else
                {
                    _crc <<= 1;
                }
            }
        }

        private void UpdateWithoutTableRef(byte input)
        {
            _crc ^= input;
            for (int j = 0; j < 8; j++)
            {
                if ((_crc & 1) == 1)
                {
                    _crc = (_crc >> 1) ^ _polyParsed;
                }
                else
                {
                    _crc >>= 1;
                }
            }
        }

        #endregion Update byte

        #region Update bytes

        internal override unsafe void Update(byte[] inputBuffer, int offset, int length)
        {
            if (_refin)
            {
                UpdateWithoutTableRef(inputBuffer, offset, length);
            }
            else
            {
                UpdateWithoutTable(inputBuffer, offset, length);
            }
        }

        private void UpdateWithoutTable(byte[] inputBuffer, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                UpdateWithoutTable(inputBuffer[i]);
            }
        }

        private void UpdateWithoutTableRef(byte[] inputBuffer, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                UpdateWithoutTableRef(inputBuffer[i]);
            }
        }

        #endregion Update bytes

        internal override void Reset()
        {
            _crc = _initParsed;
        }

        private static ulong Parse(ulong input, int moves, bool reverse)
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

        private static ulong Reverse(ulong input)
        {
            input = (input & 0x5555555555555555) << 1 | (input >> 1) & 0x5555555555555555;
            input = (input & 0x3333333333333333) << 2 | (input >> 2) & 0x3333333333333333;
            input = (input & 0x0F0F0F0F0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F0F0F0F0F;
            input = (input & 0x00FF00FF00FF00FF) << 8 | (input >> 8) & 0x00FF00FF00FF00FF;
            input = (input & 0x0000FFFF0000FFFF) << 16 | (input >> 16) & 0x0000FFFF0000FFFF;
            input = (input & 0x00000000FFFFFFFF) << 32 | (input >> 32) & 0x00000000FFFFFFFF;
            return input;
        }

        private static ulong TruncateLeft(ulong input, int bits)
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