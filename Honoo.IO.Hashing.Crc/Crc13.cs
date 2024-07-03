namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-13/BBC.
    /// </summary>
    public sealed class Crc13bbc : Crc
    {
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x1CF5;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 13;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc13bbc class.
        /// </summary>
        public Crc13bbc() : base("CRC-13/BBC", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-13/BBC", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc13bbc(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1CF5; <<(16-13) = 0xE7A8;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0xE7A8);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}