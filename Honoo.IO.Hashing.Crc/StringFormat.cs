namespace Honoo.IO.Hashing
{
    /// <summary>
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:标识符包含类型名称", Justification = "<挂起>")]
    public enum StringFormat
    {
        /// <summary>
        /// String as "11110000".
        /// </summary>
        Binary,

        /// <summary>
        /// String start with "0b". As "0b11110000".
        /// </summary>
        BinaryWithPrefix,

        /// <summary>
        /// String as "FF55".
        /// </summary>
        Hex,

        /// <summary>
        /// String start with "0x". As "0xFF55".
        /// </summary>
        HexWithPrefix,
    }
}