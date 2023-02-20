namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-5/EPC.
    /// </summary>
    public sealed class Crc5Epc : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc5Epc class.
        /// </summary>
        public Crc5Epc() : base(new CrcUInt8Engine("CRC-5/EPC", 5, false, 0x48, 0x48, 0x00, true))
        {
            // poly = 0x09; <<(8-5) = 0x48;
            // init = 0x09; <<(8-5) = 0x48;
        }
    }

    /// <summary>
    /// CRC-5/ITU.
    /// </summary>
    public sealed class Crc5Itu : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc5Itu class.
        /// </summary>

        public Crc5Itu() : base(new CrcUInt8Engine("CRC-5/ITU", 5, true, 0x15, 0x00, 0x00, true))
        {
            // poly = 0x15; reverse >>(8-5) = 0x15;
        }
    }

    /// <summary>
    /// CRC-5/USB.
    /// </summary>
    public sealed class Crc5Usb : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc5Usb class.
        /// </summary>
        public Crc5Usb() : base(new CrcUInt8Engine("CRC-5/USB", 5, true, 0x14, 0x1F, 0x1F, true))
        {
            // poly = 0x05; reverse >>(8-5) = 0x14;
            // init = 0x1F; reverse >>(8-5) = 0x1F;
            // xorout = 0x1F; reverse >>(8-5) = 0x1F;
        }
    }
}