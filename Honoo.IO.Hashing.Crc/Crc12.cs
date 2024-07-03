namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-12/CDMA2000.
    /// </summary>
    public sealed class Crc12Cdma2000 : Crc
    {
        private const ushort INIT = 0xFFF;
        private const ushort POLY = 0xF13;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 12;
        private const ushort XOROUT = 0x000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Cdma2000 class.
        /// </summary>
        public Crc12Cdma2000() : base("CRC-12/CDMA2000", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-12/CDMA2000", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc12Cdma2000(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0xF13; <<(16-12) = 0xF130;
            // init = 0xFFF; <<(16-12) = 0xFFF0;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0xF130);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-12/DECT.
    /// </summary>
    public sealed class Crc12Dect : Crc
    {
        private const ushort INIT = 0x000;
        private const ushort POLY = 0x80F;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 12;
        private const ushort XOROUT = 0x000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Dect class.
        /// </summary>
        public Crc12Dect() : base("CRC-12/DECT", GetEngine())
        {
        }

        internal Crc12Dect(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-12/DECT", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc12Dect(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc12Dect(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x80F; <<(16-12) = 0x80F0;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x80F0);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-12/GSM.
    /// </summary>
    public sealed class Crc12Gsm : Crc
    {
        private const ushort INIT = 0x000;
        private const ushort POLY = 0xD31;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 12;
        private const ushort XOROUT = 0xFFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Gsm class.
        /// </summary>
        public Crc12Gsm() : base("CRC-12/GSM", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-12/GSM", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc12Gsm(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0xD31; <<(16-12) = 0xD310;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0xD310);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-12/UMTS, CRC-12/3GPP.
    /// </summary>
    public sealed class Crc12Umts : Crc
    {
        private const ushort INIT = 0x000;
        private const ushort POLY = 0x80F;
        private const bool REFIN = false;
        private const bool REFOUT = true;
        private const int WIDTH = 12;
        private const ushort XOROUT = 0x000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Umts class.
        /// </summary>
        public Crc12Umts() : base("CRC-12/UMTS", GetEngine())
        {
        }

        internal Crc12Umts(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-12/UMTS", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc12Umts(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc12Umts(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x80F; <<(16-12) = 0x80F0;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x80F0);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}