namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-40/GSM.
    /// </summary>
    public sealed class Crc40Gsm : Crc
    {
        private const ulong INIT = 0x0000000000;
        private const ulong POLY = 0x0004820009;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 40;
        private const ulong XOROUT = 0xFFFFFFFFFF;
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc40Gsm class.
        /// </summary>
        public Crc40Gsm() : base("CRC-40/GSM", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-40/GSM", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc40Gsm(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x0004820009; <<(64-40) = 0x4820009000000;
            //
            if (_table == null)
            {
                _table = CrcEngine64.GenerateTable(0x4820009000000);
            }
            return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}