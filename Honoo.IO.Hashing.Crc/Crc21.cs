namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-21/CAN-FD.
    /// </summary>
    public sealed class Crc21CanFd : Crc
    {
        private const string DEFAULT_NAME = "CRC-21/CAN-FD";
        private const uint INIT = 0x000000;
        private const uint POLY = 0x102899;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 21;
        private const uint XOROUT = 0x000000;

        /// <summary>
        /// Initializes a new instance of the Crc21CanFd class.
        /// </summary>
        public Crc21CanFd(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc21CanFd Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc21CanFd(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt32Value(POLY, WIDTH), new CrcUInt32Value(INIT, WIDTH), new CrcUInt32Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc21CanFd(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x102899; <<(32-21) = 0x8144C800;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard: return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN));
                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN));
                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}