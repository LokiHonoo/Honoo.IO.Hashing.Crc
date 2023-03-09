namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-5/EPC, CRC-5/EPC-C1G2.
    /// </summary>
    public sealed class Crc5Epc : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc5Epc class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc5Epc(bool withTable = true) : base(GetEngine("CRC-5/EPC", withTable))
        {
        }

        internal Crc5Epc(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x09; <<(8-5) = 0x48;
            // init = 0x09; <<(8-5) = 0x48;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x48);
                }
                return new CrcEngine8(algorithmName, 5, false, false, _table, 0x48, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 5, false, false, 0x09, 0x09, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-5/ITU, CRC-5/G-704.
    /// </summary>
    public sealed class Crc5Itu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc5Itu class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc5Itu(bool withTable = true) : base(GetEngine("CRC-5/ITU", withTable))
        {
        }

        internal Crc5Itu(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x15; reverse >>(8-5) = 0x15;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x15);
                }
                return new CrcEngine8(algorithmName, 5, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 5, true, true, 0x15, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-5/USB.
    /// </summary>
    public sealed class Crc5Usb : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc5Usb class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc5Usb(bool withTable = true) : base(GetEngine("CRC-5/USB", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x05; reverse >>(8-5) = 0x14;
            // init = 0x1F; reverse >>(8-5) = 0x1F;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x14);
                }
                return new CrcEngine8(algorithmName, 5, true, true, _table, 0x1F, 0x1F);
            }
            else
            {
                return new CrcEngine8(algorithmName, 5, true, true, 0x05, 0x1F, 0x1F);
            }
        }
    }
}