﻿using System;

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
                    core = CrcCore.UInt8Table;
                }
                else if (width <= 16)
                {
                    core = CrcCore.UInt16Table;
                }
                else if (width <= 32)
                {
                    core = CrcCore.UInt32Table;
                }
                else if (width <= 64)
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
                        byte poly = CrcConverter.ToUInt8(polyHex, width);
                        byte init = CrcConverter.ToUInt8(initHex, width);
                        byte xorout = CrcConverter.ToUInt8(xoroutHex, width);
                        return new CrcEngine8(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.UInt8Table:
                    {
                        byte poly = CrcConverter.ToUInt8(polyHex, width);
                        byte init = CrcConverter.ToUInt8(initHex, width);
                        byte xorout = CrcConverter.ToUInt8(xoroutHex, width);
                        return new CrcEngine8(width, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.UInt16:
                    {
                        ushort poly = CrcConverter.ToUInt16(polyHex, width);
                        ushort init = CrcConverter.ToUInt16(initHex, width);
                        ushort xorout = CrcConverter.ToUInt16(xoroutHex, width);
                        return new CrcEngine16(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.UInt16Table:
                    {
                        ushort poly = CrcConverter.ToUInt16(polyHex, width);
                        ushort init = CrcConverter.ToUInt16(initHex, width);
                        ushort xorout = CrcConverter.ToUInt16(xoroutHex, width);
                        return new CrcEngine16(width, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.UInt32:
                    {
                        uint poly = CrcConverter.ToUInt32(polyHex, width);
                        uint init = CrcConverter.ToUInt32(initHex, width);
                        uint xorout = CrcConverter.ToUInt32(xoroutHex, width);
                        return new CrcEngine32(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.UInt32Table:
                    {
                        uint poly = CrcConverter.ToUInt32(polyHex, width);
                        uint init = CrcConverter.ToUInt32(initHex, width);
                        uint xorout = CrcConverter.ToUInt32(xoroutHex, width);
                        return new CrcEngine32(width, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.UInt64:
                    {
                        ulong poly = CrcConverter.ToUInt64(polyHex, width);
                        ulong init = CrcConverter.ToUInt64(initHex, width);
                        ulong xorout = CrcConverter.ToUInt64(xoroutHex, width);
                        return new CrcEngine64(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.UInt64Table:
                    {
                        ulong poly = CrcConverter.ToUInt64(polyHex, width);
                        ulong init = CrcConverter.ToUInt64(initHex, width);
                        ulong xorout = CrcConverter.ToUInt64(xoroutHex, width);
                        return new CrcEngine64(width, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.Sharding8:
                    {
                        byte[] poly = CrcConverter.GenerateSharding8Value(polyHex, width);
                        byte[] init = CrcConverter.GenerateSharding8Value(initHex, width);
                        byte[] xorout = CrcConverter.GenerateSharding8Value(xoroutHex, width);
                        return new CrcEngineSharding8(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.Sharding8Table:
                    {
                        byte[] poly = CrcConverter.GenerateSharding8Value(polyHex, width);
                        byte[] init = CrcConverter.GenerateSharding8Value(initHex, width);
                        byte[] xorout = CrcConverter.GenerateSharding8Value(xoroutHex, width);
                        return new CrcEngineSharding8(width, refin, refout, poly, init, xorout, true);
                    }

                case CrcCore.Sharding32:
                    {
                        uint[] poly = CrcConverter.GenerateSharding32Value(polyHex, width);
                        uint[] init = CrcConverter.GenerateSharding32Value(initHex, width);
                        uint[] xorout = CrcConverter.GenerateSharding32Value(xoroutHex, width);
                        return new CrcEngineSharding32(width, refin, refout, poly, init, xorout, false);
                    }

                case CrcCore.Sharding32Table:
                default:
                    {
                        uint[] poly = CrcConverter.GenerateSharding32Value(polyHex, width);
                        uint[] init = CrcConverter.GenerateSharding32Value(initHex, width);
                        uint[] xorout = CrcConverter.GenerateSharding32Value(xoroutHex, width);
                        return new CrcEngineSharding32(width, refin, refout, poly, init, xorout, true);
                    }
            }
        }
    }
}