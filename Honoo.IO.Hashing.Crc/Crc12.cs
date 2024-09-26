namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-12/CDMA2000.
    /// </summary>
    public sealed class Crc12Cdma2000 : Crc
    {
        private const string DEFAULT_NAME = "CRC-12/CDMA2000";
        private const ushort INIT = 0xFFF;
        private const ushort POLY = 0xF13;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 12;
        private const ushort XOROUT = 0x000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Cdma2000 class.
        /// </summary>
        public Crc12Cdma2000() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc12Cdma2000 class.
        /// </summary>
        public Crc12Cdma2000(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc12Cdma2000(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0xF13; <<(16-12) = 0xF130;
            // init = 0xFFF; <<(16-12) = 0xFFF0;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable((uint)0xF130 << 16);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-12/DECT.
    /// </summary>
    public sealed class Crc12Dect : Crc
    {
        private const string DEFAULT_NAME = "CRC-12/DECT";
        private const ushort INIT = 0x000;
        private const ushort POLY = 0x80F;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 12;
        private const ushort XOROUT = 0x000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Dect class.
        /// </summary>
        public Crc12Dect() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc12Dect class.
        /// </summary>
        public Crc12Dect(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc12Dect(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc12Dect(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc12Dect(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x80F; <<(16-12) = 0x80F0;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable((uint)0x80F0 << 16);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-12/GSM.
    /// </summary>
    public sealed class Crc12Gsm : Crc
    {
        private const string DEFAULT_NAME = "CRC-12/GSM";
        private const ushort INIT = 0x000;
        private const ushort POLY = 0xD31;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 12;
        private const ushort XOROUT = 0xFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Gsm class.
        /// </summary>
        public Crc12Gsm() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc12Gsm class.
        /// </summary>
        public Crc12Gsm(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc12Gsm(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0xD31; <<(16-12) = 0xD310;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable((uint)0xD310 << 16);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-12/UMTS, CRC-12/3GPP.
    /// </summary>
    public sealed class Crc12Umts : Crc
    {
        private const string DEFAULT_NAME = "CRC-12/UMTS";
        private const ushort INIT = 0x000;
        private const ushort POLY = 0x80F;
        private const bool REFIN = false;
        private const bool REFOUT = true;
        private const int WIDTH = 12;
        private const ushort XOROUT = 0x000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc12Umts class.
        /// </summary>
        public Crc12Umts() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc12Umts class.
        /// </summary>
        public Crc12Umts(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc12Umts(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc12Umts(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc12Umts(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x80F; <<(16-12) = 0x80F0;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine32.GenerateTable((uint)0x80F0 << 16);
                    }
                    return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTableInfo.M16x: return new CrcEngine32M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null);

                case CrcTableInfo.None: default: return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }
}