using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine32 : CrcEngine
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
        private readonly int _width;
        private readonly bool _withTable;
        private readonly uint _xoroutParsed;
        private uint _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override int Width => _width;

        internal override bool WithTable => _withTable;

        #endregion Members

        #region Construction

        internal CrcEngine32(int width, bool refin, bool refout, uint poly, uint init, uint xorout, bool generateTable)
        {
            if (width <= 0 || width > 32)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 32.", nameof(width));
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
            _table = generateTable ? _refin ? GenerateReversedTable(_polyParsed) : GenerateTable(_polyParsed) : null;
            _crc = _initParsed;
            _withTable = generateTable;
        }

        internal CrcEngine32(int width, bool refin, bool refout, uint poly, uint init, uint xorout, uint[] table)
        {
            if (width <= 0 || width > 32)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are between 0 - 32.", nameof(width));
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
            _table = table;
            _crc = _initParsed;
            _withTable = table != null;
        }

        #endregion Construction

        #region Table

        internal static uint[] GenerateReversedTable(uint polyParsed)
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

        internal static uint[] GenerateTable(uint polyParsed)
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

        internal override object CloneTable()
        {
            return _table?.Clone();
        }

        #endregion Table

        #region ComputeFinal

        internal override string ComputeFinal(CrcStringFormat outputFormat)
        {
            Finish();
            string result;
            switch (outputFormat)
            {
                case CrcStringFormat.Binary: result = GetBinaryString(_crc, _width); break;
                case CrcStringFormat.Hex: result = GetHexString(_crc, _checksumHexLength); break;
                default: throw new ArgumentException("Invalid crc string format.", nameof(outputFormat));
            }
            _crc = _initParsed;
            return result;
        }

        internal override int ComputeFinal(Endian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            if (outputEndian == Endian.LittleEndian)
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    outputBuffer[i + outputOffset] = (byte)(_crc >> (i * 8));
                }
            }
            else
            {
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    outputBuffer[_checksumByteLength - 1 - i + outputOffset] = (byte)(_crc >> (i * 8));
                }
            }
            _crc = _initParsed;
            return _checksumByteLength;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = (byte)_crc;
            _crc = _initParsed;
            return _width > 8;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = (ushort)_crc;
            _crc = _initParsed;
            return _width > 16;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _initParsed;
            return false;
        }

        internal override bool ComputeFinal(out ulong checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _initParsed;
            return false;
        }

        #endregion ComputeFinal

        #region Update byte

        internal override void Update(byte input)
        {
            if (_withTable)
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
            else
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
        }

        private void UpdateWithoutTable(byte input)
        {
            _crc ^= (uint)(input << 24);
            for (int j = 0; j < 8; j++)
            {
                if ((_crc & 0x80000000U) == 0x80000000U)
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

        private void UpdateWithTable(byte input)
        {
            _crc = (_crc << 8) ^ _table[((_crc >> 24) & 0xFF) ^ input];
        }

        private void UpdateWithTableRef(byte input)
        {
            _crc = (_crc >> 8) ^ _table[(_crc & 0xFF) ^ input];
        }

        #endregion Update byte

        #region Update bytes

        internal override unsafe void Update(byte[] inputBuffer, int offset, int length)
        {
            if (_withTable)
            {
                if (_refin)
                {
                    fixed (byte* inputPointer = inputBuffer)
                    {
                        UpdateWithTableRef(inputPointer, length);
                    }
                }
                else
                {
                    fixed (byte* inputPointer = inputBuffer)
                    {
                        UpdateWithTable(inputPointer, length);
                    }
                }
            }
            else
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

        private unsafe void UpdateWithTable(byte* inputPointer, int length)
        {
            fixed (uint* tablePointer = _table)
            {
                while (length >= 32)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[7]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[8]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[9]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[10]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[11]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[12]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[13]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[14]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[15]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[16]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[17]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[18]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[19]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[20]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[21]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[22]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[23]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[24]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[25]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[26]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[27]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[28]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[29]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[30]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[31]];
                    inputPointer += 32;
                    length -= 32;
                }
                if (length >= 16)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[7]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[8]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[9]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[10]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[11]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[12]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[13]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[14]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[15]];
                    inputPointer += 16;
                    length -= 16;
                }
                if (length >= 8)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[7]];
                    inputPointer += 8;
                    length -= 8;
                }
                if (length >= 4)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[3]];
                    inputPointer += 4;
                    length -= 4;
                }
                if (length >= 2)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[1]];
                    inputPointer += 2;
                    length -= 2;
                }
                if (length > 0)
                {
                    _crc = (_crc << 8) ^ tablePointer[((_crc >> 24) & 0xFF) ^ inputPointer[0]];
                }
            }
        }

        private unsafe void UpdateWithTableRef(byte* inputPointer, int length)
        {
            fixed (uint* tablePointer = _table)
            {
                while (length >= 32)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[7]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[8]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[9]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[10]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[11]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[12]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[13]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[14]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[15]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[16]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[17]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[18]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[19]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[20]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[21]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[22]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[23]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[24]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[25]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[26]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[27]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[28]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[29]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[30]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[31]];
                    inputPointer += 32;
                    length -= 32;
                }
                if (length >= 16)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[7]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[8]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[9]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[10]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[11]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[12]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[13]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[14]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[15]];
                    inputPointer += 16;
                    length -= 16;
                }
                if (length >= 8)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[3]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[4]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[5]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[6]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[7]];
                    inputPointer += 8;
                    length -= 8;
                }
                if (length >= 4)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[2]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[3]];
                    inputPointer += 4;
                    length -= 4;
                }
                if (length >= 2)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[1]];
                    inputPointer += 2;
                    length -= 2;
                }
                if (length > 0)
                {
                    _crc = (_crc >> 8) ^ tablePointer[(_crc & 0xFF) ^ inputPointer[0]];
                }
            }
        }

        #endregion Update bytes

        internal override void Reset()
        {
            _crc = _initParsed;
        }

        private static string GetBinaryString(uint input, int width)
        {
            string result = Convert.ToString(input, 2).PadLeft(32, '0');
            if (result.Length > width)
            {
                result = result.Substring(result.Length - width, width);
            }
            return result;
        }

        private static string GetHexString(uint input, int hexLength)
        {
            string result = Convert.ToString(input, 16).PadLeft(8, '0');
            if (result.Length > hexLength)
            {
                result = result.Substring(result.Length - hexLength, hexLength);
            }
            return result;
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