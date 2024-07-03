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
        /// Use 8 bit <see langword="WITHOUT"></see> table calculation core. The allowed width bits are between 0 - 8.
        /// </summary>
        U8,

        /// <summary>
        /// Use 8 bit <see langword="WITH"></see> table calculation core. The allowed width bits are between 0 - 8.
        /// </summary>
        U8Table,

        /// <summary>
        /// Use 16 bit <see langword="WITHOUT"></see> table calculation core. The allowed width bits are between 0 - 16.
        /// </summary>
        U16,

        /// <summary>
        /// Use 16 bit <see langword="WITH"></see> table calculation core. The allowed width bits are between 0 - 16.
        /// </summary>
        U16Table,

        /// <summary>
        /// Use 32 bit <see langword="WITHOUT"></see> table calculation core. The allowed width bits are between 0 - 32.
        /// </summary>
        U32,

        /// <summary>
        /// Use 32 bit <see langword="WITH"></see> table calculation core. The allowed width bits are between 0 - 32.
        /// </summary>
        U32Table,

        /// <summary>
        /// Use 64 bit <see langword="WITHOUT"></see> table calculation core. The allowed width bits are between 0 - 64.
        /// </summary>
        U64,

        /// <summary>
        /// Use 64 bit <see langword="WITH"></see> table calculation core. The allowed width bits are between 0 - 64.
        /// </summary>
        U64Table,

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