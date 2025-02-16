namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-3/GSM.
    /// </summary>
    public sealed class Crc3Gsm : Crc
    {
        private const string DEFAULT_NAME = "CRC-3/GSM";
        private const byte INIT = 0x0;
        private const byte POLY = 0x3;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 3;
        private const byte XOROUT = 0x7;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc3Gsm class.
        /// </summary>
        public Crc3Gsm() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc3Gsm class.
        /// </summary>
        public Crc3Gsm(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc3Gsm(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x3; <<(8-3) = 0x60;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable((uint)0x60 << 24);
                    }
                    return new CrcEngine32Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable((uint)0x60 << 24);
                    }
                    return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);
            }
        }
    }

    /// <summary>
    /// CRC-3/ROHC.
    /// </summary>
    public sealed class Crc3Rohc : Crc
    {
        private const string DEFAULT_NAME = "CRC-3/ROHC";
        private const byte INIT = 0x7;
        private const byte POLY = 0x3;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 3;
        private const byte XOROUT = 0x0;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc3Rohc class.
        /// </summary>
        public Crc3Rohc() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc3Rohc class.
        /// </summary>
        public Crc3Rohc(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc3Rohc(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x3; reverse >>(8-3) = 0x6;
            // init = 0x7; reverse >>(8-3) = 0x7;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTableRef(0x6);
                    }
                    return new CrcEngine32Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTableRef(0x6);
                    }
                    return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);
            }
        }
    }
}