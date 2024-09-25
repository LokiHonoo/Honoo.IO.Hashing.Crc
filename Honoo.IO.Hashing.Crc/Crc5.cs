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
        public Crc5Epc() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc5Epc class.
        /// </summary>
        public Crc5Epc(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc5Epc(string alias, CrcTable withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc5Epc(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc5Epc(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x09; <<(8-5) = 0x48;
            // init = 0x09; <<(8-5) = 0x48;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine8.GenerateTable(0x48);
                    }
                    return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine8M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
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
        public Crc5Itu() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc5Itu class.
        /// </summary>
        public Crc5Itu(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc5Itu(string alias, CrcTable withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc5Itu(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc5Itu(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x15; reverse >>(8-5) = 0x15;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine8.GenerateTableRef(0x15);
                    }
                    return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine8M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
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
        public Crc5Usb() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc5Usb class.
        /// </summary>
        public Crc5Usb(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc5Usb(t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x05; reverse >>(8-5) = 0x14;
            // init = 0x1F; reverse >>(8-5) = 0x1F;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine8.GenerateTableRef(0x14);
                    }
                    return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine8M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }
}