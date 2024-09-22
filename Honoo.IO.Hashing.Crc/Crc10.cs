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
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc10 class.
        /// </summary>
        public Crc10() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc10(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter( POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc10(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
             return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc10(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x233; <<(16-10) = 0x8CC0;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x8CC0);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
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
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc10Cdma2000 class.
        /// </summary>
        public Crc10Cdma2000() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter( POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc10Cdma2000(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x3D9; <<(16-10) = 0xF640;
            // init = 0x3FF; <<(16-10) = 0xFFC0;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0xF640);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
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
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc10Gsm class.
        /// </summary>
        public Crc10Gsm() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter( POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc10Gsm(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x175; <<(16-10) = 0x5D40;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x5D40);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}