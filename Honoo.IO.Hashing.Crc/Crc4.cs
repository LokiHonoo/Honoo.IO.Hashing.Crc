namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-4/INTERLAKEN.
    /// </summary>
    public sealed class Crc4Interlaken : Crc
    {
        private const string DEFAULT_NAME = "CRC-4/INTERLAKEN";
        private const byte INIT = 0xF;
        private const byte POLY = 0x3;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 4;
        private const byte XOROUT = 0xF;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc4Interlaken class.
        /// </summary>
        public Crc4Interlaken() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc4Interlaken class.
        /// </summary>
        public Crc4Interlaken(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc4Interlaken(t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x3; <<(8-4) = 0x30;
            // init = 0x0; <<(8-4) = 0xF0;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine8.GenerateTable(0x30);
                    }
                    return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine8M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-4/ITU, CRC-4/G-704.
    /// </summary>
    public sealed class Crc4Itu : Crc
    {
        private const string DEFAULT_NAME = "CRC-4/ITU";
        private const byte INIT = 0x0;
        private const byte POLY = 0x3;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 4;
        private const byte XOROUT = 0x0;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc4Itu class.
        /// </summary>
        public Crc4Itu() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc4Itu class.
        /// </summary>
        public Crc4Itu(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc4Itu(string alias, CrcTable withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc4Itu(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc4Itu(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x3; reverse >>(8-4) = 0xC;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine8.GenerateTableRef(0xC);
                    }
                    return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine8M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }
}