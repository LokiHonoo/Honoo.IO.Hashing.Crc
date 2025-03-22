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
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc17CanFd class.
        /// </summary>
        public Crc17CanFd(CrcTableInfo withTable = CrcTableInfo.Standard) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        /// <summary>
        /// Creates an instance of the algorithm.
        /// </summary>
        /// <param name="withTable">Calculate with table.</param>
        /// <returns></returns>
        public static Crc17CanFd Create(CrcTableInfo withTable = CrcTableInfo.Standard)
        {
            return new Crc17CanFd(withTable);
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt32Value(POLY, WIDTH), new CrcUInt32Value(INIT, WIDTH), new CrcUInt32Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc17CanFd(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x1685B; <<(32-17) = 0xB42D8000;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}