namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-7/MMC.
    /// </summary>
    public sealed class Crc7Mmc : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc7Mmc class.
        /// </summary>
        public Crc7Mmc() : base(new CrcUInt8Engine("CRC-7/MMC", 7, false, 0x12, 0x00, 0x00, true))
        {
            // poly = 0x09; <<(8-7) = 0x12;
        }
    }
}