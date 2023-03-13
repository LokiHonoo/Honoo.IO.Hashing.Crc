namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-3/ROHC.
    /// </summary>
    public sealed class Crc3Rohc : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc3Rohc class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc3Rohc(bool withTable = true) : base(GetEngine("CRC-3/ROHC", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x3; reverse >>(8-3) = 0x6;
            // init = 0x7; reverse >>(8-3) = 0x7;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x6);
                }
                return new CrcEngine8(algorithmName, 3, true, true, 0x3, 0x7, 0x0, _table);
            }
            else
            {
                return new CrcEngine8(algorithmName, 3, true, true, 0x3, 0x7, 0x0, false);
            }
        }
    }

    /// <summary>
    /// CRC-3/GSM.
    /// </summary>
    public sealed class Crc3Gsm : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc3Gsm class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc3Gsm(bool withTable = true) : base(GetEngine("CRC-3/GSM", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x3; <<(8-3) = 0x60;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x60);
                }
                return new CrcEngine8(algorithmName, 3, false, false, 0x3, 0x0, 0x7, _table);
            }
            else
            {
                return new CrcEngine8(algorithmName, 3, false, false, 0x3, 0x0, 0x7, false);
            }
        }
    }
}