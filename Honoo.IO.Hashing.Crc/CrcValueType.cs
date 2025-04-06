namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC value type.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:标识符包含类型名称", Justification = "<挂起>")]
    public enum CrcValueType
    {
        /// <summary>
        ///
        /// </summary>
        UInt8,

        /// <summary>
        ///
        /// </summary>
        UInt16,

        /// <summary>
        ///
        /// </summary>
        UInt32,

        /// <summary>
        ///
        /// </summary>
        UInt64,

        /// <summary>
        ///
        /// </summary>
        BitArray,

        /// <summary>
        /// String e.g. "11110000".
        /// </summary>
        Binary,

        /// <summary>
        /// String e.g. "FF55".
        /// </summary>
        Hex
    }
}