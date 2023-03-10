namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-4/INTERLAKEN.
    /// </summary>
    public sealed class Crc4Interlaken : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc4Interlaken class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc4Interlaken(bool withTable = true) : base(GetEngine("CRC-4/INTERLAKEN", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x3; <<(8-4) = 0x30;
            // init = 0x0; <<(8-4) = 0xF0;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x30);
                }
                return new CrcEngine8(algorithmName, 4, false, false, _table, 0xF0, 0xF);
            }
            else
            {
                return new CrcEngine8(algorithmName, 4, false, false, 0x3, 0xF, 0xF);
            }
        }
    }

    /// <summary>
    /// CRC-4/ITU, CRC-4/G-704.
    /// </summary>
    public sealed class Crc4Itu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc4Itu class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc4Itu(bool withTable = true) : base(GetEngine("CRC-4/ITU", withTable))
        {
        }

        internal Crc4Itu(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x3; reverse >>(8-4) = 0xC;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0xC);
                }
                return new CrcEngine8(algorithmName, 4, true, true, _table, 0x0, 0x0);
            }
            else
            {
                return new CrcEngine8(algorithmName, 4, true, true, 0x3, 0x0, 0x0);
            }
        }
    }
}