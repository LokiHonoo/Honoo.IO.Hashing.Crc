//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Honoo.IO.Hashing
//{
//    internal sealed class CrcEngine128L2M16x : CrcEngine
//    {
//        #region Members

//        private readonly int _checksumByteLength;
//        private readonly int _checksumHexLength;
//        private readonly ulong[] _initParsed;
//        private readonly int _moves;
//        private readonly ulong[] _polyParsed;
//        private readonly bool _refin;
//        private readonly bool _refout;
//        private readonly ulong[][] _table;
//        private readonly int _width;
//        private readonly ulong[] _xoroutParsed;
//        private ulong[] _crc;

//        internal override int ChecksumByteLength => _checksumByteLength;
//        internal override CrcCore Core => CrcCore.UInt128L2;
//        internal override int Width => _width;
//        internal override CrcTable WithTable => CrcTable.M16x;

//        #endregion Members

//        #region Construction

//        internal CrcEngine128L2M16x(int width, bool refin, bool refout, ulong[] poly, ulong[] init, ulong[] xorout)
//        {
//            if (width <= 0 || width > 128)
//            {
//                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 128.", nameof(width));
//            }
//            _width = width;
//            _refin = refin;
//            _refout = refout;
//            _checksumByteLength = (int)Math.Ceiling(width / 8d);
//            _checksumHexLength = (int)Math.Ceiling(width / 4d);
//            _moves = 128 - width;
//            _polyParsed = Parse(poly, _moves, _refin);
//            _initParsed = Parse(init, _moves, _refin);
//            _xoroutParsed = TruncateLeft(xorout, _moves);
//            _table = _refin ? GenerateTableRef(_polyParsed) : GenerateTable(_polyParsed);
//            _crc = (ulong[])_initParsed.Clone();
//        }

//        #endregion Construction

//        #region Table

//        internal static ulong[][] GenerateTable(ulong[] polyParsed)
//        {
//            ulong[][] table = new ulong[256 * 16][];
//            for (int i = 0; i < 256; i++)
//            {
//                ulong[] data = new ulong[2];
//                data[0] = (ulong)i << 56;
//                for (int k = 0; k < 16; k++)
//                {
//                    for (int j = 0; j < 8; j++)
//                    {
//                        if ((data[0] & 0x8000000000000000) == 0x8000000000000000)
//                        {
//                            data = ShiftLeft(data, 1);
//                            data = Xor(data, polyParsed);
//                        }
//                        else
//                        {
//                            data = ShiftLeft(data, 1);
//                        }
//                    }
//                    table[(k * 256) + i] = data;
//                }
//            }
//            return table;
//        }

//        internal static ulong[][] GenerateTableRef(ulong[] polyParsed)
//        {
//            ulong[][] table = new ulong[256 * 16][];
//            for (int i = 0; i < 256; i++)
//            {
//                ulong[] data = new ulong[2];
//                data[1] = (ulong)i;
//                for (int k = 0; k < 16; k++)
//                {
//                    for (int j = 0; j < 8; j++)
//                    {
//                        if ((data[1] & 1) == 1)
//                        {
//                            data = ShiftRight(data, 1);
//                            data = Xor(data, polyParsed);
//                        }
//                        else
//                        {
//                            data = ShiftRight(data, 1);
//                        }
//                    }
//                    table[(k * 256) + i] = data;
//                }
//            }
//            return table;
//        }

//        internal override object CloneTable()
//        {
//            if (_table != null)
//            {
//                var table = new List<ulong[]>();
//                foreach (ulong[] item in _table)
//                {
//                    table.Add((ulong[])item.Clone());
//                }
//                return table.ToArray();
//            }
//            return null;
//        }

//        #endregion Table

//        #region ComputeFinal

//        internal override string ComputeFinal(CrcStringFormat outputFormat)
//        {
//            Finish();
//            string result;
//            switch (outputFormat)
//            {
//                case CrcStringFormat.Binary: result = GetBinaryString(_crc, _width); break;
//                case CrcStringFormat.Hex: result = GetHexString(_crc, _checksumHexLength); break;
//                default: throw new ArgumentException("Invalid crc string format.", nameof(outputFormat));
//            }
//            _crc = (ulong[])_initParsed.Clone();
//            return result;
//        }

