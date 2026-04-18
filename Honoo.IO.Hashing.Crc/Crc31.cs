namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-31/PHILIPS.
    /// </summary>
    public sealed class Crc31Philips : Crc
    {
        private const string DEFAULT_NAME = "CRC-31/PHILIPS";
        private const uint INIT = 0x7FFFFFFF;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 31;
        private const uint XOROUT = 0x7FFFFFFF;

        /// <summary>
        /// Initializes a new instance of the Crc31Philips class.
        /// </summary>
        public Crc31Philips(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc31Philips Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc31Philips(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt32Value(POLY, WIDTH), new CrcUInt32Value(INIT, WIDTH), new CrcUInt32Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc31Philips(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x04C11DB7; <<(32-31) = 0x09823B6E;
            // init = 0x7FFFFFFF; <<(32-31) = 0xFFFFFFFC;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard: return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN));
                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN));
                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}