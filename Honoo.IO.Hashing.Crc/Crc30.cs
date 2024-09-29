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
        public Crc30Cdma() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc30Cdma class.
        /// </summary>
        public Crc30Cdma(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc30Cdma(t); });
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
                        _tableStandard = CrcEngine32Standard.GenerateTable(0x80C2E71C);
                    }
                    return new CrcEngine32Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(0x80C2E71C);
                    }
                    return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);
            }
        }
    }
}