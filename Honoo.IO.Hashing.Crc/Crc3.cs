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
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

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
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc3Gsm(t); });
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
        private static byte[] _tableM16x;
        private static byte[] _tableStandard;

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
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc3Rohc(t); });
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