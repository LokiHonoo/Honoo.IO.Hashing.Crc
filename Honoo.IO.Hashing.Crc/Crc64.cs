namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-64/ECMA.
    /// </summary>
    public sealed class Crc64Ecma : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc64Ecma class.
        /// </summary>
        public Crc64Ecma() : base(new CrcUInt64Engine("CRC-64/ECMA", 64, true, 0xC96C5795D7870F42, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, true))
        {
            // poly = 0x42F0E1EBA9EA3693; reverse = 0xC96C5795D7870F42;
        }
    }

    /// <summary>
    /// CRC-64/ISO.
    /// </summary>
    public sealed class Crc64Iso : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc64Iso class.
        /// </summary>
        public Crc64Iso() : base(new CrcUInt64Engine("CRC-64/ISO", 64, true, 0xD800000000000000, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, true))
        {
            // poly = 0x000000000000001B; reverse = 0xD800000000000000;
        }
    }
}