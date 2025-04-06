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
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 8.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, byte poly, byte init, byte xorout, bool refin, bool refout, CrcTableInfo withTable)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 8.</param>
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
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 16.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, ushort poly, ushort init, ushort xorout, bool refin, bool refout, CrcTableInfo withTable)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 16.</param>
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
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 32.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, uint poly, uint init, uint xorout, bool refin, bool refout, CrcTableInfo withTable)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 32.</param>
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
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 64.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, ulong poly, ulong init, ulong xorout, bool refin, bool refout, CrcTableInfo withTable)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, withTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 1 - 64.</param>
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
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, CrcValue poly, CrcValue init, CrcValue xorout, bool refin, bool refout, CrcTableInfo withTable)
            : base(name, GetEngine(width, poly, init, xorout, refin, refout, withTable))
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

        private static CrcEngine GetEngine(int width, byte poly, byte init, byte xorout, bool refin, bool refout, CrcTableInfo withTable)
        {
            return GetEngine(width, poly, init, xorout, refin, refout, new CrcTable(withTable, width, poly, refin));
        }

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

                    return new CrcEngine8Standard(width, poly, init, xorout, refin, refout, table.GetUint8Table());

                case CrcTableInfo.M16x:
                    return new CrcEngine8M16x(width, poly, init, xorout, refin, refout, table.GetUint8Table());
            }
        }

        private static CrcEngine GetEngine(int width, ushort poly, ushort init, ushort xorout, bool refin, bool refout, CrcTableInfo withTable)
        {
            return GetEngine(width, poly, init, xorout, refin, refout, new CrcTable(withTable, width, poly, refin));
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

                    return new CrcEngine16Standard(width, poly, init, xorout, refin, refout, table.GetUint16Table());

                case CrcTableInfo.M16x:
                    return new CrcEngine16M16x(width, poly, init, xorout, refin, refout, table.GetUint16Table());
            }
        }

        private static CrcEngine GetEngine(int width, uint poly, uint init, uint xorout, bool refin, bool refout, CrcTableInfo withTable)
        {
            return GetEngine(width, poly, init, xorout, refin, refout, new CrcTable(withTable, width, poly, refin));
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

                    return new CrcEngine32Standard(width, poly, init, xorout, refin, refout, table.GetUint32Table());

                case CrcTableInfo.M16x:
                    return new CrcEngine32M16x(width, poly, init, xorout, refin, refout, table.GetUint32Table());
            }
        }

        private static CrcEngine GetEngine(int width, ulong poly, ulong init, ulong xorout, bool refin, bool refout, CrcTableInfo withTable)
        {
            return GetEngine(width, poly, init, xorout, refin, refout, new CrcTable(withTable, width, poly, refin));
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
                    return new CrcEngine64Standard(width, poly, init, xorout, refin, refout, table.GetUint64Table());

                case CrcTableInfo.M16x:
                    return new CrcEngine64M16x(width, poly, init, xorout, refin, refout, table.GetUint64Table());
            }
        }

        private static CrcEngine GetEngine(int width, CrcValue poly, CrcValue init, CrcValue xorout, bool refin, bool refout, CrcTableInfo withTable)
        {
            return GetEngine(width, poly, init, xorout, refin, refout, new CrcTable(withTable, width, poly, refin));
        }

        private static CrcEngine GetEngine(int width, CrcValue poly, CrcValue init, CrcValue xorout, bool refin, bool refout, CrcTable table)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0.", nameof(width));
            }
            if (poly is null)
            {
                throw new ArgumentNullException(nameof(poly));
            }
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }
            if (xorout is null)
            {
                throw new ArgumentNullException(nameof(xorout));
            }
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table));
            }
            switch (table.Core)
            {
                case CrcCore.UInt8:
                    if (width > 8)
                    {
                        throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 8.", nameof(width));
                    }
                    byte poly8 = poly.ToUInt8();
                    byte init8 = init.ToUInt8();
                    byte xorout8 = xorout.ToUInt8();
                    switch (table.TableInfo)
                    {
                        case CrcTableInfo.None: default: return new CrcEngine8(width, poly8, init8, xorout8, refin, refout);
                        case CrcTableInfo.Standard: return new CrcEngine8Standard(width, poly8, init8, xorout8, refin, refout, table.GetUint8Table());
                        case CrcTableInfo.M16x: return new CrcEngine8M16x(width, poly8, init8, xorout8, refin, refout, table.GetUint8Table());
                    }

                case CrcCore.UInt16:
                    if (width > 16)
                    {
                        throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 16.", nameof(width));
                    }
                    ushort poly16 = poly.ToUInt16();
                    ushort init16 = init.ToUInt16();
                    ushort xorout16 = xorout.ToUInt16();
                    switch (table.TableInfo)
                    {
                        case CrcTableInfo.None: default: return new CrcEngine16(width, poly16, init16, xorout16, refin, refout);
                        case CrcTableInfo.Standard: return new CrcEngine16Standard(width, poly16, init16, xorout16, refin, refout, table.GetUint16Table());
                        case CrcTableInfo.M16x: return new CrcEngine16M16x(width, poly16, init16, xorout16, refin, refout, table.GetUint16Table());
                    }

                case CrcCore.UInt32:
                    if (width > 32)
                    {
                        throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 32.", nameof(width));
                    }
                    uint poly32 = poly.ToUInt32();
                    uint init32 = init.ToUInt32();
                    uint xorout32 = xorout.ToUInt32();
                    switch (table.TableInfo)
                    {
                        case CrcTableInfo.None: default: return new CrcEngine32(width, poly32, init32, xorout32, refin, refout);
                        case CrcTableInfo.Standard: return new CrcEngine32Standard(width, poly32, init32, xorout32, refin, refout, table.GetUint32Table());
                        case CrcTableInfo.M16x: return new CrcEngine32M16x(width, poly32, init32, xorout32, refin, refout, table.GetUint32Table());
                    }

                case CrcCore.UInt64:
                    if (width > 64)
                    {
                        throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 64.", nameof(width));
                    }
                    ulong poly64 = poly.ToUInt64();
                    ulong init64 = init.ToUInt64();
                    ulong xorout64 = xorout.ToUInt64();
                    switch (table.TableInfo)
                    {
                        case CrcTableInfo.None: default: return new CrcEngine64(width, poly64, init64, xorout64, refin, refout);
                        case CrcTableInfo.Standard: return new CrcEngine64Standard(width, poly64, init64, xorout64, refin, refout, table.GetUint64Table());
                        case CrcTableInfo.M16x: return new CrcEngine64M16x(width, poly64, init64, xorout64, refin, refout, table.GetUint64Table());
                    }

                case CrcCore.Sharding8:
                    byte[] polyA8 = CrcConverter.GetUInt8Array(CrcStringFormat.Hex, poly.ToHex(CrcCaseSensitivity.Lower), width);
                    byte[] initA8 = CrcConverter.GetUInt8Array(CrcStringFormat.Hex, init.ToHex(CrcCaseSensitivity.Lower), width);
                    byte[] xoroutA8 = CrcConverter.GetUInt8Array(CrcStringFormat.Hex, xorout.ToHex(CrcCaseSensitivity.Lower), width);
                    switch (table.TableInfo)
                    {
                        case CrcTableInfo.None: default: return new CrcEngineSharding8(width, polyA8, initA8, xoroutA8, refin, refout);
                        case CrcTableInfo.Standard: return new CrcEngineSharding8Standard(width, polyA8, initA8, xoroutA8, refin, refout, table.GetUint8Table());
                        case CrcTableInfo.M16x: return new CrcEngineSharding8Standard(width, polyA8, initA8, xoroutA8, refin, refout, table.GetUint8Table());
                    }

                case CrcCore.Sharding16:
                    ushort[] polyA16 = CrcConverter.GetUInt16Array(CrcStringFormat.Hex, poly.ToHex(CrcCaseSensitivity.Lower), width);
                    ushort[] initA16 = CrcConverter.GetUInt16Array(CrcStringFormat.Hex, init.ToHex(CrcCaseSensitivity.Lower), width);
                    ushort[] xoroutA16 = CrcConverter.GetUInt16Array(CrcStringFormat.Hex, xorout.ToHex(CrcCaseSensitivity.Lower), width);
                    switch (table.TableInfo)
                    {
                        case CrcTableInfo.None: default: return new CrcEngineSharding16(width, polyA16, initA16, xoroutA16, refin, refout);
                        case CrcTableInfo.Standard: return new CrcEngineSharding16Standard(width, polyA16, initA16, xoroutA16, refin, refout, table.GetUint16Table());
                        case CrcTableInfo.M16x: return new CrcEngineSharding16Standard(width, polyA16, initA16, xoroutA16, refin, refout, table.GetUint16Table());
                    }

                case CrcCore.Sharding32:
                default:
                    uint[] polyA32 = CrcConverter.GetUInt32Array(CrcStringFormat.Hex, poly.ToHex(CrcCaseSensitivity.Lower), width);
                    uint[] initA32 = CrcConverter.GetUInt32Array(CrcStringFormat.Hex, init.ToHex(CrcCaseSensitivity.Lower), width);
                    uint[] xoroutA32 = CrcConverter.GetUInt32Array(CrcStringFormat.Hex, xorout.ToHex(CrcCaseSensitivity.Lower), width);
                    switch (table.TableInfo)
                    {
                        case CrcTableInfo.None: default: return new CrcEngineSharding32(width, polyA32, initA32, xoroutA32, refin, refout);
                        case CrcTableInfo.Standard: return new CrcEngineSharding32Standard(width, polyA32, initA32, xoroutA32, refin, refout, table.GetUint32Table());
                        case CrcTableInfo.M16x: return new CrcEngineSharding32Standard(width, polyA32, initA32, xoroutA32, refin, refout, table.GetUint32Table());
                    }

                case CrcCore.Sharding64:
                    ulong[] polyA64 = CrcConverter.GetUInt64Array(CrcStringFormat.Hex, poly.ToHex(CrcCaseSensitivity.Lower), width);
                    ulong[] initA64 = CrcConverter.GetUInt64Array(CrcStringFormat.Hex, init.ToHex(CrcCaseSensitivity.Lower), width);
                    ulong[] xoroutA64 = CrcConverter.GetUInt64Array(CrcStringFormat.Hex, xorout.ToHex(CrcCaseSensitivity.Lower), width);
                    switch (table.TableInfo)
                    {
                        case CrcTableInfo.None: default: return new CrcEngineSharding64(width, polyA64, initA64, xoroutA64, refin, refout);
                        case CrcTableInfo.Standard: return new CrcEngineSharding64Standard(width, polyA64, initA64, xoroutA64, refin, refout, table.GetUint64Table());
                        case CrcTableInfo.M16x: return new CrcEngineSharding64Standard(width, polyA64, initA64, xoroutA64, refin, refout, table.GetUint64Table());
                    }
            }
        }
    }
}