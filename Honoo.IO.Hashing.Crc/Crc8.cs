namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-8.
    /// </summary>
    public sealed class Crc8 : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc8 class.
        /// </summary>
        public Crc8() : base(new CrcUInt8Engine("CRC-8", 8, false, 0x07, 0x00, 0x00, true))
        {
            // poly = 0x07;
        }
    }

    /// <summary>
    /// CRC-8/ITU. CRC-8/ATM.
    /// </summary>
    public sealed class Crc8Itu : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc8Itu class.
        /// </summary>
        public Crc8Itu() : base(new CrcUInt8Engine("CRC-8/ITU", 8, false, 0x07, 0x00, 0x55, true))
        {
            // poly = 0x07;
        }
    }

    /// <summary>
    /// CRC-8/MAXIM. DOW-CRC. CRC-8/IBUTTON.
    /// </summary>
    public sealed class Crc8Maxim : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc8Maxim class.
        /// </summary>
        public Crc8Maxim() : base(new CrcUInt8Engine("CRC-8/MAXIM", 8, true, 0x8C, 0x00, 0x00, true))
        {
            // poly = 0x31; reverse = 0x8C;
        }
    }

    /// <summary>
    /// CRC-8/ROHC.
    /// </summary>
    public sealed class Crc8Rohc : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc8Rohc class.
        /// </summary>
        public Crc8Rohc() : base(new CrcUInt8Engine("CRC-8/ROHC", 8, true, 0xE0, 0xFF, 0x00, true))
        {
            // poly = 0x07; reverse = 0xE0;
        }
    }
}