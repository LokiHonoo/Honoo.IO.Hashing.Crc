namespace Honoo.IO.Hashing
{

    /// <summary>
    /// CRC-15, CRC-15/CAN.
    /// </summary>
    public sealed class Crc15 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc15 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc15(bool withTable = true) : base(GetEngine("CRC-15", withTable))
        {
        }

        internal Crc15(string alias,bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x4599; <<(16-15) = 0x8B32;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x8B32);
                }
                return new CrcEngine16(algorithmName, 15, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 15, false, false, 0x4599, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-15/MPT1327.
    /// </summary>
    public sealed  class Crc15Mpt1327 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc15Mpt1327 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc15Mpt1327(bool withTable = true) : base(GetEngine("CRC-15/MPT1327", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x6815; <<(16-15) = 0xD02A;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0xD02A);
                }
                return new CrcEngine16(algorithmName, 15, false, false, _table, 0x0000, 0x0001);
            }
            else
            {
                return new CrcEngine16(algorithmName, 15, false, false, 0x6815, 0x0000, 0x0001);
            }
        }
    }
}