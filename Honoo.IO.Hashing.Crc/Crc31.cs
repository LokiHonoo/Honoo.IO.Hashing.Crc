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
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc31Philips class.
        /// </summary>
        public Crc31Philips() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc31Philips(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x04C11DB7; <<(32-31) = 0x09823B6E;
            // init = 0x7FFFFFFF; <<(32-31) = 0xFFFFFFFC;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x09823B6E);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}