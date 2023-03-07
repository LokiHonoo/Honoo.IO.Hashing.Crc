namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-8, CRC-8/SMBUS.
    /// </summary>
    public sealed class Crc8 : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8(bool withTable = true) : base(GetEngine("CRC-8", withTable))
        {
        }

        internal Crc8(string alias,bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x07;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x07);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x07, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/AUTOSAR.
    /// </summary>
    public sealed class Crc8Autosar : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Autosar class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Autosar(bool withTable = true) : base(GetEngine("CRC-8/AUTOSAR", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x2F;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x2F);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0xFF, 0xFF);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x2F, 0xFF, 0xFF);
            }
        }
    }

    /// <summary>
    /// CRC-8/BLUETOOTH.
    /// </summary>
    public sealed class Crc8Bluetooth : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Bluetooth class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Bluetooth(bool withTable = true) : base(GetEngine("CRC-8/BLUETOOTH", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0xA7; reverse = 0xE5;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0xE5);
                }
                return new CrcEngine8(algorithmName, 8, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, true, true, 0xA7, 0x00, 0x00);
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
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Cdma2000(bool withTable = true) : base(GetEngine("CRC-8/CDMA2000", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x9B;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x9B);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0xFF, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x9B, 0xFF, 0x00);
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
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Darc(bool withTable = true) : base(GetEngine("CRC-8/DARC", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x39; reverse = 0x9C;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x9C);
                }
                return new CrcEngine8(algorithmName, 8, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, true, true, 0x39, 0x00, 0x00);
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
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8DvbS2(bool withTable = true) : base(GetEngine("CRC-8/DVB-S2", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0xD5;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0xD5);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0xD5, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/EBU, CRC-8/TECH-3250, CRC-8/AES.
    /// </summary>
    public sealed class Crc8Ebu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Ebu class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Ebu(bool withTable = true) : base(GetEngine("CRC-8/EBU", withTable))
        {
        }

        internal Crc8Ebu(string alias,bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1D; reverse = 0xB8;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0xB8);
                }
                return new CrcEngine8(algorithmName, 8, true, true, _table, 0xFF, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, true, true, 0x1D, 0xFF, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/GSM-A.
    /// </summary>
    public sealed class Crc8GsmA : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8GsmA class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8GsmA(bool withTable = true) : base(GetEngine("CRC-8/GSM-A", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1D;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x1D);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x1D, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/GSM-B.
    /// </summary>
    public sealed class Crc8GsmB : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8GsmB class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8GsmB(bool withTable = true) : base(GetEngine("CRC-8/GSM-B", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x49;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x49);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0x00, 0xFF);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x49, 0x00, 0xFF);
            }
        }
    }

    /// <summary>
    /// CRC-8/HITAG.
    /// </summary>
    public sealed class Crc8Hitag : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Hitag class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Hitag(bool withTable = true) : base(GetEngine("CRC-8/HITAG", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1D;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x1D);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0xFF, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x1D, 0xFF, 0x00);
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
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8ICode(bool withTable = true) : base(GetEngine("CRC-8/I-CODE", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1D;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x1D);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0xFD, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x1D, 0xFD, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/ITU. CRC-8/I-432-1, CRC-8/ATM(?).
    /// </summary>
    public sealed class Crc8Itu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Itu class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Itu(bool withTable = true) : base(GetEngine("CRC-8/ITU", withTable))
        {
        }

        internal Crc8Itu(string alias,bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x07;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x07);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0x00, 0x55);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x07, 0x00, 0x55);
            }
        }
    }

    /// <summary>
    /// CRC-8/LTE.
    /// </summary>
    public sealed class Crc8Lte : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Lte class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Lte(bool withTable = true) : base(GetEngine("CRC-8/LTE", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x9B;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x9B);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x9B, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/MAXIM. CRC-8/MAXIM-DOW, DOW-CRC.
    /// </summary>
    public sealed class Crc8Maxim : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Maxim class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Maxim(bool withTable = true) : base(GetEngine("CRC-8/MAXIM", withTable))
        {
        }

        internal Crc8Maxim(string alias,bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x31; reverse = 0x8C;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x8C);
                }
                return new CrcEngine8(algorithmName, 8, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, true, true, 0x31, 0x00, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/MIFARE-MAD.
    /// </summary>
    public sealed class Crc8MifareMad : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8MifareMad class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8MifareMad(bool withTable = true) : base(GetEngine("CRC-8/MIFARE-MAD", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1D;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x1D);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0xC7, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x1D, 0xC7, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/NRSC-5.
    /// </summary>
    public sealed class Crc8Nrsc5 : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Nrsc5 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Nrsc5(bool withTable = true) : base(GetEngine("CRC-8/NRSC-5", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x31;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x31);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0xFF, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x31, 0xFF, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/OPENSAFETY.
    /// </summary>
    public sealed class Crc8Opensafety : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Opensafety class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Opensafety(bool withTable = true) : base(GetEngine("CRC-8/OPENSAFETY", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x2F;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x2F);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x2F, 0x00, 0x00);
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
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Rohc(bool withTable = true) : base(GetEngine("CRC-8/ROHC", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x07; reverse = 0xE0;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0xE0);
                }
                return new CrcEngine8(algorithmName, 8, true, true, _table, 0xFF, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, true, true, 0x07, 0xFF, 0x00);
            }
        }
    }

    /// <summary>
    /// CRC-8/SAE-J1850.
    /// </summary>
    public sealed class Crc8SaeJ1850 : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8SaeJ1850 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8SaeJ1850(bool withTable = true) : base(GetEngine("CRC-8/SAE-J1850", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1D;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x1D);
                }
                return new CrcEngine8(algorithmName, 8, false, false, _table, 0xFF, 0xFF);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, false, false, 0x1D, 0xFF, 0xFF);
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
        /// <param name="withTable">Calculations with the table.</param>
        public Crc8Wcdma(bool withTable = true) : base(GetEngine("CRC-8/WCDMA", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x9B; reverse = 0xD9;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0xD9);
                }
                return new CrcEngine8(algorithmName, 8, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8(algorithmName, 8, true, true, 0x9B, 0x00, 0x00);
            }
        }
    }
}