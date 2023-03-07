namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-10, CRC-10/ATM, CRC-10/I-610.
    /// </summary>
    public sealed class Crc10 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc10 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc10(bool withTable = true) : base(GetEngine("CRC-10", withTable))
        {
        }

        internal Crc10(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x233; <<(16-10) = 0x8CC0;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x8CC0);
                }
                return new CrcEngine16(algorithmName, 10, false, false, _table, 0x000, 0x000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 10, false, false, 0x233, 0x000, 0x000);
            }
        }
    }

    /// <summary>
    /// CRC-10/CDMA2000.
    /// </summary>
    public sealed class Crc10Cdma2000 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc10Cdma2000 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc10Cdma2000(bool withTable = true) : base(GetEngine("CRC-10/CDMA2000", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x3D9; <<(16-10) = 0xF640;
            // init = 0x3FF; <<(16-10) = 0xFFC0;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0xF640);
                }
                return new CrcEngine16(algorithmName, 10, false, false, _table, 0xFFC0, 0x000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 10, false, false, 0x3D9, 0x3FF, 0x000);
            }
        }
    }

    /// <summary>
    /// CRC-10/GSM.
    /// </summary>
    public sealed class Crc10Gsm : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc10Gsm class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc10Gsm(bool withTable = true) : base(GetEngine("CRC-10/GSM", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x175; <<(16-10) = 0x5D40;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x5D40);
                }
                return new CrcEngine16(algorithmName, 10, false, false, _table, 0x000, 0x3FF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 10, false, false, 0x175, 0x000, 0x3FF);
            }
        }
    }
}