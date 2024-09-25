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
        private static uint[] _table;

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
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc21CanFd(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x102899; <<(32-21) = 0x8144C800;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable(0x8144C800);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }
}