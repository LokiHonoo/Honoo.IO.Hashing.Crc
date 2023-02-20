namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-16/CCITT. CRC-16/KERMIT.
    /// </summary>
    public sealed class Crc16Ccitt : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16Ccitt class.
        /// </summary>
        public Crc16Ccitt() : base(new CrcUInt16Engine("CRC-16/CCITT", 16, true, 0x8408, 0x0000, 0x0000, true))
        {
            // poly = 0x1021; reverse = 0x8408;
        }
    }

    /// <summary>
    /// CRC-16/CCITT-FALSE.
    /// </summary>
    public sealed class Crc16CcittFalse : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16CcittFalse class.
        /// </summary>
        public Crc16CcittFalse() : base(new CrcUInt16Engine("CRC-16/CCITT-FALSE", 16, false, 0x1021, 0xFFFF, 0x0000, true))
        {
            // poly = 0x1021;
        }
    }

    /// <summary>
    /// CRC-16/DNP.
    /// </summary>
    public sealed class Crc16Dnp : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16Dnp class.
        /// </summary>
        public Crc16Dnp() : base(new CrcUInt16Engine("CRC-16/DNP", 16, true, 0xA6BC, 0x0000, 0xFFFF, true))
        {
            // poly = 0x3D65; reverse = 0xA6BC;
        }
    }

    /// <summary>
    /// CRC-16/IBM. CRC-16/ARC. CRC-16/LHA.
    /// </summary>
    public sealed class Crc16Ibm : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16Ibm class.
        /// </summary>
        public Crc16Ibm() : base(new CrcUInt16Engine("CRC-16/IBM", 16, true, 0xA001, 0x0000, 0x0000, true))
        {
            // poly = 0x8005; reverse = 0xA001;
        }
    }

    /// <summary>
    /// CRC-16/MAXIM.
    /// </summary>
    public sealed class Crc16Maxim : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16Maxim class.
        /// </summary>
        public Crc16Maxim() : base(new CrcUInt16Engine("CRC-16/MAXIM", 16, true, 0xA001, 0x0000, 0xFFFF, true))
        {
            // poly = 0x8005; reverse = 0xA001;
        }
    }

    /// <summary>
    /// CRC-16/MODBUS.
    /// </summary>
    public sealed class Crc16Modbus : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16Modbus class.
        /// </summary>
        public Crc16Modbus() : base(new CrcUInt16Engine("CRC-16/MODBUS", 16, true, 0xA001, 0xFFFF, 0x0000, true))
        {
            // poly = 0x8005; reverse = 0xA001;
        }
    }

    /// <summary>
    /// CRC-16/USB.
    /// </summary>
    public sealed class Crc16Usb : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16Usb class.
        /// </summary>
        public Crc16Usb() : base(new CrcUInt16Engine("CRC-16/USB", 16, true, 0xA001, 0xFFFF, 0xFFFF, true))
        {
            // poly = 0x8005; reverse = 0xA001;
        }
    }

    /// <summary>
    /// CRC-16/X25.
    /// </summary>
    public sealed class Crc16X25 : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16X25 class.
        /// </summary>
        public Crc16X25() : base(new CrcUInt16Engine("CRC-16/X25", 16, true, 0x8408, 0xFFFF, 0xFFFF, true))
        {
            // poly = 0x1021; reverse = 0x8408;
        }
    }

    /// <summary>
    /// CRC-16/XMODEM. CRC-16/ZMODEM. CRC-16/ACORN.
    /// </summary>
    public sealed class Crc16Xmodem : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16Xmodem class.
        /// </summary>
        public Crc16Xmodem() : base(new CrcUInt16Engine("CRC-16/XMODEM", 16, false, 0x1021, 0x0000, 0x0000, true))
        {
            // poly = 0x1021;
        }
    }

    /// <summary>
    /// CRC-16/XMODEM2.
    /// </summary>
    public sealed class Crc16Xmodem2 : Crc
    {
        /// <summary>
        /// Initializes a new instance of the Crc16Xmodem2 class.
        /// </summary>
        public Crc16Xmodem2() : base(new CrcUInt16Engine("CRC-16/XMODEM2", 16, true, 0x1021, 0x0000, 0x0000, true))
        {
            // poly = 0x8408; reverse = 0x1021;
        }
    }
}