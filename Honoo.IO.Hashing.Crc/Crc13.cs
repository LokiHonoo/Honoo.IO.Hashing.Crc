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
        private static ushort[] _tableM16x;
        private static ushort[] _tableStandard;

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
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc13bbc(t); });
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
                        _tableStandard = CrcEngine16Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine16Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine16M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine16M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine16(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}