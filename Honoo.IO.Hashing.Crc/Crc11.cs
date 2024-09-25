namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-11, CRC-11/FLEXRAY.
    /// </summary>
    public sealed class Crc11 : Crc
    {
        private const string DEFAULT_NAME = "CRC-11";
        private const ushort INIT = 0x01A;
        private const ushort POLY = 0x385;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 11;
        private const ushort XOROUT = 0x000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc11 class.
        /// </summary>
        public Crc11() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc11 class.
        /// </summary>
        public Crc11(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc11(string alias, CrcTable withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc11(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc11(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x385; <<(16-11) = 0x70A0;
            // init = 0x01A; <<(16-11) = 0x0340;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine16.GenerateTable(0x70A0);
                    }
                    return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine16M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-11/UMTS.
    /// </summary>
    public sealed class Crc11Umts : Crc
    {
        private const string DEFAULT_NAME = "CRC-11/UMTS";
        private const ushort INIT = 0x000;
        private const ushort POLY = 0x307;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 11;
        private const ushort XOROUT = 0x000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc11Umts class.
        /// </summary>
        public Crc11Umts() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc11Umts class.
        /// </summary>
        public Crc11Umts(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc11Umts(t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x307; <<(16-11) = 0x60E0;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine16.GenerateTable(0x60E0);
                    }
                    return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine16M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }
}