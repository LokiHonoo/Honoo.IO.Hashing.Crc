namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-4/INTERLAKEN.
    /// </summary>
    public sealed class Crc4Interlaken : Crc
    {
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
        public Crc4Interlaken() : base("CRC-4/INTERLAKEN", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-4/INTERLAKEN", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc4Interlaken(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x3; <<(8-4) = 0x30;
            // init = 0x0; <<(8-4) = 0xF0;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x30);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-4/ITU, CRC-4/G-704.
    /// </summary>
    public sealed class Crc4Itu : Crc
    {
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
        public Crc4Itu() : base("CRC-4/ITU", GetEngine())
        {
        }

        internal Crc4Itu(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-4/ITU", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc4Itu(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc4Itu(alias); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x3; reverse >>(8-4) = 0xC;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0xC);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}