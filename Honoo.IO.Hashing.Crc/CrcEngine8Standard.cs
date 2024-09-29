using System;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngine8Standard : CrcEngine
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
        private readonly CrcTableInfo _tableInfo = CrcTableInfo.Standard;
        private readonly int _width;
        private readonly byte _xoroutParsed;
        private byte _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override CrcTableInfo TableInfo => _tableInfo;
        internal override int Width => _width;

        #endregion Members

        #region Construction

        internal CrcEngine8Standard(int width, bool refin, bool refout, byte poly, byte init, byte xorout, byte[] table)
        {
            if (width <= 0 || width > 8)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8.", nameof(width));
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

        internal static byte[] GenerateTable(byte polyParsed)
        {
            byte[] table = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                byte data = (byte)i;
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
                table[i] = data;
            }
            return table;
        }

        internal static byte[] GenerateTableRef(byte polyParsed)
        {
            byte[] table = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                byte data = (byte)i;
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
                table[i] = data;
            }
            return table;
        }

        internal override CrcTable CloneTable()
        {
            return new CrcTable(_tableInfo, _core, _table);
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

        internal override int ComputeFinal(CrcEndian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            outputBuffer[outputOffset] = _crc;
            _crc = _initParsed;
            return 1;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _initParsed;
            return false;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = _crc;
            _crc = _initParsed;
            return false;
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
                    UpdateWithTableRef(inputP, length);
                }
                else
                {
                    UpdateWithTable(inputP, length);
                }
            }
        }

        private unsafe void UpdateWithTable(byte* inputP, int length)
        {
            fixed (byte* tableP = _table)
            {
                while (length >= 32)
                {
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[1]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[2]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[3]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[4]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[5]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[6]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[7]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[8]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[9]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[10]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[11]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[12]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[13]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[14]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[15]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[16]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[17]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[18]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[19]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[20]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[21]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[22]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[23]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[24]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[25]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[26]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[27]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[28]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[29]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[30]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[31]]);
                    inputP += 32;
                    length -= 32;
                }
                if (length >= 16)
                {
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[1]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[2]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[3]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[4]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[5]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[6]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[7]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[8]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[9]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[10]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[11]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[12]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[13]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[14]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[15]]);
                    inputP += 16;
                    length -= 16;
                }
                if (length >= 8)
                {
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[1]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[2]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[3]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[4]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[5]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[6]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[7]]);
                    inputP += 8;
                    length -= 8;
                }
                if (length >= 4)
                {
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[1]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[2]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[3]]);
                    inputP += 4;
                    length -= 4;
                }
                if (length >= 2)
                {
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[1]]);
                    inputP += 2;
                    length -= 2;
                }
                if (length > 0)
                {
                    _crc = (byte)((_crc << 8) ^ tableP[_crc ^ inputP[0]]);
                }
            }
        }

        private unsafe void UpdateWithTableRef(byte* inputP, int length)
        {
            fixed (byte* tableP = _table)
            {
                while (length >= 32)
                {
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[1]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[2]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[3]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[4]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[5]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[6]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[7]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[8]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[9]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[10]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[11]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[12]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[13]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[14]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[15]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[16]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[17]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[18]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[19]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[20]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[21]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[22]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[23]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[24]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[25]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[26]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[27]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[28]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[29]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[30]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[31]]);
                    inputP += 32;
                    length -= 32;
                }
                if (length >= 16)
                {
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[1]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[2]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[3]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[4]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[5]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[6]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[7]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[8]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[9]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[10]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[11]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[12]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[13]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[14]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[15]]);
                    inputP += 16;
                    length -= 16;
                }
                if (length >= 8)
                {
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[1]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[2]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[3]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[4]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[5]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[6]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[7]]);
                    inputP += 8;
                    length -= 8;
                }
                if (length >= 4)
                {
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[1]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[2]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[3]]);
                    inputP += 4;
                    length -= 4;
                }
                if (length >= 2)
                {
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[0]]);
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[1]]);
                    inputP += 2;
                    length -= 2;
                }
                if (length > 0)
                {
                    _crc = (byte)((_crc >> 8) ^ tableP[_crc ^ inputP[0]]);
                }
            }
        }

        #endregion Update bytes

        internal static byte Parse(byte input, int moves, bool reverse)
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

        internal override void Reset()
        {
            _crc = _initParsed;
        }

        private static string GetBinaryString(byte input, int width)
        {
            string result = Convert.ToString(input, 2).PadLeft(8, '0');
            if (result.Length > width)
            {
                result = result.Substring(result.Length - width, width);
            }
            return result;
        }

        private static string GetHexString(byte input, int hexLength)
        {
            string result = Convert.ToString(input, 16).PadLeft(2, '0');
            if (result.Length > hexLength)
            {
                result = result.Substring(result.Length - hexLength, hexLength);
            }
            return result;
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