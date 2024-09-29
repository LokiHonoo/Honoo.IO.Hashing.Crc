namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-10, CRC-10/ATM, CRC-10/I-610.
    /// </summary>
    public sealed class Crc10 : Crc
    {
        private const string DEFAULT_NAME = "CRC-10";
        private const ushort INIT = 0x000;
        private const ushort POLY = 0x233;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 10;
        private const ushort XOROUT = 0x000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc10 class.
        /// </summary>
        public Crc10() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc10 class.
        /// </summary>
        public Crc10(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc10(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc10(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc10(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x233; <<(16-10) = 0x8CC0;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable((uint)0x8CC0 << 16);
                    }
                    return new CrcEngine32Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable((uint)0x8CC0 << 16);
                    }
                    return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);
            }
        }
    }

    /// <summary>
    /// CRC-10/CDMA2000.
    /// </summary>
    public sealed class Crc10Cdma2000 : Crc
    {
        private const string DEFAULT_NAME = "CRC-10/CDMA2000";
        private const ushort INIT = 0x3FF;
        private const ushort POLY = 0x3D9;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 10;
        private const ushort XOROUT = 0x000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc10Cdma2000 class.
        /// </summary>
        public Crc10Cdma2000() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc10Cdma2000 class.
        /// </summary>
        public Crc10Cdma2000(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc10Cdma2000(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x3D9; <<(16-10) = 0xF640;
            // init = 0x3FF; <<(16-10) = 0xFFC0;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable((uint)0xF640 << 16);
                    }
                    return new CrcEngine32Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable((uint)0xF640 << 16);
                    }
                    return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);
            }
        }
    }

    /// <summary>
    /// CRC-10/GSM.
    /// </summary>
    public sealed class Crc10Gsm : Crc
    {
        private const string DEFAULT_NAME = "CRC-10/GSM";
        private const ushort INIT = 0x000;
        private const ushort POLY = 0x175;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 10;
        private const ushort XOROUT = 0x3FF;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc10Gsm class.
        /// </summary>
        public Crc10Gsm() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc10Gsm class.
        /// </summary>
        public Crc10Gsm(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc10Gsm(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x175; <<(16-10) = 0x5D40;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable((uint)0x5D40 << 16);
                    }
                    return new CrcEngine32Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable((uint)0x5D40 << 16);
                    }
                    return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);
            }
        }
    }
}