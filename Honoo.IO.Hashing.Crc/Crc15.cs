namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-15, CRC-15/CAN.
    /// </summary>
    public sealed class Crc15 : Crc
    {
        private const string DEFAULT_NAME = "CRC-15";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x4599;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 15;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _tableM16x;
        private static ushort[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc15 class.
        /// </summary>
        public Crc15(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc15(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc15 Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc15(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt16Value(POLY, WIDTH), new CrcUInt16Value(INIT, WIDTH), new CrcUInt16Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc15(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt16Value(POLY, WIDTH), new CrcUInt16Value(INIT, WIDTH), new CrcUInt16Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc15(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x4599; <<(16-15) = 0x8B32;
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
    /// CRC-15/MPT1327.
    /// </summary>
    public sealed class Crc15Mpt1327 : Crc
    {
        private const string DEFAULT_NAME = "CRC-15/MPT1327";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x6815;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 15;
        private const ushort XOROUT = 0x0001;
        private static ushort[] _tableM16x;
        private static ushort[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc15Mpt1327 class.
        /// </summary>
        public Crc15Mpt1327(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc15Mpt1327 Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc15Mpt1327(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt16Value(POLY, WIDTH), new CrcUInt16Value(INIT, WIDTH), new CrcUInt16Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc15Mpt1327(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x6815; <<(16-15) = 0xD02A;
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