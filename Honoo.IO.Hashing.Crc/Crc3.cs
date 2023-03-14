namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-3/GSM.
    /// </summary>
    public sealed class Crc3Gsm : Crc
    {
        private const byte INIT = 0x0;
        private const byte POLY = 0x3;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 3;
        private const byte XOROUT = 0x7;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc3Gsm class.
        /// </summary>
        public Crc3Gsm() : base("CRC-3/GSM", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-3/GSM", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc3Gsm(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x3; <<(8-3) = 0x60;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x60);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-3/ROHC.
    /// </summary>
    public sealed class Crc3Rohc : Crc
    {
        private const byte INIT = 0x7;
        private const byte POLY = 0x3;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 3;
        private const byte XOROUT = 0x0;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc3Rohc class.
        /// </summary>
        public Crc3Rohc() : base("CRC-3/ROHC", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-3/ROHC", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc3Rohc(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x3; reverse >>(8-3) = 0x6;
            // init = 0x7; reverse >>(8-3) = 0x7;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0x6);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}