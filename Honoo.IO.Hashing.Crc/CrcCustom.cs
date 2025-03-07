﻿using System;

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
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, byte poly, byte init, byte xorout, bool refin, bool refout, CrcTable table)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, table))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 16.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, ushort poly, ushort init, ushort xorout, bool refin, bool refout, CrcTable table)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, table))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 32.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, uint poly, uint init, uint xorout, bool refin, bool refout, CrcTable table)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, table))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 64.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, ulong poly, ulong init, ulong xorout, bool refin, bool refout, CrcTable table)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, table))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are more than 0.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="table">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, CrcParameter poly, CrcParameter init, CrcParameter xorout, bool refin, bool refout, CrcTable table)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, table))
        {
        }

        #endregion Construction

        private static CrcEngine GetEngine(int width, byte poly, byte init, byte xorout, bool refin, bool refout, CrcTable table)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table));
            }
            switch (table.TableInfo)
            {
                case CrcTableInfo.None:
                default:
                    return new CrcEngine8(width, poly, init, xorout, refin, refout);

                case CrcTableInfo.Standard:

                    return new CrcEngine8Standard(width, poly, init, xorout, refin, refout, (byte[])table.Table);

                case CrcTableInfo.M16x:
                    return new CrcEngine8M16x(width, poly, init, xorout, refin, refout, (byte[])table.Table);
            }
        }

        private static CrcEngine GetEngine(int width, ushort poly, ushort init, ushort xorout, bool refin, bool refout, CrcTable table)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table));
            }
            switch (table.TableInfo)
            {
                case CrcTableInfo.None:
                default:
                    return new CrcEngine16(width, poly, init, xorout, refin, refout);

                case CrcTableInfo.Standard:

                    return new CrcEngine16Standard(width, poly, init, xorout, refin, refout, (ushort[])table.Table);

                case CrcTableInfo.M16x:
                    return new CrcEngine16M16x(width, poly, init, xorout, refin, refout, (ushort[])table.Table);
            }
        }

        private static CrcEngine GetEngine(int width, uint poly, uint init, uint xorout, bool refin, bool refout, CrcTable table)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table));
            }
            switch (table.TableInfo)
            {
                case CrcTableInfo.None:
                default:
                    return new CrcEngine32(width, poly, init, xorout, refin, refout);

                case CrcTableInfo.Standard:

                    return new CrcEngine32Standard(width, poly, init, xorout, refin, refout, (uint[])table.Table);

                case CrcTableInfo.M16x:
                    return new CrcEngine32M16x(width, poly, init, xorout, refin, refout, (uint[])table.Table);
            }
        }

        private static CrcEngine GetEngine(int width, ulong poly, ulong init, ulong xorout, bool refin, bool refout, CrcTable table)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table));
            }
            switch (table.TableInfo)
            {
                case CrcTableInfo.None:
                default:
                    return new CrcEngine64(width, poly, init, xorout, refin, refout);

                case CrcTableInfo.Standard:
                    return new CrcEngine64Standard(width, poly, init, xorout, refin, refout, (ulong[])table.Table);

                case CrcTableInfo.M16x:
                    return new CrcEngine64M16x(width, poly, init, xorout, refin, refout, (ulong[])table.Table);
            }
        }

        private static CrcEngine GetEngine(int width, CrcParameter polyParameter, CrcParameter initParameter, CrcParameter xoroutParameter, bool refin, bool refout, CrcTable table)
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
                throw new ArgumentNullException(nameof(table));
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
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngine8(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngine8Standard(width, poly, init, xorout, refin, refout, (byte[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngine8M16x(width, poly, init, xorout, refin, refout, (byte[])table.Table);
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
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngine16(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngine16Standard(width, poly, init, xorout, refin, refout, (ushort[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngine16M16x(width, poly, init, xorout, refin, refout, (ushort[])table.Table);
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
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngine32(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngine32Standard(width, poly, init, xorout, refin, refout, (uint[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngine32M16x(width, poly, init, xorout, refin, refout, (uint[])table.Table);
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
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngine64(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngine64Standard(width, poly, init, xorout, refin, refout, (ulong[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngine64M16x(width, poly, init, xorout, refin, refout, (ulong[])table.Table);
                        }
                    }

                case CrcCore.Sharding8:
                    {
                        byte[] poly = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        byte[] init = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        byte[] xorout = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngineSharding8(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngineSharding8Standard(width, poly, init, xorout, refin, refout, (byte[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngineSharding8Standard(width, poly, init, xorout, refin, refout, (byte[])table.Table);
                        }
                    }

                case CrcCore.Sharding16:
                    {
                        ushort[] poly = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        ushort[] init = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        ushort[] xorout = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngineSharding16(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngineSharding16Standard(width, poly, init, xorout, refin, refout, (ushort[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngineSharding16Standard(width, poly, init, xorout, refin, refout, (ushort[])table.Table);
                        }
                    }
                case CrcCore.Sharding32:
                default:
                    {
                        uint[] poly = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        uint[] init = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        uint[] xorout = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngineSharding32(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngineSharding32Standard(width, poly, init, xorout, refin, refout, (uint[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngineSharding32Standard(width, poly, init, xorout, refin, refout, (uint[])table.Table);
                        }
                    }
                case CrcCore.Sharding64:
                    {
                        ulong[] poly = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                        ulong[] init = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, initParameter.ToString(CrcStringFormat.Hex), width);
                        ulong[] xorout = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, xoroutParameter.ToString(CrcStringFormat.Hex), width);
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngineSharding64(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngineSharding64Standard(width, poly, init, xorout, refin, refout, (ulong[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngineSharding64Standard(width, poly, init, xorout, refin, refout, (ulong[])table.Table);
                        }
                    }
            }
        }
    }
}