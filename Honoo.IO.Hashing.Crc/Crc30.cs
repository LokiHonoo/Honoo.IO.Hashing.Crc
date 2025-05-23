namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-30/CDMA.
    /// </summary>
    public sealed class Crc30Cdma : Crc
    {
        private const string DEFAULT_NAME = "CRC-30/CDMA";
        private const uint INIT = 0x3FFFFFFF;
        private const uint POLY = 0x2030B9C7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 30;
        private const uint XOROUT = 0x3FFFFFFF;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc30Cdma class.
        /// </summary>
        public Crc30Cdma(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc30Cdma Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc30Cdma(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt32Value(POLY, WIDTH), new CrcUInt32Value(INIT, WIDTH), new CrcUInt32Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc30Cdma(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x2030B9C7; <<(32-30) = 0x80C2E71C;
            // init = 0x3FFFFFFF; <<(32-30) = 0xFFFFFFFC;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}