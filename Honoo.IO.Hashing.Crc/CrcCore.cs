﻿namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Use the specified CRC calculation core.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:标识符包含类型名称", Justification = "<挂起>")]
    public enum CrcCore
    {
        /// <summary>
        /// Auto selected.
        /// </summary>
        Auto,

        /// <summary>
        /// Use 8 bits calculation core. The allowed width bits are between 1 - 8.
        /// </summary>
        UInt8,

        /// <summary>
        /// Use 16 bits calculation core. The allowed width bits are between 1 - 16.
        /// </summary>
        UInt16,

        /// <summary>
        /// Use 32 bits calculation core. The allowed width bits are between 1 - 32.
        /// </summary>
        UInt32,

        /// <summary>
        /// Use 64 bits calculation core. The allowed width bits are between 1 - 64.
        /// </summary>
        UInt64,

        /// <summary>
        /// Use 8 bits shardings calculation core. The allowed width bits are more than 0.
        /// </summary>
        Sharding8,

        /// <summary>
        /// Use 16 bits sharding calculation core. The allowed width bits are more than 0.
        /// </summary>
        Sharding16,

        /// <summary>
        /// Use 32 bits sharding calculation core. The allowed width bits are more than 0.
        /// </summary>
        Sharding32,

        /// <summary>
        /// Use 64 bits sharding calculation core. The allowed width bits are more than 0.
        /// </summary>
        Sharding64,
    }
}