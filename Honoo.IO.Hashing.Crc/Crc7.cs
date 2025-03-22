namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-7, CRC-7/MMC.
    /// </summary>
    public sealed class Crc7 : Crc
    {
        private const string DEFAULT_NAME = "CRC-7";
        private const byte INIT = 0x00;
        private const byte POLY = 0x09;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 7;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc7 class.
        /// </summary>
        public Crc7(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc7(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc7 Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc7(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc7(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc7(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x09; <<(8-7) = 0x12;
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
    /// CRC-7/ROHC.
    /// </summary>
    public sealed class Crc7Rohc : Crc
    {
        private const string DEFAULT_NAME = "CRC-7/ROHC";
        private const byte INIT = 0x7F;
        private const byte POLY = 0x4F;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 7;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc7Rohc class.
        /// </summary>
        public Crc7Rohc(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc7Rohc Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc7Rohc(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc7Rohc(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x4F; reverse (8-7) = 0x79;
            // init = 0x7F; reverse (8-7) = 0x7F;
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
    /// CRC-7/UMTS.
    /// </summary>
    public sealed class Crc7Umts : Crc
    {
        private const string DEFAULT_NAME = "CRC-7/UMTS";
        private const byte INIT = 0x00;
        private const byte POLY = 0x45;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 7;
        private const byte XOROUT = 0x00;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc7Umts class.
        /// </summary>
        public Crc7Umts(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc7Umts Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc7Umts(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt8Value(POLY, WIDTH), new CrcUInt8Value(INIT, WIDTH), new CrcUInt8Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc7Umts(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x45; <<(8-7) = 0x8A;
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