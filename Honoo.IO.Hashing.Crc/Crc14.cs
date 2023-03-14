namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-14/DARC.
    /// </summary>
    public sealed class Crc14Darc : Crc
    {
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
        public Crc14Darc() : base("CRC-14/DARC", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-14/DARC", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc14Darc(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x0805; reverse >>(16-14) = 0x2804;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateReversedTable(0x2804);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-14/GSM.
    /// </summary>
    public sealed class Crc14Gsm : Crc
    {
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
        public Crc14Gsm() : base("CRC-14/GSM", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-14/GSM", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc14Gsm(); });
        }

        private static CrcEngine GetEngine()
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