//        internal override int ComputeFinal(Endian outputEndian, byte[] outputBuffer, int outputOffset)
//        {
//            Finish();
//            if (outputEndian == Endian.LittleEndian)
//            {
//                int j = -1;
//                int m = 56;
//                for (int i = 0; i < _checksumByteLength; i++)
//                {
//                    if (i % 8 == 0)
//                    {
//                        j++;
//                        m = 56;
//                    }
//                    outputBuffer[i + outputOffset] = (byte)(_crc[j] << m);
//                    m -= 8;
//                }
//            }
//            else
//            {
//                int j = 2;
//                int m = 0;
//                for (int i = 0; i < _checksumByteLength; i++)
//                {
//                    if (i % 8 == 0)
//                    {
//                        j--;
//                        m = 0;
//                    }
//                    outputBuffer[_checksumByteLength - 1 - i + outputOffset] = (byte)(_crc[j] >> m);
//                    m += 8;
//                }
//            }
//            _crc = (ulong[])_initParsed.Clone();
//            return _checksumByteLength;
//        }

//        internal override bool ComputeFinal(out byte checksum)
//        {
//            Finish();
//            checksum = (byte)_crc[1];
//            _crc = (ulong[])_initParsed.Clone();
//            return _width > 8;
//        }

//        internal override bool ComputeFinal(out ushort checksum)
//        {
//            Finish();
//            checksum = (ushort)_crc[1];
//            _crc = (ulong[])_initParsed.Clone();
//            return _width > 16;
//        }

//        internal override bool ComputeFinal(out uint checksum)
//        {
//            Finish();
//            checksum = (uint)_crc[1];
//            _crc = (ulong[])_initParsed.Clone();
//            return _width > 32;
//        }

//        internal override bool ComputeFinal(out ulong checksum)
//        {
//            Finish();
//            checksum = _crc[1];
//            _crc = (ulong[])_initParsed.Clone();
//            return _width > 64;
//        }

//        #endregion ComputeFinal

//        #region Update byte

//        internal override void Update(byte input)
//        {
//            if (_refin)
//            {
//                UpdateWithTableRef(input);
//            }
//            else
//            {
//                UpdateWithTable(input);
//            }
//        }

//        private void UpdateWithTable(byte input)
//        {
//            ulong[] match = _table[(_crc[0] >> 56) ^ input];
//            _crc = ShiftLeft(_crc, 8);
//            _crc = Xor(_crc, match);
//        }

//        private void UpdateWithTableRef(byte input)
//        {
//            ulong[] match = _table[(_crc[1] ^ input) & 0xFF];
//            _crc = ShiftRight(_crc, 8);
//            _crc = Xor(_crc, match);
//        }

//        #endregion Update byte

//        #region Update bytes

//        internal override unsafe void Update(byte[] inputBuffer, int offset, int length)
//        {
//            fixed (byte* inputP = inputBuffer)
//            {
//                if (_refin)
//                {
//                    UpdateWithTableRef(inputP, length);
//                }
//                else
//                {
//                    UpdateWithTable(inputP, length);
//                }
//            }
//        }

//        private unsafe void UpdateWithTable(byte* inputP, int length)
//        {
//            fixed (ulong[]* tableP = _table)
//            {
//                while (length >= 16)
//                {
//                    var a1 = tableP[(15 * 256) + XorTailFF(ShiftRightNew(_crc, 120), inputP[0])];
//                    var a2 = tableP[(14 * 256) + XorTailFF(ShiftRightNew(_crc, 112), inputP[1])];
//                    var a3 = tableP[(13 * 256) + XorTailFF(ShiftRightNew(_crc, 104), inputP[2])];
//                    var a4 = tableP[(12 * 256) + XorTailFF(ShiftRightNew(_crc, 96), inputP[3])];
//                    var a = Xor(Xor(Xor(a1, a2), a3), a4);
//                    var b1 = tableP[(11 * 256) + XorTailFF(ShiftRightNew(_crc, 88), inputP[4])];
//                    var b2 = tableP[(10 * 256) + XorTailFF(ShiftRightNew(_crc, 80), inputP[5])];
//                    var b3 = tableP[(9 * 256) + XorTailFF(ShiftRightNew(_crc, 72), inputP[6])];
//                    var b4 = tableP[(8 * 256) + XorTailFF(ShiftRightNew(_crc, 64), inputP[7])];
//                    var b = Xor(Xor(Xor(b1, b2), b3), b4);
//                    var c1 = tableP[(7 * 256) + XorTailFF(ShiftRightNew(_crc, 56), inputP[8])];
//                    var c2 = tableP[(6 * 256) + XorTailFF(ShiftRightNew(_crc, 48), inputP[9])];
//                    var c3 = tableP[(5 * 256) + XorTailFF(ShiftRightNew(_crc, 40), inputP[10])];
//                    var c4 = tableP[(4 * 256) + XorTailFF(ShiftRightNew(_crc, 32), inputP[11])];
//                    var c = Xor(Xor(Xor(c1, c2), c3), c4);
//                    var d1 = tableP[(3 * 256) + XorTailFF(ShiftRightNew(_crc, 24), inputP[12])];
//                    var d2 = tableP[(2 * 256) + XorTailFF(ShiftRightNew(_crc, 16), inputP[13])];
//                    var d3 = tableP[(1 * 256) + XorTailFF(ShiftRightNew(_crc, 8), inputP[14])];
//                    var d4 = tableP[(0 * 256) + XorTailFF(_crc, inputP[15])];
//                    var d = Xor(Xor(Xor(d1, d2), d3), d4);
//                    _crc = Xor(Xor(Xor(a, b), c), d);
//                    inputP += 16;
//                    length -= 16;
//                }
//                while (length > 0)
//                {
//                    ulong[] match = tableP[(_crc[0] >> 56) ^ inputP[0]];
//                    _crc = ShiftLeft(_crc, 8);
//                    _crc = Xor(_crc, match);
//                    inputP++;
//                    length--;
//                }
//            }
//        }

