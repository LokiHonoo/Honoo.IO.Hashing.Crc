using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Create CRC algorithm used by custom parameters.
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
        public CrcCustom(string name, int width, CrcValue poly, CrcValue init, CrcValue xorout, bool refin, bool refout, CrcTable table)
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

        private static CrcEngine GetEngine(int width, CrcValue polyParameter, CrcValue initParameter, CrcValue xoroutParameter, bool refin, bool refout, CrcTable table)
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
                        byte poly = polyParameter.ToUInt8();
                        byte init = initParameter.ToUInt8();
                        byte xorout = xoroutParameter.ToUInt8();
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
                        ushort poly = polyParameter.ToUInt16();
                        ushort init = initParameter.ToUInt16();
                        ushort xorout = xoroutParameter.ToUInt16();
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
                        uint poly = polyParameter.ToUInt32();
                        uint init = initParameter.ToUInt32();
                        uint xorout = xoroutParameter.ToUInt32();
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
                        ulong poly = polyParameter.ToUInt64();
                        ulong init = initParameter.ToUInt64();
                        ulong xorout = xoroutParameter.ToUInt64();
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngine64(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngine64Standard(width, poly, init, xorout, refin, refout, (ulong[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngine64M16x(width, poly, init, xorout, refin, refout, (ulong[])table.Table);
                        }
                    }

                case CrcCore.Sharding8:
                    {
                        byte[] poly = CrcConverter.GetUInt8Array(CrcStringFormat.Hex, polyParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        byte[] init = CrcConverter.GetUInt8Array(CrcStringFormat.Hex, initParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        byte[] xorout = CrcConverter.GetUInt8Array(CrcStringFormat.Hex, xoroutParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngineSharding8(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngineSharding8Standard(width, poly, init, xorout, refin, refout, (byte[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngineSharding8Standard(width, poly, init, xorout, refin, refout, (byte[])table.Table);
                        }
                    }

                case CrcCore.Sharding16:
                    {
                        ushort[] poly = CrcConverter.GetUInt16Array(CrcStringFormat.Hex, polyParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        ushort[] init = CrcConverter.GetUInt16Array(CrcStringFormat.Hex, initParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        ushort[] xorout = CrcConverter.GetUInt16Array(CrcStringFormat.Hex, xoroutParameter.ToHex(CrcCaseSensitivity.Lower), width);
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
                        uint[] poly = CrcConverter.GetUInt32Array(CrcStringFormat.Hex, polyParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        uint[] init = CrcConverter.GetUInt32Array(CrcStringFormat.Hex, initParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        uint[] xorout = CrcConverter.GetUInt32Array(CrcStringFormat.Hex, xoroutParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        switch (table.TableInfo)
                        {
                            case CrcTableInfo.None: default: return new CrcEngineSharding32(width, poly, init, xorout, refin, refout);
                            case CrcTableInfo.Standard: return new CrcEngineSharding32Standard(width, poly, init, xorout, refin, refout, (uint[])table.Table);
                            case CrcTableInfo.M16x: return new CrcEngineSharding32Standard(width, poly, init, xorout, refin, refout, (uint[])table.Table);
                        }
                    }
                case CrcCore.Sharding64:
                    {
                        ulong[] poly = CrcConverter.GetUInt64Array(CrcStringFormat.Hex, polyParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        ulong[] init = CrcConverter.GetUInt64Array(CrcStringFormat.Hex, initParameter.ToHex(CrcCaseSensitivity.Lower), width);
                        ulong[] xorout = CrcConverter.GetUInt64Array(CrcStringFormat.Hex, xoroutParameter.ToHex(CrcCaseSensitivity.Lower), width);
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