namespace Honoo.IO.Hashing
{


    /// <summary>
    /// CRC-14/DARC.
    /// </summary>
    public sealed class Crc14Darc : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc14Darc class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc14Darc(bool withTable = true) : base(GetEngine("CRC-14/DARC", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x0805; reverse >>(16-14) = 0x2804;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x2804);
                }
                return new CrcEngine16(algorithmName, 14, true, true, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 14, true, true, 0x0805, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-14/GSM.
    /// </summary>
    public sealed class Crc14Gsm : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc14Gsm class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc14Gsm(bool withTable = true) : base(GetEngine("CRC-14/GSM", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x202D; <<(16-14) = 0x80B4;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x80B4);
                }
                return new CrcEngine16(algorithmName, 14, false, false, _table, 0x0000, 0x3FFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 14, false, false, 0x202D, 0x0000, 0x3FFF);
            }
        }
    }
}