//        private unsafe void UpdateWithTableRef(byte* inputP, int length)
//        {
//            fixed (ulong[]* tableP = _table)
//            {
//                while (length >= 16)
//                {
//                    var a1 = tableP[(15 * 256) + XorTailFF(_crc, inputP[0])];
//                    var a2 = tableP[(14 * 256) + XorTailFF(ShiftRightNew(_crc, 8), inputP[1])];
//                    var a3 = tableP[(13 * 256) + XorTailFF(ShiftRightNew(_crc, 16), inputP[2])];
//                    var a4 = tableP[(12 * 256) + XorTailFF(ShiftRightNew(_crc, 24), inputP[3])];
//                    var a = Xor(Xor(Xor(a1, a2), a3), a4);
//                    var b1 = tableP[(11 * 256) + XorTailFF(ShiftRightNew(_crc, 32), inputP[4])];
//                    var b2 = tableP[(10 * 256) + XorTailFF(ShiftRightNew(_crc, 40), inputP[5])];
//                    var b3 = tableP[(9 * 256) + XorTailFF(ShiftRightNew(_crc, 48), inputP[6])];
//                    var b4 = tableP[(8 * 256) + XorTailFF(ShiftRightNew(_crc, 56), inputP[7])];
//                    var b = Xor(Xor(Xor(b1, b2), b3), b4);
//                    var c1 = tableP[(7 * 256) + XorTailFF(ShiftRightNew(_crc, 64), inputP[8])];
//                    var c2 = tableP[(6 * 256) + XorTailFF(ShiftRightNew(_crc, 72), inputP[9])];
//                    var c3 = tableP[(5 * 256) + XorTailFF(ShiftRightNew(_crc, 80), inputP[10])];
//                    var c4 = tableP[(4 * 256) + XorTailFF(ShiftRightNew(_crc, 88), inputP[11])];
//                    var c = Xor(Xor(Xor(c1, c2), c3), c4);
//                    var d1 = tableP[(3 * 256) + XorTailFF(ShiftRightNew(_crc, 96), inputP[12])];
//                    var d2 = tableP[(2 * 256) + XorTailFF(ShiftRightNew(_crc, 104), inputP[13])];
//                    var d3 = tableP[(1 * 256) + XorTailFF(ShiftRightNew(_crc, 112), inputP[14])];
//                    var d4 = tableP[(0 * 256) + XorTailFF(ShiftRightNew(_crc, 120), inputP[15])];
//                    var d = Xor(Xor(Xor(d1, d2), d3), d4);
//                    _crc = Xor(Xor(Xor(a, b), c), d);
//                    inputP += 16;
//                    length -= 16;
//                }
//                while (length > 0)
//                {
//                    ulong[] match = tableP[(_crc[1] ^ inputP[0]) & 0xFF];
//                    _crc = ShiftRight(_crc, 8);
//                    _crc = Xor(_crc, match);
//                    inputP++;
//                    length--;
//                }
//            }
//        }

//        #endregion Update bytes

//        internal override void Reset()
//        {
//            _crc = (ulong[])_initParsed.Clone();
//        }

