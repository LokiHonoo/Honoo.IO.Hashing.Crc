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
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc21CanFd class.
        /// </summary>
        public Crc21CanFd() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc21CanFd class.
        /// </summary>
        public Crc21CanFd(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
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