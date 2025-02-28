namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-4/INTERLAKEN.
    /// </summary>
    public sealed class Crc4Interlaken : Crc
    {
        private const string DEFAULT_NAME = "CRC-4/INTERLAKEN";
        private const byte INIT = 0xF;
        private const byte POLY = 0x3;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 4;
        private const byte XOROUT = 0xF;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc4Interlaken class.
        /// </summary>
        public Crc4Interlaken() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc4Interlaken class.
        /// </summary>
        public Crc4Interlaken(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc4Interlaken(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x3; <<(8-4) = 0x30;
            // init = 0x0; <<(8-4) = 0xF0;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-4/ITU, CRC-4/G-704.
    /// </summary>
    public sealed class Crc4Itu : Crc
    {
        private const string DEFAULT_NAME = "CRC-4/ITU";
        private const byte INIT = 0x0;
        private const byte POLY = 0x3;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 4;
        private const byte XOROUT = 0x0;
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc4Itu class.
        /// </summary>
        public Crc4Itu() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc4Itu class.
        /// </summary>
        public Crc4Itu(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc4Itu(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc4Itu(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc4Itu(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x3; reverse >>(8-4) = 0xC;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine8Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine8M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine8M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine8(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}