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
        private static uint[][] _table;

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
                               new CrcParameter(NumericsStringFormat.Hex, POLY, WIDTH),
                               new CrcParameter(NumericsStringFormat.Hex, INIT, WIDTH),
                               new CrcParameter(NumericsStringFormat.Hex, XOROUT, WIDTH),
                               () => { return new Crc82Darc(); });
        }

        private static CrcEngineSharding32 GetEngine()
        {
            //
            // poly = 0x0308C0111011401440411; reverse >>(96-82) = 0x220808A00A2022200C430; (CrcEngineSharding8 88-82, (CrcEngineSharding32 96-82))
            //
            if (_table == null)
            {
                uint[] polyParsed = new uint[] { 0x00022080, 0x8A00A202, 0x2200C430 };
                _table = CrcEngineSharding32.GenerateReversedTable(polyParsed);
            }
            return new CrcEngineSharding32(WIDTH,
                                           REFIN,
                                           REFOUT,
                                           CrcConverter.ToUInt32Array(NumericsStringFormat.Hex, POLY, null),
                                           CrcConverter.ToUInt32Array(NumericsStringFormat.Hex, INIT, null),
                                           CrcConverter.ToUInt32Array(NumericsStringFormat.Hex, XOROUT, null),
                                           _table);
        }
    }
}