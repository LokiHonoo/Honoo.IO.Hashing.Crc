namespace Honoo.IO.HashingOld
{
    /// <summary>
    /// CRC-5/EPC.
    /// </summary>
    public sealed class Crc5Epc : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc5Epc class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc5Epc(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x09; <<(8-5) = 0x48;
            // init = 0x09; <<(8-5) = 0x48;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x48);
                }
                return new CrcEngine8("CRC-5/EPC", 5, false, false, _table, 0x48, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-5/EPC", 5, false, false, 0x09, 0x09, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-5/ITU.
    /// </summary>
    public sealed class Crc5Itu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc5Itu class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc5Itu(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x15; reverse >>(8-5) = 0x15;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x15);
                }
                return new CrcEngine8("CRC-5/ITU", 5, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-5/ITU", 5, true, true, 0x15, 0x00, 0x00);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc5Usb(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x05; reverse >>(8-5) = 0x14;
            // init = 0x1F; reverse >>(8-5) = 0x1F;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x14);
                }
                return new CrcEngine8("CRC-5/USB", 5, true, true, _table, 0x1F, 0x1F);
            }
            else
            {
                return new CrcEngine8("CRC-5/USB", 5, true, true, 0x05, 0x1F, 0x1F);
            }
        }
    }
}