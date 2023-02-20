using System;

namespace Honoo.IO.HashingOld
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
        /// <param name="refin">Do refin.</param>
        /// <param name="refout">Do refout.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="useTable">Calculations using the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, byte poly, byte init, byte xorout, bool useTable = true)
            : base(GetEngine(checksumSize, refin, refout, poly, init, xorout, useTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 16.</param>
        /// <param name="refin">Do refin.</param>
        /// <param name="refout">Do refout.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="useTable">Calculations using the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool useTable = true)
            : base(GetEngine(checksumSize, refin, refout, poly, init, xorout, useTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 32.</param>
        /// <param name="refin">Do refin.</param>
        /// <param name="refout">Do refout.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="useTable">Calculations using the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, uint poly, uint init, uint xorout, bool useTable = true)
            : base(GetEngine(checksumSize, refin, refout, poly, init, xorout, useTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 64.</param>
        /// <param name="refin">Do refin.</param>
        /// <param name="refout">Do refout.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="useTable">Calculations using the table.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool useTable = true)
            : base(GetEngine(checksumSize, refin, refout, poly, init, xorout, useTable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are more than 0.</param>
        /// <param name="refin">Do refin.</param>
        /// <param name="refout">Do refout.</param>
        /// <param name="polyHex">Polynomials value hex string.</param>
        /// <param name="initHex">Initialization value hex string.</param>
        /// <param name="xoroutHex">Output xor value hex string.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool refin, bool refout, string polyHex, string initHex, string xoroutHex)
            : base(new CrcEngineBin($"CRC-{checksumSize}/CUSTOM", checksumSize, refin, refout, polyHex, initHex, xoroutHex))
        {
        }

        #endregion Construction

        private static CrcEngine GetEngine(int checksumSize, bool refin, bool refout, byte poly, byte init, byte xorout, bool useTable)
        {
            if (useTable)
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

        private static CrcEngine GetEngine(int checksumSize, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool useTable)
        {
            if (useTable)
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

        private static CrcEngine GetEngine(int checksumSize, bool refin, bool refout, uint poly, uint init, uint xorout, bool useTable)
        {
            if (useTable)
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

        private static CrcEngine GetEngine(int checksumSize, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool useTable)
        {
            if (useTable)
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
    }
}