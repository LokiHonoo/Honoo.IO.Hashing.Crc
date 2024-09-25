﻿namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Table settings for CRC calculation.
    /// </summary>
    public enum CrcTable
    {
        /// <summary>
        /// Table are not used.
        /// </summary>
        None,

        /// <summary>
        /// Use a 256 size standard table.
        /// </summary>
        Standard,

        /// <summary>
        /// Use a 256 * 16 size fat table. Only supported CRC width bits are between 0 - 64.
        /// Only supported for <see cref="CrcCore.UInt8"/>, <see cref="CrcCore.UInt16"/>, <see cref="CrcCore.UInt32"/>, <see cref="CrcCore.UInt64"/>.
        /// </summary>
        M16x,
    }
}