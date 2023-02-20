namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-4/ITU.
    /// </summary>
    public sealed class Crc4Itu : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc4Itu class.
        /// </summary>
        public Crc4Itu() : base(new CrcUInt8Engine("CRC-4/ITU", 4, true, 0x0C, 0x00, 0x00, true))
        {
            // poly = 0x03; reverse >>(8-4) = 0x0C;
        }
    }
}