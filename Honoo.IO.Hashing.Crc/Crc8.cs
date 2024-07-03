namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-8, CRC-8/SMBUS.
    /// </summary>
    public sealed class Crc8 : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x07;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8 class.
        /// </summary>
        public Crc8() : base("CRC-8", GetEngine())
        {
        }

        internal Crc8(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8(alias); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x07;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x07);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/AUTOSAR.
    /// </summary>
    public sealed class Crc8Autosar : Crc
    {
        private const byte INIT = 0xFF;
        private const byte POLY = 0x2F;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0xFF;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Autosar class.
        /// </summary>
        public Crc8Autosar() : base("CRC-8/AUTOSAR", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/AUTOSAR", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Autosar(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x2F;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x2F);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/BLUETOOTH.
    /// </summary>
    public sealed class Crc8Bluetooth : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0xA7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Bluetooth class.
        /// </summary>
        public Crc8Bluetooth() : base("CRC-8/BLUETOOTH", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/BLUETOOTH", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Bluetooth(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0xA7; reverse = 0xE5;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0xE5);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/CDMA2000.
    /// </summary>
    public sealed class Crc8Cdma2000 : Crc
    {
        private const byte INIT = 0xFF;
        private const byte POLY = 0x9B;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Cdma2000 class.
        /// </summary>
        public Crc8Cdma2000() : base("CRC-8/CDMA2000", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/CDMA2000", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Cdma2000(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x9B;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x9B);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/DARC.
    /// </summary>
    public sealed class Crc8Darc : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x39;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Darc class.
        /// </summary>
        public Crc8Darc() : base("CRC-8/DARC", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/DARC", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Darc(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x39; reverse = 0x9C;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0x9C);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/DVB-S2.
    /// </summary>
    public sealed class Crc8DvbS2 : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0xD5;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8DvbS2 class.
        /// </summary>
        public Crc8DvbS2() : base("CRC-8/DVB-S2", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/DVB-S2", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8DvbS2(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0xD5;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0xD5);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/EBU, CRC-8/TECH-3250, CRC-8/AES.
    /// </summary>
    public sealed class Crc8Ebu : Crc
    {
        private const byte INIT = 0xFF;
        private const byte POLY = 0x1D;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Ebu class.
        /// </summary>
        public Crc8Ebu() : base("CRC-8/EBU", GetEngine())
        {
        }

        internal Crc8Ebu(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/EBU", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Ebu(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Ebu(alias); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x1D; reverse = 0xB8;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0xB8);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/GSM-A.
    /// </summary>
    public sealed class Crc8GsmA : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8GsmA class.
        /// </summary>
        public Crc8GsmA() : base("CRC-8/GSM-A", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/GSM-A", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8GsmA(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x1D;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x1D);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/GSM-B.
    /// </summary>
    public sealed class Crc8GsmB : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x49;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0xFF;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8GsmB class.
        /// </summary>
        public Crc8GsmB() : base("CRC-8/GSM-B", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/GSM-B", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8GsmB(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x49;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x49);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/HITAG.
    /// </summary>
    public sealed class Crc8Hitag : Crc
    {
        private const byte INIT = 0xFF;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Hitag class.
        /// </summary>
        public Crc8Hitag() : base("CRC-8/HITAG", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/HITAG", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Hitag(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x1D;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x1D);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/I-CODE.
    /// </summary>
    public sealed class Crc8ICode : Crc
    {
        private const byte INIT = 0xFD;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8ICode class.
        /// </summary>
        public Crc8ICode() : base("CRC-8/I-CODE", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/I-CODE", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8ICode(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x1D;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x1D);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/ITU. CRC-8/I-432-1, CRC-8/ATM(?).
    /// </summary>
    public sealed class Crc8Itu : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x07;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x55;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Itu class.
        /// </summary>
        public Crc8Itu() : base("CRC-8/ITU", GetEngine())
        {
        }

        internal Crc8Itu(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/ITU", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Itu(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Itu(alias); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x07;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x07);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/LTE.
    /// </summary>
    public sealed class Crc8Lte : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x9B;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Lte class.
        /// </summary>
        public Crc8Lte() : base("CRC-8/LTE", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/LTE", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Lte(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x9B;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x9B);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/MAXIM. CRC-8/MAXIM-DOW, DOW-CRC.
    /// </summary>
    public sealed class Crc8Maxim : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x31;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Maxim class.
        /// </summary>
        public Crc8Maxim() : base("CRC-8/MAXIM", GetEngine())
        {
        }

        internal Crc8Maxim(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/MAXIM", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Maxim(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Maxim(alias); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x31; reverse = 0x8C;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0x8C);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/MIFARE-MAD.
    /// </summary>
    public sealed class Crc8MifareMad : Crc
    {
        private const byte INIT = 0xC7;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8MifareMad class.
        /// </summary>
        public Crc8MifareMad() : base("CRC-8/MIFARE-MAD", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/MIFARE-MAD", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8MifareMad(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x1D;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x1D);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/NRSC-5.
    /// </summary>
    public sealed class Crc8Nrsc5 : Crc
    {
        private const byte INIT = 0xFF;
        private const byte POLY = 0x31;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Nrsc5 class.
        /// </summary>
        public Crc8Nrsc5() : base("CRC-8/NRSC-5", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/NRSC-5", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Nrsc5(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x31;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x31);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/OPENSAFETY.
    /// </summary>
    public sealed class Crc8Opensafety : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x2F;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Opensafety class.
        /// </summary>
        public Crc8Opensafety() : base("CRC-8/OPENSAFETY", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/OPENSAFETY", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Opensafety(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x2F;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x2F);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/ROHC.
    /// </summary>
    public sealed class Crc8Rohc : Crc
    {
        private const byte INIT = 0xFF;
        private const byte POLY = 0x07;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Rohc class.
        /// </summary>
        public Crc8Rohc() : base("CRC-8/ROHC", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/ROHC", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Rohc(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x07; reverse = 0xE0;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0xE0);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/SAE-J1850.
    /// </summary>
    public sealed class Crc8SaeJ1850 : Crc
    {
        private const byte INIT = 0xFF;
        private const byte POLY = 0x1D;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 8;
        private const byte XOROUT = 0xFF;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8SaeJ1850 class.
        /// </summary>
        public Crc8SaeJ1850() : base("CRC-8/SAE-J1850", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/SAE-J1850", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8SaeJ1850(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x1D;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateTable(0x1D);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-8/WCDMA.
    /// </summary>
    public sealed class Crc8Wcdma : Crc
    {
        private const byte INIT = 0x00;
        private const byte POLY = 0x9B;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 8;
        private const byte XOROUT = 0x00;
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc8Wcdma class.
        /// </summary>
        public Crc8Wcdma() : base("CRC-8/WCDMA", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-8/WCDMA", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc8Wcdma(); });
        }

        private static CrcEngine8 GetEngine()
        {
            //
            // poly = 0x9B; reverse = 0xD9;
            //
            if (_table == null)
            {
                _table = CrcEngine8.GenerateReversedTable(0xD9);
            }
            return new CrcEngine8(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}