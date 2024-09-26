namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-24.
    /// </summary>
    public sealed class Crc24 : Crc
    {
        private const string DEFAULT_NAME = "CRC-24";
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
        public Crc24() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc24 class.
        /// </summary>
        public Crc24(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc24(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc24(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc24(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x864CFB; <<(32-24) = 0x864CFB00;
            // init = 0xB704CE; <<(32-24) = 0xB704CE00;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable(0x864CFB00);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-24/BLE.
    /// </summary>
    public sealed class Crc24Ble : Crc
    {
        private const string DEFAULT_NAME = "CRC-24/BLE";
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
        public Crc24Ble() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc24Ble class.
        /// </summary>
        public Crc24Ble(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc24Ble(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x00065B; reverse >>(32-24) = 0xDA6000;
            // init = 0x555555; reverse >>(32-24) = 0xAAAAAA;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTableRef(0xDA6000);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-24/FLEXRAY-A.
    /// </summary>
    public sealed class Crc24FlexrayA : Crc
    {
        private const string DEFAULT_NAME = "CRC-24/FLEXRAY-A";
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
        public Crc24FlexrayA() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc24FlexrayA class.
        /// </summary>
        public Crc24FlexrayA(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc24FlexrayA(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x5D6DCB;  <<(32-24) = 0x5D6DCB00;
            // init = 0xFEDCBA;  <<(32-24) = 0xFEDCBA00;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable(0x5D6DCB00);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-24/FLEXRAY-B.
    /// </summary>
    public sealed class Crc24FlexrayB : Crc
    {
        private const string DEFAULT_NAME = "CRC-24/FLEXRAY-B";
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
        public Crc24FlexrayB() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc24FlexrayB class.
        /// </summary>
        public Crc24FlexrayB(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc24FlexrayB(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x5D6DCB;  <<(32-24) = 0x5D6DCB00;
            // init = 0xABCDEF;  <<(32-24) = 0xABCDEF00;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable(0x5D6DCB00);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-24/INTERLAKEN.
    /// </summary>
    public sealed class Crc24Interlaken : Crc
    {
        private const string DEFAULT_NAME = "CRC-24/INTERLAKEN";
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
        public Crc24Interlaken() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc24Interlaken class.
        /// </summary>
        public Crc24Interlaken(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc24Interlaken(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x328B63;  <<(32-24) = 0x328B6300;
            // init = 0xFFFFFF;  <<(32-24) = 0xFFFFFF00;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable(0x328B6300);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-24/LTE-A.
    /// </summary>
    public sealed class Crc24LteA : Crc
    {
        private const string DEFAULT_NAME = "CRC-24/LTE-A";
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
        public Crc24LteA() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc24LteA class.
        /// </summary>
        public Crc24LteA(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc24LteA(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x864CFB; <<(32-24) = 0x864CFB00;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable(0x864CFB00);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-24/LTE-B.
    /// </summary>
    public sealed class Crc24LteB : Crc
    {
        private const string DEFAULT_NAME = "CRC-24/LTE-B";
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
        public Crc24LteB() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc24LteB class.
        /// </summary>
        public Crc24LteB(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc24LteB(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x800063; <<(32-24) = 0x80006300;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable(0x80006300);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-24/OS-9.
    /// </summary>
    public sealed class Crc24Os9 : Crc
    {
        private const string DEFAULT_NAME = "CRC-24/OS-9";
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
        public Crc24Os9() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc24Os9 class.
        /// </summary>
        public Crc24Os9(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc24Os9(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x800063; <<(32-24) = 0x80006300;
            // init = 0xFFFFFF; <<(32-24) = 0xFFFFFF00;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable(0x80006300);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }
}