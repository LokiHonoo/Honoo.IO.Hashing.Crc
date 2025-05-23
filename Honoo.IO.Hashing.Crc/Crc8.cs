namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-8, CRC-8/SMBUS.
    /// </summary>
    public sealed class Crc8 : Crc
    {
        private const string DEFAULT_NAME = "CRC-8";
        private const byte INIT = 0x00;
        private const byte POLY = 0x07;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8 class.
        /// </summary>
        public Crc8(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc8(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8 Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x07;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/AUTOSAR.
    /// </summary>
    public sealed class Crc8Autosar : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/AUTOSAR";
        private const byte INIT = 0xFF;
        private const byte POLY = 0x2F;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0xFF;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Autosar class.
        /// </summary>
        public Crc8Autosar(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Autosar Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Autosar(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Autosar(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x2F;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/BLUETOOTH.
    /// </summary>
    public sealed class Crc8Bluetooth : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/BLUETOOTH";
        private const byte INIT = 0x00;
        private const byte POLY = 0xA7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Bluetooth class.
        /// </summary>
        public Crc8Bluetooth(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Bluetooth Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Bluetooth(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Bluetooth(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0xA7; reverse = 0xE5;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/CDMA2000.
    /// </summary>
    public sealed class Crc8Cdma2000 : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/CDMA2000";
        private const byte INIT = 0xFF;
        private const byte POLY = 0x9B;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Cdma2000 class.
        /// </summary>
        public Crc8Cdma2000(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Cdma2000 Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Cdma2000(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Cdma2000(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x9B;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/DARC.
    /// </summary>
    public sealed class Crc8Darc : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/DARC";
        private const byte INIT = 0x00;
        private const byte POLY = 0x39;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Darc class.
        /// </summary>
        public Crc8Darc(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Darc Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Darc(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Darc(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x39; reverse = 0x9C;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/DVB-S2.
    /// </summary>
    public sealed class Crc8DvbS2 : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/DVB-S2";
        private const byte INIT = 0x00;
        private const byte POLY = 0xD5;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8DvbS2 class.
        /// </summary>
        public Crc8DvbS2(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8DvbS2 Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8DvbS2(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8DvbS2(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0xD5;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/EBU, CRC-8/TECH-3250, CRC-8/AES.
    /// </summary>
    public sealed class Crc8Ebu : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/EBU";
        private const byte INIT = 0xFF;
        private const byte POLY = 0x1D;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Ebu class.
        /// </summary>
        public Crc8Ebu(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc8Ebu(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Ebu Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Ebu(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Ebu(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Ebu(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x1D; reverse = 0xB8;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/GSM-A.
    /// </summary>
    public sealed class Crc8GsmA : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/GSM-A";
        private const byte INIT = 0x00;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8GsmA class.
        /// </summary>
        public Crc8GsmA(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8GsmA Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8GsmA(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8GsmA(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x1D;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/GSM-B.
    /// </summary>
    public sealed class Crc8GsmB : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/GSM-B";
        private const byte INIT = 0x00;
        private const byte POLY = 0x49;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0xFF;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8GsmB class.
        /// </summary>
        public Crc8GsmB(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8GsmB Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8GsmB(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8GsmB(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x49;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/HITAG.
    /// </summary>
    public sealed class Crc8Hitag : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/HITAG";
        private const byte INIT = 0xFF;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Hitag class.
        /// </summary>
        public Crc8Hitag(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Hitag Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Hitag(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Hitag(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x1D;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/I-CODE.
    /// </summary>
    public sealed class Crc8ICode : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/I-CODE";
        private const byte INIT = 0xFD;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8ICode class.
        /// </summary>
        public Crc8ICode(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8ICode Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8ICode(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8ICode(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x1D;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/ITU. CRC-8/I-432-1, CRC-8/ATM(?).
    /// </summary>
    public sealed class Crc8Itu : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/ITU";
        private const byte INIT = 0x00;
        private const byte POLY = 0x07;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x55;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Itu class.
        /// </summary>
        public Crc8Itu(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc8Itu(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Itu Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Itu(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Itu(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Itu(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x07;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/LTE.
    /// </summary>
    public sealed class Crc8Lte : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/LTE";
        private const byte INIT = 0x00;
        private const byte POLY = 0x9B;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Lte class.
        /// </summary>
        public Crc8Lte(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Lte Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Lte(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Lte(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x9B;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/MAXIM. CRC-8/MAXIM-DOW, DOW-CRC.
    /// </summary>
    public sealed class Crc8Maxim : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/MAXIM";
        private const byte INIT = 0x00;
        private const byte POLY = 0x31;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Maxim class.
        /// </summary>
        public Crc8Maxim(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc8Maxim(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Maxim Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Maxim(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Maxim(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Maxim(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x31; reverse = 0x8C;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/MIFARE-MAD.
    /// </summary>
    public sealed class Crc8MifareMad : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/MIFARE-MAD";
        private const byte INIT = 0xC7;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8MifareMad class.
        /// </summary>
        public Crc8MifareMad(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8MifareMad Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8MifareMad(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8MifareMad(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x1D;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/NRSC-5.
    /// </summary>
    public sealed class Crc8Nrsc5 : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/NRSC-5";
        private const byte INIT = 0xFF;
        private const byte POLY = 0x31;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Nrsc5 class.
        /// </summary>
        public Crc8Nrsc5(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Nrsc5 Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Nrsc5(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Nrsc5(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x31;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/OPENSAFETY.
    /// </summary>
    public sealed class Crc8Opensafety : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/OPENSAFETY";
        private const byte INIT = 0x00;
        private const byte POLY = 0x2F;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Opensafety class.
        /// </summary>
        public Crc8Opensafety(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Opensafety Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Opensafety(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Opensafety(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x2F;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/ROHC.
    /// </summary>
    public sealed class Crc8Rohc : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/ROHC";
        private const byte INIT = 0xFF;
        private const byte POLY = 0x07;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Rohc class.
        /// </summary>
        public Crc8Rohc(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Rohc Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Rohc(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Rohc(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x07; reverse = 0xE0;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/SAE-J1850.
    /// </summary>
    public sealed class Crc8SaeJ1850 : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/SAE-J1850";
        private const byte INIT = 0xFF;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0xFF;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8SaeJ1850 class.
        /// </summary>
        public Crc8SaeJ1850(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8SaeJ1850 Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8SaeJ1850(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8SaeJ1850(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x1D;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-8/WCDMA.
    /// </summary>
    public sealed class Crc8Wcdma : Crc
    {
        private const string DEFAULT_NAME = "CRC-8/WCDMA";
        private const byte INIT = 0x00;
        private const byte POLY = 0x9B;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc8Wcdma class.
        /// </summary>
        public Crc8Wcdma(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc8Wcdma Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc8Wcdma(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc8Wcdma(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x9B; reverse = 0xD9;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}