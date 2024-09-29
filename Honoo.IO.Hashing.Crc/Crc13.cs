namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-13/BBC.
    /// </summary>
    public sealed class Crc13bbc : Crc
    {
        private const string DEFAULT_NAME = "CRC-13/BBC";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x1CF5;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 13;
        private const ushort XOROUT = 0x0000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc13bbc class.
        /// </summary>
        public Crc13bbc() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc13bbc class.
        /// </summary>
        public Crc13bbc(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc13bbc(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x1CF5; <<(16-13) = 0xE7A8;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable((uint)0xE7A8 << 16);
                    }
                    return new CrcEngine32Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable((uint)0xE7A8 << 16);
                    }
                    return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);
            }
        }
    }
}