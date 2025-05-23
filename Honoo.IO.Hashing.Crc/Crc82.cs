namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-82/DARC.
    /// </summary>
    public sealed class Crc82Darc : Crc
    {
        private const string DEFAULT_NAME = "CRC-82/DARC";
        private const string INIT = "0x000000000000000000000";
        private const string POLY = "0x0308C0111011401440411";
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 82;
        private const string XOROUT = "0x000000000000000000000";
        private static readonly uint[] _init = new uint[3];
        private static readonly uint[] _poly = new uint[] { 0x0000308C, 0x01110114, 0x01440411 };
        private static readonly uint[] _xorout = new uint[3];
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc82Darc class.
        /// </summary>
        public Crc82Darc(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc82Darc Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc82Darc(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME,
                               WIDTH,
                               new CrcHexValue(POLY, WIDTH),
                               new CrcHexValue(INIT, WIDTH),
                               new CrcHexValue(XOROUT, WIDTH),
                               REFIN,
                               REFOUT,
                               (t) => { return new Crc82Darc(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x0308C0111011401440411; reverse >>(96-82) = 0x220808A00A2022200C430;
            // Need spaces for CrcEngineSharding8 at 8*11-82>0, CrcEngineSharding32 at 32*3-82>0, CrcEngineSharding64 at 64*2-82>0
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngineSharding32Standard.GenerateTable(WIDTH, _poly, REFIN);
                    }
                    return new CrcEngineSharding32Standard(WIDTH, _poly, _init, _xorout, REFIN, REFOUT, _tableStandard);

                //case CrcTableInfo.M16x:
                //    if (_tableM16x == null)
                //    {
                //        _tableM16x = CrcEngineSharding32M16x.GenerateTableRef(new uint[] { 0x00022080, 0x8A00A202, 0x2200C430 });
                //    }
                //    return new CrcEngineSharding32M16x(WIDTH, REFIN, REFOUT, _poly, _init, _xorout, _tableM16x);

                default: return new CrcEngineSharding32(WIDTH, _poly, _init, _xorout, REFIN, REFOUT);
            }
        }
    }
}