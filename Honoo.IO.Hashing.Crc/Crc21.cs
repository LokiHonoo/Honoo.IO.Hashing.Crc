namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-21/CAN-FD.
    /// </summary>
    public sealed class Crc21CanFd : Crc
    {
        private const uint INIT = 0x000000;
        private const uint POLY = 0x102899;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 21;
        private const uint XOROUT = 0x000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc21CanFd class.
        /// </summary>
        public Crc21CanFd() : base("CRC-21/CAN-FD", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-21/CAN-FD", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc21CanFd(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x102899; <<(32-21) = 0x8144C800;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x8144C800);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}