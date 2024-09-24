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
        /// Use 8 bits calculation core. The allowed width bits length are between 0 - 8.
        /// </summary>
        UInt8,

        /// <summary>
        /// Use 16 bits calculation core. The allowed width bits length are between 0 - 16.
        /// </summary>
        UInt16,

        /// <summary>
        /// Use 32 bits calculation core. The allowed width bits length are between 0 - 32.
        /// </summary>
        UInt32,

        /// <summary>
        /// Use 64 bits calculation core. The allowed width bits length are between 0 - 64.
        /// </summary>
        UInt64,

        /// <summary>
        /// Use 128 bits (link 2 UInt64) calculation core. The allowed width bits length are between 0 - 128.
        /// </summary>
        UInt128L2,

        /// <summary>
        /// Use 8 bits shardings calculation core. The allowed width bits length are more than 0.
        /// </summary>
        Sharding8,

        /// <summary>
        /// Use 16 bits sharding calculation core. The allowed width bits length are more than 0.
        /// </summary>
        Sharding16,

        /// <summary>
        /// Use 32 bits sharding calculation core. The allowed width bits length are more than 0.
        /// </summary>
        Sharding32,

        /// <summary>
        /// Use 64 bits sharding calculation core. The allowed width bits length are more than 0.
        /// </summary>
        Sharding64,
    }
}