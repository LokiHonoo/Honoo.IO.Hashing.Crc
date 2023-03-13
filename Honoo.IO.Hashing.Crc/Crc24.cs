namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-24.
    /// </summary>
    public sealed class Crc24 : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc24(bool withTable = true) : base(GetEngine("CRC-24", withTable))
        {
        }

        internal Crc24(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x864CFB; <<(32-24) = 0x864CFB00;
            // init = 0xB704CE; <<(32-24) = 0xB704CE00;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x864CFB00);
                }
                return new CrcEngine32(algorithmName, 24, false, false, 0x864CFB, 0xB704CE, 0x000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 24, false, false, 0x864CFB, 0xB704CE, 0x000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-24/BLE.
    /// </summary>
    public sealed class Crc24Ble : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24Ble class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc24Ble(bool withTable = true) : base(GetEngine("CRC-24/BLE", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x00065B; reverse >>(32-24) = 0xDA6000;
            // init = 0x555555; reverse >>(32-24) = 0xAAAAAA;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xDA6000);
                }
                return new CrcEngine32(algorithmName, 24, true, true, 0x00065B, 0x555555, 0x000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 24, true, true, 0x00065B, 0x555555, 0x000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-24/FLEXRAY-A.
    /// </summary>
    public sealed class Crc24FlexrayA : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24FlexrayA class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc24FlexrayA(bool withTable = true) : base(GetEngine("CRC-24/FLEXRAY-A", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x5D6DCB;  <<(32-24) = 0x5D6DCB00;
            // init = 0xFEDCBA;  <<(32-24) = 0xFEDCBA00;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x5D6DCB00);
                }
                return new CrcEngine32(algorithmName, 24, false, false, 0x5D6DCB, 0xFEDCBA, 0x000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 24, false, false, 0x5D6DCB, 0xFEDCBA, 0x000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-24/FLEXRAY-B.
    /// </summary>
    public sealed class Crc24FlexrayB : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24FlexrayB class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc24FlexrayB(bool withTable = true) : base(GetEngine("CRC-24/FLEXRAY-B", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x5D6DCB;  <<(32-24) = 0x5D6DCB00;
            // init = 0xABCDEF;  <<(32-24) = 0xABCDEF00;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x5D6DCB00);
                }
                return new CrcEngine32(algorithmName, 24, false, false, 0x5D6DCB, 0xABCDEF, 0x000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 24, false, false, 0x5D6DCB, 0xABCDEF, 0x000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-24/INTERLAKEN.
    /// </summary>
    public sealed class Crc24Interlaken : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24Interlaken class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc24Interlaken(bool withTable = true) : base(GetEngine("CRC-24/INTERLAKEN", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x328B63;  <<(32-24) = 0x328B6300;
            // init = 0xFFFFFF;  <<(32-24) = 0xFFFFFF00;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x328B6300);
                }
                return new CrcEngine32(algorithmName, 24, false, false, 0x328B63, 0xFFFFFF, 0xFFFFFF, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 24, false, false, 0x328B63, 0xFFFFFF, 0xFFFFFF, false);
            }
        }
    }

    /// <summary>
    /// CRC-24/LTE-A.
    /// </summary>
    public sealed class Crc24LteA : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24LteA class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc24LteA(bool withTable = true) : base(GetEngine("CRC-24/LTE-A", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x864CFB; <<(32-24) = 0x864CFB00;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x864CFB00);
                }
                return new CrcEngine32(algorithmName, 24, false, false, 0x864CFB, 0x000000, 0x000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 24, false, false, 0x864CFB, 0x000000, 0x000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-24/LTE-B.
    /// </summary>
    public sealed class Crc24LteB : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24LteB class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc24LteB(bool withTable = true) : base(GetEngine("CRC-24/LTE-B", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x800063; <<(32-24) = 0x80006300;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x80006300);
                }
                return new CrcEngine32(algorithmName, 24, false, false, 0x800063, 0x000000, 0x000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 24, false, false, 0x800063, 0x000000, 0x000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-24/OS-9.
    /// </summary>
    public sealed class Crc24Os9 : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24Os9 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc24Os9(bool withTable = true) : base(GetEngine("CRC-24/OS-9", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x800063; <<(32-24) = 0x80006300;
            // init = 0xFFFFFF; <<(32-24) = 0xFFFFFF00;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x80006300);
                }
                return new CrcEngine32(algorithmName, 24, false, false, 0x800063, 0xFFFFFF, 0xFFFFFF, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 24, false, false, 0x800063, 0xFFFFFF, 0xFFFFFF, false);
            }
        }
    }
}