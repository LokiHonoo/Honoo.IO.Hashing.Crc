namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-7, CRC-7/MMC.
    /// </summary>
    public sealed class Crc7 : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc7 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc7(bool withTable = true) : base(GetEngine("CRC-7", withTable))
        {
        }

        internal Crc7(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x09; <<(8-7) = 0x12;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x12);
                }
                return new CrcEngine8(algorithmName, 7, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 7, false, false, 0x09, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-7/ROHC.
    /// </summary>
    public sealed class Crc7Rohc : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc7Rohc class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc7Rohc(bool withTable = true) : base(GetEngine("CRC-7/ROHC", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x4F; reverse (8-7) = 0x79;
            // init = 0x7F; reverse (8-7) = 0x7F;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x79);
                }
                return new CrcEngine8(algorithmName, 7, true, true, _table, 0x7F, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 7, true, true, 0x4F, 0x7F, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-7/UMTS.
    /// </summary>
    public sealed class Crc7Umts : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc7Umts class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc7Umts(bool withTable = true) : base(GetEngine("CRC-7/UMTS", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x45; <<(8-7) = 0x8A;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x8A);
                }
                return new CrcEngine8(algorithmName, 7, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 7, false, false, 0x45, 0x00, 0x00);
            }
        }
    }
}