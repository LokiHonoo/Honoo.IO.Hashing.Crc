namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-16. CRC-16/ARC, ARC, CRC-16/LHA, CRC-IBM.
    /// </summary>
    public sealed class Crc16 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Ccitt class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16(bool withTable = true) : base(GetEngine("CRC-16", withTable))
        {
        }

        internal Crc16(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA001);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x8005, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/CCITT. CRC-CCITT, CRC-16/CCITT-TRUE, CRC-16/KERMIT, KERMIT, CRC-16/BLUETOOTH, CRC-16/V-41-LSB.
    /// </summary>
    public sealed class Crc16Ccitt : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Ccitt class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Ccitt(bool withTable = true) : base(GetEngine("CRC-16/CCITT", withTable))
        {
        }

        internal Crc16Ccitt(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x1021, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/CCITT-FALSE, CRC-16/IBM-3740, CRC-16/AUTOSAR.
    /// </summary>
    public sealed class Crc16CcittFalse : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16CcittFalse class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16CcittFalse(bool withTable = true) : base(GetEngine("CRC-16/CCITT-FALSE", withTable))
        {
        }

        internal Crc16CcittFalse(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1021);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x1021, 0xFFFF, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/CDMA2000.
    /// </summary>
    public sealed class Crc16Cdma2000 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Cdma2000 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Cdma2000(bool withTable = true) : base(GetEngine("CRC-16/CDMA2000", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0xC867;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0xC867);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0xC867, 0xFFFF, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/CMS.
    /// </summary>
    public sealed class Crc16Cms : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Cms class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Cms(bool withTable = true) : base(GetEngine("CRC-16/CMS", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x8005;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x8005);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x8005, 0xFFFF, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/DDS-110.
    /// </summary>
    public sealed class Crc16Dds110 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Dds110 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Dds110(bool withTable = true) : base(GetEngine("CRC-16/DDS-110", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x8005;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x8005);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x800D, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x8005, 0x800D, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/DECT-R, R-CRC-16.
    /// </summary>
    public sealed class Crc16DectR : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16DectR class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16DectR(bool withTable = true) : base(GetEngine("CRC-16/DECT-R", withTable))
        {
        }

        internal Crc16DectR(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x0589;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x0589);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0x0001);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x0589, 0x0000, 0x0001);
            }
        }
    }

    /// <summary>
    /// CRC-16/DECT-X, X-CRC-16.
    /// </summary>
    public sealed class Crc16DectX : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16DectX class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16DectX(bool withTable = true) : base(GetEngine("CRC-16/DECT-X", withTable))
        {
        }

        internal Crc16DectX(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x0589;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x0589);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x0589, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/DNP.
    /// </summary>
    public sealed class Crc16Dnp : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Dnp class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Dnp(bool withTable = true) : base(GetEngine("CRC-16/DNP", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x3D65; reverse = 0xA6BC;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA6BC);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0x0000, 0xFFFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x3D65, 0x0000, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/EN-13757.
    /// </summary>
    public sealed class Crc16En13757 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16En13757 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16En13757(bool withTable = true) : base(GetEngine("CRC-16/EN-13757", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x3D65;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x3D65);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0xFFFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x3D65, 0x0000, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/GENIBUS, CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE.
    /// </summary>
    public sealed class Crc16Genibus : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Genibus class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Genibus(bool withTable = true) : base(GetEngine("CRC-16/GENIBUS", withTable))
        {
        }

        internal Crc16Genibus(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1021);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0xFFFF, 0xFFFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x1021, 0xFFFF, 0xFFFF);
            }
        }
    }

    /// <summary>
    ///  CRC-16/GSM.
    /// </summary>
    public sealed class Crc16Gsm : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Gsm class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Gsm(bool withTable = true) : base(GetEngine("CRC-16/GSM", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1021);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0xFFFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x1021, 0x0000, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/LJ1200.
    /// </summary>
    public sealed class Crc16Lj1200 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Lj1200 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Lj1200(bool withTable = true) : base(GetEngine("CRC-16/LJ1200", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x6F63;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x6F63);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x6F63, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/M17.
    /// </summary>
    public sealed class Crc16M17 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16M17 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16M17(bool withTable = true) : base(GetEngine("CRC-16/M17", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x5935;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x5935);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x5935, 0xFFFF, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/MAXIM, CRC-16/MAXIM-DOW.
    /// </summary>
    public sealed class Crc16Maxim : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Ibm class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Maxim(bool withTable = true) : base(GetEngine("CRC-16/MAXIM", withTable))
        {
        }

        internal Crc16Maxim(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA001);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0x0000, 0xFFFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x8005, 0x0000, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/MCRF4XX.
    /// </summary>
    public sealed class Crc16Mcrf4XX : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Mcrf4XX class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Mcrf4XX(bool withTable = true) : base(GetEngine("CRC-16/MCRF4XX", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x1021, 0xFFFF, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/MODBUS, MODBUS.
    /// </summary>
    public sealed class Crc16Modbus : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Modbus class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Modbus(bool withTable = true) : base(GetEngine("CRC-16/MODBUS", withTable))
        {
        }

        internal Crc16Modbus(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA001);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x8005, 0xFFFF, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/NRSC-5.
    /// </summary>
    public sealed class Crc16Nrsc5 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Nrsc5 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Nrsc5(bool withTable = true) : base(GetEngine("CRC-16/NRSC-5", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x080B; reverse = 0xD010;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xD010);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x080B, 0xFFFF, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/OPENSAFETY-A.
    /// </summary>
    public sealed class Crc16OpensafetyA : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16OpensafetyA class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16OpensafetyA(bool withTable = true) : base(GetEngine("CRC-16/OPENSAFETY-A", withTable))
        {
        }

        internal Crc16OpensafetyA(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x5935;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x5935);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x5935, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/OPENSAFETY-B.
    /// </summary>
    public sealed class Crc16OpensafetyB : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16OpensafetyB class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16OpensafetyB(bool withTable = true) : base(GetEngine("CRC-16/OPENSAFETY-B", withTable))
        {
        }

        internal Crc16OpensafetyB(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x755B;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x755B);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x755B, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/PROFIBUS, CRC-16/IEC-61158-2.
    /// </summary>
    public sealed class Crc16Profibus : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Profibus class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Profibus(bool withTable = true) : base(GetEngine("CRC-16/PROFIBUS", withTable))
        {
        }

        internal Crc16Profibus(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1DCF;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1DCF);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0xFFFF, 0xFFFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x1DCF, 0xFFFF, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/RIELLO.
    /// </summary>
    public sealed class Crc16Riello : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Riello class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Riello(bool withTable = true) : base(GetEngine("CRC-16/RIELLO", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            // init = 0xB2AA; reverse = 0x554D;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0x554D, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x1021, 0xB2AA, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/SPI-FUJITSU, CRC-16/AUG-CCITT.
    /// </summary>
    public sealed class Crc16SpiFujitsu : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16AugCcitt class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16SpiFujitsu(bool withTable = true) : base(GetEngine("CRC-16/SPI-FUJITSU", withTable))
        {
        }

        internal Crc16SpiFujitsu(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1021);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x1D0F, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x1021, 0x1D0F, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/T10-DIF.
    /// </summary>
    public sealed class Crc16T10Dif : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16T10Dif class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16T10Dif(bool withTable = true) : base(GetEngine("CRC-16/T10-DIF", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x8BB7;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x8BB7);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x8BB7, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/TELEDISK.
    /// </summary>
    public sealed class Crc16Teledisk : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Teledisk class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Teledisk(bool withTable = true) : base(GetEngine("CRC-16/TELEDISK", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0xA097;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0xA097);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0xA097, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/TMS37157.
    /// </summary>
    public sealed class Crc16Tms37157 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Tms37157 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Tms37157(bool withTable = true) : base(GetEngine("CRC-16/TMS37157", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            // poly = 0x89EC; reverse = 0x3791;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0x3791, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x1021, 0x89EC, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/UMTS, CRC-16/BUYPASS, CRC-16/VERIFONE.
    /// </summary>
    public sealed class Crc16Umts : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Umts class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Umts(bool withTable = true) : base(GetEngine("CRC-16/UMTS", withTable))
        {
        }

        internal Crc16Umts(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x8005);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x8005, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/USB.
    /// </summary>
    public sealed class Crc16Usb : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Usb class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Usb(bool withTable = true) : base(GetEngine("CRC-16/USB", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA001);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0xFFFF, 0xFFFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x8005, 0xFFFF, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/X-25. X-25, CRC-16/IBM-SDLC, CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-B.
    /// </summary>
    public sealed class Crc16X25 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16X25 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16X25(bool withTable = true) : base(GetEngine("CRC-16/X-25", withTable))
        {
        }

        internal Crc16X25(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0xFFFF, 0xFFFF);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x1021, 0xFFFF, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/XMODEM. XMODEM, ZMODEM, CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB.
    /// </summary>
    public sealed class Crc16Xmodem : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Xmodem class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Xmodem(bool withTable = true) : base(GetEngine("CRC-16/XMODEM", withTable))
        {
        }

        internal Crc16Xmodem(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1021);
                }
                return new CrcEngine16(algorithmName, 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, false, false, 0x1021, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/XMODEM2.
    /// </summary>
    public sealed class Crc16Xmodem2 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Xmodem2 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc16Xmodem2(bool withTable = true) : base(GetEngine("CRC-16/XMODEM2", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x8408; reverse = 0x1021;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x1021);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x8408, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-A, CRC-16/ISO-IEC-14443-3-A.
    /// </summary>
    public sealed class CrcA : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the CrcA class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public CrcA(bool withTable = true) : base(GetEngine("CRC-A", withTable))
        {
        }

        internal CrcA(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            // init = 0xC6C6; reverse = 0x6363;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16(algorithmName, 16, true, true, _table, 0x6363, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 16, true, true, 0x1021, 0xC6C6, 0x0000);
            }
        }
    }
}