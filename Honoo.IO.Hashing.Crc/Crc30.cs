namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-30/CDMA.
    /// </summary>
    public sealed class Crc30Cdma : Crc
    {
        private const uint INIT = 0x3FFFFFFF;
        private const uint POLY = 0x2030B9C7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 30;
        private const uint XOROUT = 0x3FFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc30Cdma class.
        /// </summary>
        public Crc30Cdma() : base("CRC-30/CDMA", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-30/CDMA", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc30Cdma(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x2030B9C7; <<(32-30) = 0x80C2E71C;
            // init = 0x3FFFFFFF; <<(32-30) = 0xFFFFFFFC;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x80C2E71C);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}