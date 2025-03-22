namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-11, CRC-11/FLEXRAY.
    /// </summary>
    public sealed class Crc11 : Crc
    {
        private const string DEFAULT_NAME = "CRC-11";
        private const ushort INIT = 0x01A;
        private const ushort POLY = 0x385;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 11;
        private const ushort XOROUT = 0x000;
        private static ushort[] _tableM16x;
        private static ushort[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc11 class.
        /// </summary>
        public Crc11(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc11(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc11 Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc11(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt16Value(POLY, WIDTH), new CrcUInt16Value(INIT, WIDTH), new CrcUInt16Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc11(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt16Value(POLY, WIDTH), new CrcUInt16Value(INIT, WIDTH), new CrcUInt16Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc11(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x385; <<(16-11) = 0x70A0;
            // init = 0x01A; <<(16-11) = 0x0340;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine16Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine16Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine16M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine16M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine16(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-11/UMTS.
    /// </summary>
    public sealed class Crc11Umts : Crc
    {
        private const string DEFAULT_NAME = "CRC-11/UMTS";
        private const ushort INIT = 0x000;
        private const ushort POLY = 0x307;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 11;
        private const ushort XOROUT = 0x000;
        private static ushort[] _tableM16x;
        private static ushort[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc11Umts class.
        /// </summary>
        public Crc11Umts(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc11Umts Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc11Umts(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt16Value(POLY, WIDTH), new CrcUInt16Value(INIT, WIDTH), new CrcUInt16Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc11Umts(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x307; <<(16-11) = 0x60E0;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine16Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine16Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine16M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine16M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine16(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}