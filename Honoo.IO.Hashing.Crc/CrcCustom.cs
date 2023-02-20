using System;
using System.Numerics;

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
        /// <param name="checksumSize">Checkum size bits.</param>
        /// <param name="reverse">Do reverse.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool reverse, byte poly, byte init, byte xorout)
            : base(new CrcUInt8Engine($"CRC-{checksumSize}/CUSTOM", checksumSize, reverse, poly, init, xorout, false))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits.</param>
        /// <param name="reverse">Do reverse.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool reverse, ushort poly, ushort init, ushort xorout)
            : base(new CrcUInt16Engine($"CRC-{checksumSize}/CUSTOM", checksumSize, reverse, poly, init, xorout, false))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits.</param>
        /// <param name="reverse">Do reverse.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool reverse, uint poly, uint init, uint xorout)
            : base(new CrcUInt32Engine($"CRC-{checksumSize}/CUSTOM", checksumSize, reverse, poly, init, xorout, false))
        {
        }

        /// <summary>
        /// Initializes a new instance of the CrcCustom class.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits.</param>
        /// <param name="reverse">Do reverse.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <exception cref="Exception"></exception>
        public CrcCustom(int checksumSize, bool reverse, ulong poly, ulong init, ulong xorout)
            : base(new CrcUInt64Engine($"CRC-{checksumSize}/CUSTOM", checksumSize, reverse, poly, init, xorout, false))
        {
        }

        ///// <summary>
        ///// Initializes a new instance of the CrcCustom class.
        ///// </summary>
        ///// <param name="checksumSize">Checkum size bits.</param>
        ///// <param name="reverse">Do reverse.</param>
        ///// <param name="poly">Polynomials value.</param>
        ///// <param name="init">Initialization value.</param>
        ///// <param name="xorout">Output xor value.</param>
        ///// <exception cref="Exception"></exception>
        //public CrcCustom(int checksumSize, bool reverse, byte[] poly, byte[] init, byte[] xorout)
        //    : base(new CrcBinEngine($"CRC-{checksumSize}/CUSTOM", checksumSize, reverse, poly, init, xorout, false))
        //{
        //}

        #endregion Construction
    }
}