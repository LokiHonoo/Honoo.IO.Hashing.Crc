namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-32. CRC-32/ADCCP.
    /// </summary>
    public sealed class Crc32 : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc32 class.
        /// </summary>
        public Crc32() : base(new CrcUInt32Engine("CRC-32", 32, true, 0xEDB88320, 0xFFFFFFFF, 0xFFFFFFFF, true))
        {
            // poly = 0x04C11DB7; reverse = 0xEDB88320;
        }
    }

    /// <summary>
    /// CRC-32/C.
    /// </summary>
    public sealed class Crc32c : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc32c class.
        /// </summary>
        public Crc32c() : base(new CrcUInt32Engine("CRC-32/C", 32, true, 0x82F63B78, 0xFFFFFFFF, 0xFFFFFFFF, true))
        {
            // poly = 0x1EDC6F41; reverse = 0x82F63B78;
        }
    }

    /// <summary>
    /// CRC-32/KOOPMAN.
    /// </summary>
    public sealed class Crc32Koopman : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc32Koopman class.
        /// </summary>
        public Crc32Koopman() : base(new CrcUInt32Engine("CRC-32/KOOPMAN", 32, true, 0xEB31D82E, 0xFFFFFFFF, 0xFFFFFFFF, true))
        {
            // poly = 0x741B8CD7; reverse = 0xEB31D82E;
        }
    }

    /// <summary>
    /// CRC-32/MPEG-2.
    /// </summary>
    public sealed class Crc32Mpeg2 : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc32Mpeg2 class.
        /// </summary>
        public Crc32Mpeg2() : base(new CrcUInt32Engine("CRC-32/MPEG-2", 32, false, 0x04C11DB7, 0xFFFFFFFF, 0x00000000, true))
        {
            // poly = 0x04C11DB7;
        }
    }
}