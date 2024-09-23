namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-5/EPC, CRC-5/EPC-C1G2.
    /// </summary>
    public sealed class Crc5Epc : Crc
    {
        private const string DEFAULT_NAME = "CRC-5/EPC";
        private const byte INIT = 0x09;
        private const byte POLY = 0x09;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 5;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc5Epc class.
        /// </summary>
        public Crc5Epc() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc5Epc(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc5Epc(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc5Epc(alias); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x09; <<(8-5) = 0x48;
            // init = 0x09; <<(8-5) = 0x48;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x48);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-5/ITU, CRC-5/G-704.
    /// </summary>
    public sealed class Crc5Itu : Crc
    {
        private const string DEFAULT_NAME = "CRC-5/ITU";
        private const byte INIT = 0x00;
        private const byte POLY = 0x15;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 5;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc5Itu class.
        /// </summary>
        public Crc5Itu() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc5Itu(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc5Itu(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc5Itu(alias); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x15; reverse >>(8-5) = 0x15;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0x15);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-5/USB.
    /// </summary>
    public sealed class Crc5Usb : Crc
    {
        private const string DEFAULT_NAME = "CRC-5/USB";
        private const byte INIT = 0x1F;
        private const byte POLY = 0x05;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 5;
        private const byte XOROUT = 0x1F;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc5Usb class.
        /// </summary>
        public Crc5Usb() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc5Usb(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x05; reverse >>(8-5) = 0x14;
            // init = 0x1F; reverse >>(8-5) = 0x1F;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0x14);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}