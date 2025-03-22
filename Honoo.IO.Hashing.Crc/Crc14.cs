namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-14/DARC.
    /// </summary>
    public sealed class Crc14Darc : Crc
    {
        private const string DEFAULT_NAME = "CRC-14/DARC";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x0805;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 14;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _tableM16x;
        private static ushort[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc14Darc class.
        /// </summary>
        public Crc14Darc(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc14Darc Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc14Darc(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt16Value(POLY, WIDTH), new CrcUInt16Value(INIT, WIDTH), new CrcUInt16Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc14Darc(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x0805; reverse >>(16-14) = 0x2804;
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
    /// CRC-14/GSM.
    /// </summary>
    public sealed class Crc14Gsm : Crc
    {
        private const string DEFAULT_NAME = "CRC-14/GSM";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x202D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 14;
        private const ushort XOROUT = 0x3FFF;
        private static ushort[] _tableM16x;
        private static ushort[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc14Gsm class.
        /// </summary>
        public Crc14Gsm(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc14Gsm Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc14Gsm(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt16Value(POLY, WIDTH), new CrcUInt16Value(INIT, WIDTH), new CrcUInt16Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc14Gsm(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x202D; <<(16-14) = 0x80B4;
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