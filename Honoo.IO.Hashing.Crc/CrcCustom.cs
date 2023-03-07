using System;

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
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 8.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, byte poly, byte init, byte xorout, bool withTable = true)
            : base(GetEngine(checksumSize, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 16.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool withTable = true)
            : base(GetEngine(checksumSize, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 32.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, uint poly, uint init, uint xorout, bool withTable = true)
            : base(GetEngine(checksumSize, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 64.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool withTable = true)
            : base(GetEngine(checksumSize, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are more than 0.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="polyHex">Polynomials value hex string.</param>
        /// <param name="initHex">Initialization value hex string.</param>
        /// <param name="xoroutHex">Output xor value hex string.</param>
        /// <param name="core">Use the specified CRC calculation core.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, string polyHex, string initHex, string xoroutHex, CrcCore core = CrcCore.Auto)
            : base(GetEngine(checksumSize, refin, refout, polyHex, initHex, xoroutHex, core))
        {
        }

        #endregion Construction

        private static ushort GetBEUInt16(byte[] input)
        {
            ushort result = 0;
            int length = Math.Min(input.Length, 2);
            for (int i = 0; i < length; i++)
            {
                result |= (ushort)((input[length - 1 - i] & 0xFF) << (8 * i));
            }
            return result;
        }

        private static uint GetBEUInt32(byte[] input)
        {
            uint result = 0;
            int length = Math.Min(input.Length, 4);
            for (int i = 0; i < length; i++)
            {
                result |= (input[length - 1 - i] & 0xFFU) << (8 * i);
            }
            return result;
        }

        private static ulong GetBEUInt64(byte[] input)
        {
            ulong result = 0;
            int length = Math.Min(input.Length, 8);
            for (int i = 0; i < length; i++)
            {
                result |= (input[length - 1 - i] & 0xFFUL) << (8 * i);
            }
            return result;
        }

        private static CrcEngine GetEngine(int checksumSize, bool refin, bool refout, byte poly, byte init, byte xorout, bool withTable)
        {
            if (withTable)
            {
                poly = CrcEngine8.Parse(poly, 8 - checksumSize, refin);
                init = CrcEngine8.Parse(init, 8 - checksumSize, refin);
                byte[] table = refin ? CrcEngine8.GenerateReversedTable(poly) : CrcEngine8.GenerateTable(poly);
                return new CrcEngine8($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, table, init, xorout);
            }
            else
            {
                return new CrcEngine8($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, poly, init, xorout);
            }
        }

        private static CrcEngine GetEngine(int checksumSize, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool withTable)
        {
            if (withTable)
            {
                poly = CrcEngine16.Parse(poly, 16 - checksumSize, refin);
                init = CrcEngine16.Parse(init, 16 - checksumSize, refin);
                ushort[] table = refin ? CrcEngine16.GenerateReversedTable(poly) : CrcEngine16.GenerateTable(poly);
                return new CrcEngine16($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, table, init, xorout);
            }
            else
            {
                return new CrcEngine16($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, poly, init, xorout);
            }
        }

        private static CrcEngine GetEngine(int checksumSize, bool refin, bool refout, uint poly, uint init, uint xorout, bool withTable)
        {
            if (withTable)
            {
                poly = CrcEngine32.Parse(poly, 32 - checksumSize, refin);
                init = CrcEngine32.Parse(init, 32 - checksumSize, refin);
                uint[] table = refin ? CrcEngine32.GenerateReversedTable(poly) : CrcEngine32.GenerateTable(poly);
                return new CrcEngine32($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, table, init, xorout);
            }
            else
            {
                return new CrcEngine32($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, poly, init, xorout);
            }
        }

        private static CrcEngine GetEngine(int checksumSize, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool withTable)
        {
            if (withTable)
            {
                poly = CrcEngine64.Parse(poly, 64 - checksumSize, refin);
                init = CrcEngine64.Parse(init, 64 - checksumSize, refin);
                ulong[] table = refin ? CrcEngine64.GenerateReversedTable(poly) : CrcEngine64.GenerateTable(poly);
                return new CrcEngine64($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, table, init, xorout);
            }
            else
            {
                return new CrcEngine64($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, poly, init, xorout);
            }
        }

        private static CrcEngine GetEngine(int checksumSize, bool refin, bool refout, string polyHex, string initHex, string xoroutHex, CrcCore core)
        {
            int checksumLength = (int)Math.Ceiling(checksumSize / 8d);
            if (core == CrcCore.Auto)
            {
                if (checksumSize <= 8)
                {
                    core = CrcCore.UInt8Table;
                }
                else if (checksumSize <= 16)
                {
                    core = CrcCore.UInt16Table;
                }
                else if (checksumSize <= 32)
                {
                    core = CrcCore.UInt32Table;
                }
                else if (checksumSize <= 64)
                {
                    core = CrcCore.UInt64Table;
                }
                else
                {
                    core = CrcCore.Sharding32Table;
                }
            }
            switch (core)
            {
                case CrcCore.UInt8:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        byte poly = polyBytes[polyBytes.Length - 1];
                        byte init = initBytes[initBytes.Length - 1];
                        byte xorout = xoroutBytes[xoroutBytes.Length - 1];
                        return GetEngine(checksumSize, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.UInt8Table:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        byte poly = polyBytes[polyBytes.Length - 1];
                        byte init = initBytes[initBytes.Length - 1];
                        byte xorout = xoroutBytes[xoroutBytes.Length - 1];
                        return GetEngine(checksumSize, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.UInt16:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        ushort poly = GetBEUInt16(polyBytes);
                        ushort init = GetBEUInt16(initBytes);
                        ushort xorout = GetBEUInt16(xoroutBytes);
                        return GetEngine(checksumSize, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.UInt16Table:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        ushort poly = GetBEUInt16(polyBytes);
                        ushort init = GetBEUInt16(initBytes);
                        ushort xorout = GetBEUInt16(xoroutBytes);
                        return GetEngine(checksumSize, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.UInt32:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        uint poly = GetBEUInt32(polyBytes);
                        uint init = GetBEUInt32(initBytes);
                        uint xorout = GetBEUInt32(xoroutBytes);
                        return GetEngine(checksumSize, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.UInt32Table:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        uint poly = GetBEUInt32(polyBytes);
                        uint init = GetBEUInt32(initBytes);
                        uint xorout = GetBEUInt32(xoroutBytes);
                        return GetEngine(checksumSize, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.UInt64:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        ulong poly = GetBEUInt64(polyBytes);
                        ulong init = GetBEUInt64(initBytes);
                        ulong xorout = GetBEUInt64(xoroutBytes);
                        return GetEngine(checksumSize, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.UInt64Table:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        ulong poly = GetBEUInt64(polyBytes);
                        ulong init = GetBEUInt64(initBytes);
                        ulong xorout = GetBEUInt64(xoroutBytes);
                        return GetEngine(checksumSize, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.Sharding8:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        return new CrcEngineX($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, polyBytes, initBytes, xoroutBytes);
                    }

                case CrcCore.Sharding8Table:
                    {
                        byte[] polyBytes = CrcEngineX.ParseS1(polyHex, checksumLength);
                        byte[] initBytes = CrcEngineX.ParseS1(initHex, checksumLength);
                        byte[] xoroutBytes = CrcEngineX.ParseS1(xoroutHex, checksumLength);
                        int rem = checksumSize % 8;
                        int move = rem > 0 ? 8 - rem : 0;
                        byte[] poly = CrcEngineX.ParseS2(polyBytes, move, refin);
                        byte[] init = CrcEngineX.ParseS2(initBytes, move, refin);
                        byte[][] table = refin ? CrcEngineX.GenerateReversedTable(poly) : CrcEngineX.GenerateTable(poly);
                        return new CrcEngineX($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, table, init, xoroutBytes);
                    }

                case CrcCore.Sharding32:
                    {
                        uint[] poly = CrcEngineX2.ParseS1(polyHex, checksumLength);
                        uint[] init = CrcEngineX2.ParseS1(initHex, checksumLength);
                        uint[] xorout = CrcEngineX2.ParseS1(xoroutHex, checksumLength);
                        return new CrcEngineX2($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, poly, init, xorout);
                    }

                case CrcCore.Sharding32Table:
                default:
                    {
                        uint[] poly = CrcEngineX2.ParseS1(polyHex, checksumLength);
                        uint[] init = CrcEngineX2.ParseS1(initHex, checksumLength);
                        uint[] xorout = CrcEngineX2.ParseS1(xoroutHex, checksumLength);
                        int rem = checksumSize % 32;
                        int move = rem > 0 ? 32 - rem : 0;
                        poly = CrcEngineX2.ParseS2(poly, move, refin);
                        init = CrcEngineX2.ParseS2(init, move, refin);
                        uint[][] table = refin ? CrcEngineX2.GenerateReversedTable(poly) : CrcEngineX2.GenerateTable(poly);
                        return new CrcEngineX2($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, table, init, xorout);
                    }
            }
        }
    }
}