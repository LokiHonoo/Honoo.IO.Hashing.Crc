namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-12/CDMA2000.
    /// </summary>
    public sealed class Crc12Cdma2000 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Cdma2000 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc12Cdma2000(bool withTable = true) : base(GetEngine("CRC-12/CDMA2000", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0xF13; <<(16-12) = 0xF130;
            // init = 0xFFF; <<(16-12) = 0xFFF0;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0xF130);
                }
                return new CrcEngine16(algorithmName, 12, false, false, _table, 0xFFF0, 0x000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 12, false, false, 0xF13, 0xFFF, 0x000);
            }
        }
    }

    /// <summary>
    /// CRC-12/DECT.
    /// </summary>
    public sealed class Crc12Dect : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Dect class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc12Dect(bool withTable = true) : base(GetEngine("CRC-12/DECT", withTable))
        {
        }

        internal Crc12Dect(string alias,bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x80F; <<(16-12) = 0x80F0;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x80F0);
                }
                return new CrcEngine16(algorithmName, 12, false, false, _table, 0x000, 0x000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 12, false, false, 0x80F, 0x000, 0x000);
            }
        }
    }

    /// <summary>
    /// CRC-12/GSM.
    /// </summary>
    public sealed class Crc12Gsm : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Gsm class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc12Gsm(bool withTable = true) : base(GetEngine("CRC-12/GSM", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0xD31; <<(16-12) = 0xD310;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0xD310);
                }
                return new CrcEngine16(algorithmName, 12, false, false, _table, 0x000, 0xFFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 12, false, false, 0xD31, 0x000, 0xFFF);
            }
        }
    }

    /// <summary>
    /// CRC-12/UMTS, CRC-12/3GPP.
    /// </summary>
    public sealed class Crc12Umts : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Umts class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc12Umts(bool withTable = true) : base(GetEngine("CRC-12/UMTS", withTable))
        {
        }

        internal Crc12Umts(string alias,bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x80F; <<(16-12) = 0x80F0;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x80F0);
                }
                return new CrcEngine16(algorithmName, 12, false, true, _table, 0x000, 0x000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 12, false, true, 0x80F, 0x000, 0x000);
            }
        }
    }
}