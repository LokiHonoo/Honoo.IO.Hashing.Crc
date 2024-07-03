namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-82/DARC.
    /// </summary>
    public sealed class Crc82Darc : Crc
    {
        private const string INIT = "000000000000000000000";
        private const string POLY = "0308C0111011401440411";
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 82;
        private const string XOROUT = "000000000000000000000";
        private static readonly uint[] REVERSED_POLY = new uint[] { 0x00022080, 0x8A00A202, 0x2200C430 };
        private static uint[][] _table;

        /// <summary>
        /// Initializes a new instance of the Crc82Darc class.
        /// </summary>
        public Crc82Darc() : base("CRC-82/DARC", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-82/DARC", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc82Darc(); });
        }

        private static CrcEngineX2 GetEngine()
        {
            //
            // poly = 0x0308C0111011401440411; reverse >>(96-82) = 0x220808A00A2022200C430; (CrcEngineX 88-82, (CrcEngineX2 96-82))
            //
            if (_table == null)
            {
                _table = CrcEngineX2.GenerateReversedTable(REVERSED_POLY);
            }
            return new CrcEngineX2(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}