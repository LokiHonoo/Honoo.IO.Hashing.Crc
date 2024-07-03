namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-15, CRC-15/CAN.
    /// </summary>
    public sealed class Crc15 : Crc
    {
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
        public Crc15() : base("CRC-15", GetEngine())
        {
        }

        internal Crc15(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-15", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc15(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc15(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x4599; <<(16-15) = 0x8B32;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x8B32);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-15/MPT1327.
    /// </summary>
    public sealed class Crc15Mpt1327 : Crc
    {
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
        public Crc15Mpt1327() : base("CRC-15/MPT1327", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-15/MPT1327", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc15Mpt1327(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x6815; <<(16-15) = 0xD02A;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0xD02A);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}