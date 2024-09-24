namespace Honoo.IO.Hashing
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
        /// Use a 256 * 16 size fat table.
        /// </summary>
        M16x,
    }
}