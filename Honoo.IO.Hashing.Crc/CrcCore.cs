namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Use the specified CRC calculation core.
    /// </summary>
    public enum CrcCore
    {
        /// <summary>
        /// Auto selected.
        /// </summary>
        Auto,

        /// <summary>
        /// Use 8 bit calculation core. The allowed checksum size are between 0 - 8.
        /// </summary>
        UInt8,

        /// <summary>
        /// Use 8 bit with table calculation core. The allowed checksum size are between 0 - 8.
        /// </summary>
        UInt8Table,

        /// <summary>
        /// Use 16 bit calculation core. The allowed checksum size are between 0 - 16.
        /// </summary>
        UInt16,

        /// <summary>
        /// Use 16 bit with table calculation core. The allowed checksum size are between 0 - 16.
        /// </summary>
        UInt16Table,

        /// <summary>
        /// Use 32 bit calculation core. The allowed checksum size are between 0 - 32.
        /// </summary>
        UInt32,

        /// <summary>
        /// Use 32 bit with table calculation core. The allowed checksum size are between 0 - 32.
        /// </summary>
        UInt32Table,

        /// <summary>
        /// Use 64 bit calculation core. The allowed checksum size are between 0 - 64.
        /// </summary>
        UInt64,

        /// <summary>
        /// Use 64 bit with table calculation core. The allowed checksum size are between 0 - 64.
        /// </summary>
        UInt64Table,

        /// <summary>
        /// Use 8 bit sharding calculation core. The allowed checksum size are are more than 0.
        /// </summary>
        Sharding8,

        /// <summary>
        /// Use 8 bit sharding with table calculation core. The allowed checksum size are are more than 0.
        /// </summary>
        Sharding8Table,

        /// <summary>
        /// Use 32 bit sharding calculation core. The allowed checksum size are are more than 0.
        /// </summary>
        Sharding32,

        /// <summary>
        /// Use 32 bit sharding with table calculation core. The allowed checksum size are are more than 0.
        /// </summary>
        Sharding32Table,
    }
}