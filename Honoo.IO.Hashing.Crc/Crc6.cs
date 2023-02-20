namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-6/ITU.
    /// </summary>
    public sealed class Crc6Itu : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc6Itu class.
        /// </summary>
        public Crc6Itu() : base(new CrcUInt8Engine("CRC-6/ITU", 6, true, 0x30, 0x00, 0x00, true))
        {
            // poly = 0x03; reverse >>(8-6) = 0x30;
        }
    }
}