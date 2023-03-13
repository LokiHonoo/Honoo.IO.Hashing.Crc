namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-11, CRC-11/FLEXRAY.
    /// </summary>
    public sealed class Crc11 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc11 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc11(bool withTable = true) : base(GetEngine("CRC-11", withTable))
        {
        }

        internal Crc11(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x385; <<(16-11) = 0x70A0;
            // init = 0x01A; <<(16-11) = 0x0340;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x70A0);
                }
                return new CrcEngine16(algorithmName, 11, false, false, 0x385, 0x01A, 0x000, _table);
            }
            else
            {
                return new CrcEngine16(algorithmName, 11, false, false, 0x385, 0x01A, 0x000, false);
            }
        }
    }

    /// <summary>
    /// CRC-11/UMTS.
    /// </summary>
    public sealed class Crc11Umts : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc11Umts class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc11Umts(bool withTable = true) : base(GetEngine("CRC-11/UMTS", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x307; <<(16-11) = 0x60E0;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x60E0);
                }
                return new CrcEngine16(algorithmName, 11, false, false, 0x307, 0x000, 0x000, _table);
            }
            else
            {
                return new CrcEngine16(algorithmName, 11, false, false, 0x307, 0x000, 0x000, false);
            }
        }
    }
}