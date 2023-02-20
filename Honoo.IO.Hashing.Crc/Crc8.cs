namespace Honoo.IO.HashingOld
{
    /// <summary>
    /// CRC-8.
    /// </summary>
    public sealed class Crc8 : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8 class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x07;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x07);
                }
                return new CrcEngine8("CRC-8", 8, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-8", 8, false, false, 0x07, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/CDMA2000.
    /// </summary>
    public sealed class Crc8Cdma2000 : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Cdma2000 class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8Cdma2000(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x9B;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x9B);
                }
                return new CrcEngine8("CRC-8/CDMA2000", 8, false, false, _table, 0xFF, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-8/CDMA2000", 8, false, false, 0x9B, 0xFF, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/DARC.
    /// </summary>
    public sealed class Crc8Darc : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Darc class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8Darc(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x39; reverse = 0x9C;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x9C);
                }
                return new CrcEngine8("CRC-8/DARC", 8, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-8/DARC", 8, true, true, 0x39, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/DVB-S2.
    /// </summary>
    public sealed class Crc8DvbS2 : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8DvbS2 class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8DvbS2(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0xD5;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0xD5);
                }
                return new CrcEngine8("CRC-8/DVB-S2", 8, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-8/DVB-S2", 8, false, false, 0xD5, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/EBU.
    /// </summary>
    public sealed class Crc8Ebu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Ebu class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8Ebu(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1D; reverse = 0xB8;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0xB8);
                }
                return new CrcEngine8("CRC-8/EBU", 8, true, true, _table, 0xFF, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-8/EBU", 8, true, true, 0x1D, 0xFF, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/I-CODE.
    /// </summary>
    public sealed class Crc8ICode : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8ICode class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8ICode(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1D;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x1D);
                }
                return new CrcEngine8("CRC-8/I-CODE", 8, false, false, _table, 0xFD, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-8/I-CODE", 8, false, false, 0x1D, 0xFD, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/ITU. CRC-8/ATM.
    /// </summary>
    public sealed class Crc8Itu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Itu class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8Itu(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x07;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x07);
                }
                return new CrcEngine8("CRC-8/ITU", 8, false, false, _table, 0x00, 0x55);
            }
            else
            {
                return new CrcEngine8("CRC-8/ITU", 8, false, false, 0x07, 0x00, 0x55);
            }
        }
    }

    /// <summary>
    /// CRC-8/MAXIM. DOW-CRC. CRC-8/IBUTTON.
    /// </summary>
    public sealed class Crc8Maxim : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Maxim class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8Maxim(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x31; reverse = 0x8C;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x8C);
                }
                return new CrcEngine8("CRC-8/MAXIM", 8, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-8/MAXIM", 8, true, true, 0x31, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/ROHC.
    /// </summary>
    public sealed class Crc8Rohc : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Rohc class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8Rohc(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x07; reverse = 0xE0;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0xE0);
                }
                return new CrcEngine8("CRC-8/ROHC", 8, true, true, _table, 0xFF, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-8/ROHC", 8, true, true, 0x07, 0xFF, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/WCDMA.
    /// </summary>
    public sealed class Crc8Wcdma : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Wcdma class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc8Wcdma(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x9B; reverse = 0xD9;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0xD9);
                }
                return new CrcEngine8("CRC-8/WCDMA", 8, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-8/WCDMA", 8, true, true, 0x9B, 0x00, 0x00);
            }
        }
    }
}