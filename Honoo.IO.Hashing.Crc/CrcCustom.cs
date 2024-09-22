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
        /// <param name="name">Custom name.</param>
        /// <param name="width">Crc width in bits. The allowed values are between 0 - 8.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculate with table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(string name, int width, bool refin, bool refout, byte poly, byte init, byte xorout, bool withTable = true)
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
        public CrcCustom(string name, int width, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool withTable = true)
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
        public CrcCustom(string name, int width, bool refin, bool refout, uint poly, uint init, uint xorout, bool withTable = true)
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
        public CrcCustom(string name, int width, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool withTable = true)
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
        public CrcCustom(string name, int width, bool refin, bool refout, CrcParameter poly, CrcParameter init, CrcParameter xorout, bool withTable = true, CrcCore core = CrcCore.Auto)
            : base(name, GetEngine(width, refin, refout, poly, init, xorout, withTable, core))
        {
        }

        #endregion Construction

        private static CrcEngine GetEngine(int width, bool refin, bool refout, CrcParameter polyParameter, CrcParameter initParameter, CrcParameter xoroutParameter, bool withTable, CrcCore core)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid checkcum width. The allowed values are more than 0.", nameof(width));
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
                if (width <= 8) core = CrcCore.UInt8;
                else if (width <= 16) core = CrcCore.UInt16;
                else if (width <= 32) core = CrcCore.UInt32;
                else if (width <= 64) core = CrcCore.UInt64;
                else core = CrcCore.Sharding32;
            }
            switch (core)
            {
                case CrcCore.UInt8:
                    {
                        byte poly = CrcConverter.ToUInt8(NumericsStringFormat.Hex, polyParameter.ToHexString(), width);
                        byte init = CrcConverter.ToUInt8(NumericsStringFormat.Hex, initParameter.ToHexString(), width);
                        byte xorout = CrcConverter.ToUInt8(NumericsStringFormat.Hex, xoroutParameter.ToHexString(), width);
                        return new CrcEngine8(width, refin, refout, poly, init, xorout, withTable);
                    }

                case CrcCore.UInt16:
                    {
                        ushort poly = CrcConverter.ToUInt16(NumericsStringFormat.Hex, polyParameter.ToHexString(), width);
                        ushort init = CrcConverter.ToUInt16(NumericsStringFormat.Hex, initParameter.ToHexString(), width);
                        ushort xorout = CrcConverter.ToUInt16(NumericsStringFormat.Hex, xoroutParameter.ToHexString(), width);
                        return new CrcEngine16(width, refin, refout, poly, init, xorout, withTable);
                    }

                case CrcCore.UInt32:
                    {
                        uint poly = CrcConverter.ToUInt32(NumericsStringFormat.Hex, polyParameter.ToHexString(), width);
                        uint init = CrcConverter.ToUInt32(NumericsStringFormat.Hex, initParameter.ToHexString(), width);
                        uint xorout = CrcConverter.ToUInt32(NumericsStringFormat.Hex, xoroutParameter.ToHexString(), width);
                        return new CrcEngine32(width, refin, refout, poly, init, xorout, withTable);
                    }

                case CrcCore.UInt64:
                    {
                        ulong poly = CrcConverter.ToUInt64(NumericsStringFormat.Hex, polyParameter.ToHexString(), width);
                        ulong init = CrcConverter.ToUInt64(NumericsStringFormat.Hex, initParameter.ToHexString(), width);
                        ulong xorout = CrcConverter.ToUInt64(NumericsStringFormat.Hex, xoroutParameter.ToHexString(), width);
                        return new CrcEngine64(width, refin, refout, poly, init, xorout, withTable);
                    }

                case CrcCore.Sharding8:
                    {
                        byte[] poly = CrcConverter.ToUInt8Array(NumericsStringFormat.Hex, polyParameter.ToHexString(), width);
                        byte[] init = CrcConverter.ToUInt8Array(NumericsStringFormat.Hex, initParameter.ToHexString(), width);
                        byte[] xorout = CrcConverter.ToUInt8Array(NumericsStringFormat.Hex, xoroutParameter.ToHexString(), width);
                        return new CrcEngineSharding8(width, refin, refout, poly, init, xorout, withTable);
                    }

                case CrcCore.Sharding16:
                    {
                        ushort[] poly = CrcConverter.ToUInt16Array(NumericsStringFormat.Hex, polyParameter.ToHexString(), width);
                        ushort[] init = CrcConverter.ToUInt16Array(NumericsStringFormat.Hex, initParameter.ToHexString(), width);
                        ushort[] xorout = CrcConverter.ToUInt16Array(NumericsStringFormat.Hex, xoroutParameter.ToHexString(), width);
                        return new CrcEngineSharding16(width, refin, refout, poly, init, xorout, withTable);
                    }
                case CrcCore.Sharding64:
                    {
                        ulong[] poly = CrcConverter.ToUInt64Array(NumericsStringFormat.Hex, polyParameter.ToHexString(), width);
                        ulong[] init = CrcConverter.ToUInt64Array(NumericsStringFormat.Hex, initParameter.ToHexString(), width);
                        ulong[] xorout = CrcConverter.ToUInt64Array(NumericsStringFormat.Hex, xoroutParameter.ToHexString(), width);
                        return new CrcEngineSharding64(width, refin, refout, poly, init, xorout, withTable);
                    }
                case CrcCore.Sharding32:
                default:
                    {
                        uint[] poly = CrcConverter.ToUInt32Array(NumericsStringFormat.Hex, polyParameter.ToHexString(), width);
                        uint[] init = CrcConverter.ToUInt32Array(NumericsStringFormat.Hex, initParameter.ToHexString(), width);
                        uint[] xorout = CrcConverter.ToUInt32Array(NumericsStringFormat.Hex, xoroutParameter.ToHexString(), width);
                        return new CrcEngineSharding32(width, refin, refout, poly, init, xorout, withTable);
                    }
            }
        }
    }
}