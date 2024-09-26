using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Create crc algorithm used by custom parameters.
    /// </summary>
    public sealed class CrcCustom : Crc
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 8.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, bool refin, bool refout, byte poly, byte init, byte xorout, CrcTableInfo withTable = CrcTableInfo.Standard)
            : base(name, new CrcEngine8(width, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 16.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, CrcTableInfo withTable = CrcTableInfo.Standard)
            : base(name, new CrcEngine16(width, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 32.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, bool refin, bool refout, uint poly, uint init, uint xorout, CrcTableInfo withTable = CrcTableInfo.Standard)
            : base(name, new CrcEngine32(width, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 64.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, CrcTableInfo withTable = CrcTableInfo.Standard)
            : base(name, new CrcEngine64(width, refin, refout, poly, init, xorout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are more than 0.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <param name="core">Use the specified CRC calculation core.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, bool refin, bool refout, CrcParameter poly, CrcParameter init, CrcParameter xorout, CrcTableInfo withTable = CrcTableInfo.Standard, CrcCore core = CrcCore.Auto)
            : base(name, GetEngine(width, refin, refout, poly, init, xorout, withTable, core))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are more than 0.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, bool refin, bool refout, CrcParameter poly, CrcParameter init, CrcParameter xorout, CrcTableData table)
            : base(name, GetEngine(width, refin, refout, poly, init, xorout, table))
        {
        }

        #endregion Construction

        private static CrcEngine GetEngine(int width, bool refin, bool refout, CrcParameter polyParameter, CrcParameter initParameter, CrcParameter xoroutParameter, CrcTableInfo withTable, CrcCore core)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0.", nameof(width));
            }
            if (polyParameter is null)
            {
                throw new ArgumentNullException(nameof(polyParameter));
            }
            if (initParameter is null)
            {
                throw new ArgumentNullException(nameof(initParameter));
            }
            if (xoroutParameter is null)
            {
                throw new ArgumentNullException(nameof(xoroutParameter));
            }
            if (core == CrcCore.Auto)
            {
                if (width <= 32) core = CrcCore.UInt32;
                else if (width <= 64) core = CrcCore.UInt64;
                else if (width <= 128) core = CrcCore.UInt128L2;
                else core = CrcCore.Sharding64;
            }
            switch (core)
            {
                case CrcCore.UInt8:
                    {
                        if (width > 8)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8.", nameof(width));
                        }
                        byte poly = CrcConverter.ToUInt8(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        byte init = CrcConverter.ToUInt8(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        byte xorout = CrcConverter.ToUInt8(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (withTable)
                        {
                            case CrcTableInfo.M16x: return new CrcEngine8M16x(width, refin, refout, poly, init, xorout, null);
                            default: return new CrcEngine8(width, refin, refout, poly, init, xorout, withTable);
                        }
                    }

                case CrcCore.UInt16:
                    {
                        if (width > 16)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
                        }
                        ushort poly = CrcConverter.ToUInt16(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        ushort init = CrcConverter.ToUInt16(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        ushort xorout = CrcConverter.ToUInt16(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (withTable)
                        {
                            case CrcTableInfo.M16x: return new CrcEngine16M16x(width, refin, refout, poly, init, xorout, null);
                            default: return new CrcEngine16(width, refin, refout, poly, init, xorout, withTable);
                        }
                    }

                case CrcCore.UInt32:
                    {
                        if (width > 32)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 32.", nameof(width));
                        }
                        uint poly = CrcConverter.ToUInt32(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        uint init = CrcConverter.ToUInt32(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        uint xorout = CrcConverter.ToUInt32(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (withTable)
                        {
                            case CrcTableInfo.M16x: return new CrcEngine32M16x(width, refin, refout, poly, init, xorout, null);
                            default: return new CrcEngine32(width, refin, refout, poly, init, xorout, withTable);
                        }
                    }

                case CrcCore.UInt64:
                    {
                        if (width > 64)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64.", nameof(width));
                        }
                        ulong poly = CrcConverter.ToUInt64(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        ulong init = CrcConverter.ToUInt64(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        ulong xorout = CrcConverter.ToUInt64(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (withTable)
                        {
                            case CrcTableInfo.M16x: return new CrcEngine64M16x(width, refin, refout, poly, init, xorout, null);
                            default: return new CrcEngine64(width, refin, refout, poly, init, xorout, withTable);
                        }
                    }

                case CrcCore.UInt128L2:
                    {
                        if (width > 128)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 128.", nameof(width));
                        }
                        ulong[] poly = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, 2);
                        ulong[] init = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, 2);
                        ulong[] xorout = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, 2);
                        return new CrcEngine128L2(width, refin, refout, poly, init, xorout, withTable);
                    }

                case CrcCore.Sharding8:
                    {
                        byte[] poly = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, null);
                        byte[] init = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, null);
                        byte[] xorout = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, null);
                        return new CrcEngineSharding8(width, refin, refout, poly, init, xorout, withTable);
                    }

                case CrcCore.Sharding16:
                    {
                        ushort[] poly = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, null);
                        ushort[] init = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, null);
                        ushort[] xorout = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, null);
                        return new CrcEngineSharding16(width, refin, refout, poly, init, xorout, withTable);
                    }
                case CrcCore.Sharding32:

                    {
                        uint[] poly = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, null);
                        uint[] init = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, null);
                        uint[] xorout = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, null);
                        return new CrcEngineSharding32(width, refin, refout, poly, init, xorout, withTable);
                    }
                case CrcCore.Sharding64:
                default:
                    {
                        ulong[] poly = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, null);
                        ulong[] init = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, null);
                        ulong[] xorout = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, null);
                        return new CrcEngineSharding64(width, refin, refout, poly, init, xorout, withTable);
                    }
            }
        }

        private static CrcEngine GetEngine(int width, bool refin, bool refout, CrcParameter polyParameter, CrcParameter initParameter, CrcParameter xoroutParameter, CrcTableData table)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0.", nameof(width));
            }
            if (polyParameter is null)
            {
                throw new ArgumentNullException(nameof(polyParameter));
            }
            if (initParameter is null)
            {
                throw new ArgumentNullException(nameof(initParameter));
            }
            if (xoroutParameter is null)
            {
                throw new ArgumentNullException(nameof(xoroutParameter));
            }
            if (table is null)
            {
                throw new ArgumentNullException(nameof(xoroutParameter));
            }
            switch (table.Core)
            {
                case CrcCore.UInt8:
                    {
                        if (width > 8)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8.", nameof(width));
                        }
                        byte poly = CrcConverter.ToUInt8(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        byte init = CrcConverter.ToUInt8(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        byte xorout = CrcConverter.ToUInt8(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (table.Info)
                        {
                            case CrcTableInfo.M16x: return new CrcEngine8M16x(width, refin, refout, poly, init, xorout, (byte[])table.Table);
                            default: return new CrcEngine8(width, refin, refout, poly, init, xorout, (byte[])table.Table);
                        }
                    }

                case CrcCore.UInt16:
                    {
                        if (width > 16)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
                        }
                        ushort poly = CrcConverter.ToUInt16(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        ushort init = CrcConverter.ToUInt16(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        ushort xorout = CrcConverter.ToUInt16(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (table.Info)
                        {
                            case CrcTableInfo.M16x: return new CrcEngine16M16x(width, refin, refout, poly, init, xorout, (ushort[])table.Table);
                            default: return new CrcEngine16(width, refin, refout, poly, init, xorout, (ushort[])table.Table);
                        }
                    }

                case CrcCore.UInt32:
                    {
                        if (width > 32)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 32.", nameof(width));
                        }
                        uint poly = CrcConverter.ToUInt32(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        uint init = CrcConverter.ToUInt32(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        uint xorout = CrcConverter.ToUInt32(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (table.Info)
                        {
                            case CrcTableInfo.M16x: return new CrcEngine32M16x(width, refin, refout, poly, init, xorout, (uint[])table.Table);
                            default: return new CrcEngine32(width, refin, refout, poly, init, xorout, (uint[])table.Table);
                        }
                    }

                case CrcCore.UInt64:
                    {
                        if (width > 64)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64.", nameof(width));
                        }
                        ulong poly = CrcConverter.ToUInt64(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        ulong init = CrcConverter.ToUInt64(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        ulong xorout = CrcConverter.ToUInt64(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (table.Info)
                        {
                            case CrcTableInfo.M16x: return new CrcEngine64M16x(width, refin, refout, poly, init, xorout, (ulong[])table.Table);
                            default: return new CrcEngine64(width, refin, refout, poly, init, xorout, (ulong[])table.Table);
                        }
                    }

                case CrcCore.UInt128L2:
                    {
                        if (width > 128)
                        {
                            throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 128.", nameof(width));
                        }
                        ulong[] poly = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, 2);
                        ulong[] init = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, 2);
                        ulong[] xorout = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, 2);
                        return new CrcEngine128L2(width, refin, refout, poly, init, xorout, (ulong[][])table.Table);
                    }

                case CrcCore.Sharding8:
                    {
                        byte[] poly = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, null);
                        byte[] init = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, null);
                        byte[] xorout = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, null);
                        return new CrcEngineSharding8(width, refin, refout, poly, init, xorout, (byte[][])table.Table);
                    }

                case CrcCore.Sharding16:
                    {
                        ushort[] poly = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, null);
                        ushort[] init = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, null);
                        ushort[] xorout = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, null);
                        return new CrcEngineSharding16(width, refin, refout, poly, init, xorout, (ushort[][])table.Table);
                    }
                case CrcCore.Sharding32:

                    {
                        uint[] poly = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, null);
                        uint[] init = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, null);
                        uint[] xorout = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, null);
                        return new CrcEngineSharding32(width, refin, refout, poly, init, xorout, (uint[][])table.Table);
                    }
                case CrcCore.Sharding64:
                default:
                    {
                        ulong[] poly = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width, null);
                        ulong[] init = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width, null);
                        ulong[] xorout = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width, null);
                        return new CrcEngineSharding64(width, refin, refout, poly, init, xorout, (ulong[][])table.Table);
                    }
            }
        }
    }
}