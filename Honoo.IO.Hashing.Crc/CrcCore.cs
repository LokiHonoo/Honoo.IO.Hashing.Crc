namespace Honoo.IO.Hashing
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
        /// Use 8 bit <see langword="WITHOUT"></see> table calculation core. The allowed width bits are between 0 - 8.
        /// </summary>
        UInt8,

        /// <summary>
        /// Use 8 bit <see langword="WITH"></see> table calculation core. The allowed width bits are between 0 - 8.
        /// </summary>
        UInt8Table,

        /// <summary>
        /// Use 16 bit <see langword="WITHOUT"></see> table calculation core. The allowed width bits are between 0 - 16.
        /// </summary>
        UInt16,

        /// <summary>
        /// Use 16 bit <see langword="WITH"></see> table calculation core. The allowed width bits are between 0 - 16.
        /// </summary>
        UInt16Table,

        /// <summary>
        /// Use 32 bit <see langword="WITHOUT"></see> table calculation core. The allowed width bits are between 0 - 32.
        /// </summary>
        UInt32,

        /// <summary>
        /// Use 32 bit <see langword="WITH"></see> table calculation core. The allowed width bits are between 0 - 32.
        /// </summary>
        UInt32Table,

        /// <summary>
        /// Use 64 bit <see langword="WITHOUT"></see> table calculation core. The allowed width bits are between 0 - 64.
        /// </summary>
        UInt64,

        /// <summary>
        /// Use 64 bit <see langword="WITH"></see> table calculation core. The allowed width bits are between 0 - 64.
        /// </summary>
        UInt64Table,

        /// <summary>
        /// Use 8 bit shardings <see langword="WITHOUT"></see> calculation core. The allowed width bits are more than 0.
        /// </summary>
        Sharding8,

        /// <summary>
        /// Use 8 bit sharding <see langword="WITH"></see> table calculation core. The allowed width bits are more than 0.
        /// </summary>
        Sharding8Table,

        /// <summary>
        /// Use 32 bit sharding <see langword="WITHOUT"></see> calculation core. The allowed width bits are more than 0.
        /// </summary>
        Sharding32,

        /// <summary>
        /// Use 32 bit sharding <see langword="WITH"></see> table calculation core. The allowed width bits are more than 0.
        /// </summary>
        Sharding32Table,
    }
}