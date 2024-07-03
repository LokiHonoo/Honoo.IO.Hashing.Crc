namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-11, CRC-11/FLEXRAY.
    /// </summary>
    public sealed class Crc11 : Crc
    {
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
        public Crc11() : base("CRC-11", GetEngine())
        {
        }

        internal Crc11(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-11", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc11(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc11(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x385; <<(16-11) = 0x70A0;
            // init = 0x01A; <<(16-11) = 0x0340;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x70A0);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-11/UMTS.
    /// </summary>
    public sealed class Crc11Umts : Crc
    {
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
        public Crc11Umts() : base("CRC-11/UMTS", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-11/UMTS", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc11Umts(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x307; <<(16-11) = 0x60E0;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x60E0);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}