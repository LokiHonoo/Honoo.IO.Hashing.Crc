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
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc31Philips class.
        /// </summary>
        public Crc31Philips() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc31Philips class.
        /// </summary>
        public Crc31Philips(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc31Philips(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x04C11DB7; <<(32-31) = 0x09823B6E;
            // init = 0x7FFFFFFF; <<(32-31) = 0xFFFFFFFC;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(0x09823B6E);
                    }
                    return new CrcEngine32Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(0x09823B6E);
                    }
                    return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);
            }
        }
    }
}