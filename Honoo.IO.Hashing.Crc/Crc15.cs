namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-15, CRC-15/CAN.
    /// </summary>
    public sealed class Crc15 : Crc
    {
        private const string DEFAULT_NAME = "CRC-15";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x4599;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 15;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc15 class.
        /// </summary>
        public Crc15() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc15 class.
        /// </summary>
        public Crc15(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc15(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc15(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc15(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x4599; <<(16-15) = 0x8B32;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine16.GenerateTable(0x8B32);
                    }
                    return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine16M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTableInfo.None: default: return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-15/MPT1327.
    /// </summary>
    public sealed class Crc15Mpt1327 : Crc
    {
        private const string DEFAULT_NAME = "CRC-15/MPT1327";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x6815;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 15;
        private const ushort XOROUT = 0x0001;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc15Mpt1327 class.
        /// </summary>
        public Crc15Mpt1327() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc15Mpt1327 class.
        /// </summary>
        public Crc15Mpt1327(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc15Mpt1327(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x6815; <<(16-15) = 0xD02A;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine16.GenerateTable(0xD02A);
                    }
                    return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine16M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTableInfo.None: default: return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }
}