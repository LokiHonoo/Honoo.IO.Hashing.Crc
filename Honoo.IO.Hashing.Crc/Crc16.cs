namespace Honoo.IO.HashingOld
{
    /// <summary>
    /// CRC-16/AUG-CCITT.
    /// </summary>
    public sealed class Crc16AugCcitt : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16AugCcitt class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16AugCcitt(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1021);
                }
                return new CrcEngine16("CRC-16/AUG-CCITT", 16, false, false, _table, 0x1D0F, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/AUG-CCITT", 16, false, false, 0x1021, 0x1D0F, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/BUYPASS.
    /// </summary>
    public sealed class Crc16BuyPass : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16BuyPass class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16BuyPass(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x8005);
                }
                return new CrcEngine16("CRC-16/BUYPASS", 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/BUYPASS", 16, false, false, 0x8005, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/CCITT. CRC-16/KERMIT.
    /// </summary>
    public sealed class Crc16Ccitt : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Ccitt class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Ccitt(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16("CRC-16/CCITT", 16, true, true, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/CCITT", 16, true, true, 0x1021, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/CCITT-FALSE.
    /// </summary>
    public sealed class Crc16CcittFalse : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16CcittFalse class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16CcittFalse(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1021);
                }
                return new CrcEngine16("CRC-16/CCITT-FALSE", 16, false, false, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/CCITT-FALSE", 16, false, false, 0x1021, 0xFFFF, 0x0000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Cdma2000(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0xC867;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0xC867);
                }
                return new CrcEngine16("CRC-16/CDMA2000", 16, false, false, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/CDMA2000", 16, false, false, 0xC867, 0xFFFF, 0x0000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Dds110(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x8005;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x8005);
                }
                return new CrcEngine16("CRC-16/DDS-110", 16, false, false, _table, 0x800D, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/DDS-110", 16, false, false, 0x8005, 0x800D, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/DECT-R.
    /// </summary>
    public sealed class Crc16DectR : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16DectR class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16DectR(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x0589;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x0589);
                }
                return new CrcEngine16("CRC-16/DECT-R", 16, false, false, _table, 0x0000, 0x0001);
            }
            else
            {
                return new CrcEngine16("CRC-16/DECT-R", 16, false, false, 0x0589, 0x0000, 0x0001);
            }
        }
    }

    /// <summary>
    /// CRC-16/DECT-X.
    /// </summary>
    public sealed class Crc16DectX : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16DectX class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16DectX(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x0589;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x0589);
                }
                return new CrcEngine16("CRC-16/DECT-X", 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/DECT-X", 16, false, false, 0x0589, 0x0000, 0x0000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Dnp(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x3D65; reverse = 0xA6BC;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA6BC);
                }
                return new CrcEngine16("CRC-16/DNP", 16, true, true, _table, 0x0000, 0xFFFF);
            }
            else
            {
                return new CrcEngine16("CRC-16/DNP", 16, true, true, 0x3D65, 0x0000, 0xFFFF);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16En13757(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x3D65;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x3D65);
                }
                return new CrcEngine16("CRC-16/EN-13757", 16, false, false, _table, 0x0000, 0xFFFF);
            }
            else
            {
                return new CrcEngine16("CRC-16/EN-13757", 16, false, false, 0x3D65, 0x0000, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/GENIBUS.
    /// </summary>
    public sealed class Crc16Genibus : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Genibus class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Genibus(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1021);
                }
                return new CrcEngine16("CRC-16/GENIBUS", 16, false, false, _table, 0xFFFF, 0xFFFF);
            }
            else
            {
                return new CrcEngine16("CRC-16/GENIBUS", 16, false, false, 0x1021, 0xFFFF, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/IBM. CRC-16/ARC. CRC-16/LHA.
    /// </summary>
    public sealed class Crc16Ibm : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Ibm class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Ibm(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA001);
                }
                return new CrcEngine16("CRC-16/IBM", 16, true, true, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/IBM", 16, true, true, 0x8005, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/MAXIM.
    /// </summary>
    public sealed class Crc16Maxim : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Ibm class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Maxim(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA001);
                }
                return new CrcEngine16("CRC-16/MAXIM", 16, true, true, _table, 0x0000, 0xFFFF);
            }
            else
            {
                return new CrcEngine16("CRC-16/MAXIM", 16, true, true, 0x8005, 0x0000, 0xFFFF);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Mcrf4XX(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16("CRC-16/MCRF4XX", 16, true, true, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/MCRF4XX", 16, true, true, 0x1021, 0xFFFF, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-16/MODBUS.
    /// </summary>
    public sealed class Crc16Modbus : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Modbus class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Modbus(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA001);
                }
                return new CrcEngine16("CRC-16/MODBUS", 16, true, true, _table, 0xFFFF, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/MODBUS", 16, true, true, 0x8005, 0xFFFF, 0x0000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Riello(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            // init = 0xB2AA; reverse = 0x554D;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16("CRC-16/RIELLO", 16, true, true, _table, 0x554D, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/RIELLO", 16, true, true, 0x1021, 0xB2AA, 0x0000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16T10Dif(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x8BB7;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x8BB7);
                }
                return new CrcEngine16("CRC-16/T10-DIF", 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/T10-DIF", 16, false, false, 0x8BB7, 0x0000, 0x0000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Teledisk(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0xA097;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0xA097);
                }
                return new CrcEngine16("CRC-16/TELEDISK", 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/TELEDISK", 16, false, false, 0xA097, 0x0000, 0x0000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Tms37157(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            // poly = 0x89EC; reverse = 0x3791;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16("CRC-16/TMS37157", 16, true, true, _table, 0x3791, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/TMS37157", 16, true, true, 0x1021, 0x89EC, 0x0000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Usb(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x8005; reverse = 0xA001;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0xA001);
                }
                return new CrcEngine16("CRC-16/USB", 16, true, true, _table, 0xFFFF, 0xFFFF);
            }
            else
            {
                return new CrcEngine16("CRC-16/USB", 16, true, true, 0x8005, 0xFFFF, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/X25.
    /// </summary>
    public sealed class Crc16X25 : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16X25 class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16X25(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16("CRC-16/X25", 16, true, true, _table, 0xFFFF, 0xFFFF);
            }
            else
            {
                return new CrcEngine16("CRC-16/X25", 16, true, true, 0x1021, 0xFFFF, 0xFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-16/XMODEM. CRC-16/ZMODEM. CRC-16/ACORN.
    /// </summary>
    public sealed class Crc16Xmodem : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc16Xmodem class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Xmodem(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0x1021);
                }
                return new CrcEngine16("CRC-16/XMODEM", 16, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/XMODEM", 16, false, false, 0x1021, 0x0000, 0x0000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc16Xmodem2(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x8408; reverse = 0x1021;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x1021);
                }
                return new CrcEngine16("CRC-16/XMODEM2", 16, true, true, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-16/XMODEM2", 16, true, true, 0x8408, 0x0000, 0x0000);
            }
        }
    }

    /// <summary>
    /// CRC-A.
    /// </summary>
    public sealed class CrcA : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the CrcA class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public CrcA(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1021; reverse = 0x8408;
            // init = 0xC6C6; reverse = 0x6363;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateReversedTable(0x8408);
                }
                return new CrcEngine16("CRC-A", 16, true, true, _table, 0x6363, 0x0000);
            }
            else
            {
                return new CrcEngine16("CRC-A", 16, true, true, 0x1021, 0xC6C6, 0x0000);
            }
        }
    }
}