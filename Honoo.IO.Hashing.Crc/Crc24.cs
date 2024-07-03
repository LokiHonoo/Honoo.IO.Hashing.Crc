namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-24.
    /// </summary>
    public sealed class Crc24 : Crc
    {
        private const uint INIT = 0xB704CE;
        private const uint POLY = 0x864CFB;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 24;
        private const uint XOROUT = 0x000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24 class.
        /// </summary>
        public Crc24() : base("CRC-24", GetEngine())
        {
        }

        internal Crc24(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-24", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc24(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc24(alias); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x864CFB; <<(32-24) = 0x864CFB00;
            // init = 0xB704CE; <<(32-24) = 0xB704CE00;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x864CFB00);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-24/BLE.
    /// </summary>
    public sealed class Crc24Ble : Crc
    {
        private const uint INIT = 0x555555;
        private const uint POLY = 0x00065B;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 24;
        private const uint XOROUT = 0x000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24Ble class.
        /// </summary>
        public Crc24Ble() : base("CRC-24/BLE", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-24/BLE", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc24Ble(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x00065B; reverse >>(32-24) = 0xDA6000;
            // init = 0x555555; reverse >>(32-24) = 0xAAAAAA;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateReversedTable(0xDA6000);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-24/FLEXRAY-A.
    /// </summary>
    public sealed class Crc24FlexrayA : Crc
    {
        private const uint INIT = 0xFEDCBA;
        private const uint POLY = 0x5D6DCB;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 24;
        private const uint XOROUT = 0x000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24FlexrayA class.
        /// </summary>
        public Crc24FlexrayA() : base("CRC-24/FLEXRAY-A", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-24/FLEXRAY-A", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc24FlexrayA(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x5D6DCB;  <<(32-24) = 0x5D6DCB00;
            // init = 0xFEDCBA;  <<(32-24) = 0xFEDCBA00;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x5D6DCB00);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-24/FLEXRAY-B.
    /// </summary>
    public sealed class Crc24FlexrayB : Crc
    {
        private const uint INIT = 0xABCDEF;
        private const uint POLY = 0x5D6DCB;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 24;
        private const uint XOROUT = 0x000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24FlexrayB class.
        /// </summary>
        public Crc24FlexrayB() : base("CRC-24/FLEXRAY-B", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-24/FLEXRAY-B", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc24FlexrayB(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x5D6DCB;  <<(32-24) = 0x5D6DCB00;
            // init = 0xABCDEF;  <<(32-24) = 0xABCDEF00;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x5D6DCB00);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-24/INTERLAKEN.
    /// </summary>
    public sealed class Crc24Interlaken : Crc
    {
        private const uint INIT = 0xFFFFFF;
        private const uint POLY = 0x328B63;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 24;
        private const uint XOROUT = 0xFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24Interlaken class.
        /// </summary>
        public Crc24Interlaken() : base("CRC-24/INTERLAKEN", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-24/INTERLAKEN", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc24Interlaken(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x328B63;  <<(32-24) = 0x328B6300;
            // init = 0xFFFFFF;  <<(32-24) = 0xFFFFFF00;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x328B6300);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-24/LTE-A.
    /// </summary>
    public sealed class Crc24LteA : Crc
    {
        private const uint INIT = 0x000000;
        private const uint POLY = 0x864CFB;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 24;
        private const uint XOROUT = 0x000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24LteA class.
        /// </summary>
        public Crc24LteA() : base("CRC-24/LTE-A", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-24/LTE-A", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc24LteA(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x864CFB; <<(32-24) = 0x864CFB00;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x864CFB00);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-24/LTE-B.
    /// </summary>
    public sealed class Crc24LteB : Crc
    {
        private const uint INIT = 0x000000;
        private const uint POLY = 0x800063;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 24;
        private const uint XOROUT = 0x000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24LteB class.
        /// </summary>
        public Crc24LteB() : base("CRC-24/LTE-B", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-24/LTE-B", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc24LteB(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x800063; <<(32-24) = 0x80006300;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x80006300);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-24/OS-9.
    /// </summary>
    public sealed class Crc24Os9 : Crc
    {
        private const uint INIT = 0xFFFFFF;
        private const uint POLY = 0x800063;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 24;
        private const uint XOROUT = 0xFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc24Os9 class.
        /// </summary>
        public Crc24Os9() : base("CRC-24/OS-9", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-24/OS-9", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc24Os9(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x800063; <<(32-24) = 0x80006300;
            // init = 0xFFFFFF; <<(32-24) = 0xFFFFFF00;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x80006300);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}