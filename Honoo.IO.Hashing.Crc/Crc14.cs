namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-14/DARC.
    /// </summary>
    public sealed class Crc14Darc : Crc
    {
        private const string DEFAULT_NAME = "CRC-14/DARC";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x0805;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 14;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc14Darc class.
        /// </summary>
        public Crc14Darc() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc14Darc(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x0805; reverse >>(16-14) = 0x2804;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0x2804);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-14/GSM.
    /// </summary>
    public sealed class Crc14Gsm : Crc
    {
        private const string DEFAULT_NAME = "CRC-14/GSM";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x202D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 14;
        private const ushort XOROUT = 0x3FFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc14Gsm class.
        /// </summary>
        public Crc14Gsm() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc14Gsm(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x202D; <<(16-14) = 0x80B4;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x80B4);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}