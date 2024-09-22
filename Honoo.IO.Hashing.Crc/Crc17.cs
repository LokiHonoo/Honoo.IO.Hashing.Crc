namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-17/CAN-FD.
    /// </summary>
    public sealed class Crc17CanFd : Crc
    {
        private const string DEFAULT_NAME = "CRC-17/CAN-FD";
        private const uint INIT = 0x00000;
        private const uint POLY = 0x1685B;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 17;
        private const uint XOROUT = 0x00000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc17CanFd class.
        /// </summary>
        public Crc17CanFd() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter( POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc17CanFd(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x1685B; <<(32-17) = 0xB42D8000;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0xB42D8000);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}