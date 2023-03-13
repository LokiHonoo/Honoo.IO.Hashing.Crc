namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-6/CDMA2000-A.
    /// </summary>
    public sealed class Crc6Cdma2000A : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Cdma2000A class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc6Cdma2000A(bool withTable = true) : base(GetEngine("CRC-6/CDMA2000-A", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x27; <<(8-6) = 0x9C;
            // init = 0x3F; <<(8-6) = 0xFC;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x9C);
                }
                return new CrcEngine8(algorithmName, 6, false, false, 0x27, 0x3F, 0x00, _table);
            }
            else
            {
                return new CrcEngine8(algorithmName, 6, false, false, 0x27, 0x3F, 0x00, false);
            }
        }
    }

    /// <summary>
    /// CRC-6/CDMA2000-B.
    /// </summary>
    public sealed class Crc6Cdma2000B : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Cdma2000B class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc6Cdma2000B(bool withTable = true) : base(GetEngine("CRC-6/CDMA2000-B", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x07; <<(8-6) = 0x1C;
            // init = 0x3F; <<(8-6) = 0xFC;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x1C);
                }
                return new CrcEngine8(algorithmName, 6, false, false, 0x07, 0x3F, 0x00, _table);
            }
            else
            {
                return new CrcEngine8(algorithmName, 6, false, false, 0x07, 0x3F, 0x00, false);
            }
        }
    }

    /// <summary>
    /// CRC-6/DARC.
    /// </summary>
    public sealed class Crc6Darc : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Darc class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc6Darc(bool withTable = true) : base(GetEngine("CRC-6/DARC", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x19; reverse >>(8-6) = 0x26;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x26);
                }
                return new CrcEngine8(algorithmName, 6, true, true, 0x19, 0x00, 0x00, _table);
            }
            else
            {
                return new CrcEngine8(algorithmName, 6, true, true, 0x19, 0x00, 0x00, false);
            }
        }
    }

    /// <summary>
    /// CRC-6/GSM.
    /// </summary>
    public sealed class Crc6Gsm : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Gsm class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc6Gsm(bool withTable = true) : base(GetEngine("CRC-6/GSM", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x2F; <<(8-6) = 0xBC;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0xBC);
                }
                return new CrcEngine8(algorithmName, 6, false, false, 0x2F, 0x00, 0x3F, _table);
            }
            else
            {
                return new CrcEngine8(algorithmName, 6, false, false, 0x2F, 0x00, 0x3F, false);
            }
        }
    }

    /// <summary>
    /// CRC-6/ITU, CRC-6/G-704.
    /// </summary>
    public sealed class Crc6Itu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Itu class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc6Itu(bool withTable = true) : base(GetEngine("CRC-6/ITU", withTable))
        {
        }

        internal Crc6Itu(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x03; reverse >>(8-6) = 0x30;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x30);
                }
                return new CrcEngine8(algorithmName, 6, true, true, 0x03, 0x00, 0x00, _table);
            }
            else
            {
                return new CrcEngine8(algorithmName, 6, true, true, 0x03, 0x00, 0x00, false);
            }
        }
    }
}