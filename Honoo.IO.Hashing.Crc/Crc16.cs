namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-16. CRC-16/ARC, ARC, CRC-16/LHA, CRC-IBM.
    /// </summary>
    public sealed class Crc16 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x8005;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Ccitt class.
        /// </summary>
        public Crc16() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0xA001);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/CCITT. CRC-CCITT, CRC-16/CCITT-TRUE, CRC-16/KERMIT, KERMIT, CRC-16/BLUETOOTH, CRC-16/V-41-LSB.
    /// </summary>
    public sealed class Crc16Ccitt : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/CCITT";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x1021;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Ccitt class.
        /// </summary>
        public Crc16Ccitt() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16Ccitt(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Ccitt(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Ccitt(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0x8408);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/CCITT-FALSE, CRC-16/IBM-3740, CRC-16/AUTOSAR.
    /// </summary>
    public sealed class Crc16CcittFalse : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/CCITT-FALSE";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x1021;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16CcittFalse class.
        /// </summary>
        public Crc16CcittFalse() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16CcittFalse(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16CcittFalse(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16CcittFalse(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x1021);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/CDMA2000.
    /// </summary>
    public sealed class Crc16Cdma2000 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/CDMA2000";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0xC867;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Cdma2000 class.
        /// </summary>
        public Crc16Cdma2000() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Cdma2000(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0xC867;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0xC867);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/CMS.
    /// </summary>
    public sealed class Crc16Cms : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/CMS";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x8005;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Cms class.
        /// </summary>
        public Crc16Cms() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Cms(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x8005;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x8005);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/DDS-110.
    /// </summary>
    public sealed class Crc16Dds110 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/DDS-110";
        private const ushort INIT = 0x800D;
        private const ushort POLY = 0x8005;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Dds110 class.
        /// </summary>
        public Crc16Dds110() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Dds110(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x8005;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x8005);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/DECT-R, R-CRC-16.
    /// </summary>
    public sealed class Crc16DectR : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/DECT-R";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x0589;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0001;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16DectR class.
        /// </summary>
        public Crc16DectR() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16DectR(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16DectR(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16DectR(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x0589;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x0589);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/DECT-X, X-CRC-16.
    /// </summary>
    public sealed class Crc16DectX : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/DECT-X";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x0589;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16DectX class.
        /// </summary>
        public Crc16DectX() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16DectX(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16DectX(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16DectX(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x0589;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x0589);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/DNP.
    /// </summary>
    public sealed class Crc16Dnp : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/DNP";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x3D65;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0xFFFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Dnp class.
        /// </summary>
        public Crc16Dnp() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Dnp(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x3D65; reverse = 0xA6BC;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0xA6BC);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/EN-13757.
    /// </summary>
    public sealed class Crc16En13757 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/EN-13757";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x3D65;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0xFFFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16En13757 class.
        /// </summary>
        public Crc16En13757() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16En13757(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x3D65;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x3D65);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/GENIBUS, CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE.
    /// </summary>
    public sealed class Crc16Genibus : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/GENIBUS";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x1021;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0xFFFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Genibus class.
        /// </summary>
        public Crc16Genibus() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16Genibus(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Genibus(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Genibus(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x1021);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    ///  CRC-16/GSM.
    /// </summary>
    public sealed class Crc16Gsm : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/GSM";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x1021;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0xFFFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Gsm class.
        /// </summary>
        public Crc16Gsm() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Gsm(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x1021);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/LJ1200.
    /// </summary>
    public sealed class Crc16Lj1200 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/LJ1200";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x6F63;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Lj1200 class.
        /// </summary>
        public Crc16Lj1200() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Lj1200(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x6F63;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x6F63);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/M17.
    /// </summary>
    public sealed class Crc16M17 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/M17";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x5935;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16M17 class.
        /// </summary>
        public Crc16M17() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16M17(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x5935;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x5935);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/MAXIM, CRC-16/MAXIM-DOW.
    /// </summary>
    public sealed class Crc16Maxim : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/MAXIM";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x8005;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0xFFFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Ibm class.
        /// </summary>
        public Crc16Maxim() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16Maxim(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Maxim(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Maxim(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0xA001);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/MCRF4XX.
    /// </summary>
    public sealed class Crc16Mcrf4XX : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/MCRF4XX";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x1021;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Mcrf4XX class.
        /// </summary>
        public Crc16Mcrf4XX() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Mcrf4XX(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0x8408);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/MODBUS, MODBUS.
    /// </summary>
    public sealed class Crc16Modbus : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/MODBUS";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x8005;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Modbus class.
        /// </summary>
        public Crc16Modbus() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16Modbus(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Modbus(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Modbus(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0xA001);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/NRSC-5.
    /// </summary>
    public sealed class Crc16Nrsc5 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/NRSC-5";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x080B;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Nrsc5 class.
        /// </summary>
        public Crc16Nrsc5() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Nrsc5(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x080B; reverse = 0xD010;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0xD010);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/OPENSAFETY-A.
    /// </summary>
    public sealed class Crc16OpensafetyA : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/OPENSAFETY-A";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x5935;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16OpensafetyA class.
        /// </summary>
        public Crc16OpensafetyA() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16OpensafetyA(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16OpensafetyA(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x5935;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x5935);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/OPENSAFETY-B.
    /// </summary>
    public sealed class Crc16OpensafetyB : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/OPENSAFETY-B";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x755B;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16OpensafetyB class.
        /// </summary>
        public Crc16OpensafetyB() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16OpensafetyB(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16OpensafetyB(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x755B;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x755B);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/PROFIBUS, CRC-16/IEC-61158-2.
    /// </summary>
    public sealed class Crc16Profibus : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/PROFIBUS";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x1DCF;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0xFFFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Profibus class.
        /// </summary>
        public Crc16Profibus() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16Profibus(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Profibus(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Profibus(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1DCF;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x1DCF);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/RIELLO.
    /// </summary>
    public sealed class Crc16Riello : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/RIELLO";
        private const ushort INIT = 0xB2AA;
        private const ushort POLY = 0x1021;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Riello class.
        /// </summary>
        public Crc16Riello() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Riello(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            // init = 0xB2AA; reverse = 0x554D;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0x8408);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/SPI-FUJITSU, CRC-16/AUG-CCITT.
    /// </summary>
    public sealed class Crc16SpiFujitsu : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/SPI-FUJITSU";
        private const ushort INIT = 0x1D0F;
        private const ushort POLY = 0x1021;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16AugCcitt class.
        /// </summary>
        public Crc16SpiFujitsu() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16SpiFujitsu(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16SpiFujitsu(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16SpiFujitsu(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x1021);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/T10-DIF.
    /// </summary>
    public sealed class Crc16T10Dif : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/T10-DIF";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x8BB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16T10Dif class.
        /// </summary>
        public Crc16T10Dif() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16T10Dif(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x8BB7;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x8BB7);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/TELEDISK.
    /// </summary>
    public sealed class Crc16Teledisk : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/TELEDISK";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0xA097;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Teledisk class.
        /// </summary>
        public Crc16Teledisk() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Teledisk(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0xA097;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0xA097);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/TMS37157.
    /// </summary>
    public sealed class Crc16Tms37157 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/TMS37157";
        private const ushort INIT = 0x89EC;
        private const ushort POLY = 0x1021;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Tms37157 class.
        /// </summary>
        public Crc16Tms37157() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Tms37157(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            // poly = 0x89EC; reverse = 0x3791;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0x8408);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/UMTS, CRC-16/BUYPASS, CRC-16/VERIFONE.
    /// </summary>
    public sealed class Crc16Umts : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/UMTS";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x8005;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Umts class.
        /// </summary>
        public Crc16Umts() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16Umts(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Umts(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Umts(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x8005);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/USB.
    /// </summary>
    public sealed class Crc16Usb : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/USB";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x8005;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0xFFFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Usb class.
        /// </summary>
        public Crc16Usb() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Usb(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0xA001);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/X-25. X-25, CRC-16/IBM-SDLC, CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-B.
    /// </summary>
    public sealed class Crc16X25 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/X-25";
        private const ushort INIT = 0xFFFF;
        private const ushort POLY = 0x1021;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0xFFFF;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16X25 class.
        /// </summary>
        public Crc16X25() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16X25(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16X25(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16X25(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0x8408);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/XMODEM. XMODEM, ZMODEM, CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB.
    /// </summary>
    public sealed class Crc16Xmodem : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/XMODEM";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x1021;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Xmodem class.
        /// </summary>
        public Crc16Xmodem() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal Crc16Xmodem(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Xmodem(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Xmodem(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTable(0x1021);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-16/XMODEM2.
    /// </summary>
    public sealed class Crc16Xmodem2 : Crc
    {
        private const string DEFAULT_NAME = "CRC-16/XMODEM2";
        private const ushort INIT = 0x0000;
        private const ushort POLY = 0x8408;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Xmodem2 class.
        /// </summary>
        public Crc16Xmodem2() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new Crc16Xmodem2(); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x8408; reverse = 0x1021;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0x1021);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-A, CRC-16/ISO-IEC-14443-3-A.
    /// </summary>
    public sealed class CrcA : Crc
    {
        private const string DEFAULT_NAME = "CRC-A";
        private const ushort INIT = 0xC6C6;
        private const ushort POLY = 0x1021;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 16;
        private const ushort XOROUT = 0x0000;
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the CrcA class.
        /// </summary>
        public CrcA() : base(DEFAULT_NAME, GetEngine())
        {
        }

        internal CrcA(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new CrcA(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), () => { return new CrcA(alias); });
        }

        private static CrcEngine16 GetEngine()
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            // init = 0xC6C6; reverse = 0x6363;
            //
            if (_table == null)
            {
                _table = CrcEngine16.GenerateTableRef(0x8408);
            }
            return new CrcEngine16(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}