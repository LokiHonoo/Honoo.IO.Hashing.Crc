using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC table.
    /// </summary>
    public sealed class CrcTable
    {
        #region Members

        private readonly CrcCore _core;
        private readonly object _table;
        private readonly CrcTableInfo _tableInfo;

        /// <summary>
        /// Gets a value indicating whether the calculate core.
        /// </summary>
        public CrcCore Core => _core;

        /// <summary>
        /// Gets table. Type maybe <see langword="byte[]"/>, <see langword="ushort[]"/>, <see langword="uint[]"/>, <see langword="ulong[]"/>,
        /// <see langword="byte[][]"/>, <see langword="ushort[][]"/>, <see langword="uint[][]"/>, <see langword="ulong[][]"/>.
        /// </summary>
        public object Table => _table;

        /// <summary>
        /// Gets a value indicating whether the calculate with table.
        /// </summary>
        public CrcTableInfo TableInfo => _tableInfo;

        #endregion Members

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CrcUInt8Table class.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 8.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, bool refin, byte poly)
        {
            if (width <= 0 || width > 8)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8.", nameof(width));
            }
            switch (tableInfo)
            {
                case CrcTableInfo.None: default: break;
                case CrcTableInfo.Standard:
                    {
                        byte polyParsed = CrcEngine8Standard.Parse(poly, 8 - width, refin);
                        _table = refin ? CrcEngine8Standard.GenerateTableRef(polyParsed) : CrcEngine8Standard.GenerateTable(polyParsed);
                        break;
                    }
                case CrcTableInfo.M16x:
                    {
                        byte polyParsed = CrcEngine8M16x.Parse(poly, 8 - width, refin);
                        _table = refin ? CrcEngine8M16x.GenerateTableRef(polyParsed) : CrcEngine8M16x.GenerateTable(polyParsed);
                        break;
                    }
            }
            _tableInfo = tableInfo;
            _core = CrcCore.UInt8;
        }

        /// <summary>
        /// Initializes a new instance of the CrcUInt16Table class.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 16.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, bool refin, ushort poly)
        {
            if (width <= 0 || width > 16)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
            }
            switch (tableInfo)
            {
                case CrcTableInfo.None: default: break;
                case CrcTableInfo.Standard:
                    {
                        ushort polyParsed = CrcEngine16Standard.Parse(poly, 16 - width, refin);
                        _table = refin ? CrcEngine16Standard.GenerateTableRef(polyParsed) : CrcEngine16Standard.GenerateTable(polyParsed);
                        break;
                    }
                case CrcTableInfo.M16x:
                    {
                        ushort polyParsed = CrcEngine16M16x.Parse(poly, 16 - width, refin);
                        _table = refin ? CrcEngine16M16x.GenerateTableRef(polyParsed) : CrcEngine16M16x.GenerateTable(polyParsed);
                        break;
                    }
            }
            _tableInfo = tableInfo;
            _core = CrcCore.UInt16;
        }

        /// <summary>
        /// Initializes a new instance of the CrcUInt32Table class.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 32.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, bool refin, uint poly)
        {
            if (width <= 0 || width > 32)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 32.", nameof(width));
            }
            switch (tableInfo)
            {
                case CrcTableInfo.None: default: break;
                case CrcTableInfo.Standard:
                    {
                        uint polyParsed = CrcEngine32Standard.Parse(poly, 32 - width, refin);
                        _table = refin ? CrcEngine32Standard.GenerateTableRef(polyParsed) : CrcEngine32Standard.GenerateTable(polyParsed);
                        break;
                    }
                case CrcTableInfo.M16x:
                    {
                        uint polyParsed = CrcEngine32M16x.Parse(poly, 32 - width, refin);
                        _table = refin ? CrcEngine32M16x.GenerateTableRef(polyParsed) : CrcEngine32M16x.GenerateTable(polyParsed);
                        break;
                    }
            }
            _tableInfo = tableInfo;
            _core = CrcCore.UInt32;
        }

        /// <summary>
        /// Initializes a new instance of the CrcUInt64Table class.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 64.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, bool refin, ulong poly)
        {
            if (width <= 0 || width > 64)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64.", nameof(width));
            }
            switch (tableInfo)
            {
                case CrcTableInfo.None: default: break;
                case CrcTableInfo.Standard:
                    {
                        ulong polyParsed = CrcEngine64Standard.Parse(poly, 64 - width, refin);
                        _table = refin ? CrcEngine64Standard.GenerateTableRef(polyParsed) : CrcEngine64Standard.GenerateTable(polyParsed);
                        break;
                    }
                case CrcTableInfo.M16x:
                    {
                        ulong polyParsed = CrcEngine64M16x.Parse(poly, 64 - width, refin);
                        _table = refin ? CrcEngine64M16x.GenerateTableRef(polyParsed) : CrcEngine64M16x.GenerateTable(polyParsed);
                        break;
                    }
            }
            _tableInfo = tableInfo;
            _core = CrcCore.UInt64;
        }

        /// <summary>
        /// Initializes a new instance of the CrcTable class.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are more than 0.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="polyParameter">Polynomials value.</param>
        /// <param name="core">Use the specified CRC calculation core.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, bool refin, CrcParameter polyParameter, CrcCore core)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0.", nameof(width));
            }
            if (polyParameter is null)
            {
                throw new ArgumentNullException(nameof(polyParameter));
            }
            if (core == CrcCore.Auto)
            {
                if (width <= 32) core = CrcCore.UInt32;
                else if (width <= 64) core = CrcCore.UInt64;
                else core = CrcCore.Sharding32;
            }
            switch (core)
            {
                case CrcCore.UInt8:
                    switch (tableInfo)
                    {
                        case CrcTableInfo.None: default: break;
                        case CrcTableInfo.Standard:
                            {
                                if (width > 8)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8.", nameof(width));
                                }
                                byte poly = CrcConverter.ToUInt8(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                byte polyParsed = CrcEngine8Standard.Parse(poly, 8 - width, refin);
                                _table = refin ? CrcEngine8Standard.GenerateTableRef(polyParsed) : CrcEngine8Standard.GenerateTable(polyParsed);
                                break;
                            }
                        case CrcTableInfo.M16x:
                            {
                                if (width > 8)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8.", nameof(width));
                                }
                                byte poly = CrcConverter.ToUInt8(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                byte polyParsed = CrcEngine8M16x.Parse(poly, 8 - width, refin);
                                _table = refin ? CrcEngine8M16x.GenerateTableRef(polyParsed) : CrcEngine8M16x.GenerateTable(polyParsed);
                                break;
                            }
                    }
                    break;

                case CrcCore.UInt16:
                    switch (tableInfo)
                    {
                        case CrcTableInfo.None: default: break;
                        case CrcTableInfo.Standard:
                            {
                                if (width > 16)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
                                }
                                ushort poly = CrcConverter.ToUInt16(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                ushort polyParsed = CrcEngine16Standard.Parse(poly, 16 - width, refin);
                                _table = refin ? CrcEngine16Standard.GenerateTableRef(polyParsed) : CrcEngine16Standard.GenerateTable(polyParsed);
                                break;
                            }
                        case CrcTableInfo.M16x:
                            {
                                if (width > 16)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
                                }
                                ushort poly = CrcConverter.ToUInt16(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                ushort polyParsed = CrcEngine16M16x.Parse(poly, 16 - width, refin);
                                _table = refin ? CrcEngine16M16x.GenerateTableRef(polyParsed) : CrcEngine16M16x.GenerateTable(polyParsed);
                                break;
                            }
                    }
                    break;

                case CrcCore.UInt32:
                    switch (tableInfo)
                    {
                        case CrcTableInfo.None: default: break;
                        case CrcTableInfo.Standard:
                            {
                                if (width > 32)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 32.", nameof(width));
                                }
                                uint poly = CrcConverter.ToUInt32(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                uint polyParsed = CrcEngine32Standard.Parse(poly, 32 - width, refin);
                                _table = refin ? CrcEngine32Standard.GenerateTableRef(polyParsed) : CrcEngine32Standard.GenerateTable(polyParsed);
                                break;
                            }
                        case CrcTableInfo.M16x:
                            {
                                if (width > 32)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 32.", nameof(width));
                                }
                                uint poly = CrcConverter.ToUInt32(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                uint polyParsed = CrcEngine32M16x.Parse(poly, 32 - width, refin);
                                _table = refin ? CrcEngine32M16x.GenerateTableRef(polyParsed) : CrcEngine32M16x.GenerateTable(polyParsed);
                                break;
                            }
                    }
                    break;

                case CrcCore.UInt64:
                    switch (tableInfo)
                    {
                        case CrcTableInfo.None: default: break;
                        case CrcTableInfo.Standard:
                            {
                                if (width > 64)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64.", nameof(width));
                                }
                                ulong poly = CrcConverter.ToUInt64(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                ulong polyParsed = CrcEngine64Standard.Parse(poly, 64 - width, refin);
                                _table = refin ? CrcEngine64Standard.GenerateTableRef(polyParsed) : CrcEngine64Standard.GenerateTable(polyParsed);
                                break;
                            }
                        case CrcTableInfo.M16x:
                            {
                                if (width > 64)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64.", nameof(width));
                                }
                                ulong poly = CrcConverter.ToUInt64(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                ulong polyParsed = CrcEngine64M16x.Parse(poly, 64 - width, refin);
                                _table = refin ? CrcEngine64M16x.GenerateTableRef(polyParsed) : CrcEngine64M16x.GenerateTable(polyParsed);
                                break;
                            }
                    }
                    break;

                case CrcCore.Sharding8:
                    switch (tableInfo)
                    {
                        case CrcTableInfo.None: default: break;
                        case CrcTableInfo.Standard:
                        case CrcTableInfo.M16x:
                            {
                                int rem = width % 8;
                                int moves = rem > 0 ? 8 - rem : 0;
                                byte[] poly = CrcConverter.ToUInt8Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                byte[] polyParsed = CrcEngineSharding8Standard.Parse(poly, moves, refin);
                                _table = refin ? CrcEngineSharding8Standard.GenerateTableRef(polyParsed) : CrcEngineSharding8Standard.GenerateTable(polyParsed);
                                break;
                            }
                    }
                    break;

                case CrcCore.Sharding16:
                    switch (tableInfo)
                    {
                        case CrcTableInfo.None: default: break;
                        case CrcTableInfo.Standard:
                        case CrcTableInfo.M16x:
                            {
                                int rem = width % 16;
                                int moves = rem > 0 ? 16 - rem : 0;
                                ushort[] poly = CrcConverter.ToUInt16Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                ushort[] polyParsed = CrcEngineSharding16Standard.Parse(poly, moves, refin);
                                _table = refin ? CrcEngineSharding16Standard.GenerateTableRef(polyParsed) : CrcEngineSharding16Standard.GenerateTable(polyParsed);
                                break;
                            }
                    }
                    break;

                case CrcCore.Sharding32:
                default:
                    switch (tableInfo)
                    {
                        case CrcTableInfo.None: default: break;
                        case CrcTableInfo.Standard:
                        case CrcTableInfo.M16x:
                            {
                                int rem = width % 32;
                                int moves = rem > 0 ? 32 - rem : 0;
                                uint[] poly = CrcConverter.ToUInt32Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                uint[] polyParsed = CrcEngineSharding32Standard.Parse(poly, moves, refin);
                                _table = refin ? CrcEngineSharding32Standard.GenerateTableRef(polyParsed) : CrcEngineSharding32Standard.GenerateTable(polyParsed);
                                break;
                            }
                    }
                    break;

                case CrcCore.Sharding64:
                    switch (tableInfo)
                    {
                        case CrcTableInfo.None: default: break;
                        case CrcTableInfo.Standard:
                        case CrcTableInfo.M16x:
                            {
                                int rem = width % 64;
                                int moves = rem > 0 ? 64 - rem : 0;
                                ulong[] poly = CrcConverter.ToUInt64Array(CrcStringFormat.Hex, polyParameter.ToString(CrcStringFormat.Hex), width);
                                ulong[] polyParsed = CrcEngineSharding64Standard.Parse(poly, moves, refin);
                                _table = refin ? CrcEngineSharding64Standard.GenerateTableRef(polyParsed) : CrcEngineSharding64Standard.GenerateTable(polyParsed);
                                break;
                            }
                    }
                    break;
            }
            _tableInfo = tableInfo;
            _core = core;
        }

        internal CrcTable(CrcTableInfo tableInfo, CrcCore core, object table)
        {
            _tableInfo = tableInfo;
            _core = core;
            _table = table;
        }

        #endregion Construction
    }
}