using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Cyclic redundancy check used by custom parameters.
    /// </summary>
    public sealed class CrcCustom : Crc
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are between 0 - 8.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int width, bool refin, bool refout, byte poly, byte init, byte xorout, bool withTable = true)
            : base($"CRC-{width}/CUSTOM", new CrcEngine8(width, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are between 0 - 16.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool withTable = true)
            : base($"CRC-{width}/CUSTOM", new CrcEngine16(width, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are between 0 - 32.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int width, bool refin, bool refout, uint poly, uint init, uint xorout, bool withTable = true)
            : base($"CRC-{width}/CUSTOM", new CrcEngine32(width, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are between 0 - 64.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool withTable = true)
            : base($"CRC-{width}/CUSTOM", new CrcEngine64(width, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="width">Width bits. The allowed values are more than 0.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="polyHex">Polynomials value hex string.</param>
        /// <param name="initHex">Initialization value hex string.</param>
        /// <param name="xoroutHex">Output xor value hex string.</param>
        /// <param name="core">Use the specified CRC calculation core.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int width, bool refin, bool refout, string polyHex, string initHex, string xoroutHex, CrcCore core = CrcCore.Auto)
            : base($"CRC-{width}/CUSTOM", GetEngine(width, refin, refout, polyHex, initHex, xoroutHex, core))
        {
        }

        #endregion Construction

        private static ushort BEToUInt16(byte[] input)
        {
            ushort result = 0;
            int length = Math.Min(input.Length, 2);
            for (int i = 0; i < length; i++)
            {
                result |= (ushort)((input[input.Length - 1 - i] & 0xFF) << (8 * i));
            }
            return result;
        }

        private static uint BEToUInt32(byte[] input)
        {
            uint result = 0;
            int length = Math.Min(input.Length, 4);
            for (int i = 0; i < length; i++)
            {
                result |= (input[input.Length - 1 - i] & 0xFFU) << (8 * i);
            }
            return result;
        }

        private static ulong BEToUInt64(byte[] input)
        {
            ulong result = 0;
            int length = Math.Min(input.Length, 8);
            for (int i = 0; i < length; i++)
            {
                result |= (input[input.Length - 1 - i] & 0xFFUL) << (8 * i);
            }
            return result;
        }

        /// <summary>
        /// Convert checksum/poly/init/xorout hex string to the bytes, Truncate bits form header if the input length is greater than width bits.
        /// </summary>
        /// <param name="inputHex">Input hex string.</param>
        /// <param name="width">Width bits.</param>
        /// <returns></returns>
        private static byte[] GetBytes(string inputHex, int width)
        {
            int rem = width % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            byte[] result = new byte[(int)Math.Ceiling(width / 8d)];
            StringBuilder bin = new StringBuilder();
            if (inputHex.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase) || inputHex.StartsWith("&h", StringComparison.InvariantCultureIgnoreCase))
            {
                inputHex = inputHex.Substring(2, inputHex.Length - 2).Replace("_", null).Replace("-", null);
            }
            else
            {
                inputHex = inputHex.Replace("_", null).Replace("-", null);
            }
            for (int i = 0; i < inputHex.Length; i++)
            {
                bin.Append(Convert.ToString(Convert.ToByte(inputHex[i].ToString(), 16), 2).PadLeft(4, '0'));
            }
            if (bin.Length > width)
            {
                bin.Remove(0, bin.Length - width);
            }
            else if (bin.Length < width)
            {
                bin.Insert(0, "0", width - bin.Length);
            }
            if (truncates > 0)
            {
                bin.Insert(0, "0", truncates);
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(bin.ToString(i * 8, 8), 2);
            }
            return result;
        }

        private static CrcEngine GetEngine(int width, bool refin, bool refout, string polyHex, string initHex, string xoroutHex, CrcCore core)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum width. The allowed values are more than 0.", nameof(width));
            }
            if (core == CrcCore.Auto)
            {
                if (width <= 8)
                {
                    core = CrcCore.U8Table;
                }
                else if (width <= 16)
                {
                    core = CrcCore.U16Table;
                }
                else if (width <= 32)
                {
                    core = CrcCore.U32Table;
                }
                else if (width <= 64)
                {
                    core = CrcCore.U64Table;
                }
                else
                {
                    core = CrcCore.Sharding32Table;
                }
            }
            switch (core)
            {
                case CrcCore.U8:
                    {
                        byte[] polyBytes = GetBytes(polyHex, width);
                        byte[] initBytes = GetBytes(initHex, width);
                        byte[] xoroutBytes = GetBytes(xoroutHex, width);
                        byte poly = polyBytes[polyBytes.Length - 1];
                        byte init = initBytes[initBytes.Length - 1];
                        byte xorout = xoroutBytes[xoroutBytes.Length - 1];
                        return new CrcEngine8(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.U8Table:
                    {
                        byte[] polyBytes = GetBytes(polyHex, width);
                        byte[] initBytes = GetBytes(initHex, width);
                        byte[] xoroutBytes = GetBytes(xoroutHex, width);
                        byte poly = polyBytes[polyBytes.Length - 1];
                        byte init = initBytes[initBytes.Length - 1];
                        byte xorout = xoroutBytes[xoroutBytes.Length - 1];
                        return new CrcEngine8(width, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.U16:
                    {
                        ushort poly = BEToUInt16(GetBytes(polyHex, width));
                        ushort init = BEToUInt16(GetBytes(initHex, width));
                        ushort xorout = BEToUInt16(GetBytes(xoroutHex, width));
                        return new CrcEngine16(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.U16Table:
                    {
                        ushort poly = BEToUInt16(GetBytes(polyHex, width));
                        ushort init = BEToUInt16(GetBytes(initHex, width));
                        ushort xorout = BEToUInt16(GetBytes(xoroutHex, width));
                        return new CrcEngine16(width, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.U32:
                    {
                        uint poly = BEToUInt32(GetBytes(polyHex, width));
                        uint init = BEToUInt32(GetBytes(initHex, width));
                        uint xorout = BEToUInt32(GetBytes(xoroutHex, width));
                        return new CrcEngine32(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.U32Table:
                    {
                        uint poly = BEToUInt32(GetBytes(polyHex, width));
                        uint init = BEToUInt32(GetBytes(initHex, width));
                        uint xorout = BEToUInt32(GetBytes(xoroutHex, width));
                        return new CrcEngine32(width, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.U64:
                    {
                        ulong poly = BEToUInt64(GetBytes(polyHex, width));
                        ulong init = BEToUInt64(GetBytes(initHex, width));
                        ulong xorout = BEToUInt64(GetBytes(xoroutHex, width));
                        return new CrcEngine64(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.U64Table:
                    {
                        ulong poly = BEToUInt64(GetBytes(polyHex, width));
                        ulong init = BEToUInt64(GetBytes(initHex, width));
                        ulong xorout = BEToUInt64(GetBytes(xoroutHex, width));
                        return new CrcEngine64(width, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.Sharding8:
                    return new CrcEngineX(width, refin, refout, polyHex, initHex, xoroutHex, false);

                case CrcCore.Sharding8Table:
                    return new CrcEngineX(width, refin, refout, polyHex, initHex, xoroutHex, true);

                case CrcCore.Sharding32:
                    return new CrcEngineX2(width, refin, refout, polyHex, initHex, xoroutHex, false);

                case CrcCore.Sharding32Table:
                default:
                    return new CrcEngineX2(width, refin, refout, polyHex, initHex, xoroutHex, true);
            }
        }
    }
}