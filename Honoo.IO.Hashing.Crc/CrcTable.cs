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
        /// Gets table. Type maybe <see langword="byte[]"/>, <see langword="ushort[]"/>, <see langword="uint[]"/>, <see langword="ulong[]"/>.
        /// </summary>
        public object Table => _table;

        /// <summary>
        /// Gets a value indicating whether the calculate with table.
        /// </summary>
        public CrcTableInfo TableInfo => _tableInfo;

        #endregion Members

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CrcTable class. Table using by UInt8 calculation.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 8.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, byte poly, bool refin)
        {
            if (width <= 0 || width > 8)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8.", nameof(width));
            }
            switch (tableInfo)
            {
                case CrcTableInfo.None: default: break;
                case CrcTableInfo.Standard: _table = CrcEngine8Standard.GenerateTable(width, poly, refin); break;
                case CrcTableInfo.M16x: _table = CrcEngine8M16x.GenerateTable(width, poly, refin); break;
            }
            _tableInfo = tableInfo;
            _core = CrcCore.UInt8;
        }

        /// <summary>
        /// Initializes a new instance of the CrcTable class. Table using by UInt16 calculation.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 16.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, ushort poly, bool refin)
        {
            if (width <= 0 || width > 16)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
            }
            switch (tableInfo)
            {
                case CrcTableInfo.None: default: break;
                case CrcTableInfo.Standard: _table = CrcEngine16Standard.GenerateTable(width, poly, refin); break;
                case CrcTableInfo.M16x: _table = CrcEngine16M16x.GenerateTable(width, poly, refin); break;
            }
            _tableInfo = tableInfo;
            _core = CrcCore.UInt16;
        }

        /// <summary>
        /// Initializes a new instance of the CrcTable class. Table using by UInt32 calculation.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 32.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, uint poly, bool refin)
        {
            if (width <= 0 || width > 32)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 32.", nameof(width));
            }
            switch (tableInfo)
            {
                case CrcTableInfo.None: default: break;
                case CrcTableInfo.Standard: _table = CrcEngine32Standard.GenerateTable(width, poly, refin); break;
                case CrcTableInfo.M16x: _table = CrcEngine32M16x.GenerateTable(width, poly, refin); break;
            }
            _tableInfo = tableInfo;
            _core = CrcCore.UInt32;
        }

        /// <summary>
        /// Initializes a new instance of the CrcTable class. Table using by UInt64 calculation.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 64.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, ulong poly, bool refin)
        {
            if (width <= 0 || width > 64)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64.", nameof(width));
            }
            switch (tableInfo)
            {
                case CrcTableInfo.None: default: break;
                case CrcTableInfo.Standard: _table = CrcEngine64Standard.GenerateTable(width, poly, refin); break;
                case CrcTableInfo.M16x: _table = CrcEngine64M16x.GenerateTable(width, poly, refin); break;
            }
            _tableInfo = tableInfo;
            _core = CrcCore.UInt64;
        }

        /// <summary>
        /// Initializes a new instance of the CrcTable class.
        /// </summary>
        /// <param name="tableInfo">Calculate with table.</param>
        /// <param name="width">Crc width in bits. The allowed values are more than 0.</param>
        /// <param name="polyParameter">Polynomials value.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="core">Use the specified CRC calculation core.</param>
        /// <exception cref="Exception"></exception>
        public CrcTable(CrcTableInfo tableInfo, int width, CrcValue polyParameter, bool refin, CrcCore core)
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
                if (width <= 8) core = CrcCore.UInt8;
                else if (width <= 16) core = CrcCore.UInt16;
                else if (width <= 32) core = CrcCore.UInt32;
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
                                byte poly = polyParameter.ToUInt8();
                                _table = CrcEngine8Standard.GenerateTable(width, poly, refin);
                                break;
                            }
                        case CrcTableInfo.M16x:
                            {
                                if (width > 8)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8.", nameof(width));
                                }
                                byte poly = polyParameter.ToUInt8();
                                _table = CrcEngine8M16x.GenerateTable(width, poly, refin);
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
                                ushort poly = polyParameter.ToUInt16();
                                _table = CrcEngine16Standard.GenerateTable(width, poly, refin);
                                break;
                            }
                        case CrcTableInfo.M16x:
                            {
                                if (width > 16)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16.", nameof(width));
                                }
                                ushort poly = polyParameter.ToUInt16();
                                _table = CrcEngine16M16x.GenerateTable(width, poly, refin);
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
                                uint poly = polyParameter.ToUInt32();
                                _table = CrcEngine32Standard.GenerateTable(width, poly, refin);
                                break;
                            }
                        case CrcTableInfo.M16x:
                            {
                                if (width > 32)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 32.", nameof(width));
                                }
                                uint poly = polyParameter.ToUInt32();
                                _table = CrcEngine32M16x.GenerateTable(width, poly, refin);
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
                                ulong poly = polyParameter.ToUInt64();
                                _table = CrcEngine64Standard.GenerateTable(width, poly, refin);
                                break;
                            }
                        case CrcTableInfo.M16x:
                            {
                                if (width > 64)
                                {
                                    throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64.", nameof(width));
                                }
                                ulong poly = polyParameter.ToUInt64();
                                _table = CrcEngine64M16x.GenerateTable(width, poly, refin);
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
                                byte[] poly = CrcConverter.GetUInt8Array(CrcStringFormat.Hex, polyParameter.ToHex(CrcCaseSensitivity.Lower), width);
                                _table = CrcEngineSharding8Standard.GenerateTable(width, poly, refin);
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
                                ushort[] poly = CrcConverter.GetUInt16Array(CrcStringFormat.Hex, polyParameter.ToHex(CrcCaseSensitivity.Lower), width);
                                _table = CrcEngineSharding16Standard.GenerateTable(width, poly, refin);
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
                                uint[] poly = CrcConverter.GetUInt32Array(CrcStringFormat.Hex, polyParameter.ToHex(CrcCaseSensitivity.Lower), width);
                                _table = CrcEngineSharding32Standard.GenerateTable(width, poly, refin);
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
                                ulong[] poly = CrcConverter.GetUInt64Array(CrcStringFormat.Hex, polyParameter.ToHex(CrcCaseSensitivity.Lower), width);
                                _table = CrcEngineSharding64Standard.GenerateTable(width, poly, refin);
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