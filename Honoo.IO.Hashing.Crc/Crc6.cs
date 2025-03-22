namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-6/CDMA2000-A.
    /// </summary>
    public sealed class Crc6Cdma2000A : Crc
    {
        private const string DEFAULT_NAME = "CRC-6/CDMA2000-A";
        private const byte INIT = 0x3F;
        private const byte POLY = 0x27;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc6Cdma2000A class.
        /// </summary>
        public Crc6Cdma2000A(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc6Cdma2000A Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc6Cdma2000A(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc6Cdma2000A(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x27; <<(8-6) = 0x9C;
            // init = 0x3F; <<(8-6) = 0xFC;
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
    /// CRC-6/CDMA2000-B.
    /// </summary>
    public sealed class Crc6Cdma2000B : Crc
    {
        private const string DEFAULT_NAME = "CRC-6/CDMA2000-B";
        private const byte INIT = 0x3F;
        private const byte POLY = 0x07;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc6Cdma2000B class.
        /// </summary>
        public Crc6Cdma2000B(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc6Cdma2000B Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc6Cdma2000B(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc6Cdma2000B(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x07; <<(8-6) = 0x1C;
            // init = 0x3F; <<(8-6) = 0xFC;
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
    /// CRC-6/DARC.
    /// </summary>
    public sealed class Crc6Darc : Crc
    {
        private const string DEFAULT_NAME = "CRC-6/DARC";
        private const byte INIT = 0x00;
        private const byte POLY = 0x19;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc6Darc class.
        /// </summary>
        public Crc6Darc(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc6Darc Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc6Darc(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc6Darc(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x19; reverse >>(8-6) = 0x26;
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
    /// CRC-6/GSM.
    /// </summary>
    public sealed class Crc6Gsm : Crc
    {
        private const string DEFAULT_NAME = "CRC-6/GSM";
        private const byte INIT = 0x00;
        private const byte POLY = 0x2F;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x3F;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc6Gsm class.
        /// </summary>
        public Crc6Gsm(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc6Gsm Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc6Gsm(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc6Gsm(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x2F; <<(8-6) = 0xBC;
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
    /// CRC-6/ITU, CRC-6/G-704.
    /// </summary>
    public sealed class Crc6Itu : Crc
    {
        private const string DEFAULT_NAME = "CRC-6/ITU";
        private const byte INIT = 0x00;
        private const byte POLY = 0x03;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc6Itu class.
        /// </summary>
        public Crc6Itu(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc6Itu(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc6Itu Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc6Itu(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc6Itu(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc6Itu(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x03; reverse >>(8-6) = 0x30;
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