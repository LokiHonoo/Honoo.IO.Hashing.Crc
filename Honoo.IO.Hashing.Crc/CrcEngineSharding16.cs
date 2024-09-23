﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Honoo.IO.Hashing
{
    internal sealed class CrcEngineSharding16 : CrcEngine
    {
        #region Members

        private readonly int _checksumByteLength;
        private readonly int _checksumHexLength;
        private readonly CrcCore _core = CrcCore.Sharding16;
        private readonly ushort[] _initParsed;
        private readonly int _moves;
        private readonly ushort[] _polyParsed;
        private readonly bool _refin;
        private readonly bool _refout;
        private readonly ushort[][] _table;
        private readonly int _width;
        private readonly bool _withTable;
        private readonly ushort[] _xoroutParsed;

        private ushort[] _crc;
        internal override int ChecksumByteLength => _checksumByteLength;
        internal override CrcCore Core => _core;
        internal override int Width => _width;

        internal override bool WithTable => _withTable;

        #endregion Members

        #region Construction

        internal CrcEngineSharding16(int width, bool refin, bool refout, ushort[] poly, ushort[] init, ushort[] xorout, bool generateTable)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum size. The allowed values are more than 0.", nameof(width));
            }
            _width = width;
            _checksumByteLength = (int)Math.Ceiling(width / 8d);
            _checksumHexLength = (int)Math.Ceiling(width / 4d);
            _refin = refin;
            _refout = refout;
            _withTable = generateTable;
            //
            int rem = width % 16;
            _moves = rem > 0 ? 16 - rem : 0;
            _polyParsed = Parse(poly, _moves, _refin);
            _initParsed = Parse(init, _moves, _refin);
            _xoroutParsed = TruncateLeft(xorout, _moves);
            _table = generateTable ? _refin ? GenerateReversedTable(_polyParsed) : GenerateTable(_polyParsed) : null;
            _crc = (ushort[])_initParsed.Clone();
        }

        #endregion Construction

        #region Table

        internal static ushort[][] GenerateReversedTable(ushort[] polyParsed)
        {
            ushort[][] table = new ushort[256][];
            for (int i = 0; i < 256; i++)
            {
                ushort[] data = new ushort[polyParsed.Length];
                data[data.Length - 1] = (ushort)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((data[data.Length - 1] & 1) == 1)
                    {
                        data = ShiftRight(data, 1);
                        data = Xor(data, polyParsed);
                    }
                    else
                    {
                        data = ShiftRight(data, 1);
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal static ushort[][] GenerateTable(ushort[] polyParsed)
        {
            ushort[][] table = new ushort[256][];
            for (int i = 0; i < 256; i++)
            {
                ushort[] data = new ushort[polyParsed.Length];
                data[0] = (ushort)(i << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((data[0] & 0x8000) == 0x8000)
                    {
                        data = ShiftLeft(data, 1);
                        data = Xor(data, polyParsed);
                    }
                    else
                    {
                        data = ShiftLeft(data, 1);
                    }
                }
                table[i] = data;
            }
            return table;
        }

        internal override object CloneTable()
        {
            if (_table != null)
            {
                var table = new List<ushort[]>();
                foreach (ushort[] item in _table)
                {
                    table.Add((ushort[])item.Clone());
                }
                return table.ToArray();
            }
            return null;
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
            _crc = (ushort[])_initParsed.Clone();
            return result;
        }

        internal override int ComputeFinal(Endian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            Finish();
            if (outputEndian == Endian.LittleEndian)
            {
                int j = -1;
                int m = 8;
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    if (i % 2 == 0)
                    {
                        j++;
                        m = 8;
                    }
                    outputBuffer[i + outputOffset] = (byte)(_crc[j] << m);
                    m -= 8;
                }
            }
            else
            {
                int j = _crc.Length;
                int m = 0;
                for (int i = 0; i < _checksumByteLength; i++)
                {
                    if (i % 2 == 0)
                    {
                        j--;
                        m = 0;
                    }
                    outputBuffer[_checksumByteLength - 1 - i + outputOffset] = (byte)(_crc[j] >> m);
                    m += 8;
                }
            }
            _crc = (ushort[])_initParsed.Clone();
            return _checksumByteLength;
        }

        internal override bool ComputeFinal(out byte checksum)
        {
            Finish();
            checksum = (byte)_crc[_crc.Length - 1];
            _crc = (ushort[])_initParsed.Clone();
            return _width > 8;
        }

        internal override bool ComputeFinal(out ushort checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            _crc = (ushort[])_initParsed.Clone();
            return _width > 16;
        }

        internal override bool ComputeFinal(out uint checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            if (_crc.Length > 1) checksum |= (_crc[_crc.Length - 1 - 1] & 0xFFFFU) << 16;
            _crc = (ushort[])_initParsed.Clone();
            return _width > 32;
        }

        internal override bool ComputeFinal(out ulong checksum)
        {
            Finish();
            checksum = _crc[_crc.Length - 1];
            if (_crc.Length > 1) checksum |= (_crc[_crc.Length - 1 - 1] & 0xFFFFUL) << 16;
            if (_crc.Length > 2) checksum |= (_crc[_crc.Length - 1 - 2] & 0xFFFFUL) << 32;
            if (_crc.Length > 3) checksum |= (_crc[_crc.Length - 1 - 3] & 0xFFFFUL) << 48;
            _crc = (ushort[])_initParsed.Clone();
            return _width > 64;
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
            for (int i = _crc.Length - 1; i >= 1; i--)
            {
                _crc[i] ^= 0;
            }
            _crc[0] ^= (ushort)(input << 8);
            for (int j = 0; j < 8; j++)
            {
                if ((_crc[0] & 0x8000) == 0x8000)
                {
                    _crc = ShiftLeft(_crc, 1);
                    _crc = Xor(_crc, _polyParsed);
                }
                else
                {
                    _crc = ShiftLeft(_crc, 1);
                }
            }
        }

        private void UpdateWithoutTableRef(byte input)
        {
            for (int i = 0; i < _crc.Length - 1; i++)
            {
                _crc[i] ^= 0;
            }
            _crc[_crc.Length - 1] ^= input;
            for (int j = 0; j < 8; j++)
            {
                if ((_crc[_crc.Length - 1] & 1) == 1)
                {
                    _crc = ShiftRight(_crc, 1);
                    _crc = Xor(_crc, _polyParsed);
                }
                else
                {
                    _crc = ShiftRight(_crc, 1);
                }
            }
        }

        private void UpdateWithTable(byte input)
        {
            ushort[] match = _table[((_crc[0] >> 8) & 0xFF) ^ input];
            _crc = ShiftLeft(_crc, 8);
            _crc = Xor(_crc, match);
        }

        private void UpdateWithTableRef(byte input)
        {
            ushort[] match = _table[(_crc[_crc.Length - 1] & 0xFF) ^ input];
            _crc = ShiftRight(_crc, 8);
            _crc = Xor(_crc, match);
        }

        #endregion Update byte

        #region Update bytes

        internal override void Update(byte[] inputBuffer, int offset, int length)
        {
            if (_withTable)
            {
                if (_refin)
                {
                    UpdateWithTableRef(inputBuffer, offset, length);
                }
                else
                {
                    UpdateWithTable(inputBuffer, offset, length);
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

        private void UpdateWithTable(byte[] inputBuffer, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                UpdateWithTable(inputBuffer[i]);
            }
        }

        private void UpdateWithTableRef(byte[] inputBuffer, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                UpdateWithTableRef(inputBuffer[i]);
            }
        }

        #endregion Update bytes

        internal override void Reset()
        {
            _crc = (ushort[])_initParsed.Clone();
        }

        private static string GetBinaryString(ushort[] input, int width)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 2).PadLeft(16, '0'));
            }
            if (result.Length > width)
            {
                result.Remove(0, result.Length - width);
            }
            return result.ToString();
        }

        private static string GetHexString(ushort[] input, int hexLength)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 16).PadLeft(4, '0'));
            }
            if (result.Length > hexLength)
            {
                result.Remove(0, result.Length - hexLength);
            }
            return result.ToString();
        }

        private static ushort[] Parse(ushort[] input, int moves, bool reverse)
        {
            if (moves > 0)
            {
                input = ShiftLeft(input, moves);
            }
            if (reverse)
            {
                input = Reverse(input);
            }
            return input;
        }

        private static ushort Reverse(ushort input)
        {
            input = (ushort)((input & 0x5555) << 1 | (input >> 1) & 0x5555);
            input = (ushort)((input & 0x3333) << 2 | (input >> 2) & 0x3333);
            input = (ushort)((input & 0x0F0F) << 4 | (input >> 4) & 0x0F0F);
            input = (ushort)((input & 0x00FF) << 8 | (input >> 8) & 0x00FF);
            return input;
        }

        private static ushort[] Reverse(ushort[] input)
        {
            ushort tmp;
            for (int i = 0; i < (int)Math.Ceiling(input.Length / 2d); i++)
            {
                tmp = Reverse(input[input.Length - 1 - i]);
                input[input.Length - 1 - i] = Reverse(input[i]);
                input[i] = tmp;
            }
            return input;
        }

        private static ushort[] ShiftLeft(ushort[] input, int bits)
        {
            if (bits > 0)
            {
                for (int i = 0; i < input.Length - 1; i++)
                {
                    input[i] = (ushort)((input[i] << bits) | (input[i + 1] >> (16 - bits)));
                }
                input[input.Length - 1] <<= bits;
            }
            return input;
        }

        private static ushort[] ShiftRight(ushort[] input, int bits)
        {
            if (bits > 0)
            {
                for (int i = input.Length - 1; i >= 1; i--)
                {
                    input[i] = (ushort)((input[i] >> bits) | (input[i - 1] << (16 - bits)));
                }
                input[0] >>= bits;
            }
            return input;
        }

        private static ushort[] TruncateLeft(ushort[] input, int bits)
        {
            if (bits > 0)
            {
                input = ShiftLeft(input, bits);
                input = ShiftRight(input, bits);
            }
            return input;
        }

        private static ushort[] Xor(ushort[] input, ushort[] input2)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] ^= input2[i];
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
                _crc = ShiftRight(_crc, _moves);
            }
            _crc = Xor(_crc, _xoroutParsed);
        }
    }
}