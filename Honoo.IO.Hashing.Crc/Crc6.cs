namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-6/CDMA2000-A.
    /// </summary>
    public sealed class Crc6Cdma2000A : Crc
    {
        private const byte INIT = 0x3F;
        private const byte POLY = 0x27;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Cdma2000A class.
        /// </summary>
        public Crc6Cdma2000A() : base("CRC-6/CDMA2000-A", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-6/CDMA2000-A", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc6Cdma2000A(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x27; <<(8-6) = 0x9C;
            // init = 0x3F; <<(8-6) = 0xFC;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x9C);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-6/CDMA2000-B.
    /// </summary>
    public sealed class Crc6Cdma2000B : Crc
    {
        private const byte INIT = 0x3F;
        private const byte POLY = 0x07;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Cdma2000B class.
        /// </summary>
        public Crc6Cdma2000B() : base("CRC-6/CDMA2000-B", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-6/CDMA2000-B", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc6Cdma2000B(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x07; <<(8-6) = 0x1C;
            // init = 0x3F; <<(8-6) = 0xFC;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x1C);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-6/DARC.
    /// </summary>
    public sealed class Crc6Darc : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x19;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Darc class.
        /// </summary>
        public Crc6Darc() : base("CRC-6/DARC", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-6/DARC", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc6Darc(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x19; reverse >>(8-6) = 0x26;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0x26);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-6/GSM.
    /// </summary>
    public sealed class Crc6Gsm : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x2F;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x3F;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Gsm class.
        /// </summary>
        public Crc6Gsm() : base("CRC-6/GSM", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-6/GSM", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc6Gsm(); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x2F; <<(8-6) = 0xBC;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0xBC);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-6/ITU, CRC-6/G-704.
    /// </summary>
    public sealed class Crc6Itu : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x03;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 6;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Itu class.
        /// </summary>
        public Crc6Itu() : base("CRC-6/ITU", GetEngine())
        {
        }

        internal Crc6Itu(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-6/ITU", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc6Itu(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc6Itu(alias); });
        }

        private static CrcEngine GetEngine()
        {
            //
            // poly = 0x03; reverse >>(8-6) = 0x30;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0x30);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}