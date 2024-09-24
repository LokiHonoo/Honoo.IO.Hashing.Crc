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
        private static ulong[][] _table;

        /// <summary>
        /// Initializes a new instance of the Crc82Darc class.
        /// </summary>
        public Crc82Darc() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME,
                               WIDTH,
                               REFIN,
                               REFOUT,
                               new CrcParameter(CrcStringFormat.Hex, POLY, WIDTH),
                               new CrcParameter(CrcStringFormat.Hex, INIT, WIDTH),
                               new CrcParameter(CrcStringFormat.Hex, XOROUT, WIDTH),
                               () => { return new Crc82Darc(); });
        }

        private static CrcEngine128L2 GetEngine()
        {
            //
            // poly = 0x0308C0111011401440411; reverse >>(96-82) = 0x220808A00A2022200C430;
            // Need spaces for CrcEngineSharding8 at 8*11-82>0, CrcEngineSharding32 at 32*3-82>0, CrcEngineSharding64 at 64*2-82>0
            //
            if (_table == null)
            {
                _table = CrcEngine128L2.GenerateTableRef(new ulong[] { 0x0000000000022080, 0x8A00A2022200C430 });
            }
            return new CrcEngine128L2(WIDTH, REFIN, REFOUT, new ulong[] { 0x000000000000308C, 0x0111011401440411 }, new ulong[2], new ulong[2], _table);
        }
    }
}