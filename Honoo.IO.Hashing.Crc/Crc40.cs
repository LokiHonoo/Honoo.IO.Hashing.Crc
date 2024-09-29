namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-40/GSM.
    /// </summary>
    public sealed class Crc40Gsm : Crc
    {
        private const string DEFAULT_NAME = "CRC-40/GSM";
        private const ulong INIT = 0x0000000000;
        private const ulong POLY = 0x0004820009;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 40;
        private const ulong XOROUT = 0xFFFFFFFFFF;
        private static ulong[] _tableM16x;
        private static ulong[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc40Gsm class.
        /// </summary>
        public Crc40Gsm() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc40Gsm class.
        /// </summary>
        public Crc40Gsm(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc40Gsm(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x0004820009; <<(64-40) = 0x4820009000000;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine64Standard.GenerateTable(0x4820009000000);
                    }
                    return new CrcEngine64Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine64M16x.GenerateTable(0x4820009000000);
                    }
                    return new CrcEngine64M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);

                default: return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);
            }
        }
    }
}