//        private static string GetBinaryString(ulong[] input, int width)
//        {
//            StringBuilder result = new StringBuilder();
//            result.Append(Convert.ToString((long)input[0], 2).PadLeft(64, '0'));
//            result.Append(Convert.ToString((long)input[1], 2).PadLeft(64, '0'));
//            if (result.Length > width)
//            {
//                result.Remove(0, result.Length - width);
//            }
//            return result.ToString();
//        }

//        private static string GetHexString(ulong[] input, int hexLength)
//        {
//            StringBuilder result = new StringBuilder();
//            result.Append(Convert.ToString((long)input[0], 16).PadLeft(16, '0'));
//            result.Append(Convert.ToString((long)input[1], 16).PadLeft(16, '0'));
//            if (result.Length > hexLength)
//            {
//                result.Remove(0, result.Length - hexLength);
//            }
//            return result.ToString();
//        }

//        private static ulong[] Parse(ulong[] input, int moves, bool reverse)
//        {
//            if (moves > 0)
//            {
//                input = ShiftLeft(input, moves);
//            }
//            if (reverse)
//            {
//                input = Reverse(input);
//            }
//            return input;
//        }

//        private static ulong Reverse(ulong input)
//        {
//            input = (input & 0x5555555555555555) << 1 | (input >> 1) & 0x5555555555555555;
//            input = (input & 0x3333333333333333) << 2 | (input >> 2) & 0x3333333333333333;
//            input = (input & 0x0F0F0F0F0F0F0F0F) << 4 | (input >> 4) & 0x0F0F0F0F0F0F0F0F;
//            input = (input & 0x00FF00FF00FF00FF) << 8 | (input >> 8) & 0x00FF00FF00FF00FF;
//            input = (input & 0x0000FFFF0000FFFF) << 16 | (input >> 16) & 0x0000FFFF0000FFFF;
//            input = (input & 0x00000000FFFFFFFF) << 32 | (input >> 32) & 0x00000000FFFFFFFF;
//            return input;
//        }

//        private static ulong[] Reverse(ulong[] input)
//        {
//            ulong tmp = Reverse(input[1]);
//            input[1] = Reverse(input[0]);
//            input[0] = tmp;
//            return input;
//        }

//        private static ulong[] ShiftLeft(ulong[] input, int bits)
//        {
//            if (bits > 64)
//            {
//                input[0] = input[1] << (bits - 64);
//                input[1] = 0;
//            }
//            else if (bits > 0)
//            {
//                input[0] = (input[0] << bits) | (input[1] >> (64 - bits));
//                input[1] <<= bits;
//            }
//            return input;
//        }

//        private static ulong[] ShiftRight(ulong[] input, int bits)
//        {
//            if (bits > 64)
//            {
//                input[1] = input[0] >> (bits - 64);
//                input[0] = 0;
//            }
//            else if (bits > 0)
//            {
//                input[1] = (input[1] >> bits) | (input[0] << (64 - bits));
//                input[0] >>= bits;
//            }
//            return input;
//        }

//        private static ulong[] ShiftRightNew(ulong[] input, int bits)
//        {
//            if (bits > 64)
//            {
//                ulong[] result = new ulong[2];
//                result[1] = input[0] >> (bits - 64);
//                result[0] = 0;
//                return result;
//            }
//            else if (bits > 0)
//            {
//                ulong[] result = new ulong[2];

//                result[1] = (input[1] >> bits) | (input[0] << (64 - bits));
//                result[0] = input[0] >> bits;
//                return result;
//            }
//            return input;
//        }

//        private static ulong[] TruncateLeft(ulong[] input, int bits)
//        {
//            if (bits > 0)
//            {
//                input = ShiftLeft(input, bits);
//                input = ShiftRight(input, bits);
//            }
//            return input;
//        }

//        private static ulong[] Xor(ulong[] input, ulong[] input2)
//        {
//            input[0] ^= input2[0];
//            input[1] ^= input2[1];
//            return input;
//        }

//        private static ulong XorTailFF(ulong[] input, ulong input2)
//        {
//            return (input[1] ^ input2) & 0xFF;
//        }

//        private void Finish()
//        {
//            if (_refout ^ _refin)
//            {
//                _crc = Reverse(_crc);
//            }
//            if (_moves > 0 && !_refout)
//            {
//                _crc = ShiftRight(_crc, _moves);
//            }
//            _crc = Xor(_crc, _xoroutParsed);
//        }
//    }
//}