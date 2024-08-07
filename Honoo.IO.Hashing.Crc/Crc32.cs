namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-32, CRC-32/ISO-HDLC, CRC-32/ADCCP, CRC-32/V-42, CRC-32/XZ, PKZIP.
    /// </summary>
    public sealed class Crc32 : Crc
    {
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32 class.
        /// </summary>
        public Crc32() : base("CRC-32", GetEngine())
        {
        }

        internal Crc32(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32(alias); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x04C11DB7; reverse = 0xEDB88320;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateReversedTable(0xEDB88320);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32/AUTOSAR.
    /// </summary>
    public sealed class Crc32Autosar : Crc
    {
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0xF4ACFB13;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Autosar class.
        /// </summary>
        public Crc32Autosar() : base("CRC-32/AUTOSAR", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/AUTOSAR", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Autosar(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0xF4ACFB13; reverse = 0xC8DF352F;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateReversedTable(0xC8DF352F);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32/BZIP2, CRC-32/AAL5, CRC-32/DECT-B, B-CRC-32.
    /// </summary>
    public sealed class Crc32Bzip2 : Crc
    {
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Bzip2 class.
        /// </summary>
        public Crc32Bzip2() : base("CRC-32/BZIP2", GetEngine())
        {
        }

        internal Crc32Bzip2(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/BZIP2", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Bzip2(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Bzip2(alias); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x04C11DB7;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x04C11DB7);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32C, CRC-32/ISCSI, CRC-32/BASE91-C, CRC-32/CASTAGNOLI, CRC-32/INTERLAKEN.
    /// </summary>
    public sealed class Crc32c : Crc
    {
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x1EDC6F41;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32c class.
        /// </summary>
        public Crc32c() : base("CRC-32C", GetEngine())
        {
        }

        internal Crc32c(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32C", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32c(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32c(alias); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x1EDC6F41; reverse = 0x82F63B78;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateReversedTable(0x82F63B78);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32/CD-ROM-EDC.
    /// </summary>
    public sealed class Crc32CdromEdc : Crc
    {
        private const uint INIT = 0x00000000;
        private const uint POLY = 0x8001801B;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32CdromEdc class.
        /// </summary>
        public Crc32CdromEdc() : base("CRC-32/CD-ROM-EDC", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/CD-ROM-EDC", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32CdromEdc(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x8001801B; reverse = 0xD8018001;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateReversedTable(0xD8018001);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32/CKSUM, CKSUM, CRC-32/POSIX.
    /// </summary>
    public sealed class Crc32Cksum : Crc
    {
        private const uint INIT = 0x00000000;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Cksum class.
        /// </summary>
        public Crc32Cksum() : base("CRC-32/CKSUM", GetEngine())
        {
        }

        internal Crc32Cksum(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/CKSUM", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Cksum(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Cksum(alias); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x04C11DB7;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x04C11DB7);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32D, CRC-32/BASE91-D.
    /// </summary>
    public sealed class Crc32d : Crc
    {
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0xA833982B;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32d class.
        /// </summary>
        public Crc32d() : base("CRC-32D", GetEngine())
        {
        }

        internal Crc32d(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32D", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32d(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32d(alias); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0xA833982B; reverse = 0xD419CC15;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateReversedTable(0xD419CC15);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32/JAMCRC.
    /// </summary>
    public sealed class Crc32JamCrc : Crc
    {
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32JamCrc class.
        /// </summary>
        public Crc32JamCrc() : base("CRC-32/JAMCRC", GetEngine())
        {
        }

        internal Crc32JamCrc(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/JAMCRC", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32JamCrc(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32JamCrc(alias); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x04C11DB7; reverse = 0xEDB88320;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateReversedTable(0xEDB88320);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32/KOOPMAN.
    /// </summary>
    public sealed class Crc32Koopman : Crc
    {
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x741B8CD7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Koopman class.
        /// </summary>
        public Crc32Koopman() : base("CRC-32/KOOPMAN", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/KOOPMAN", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Koopman(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x741B8CD7; reverse = 0xEB31D82E;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateReversedTable(0xEB31D82E);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    ///  CRC-32/MEF.
    /// </summary>
    public sealed class Crc32Mef : Crc
    {
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x741B8CD7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Mef class.
        /// </summary>
        public Crc32Mef() : base("CRC-32/MEF", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/MEF", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Mef(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x741B8CD7; reverse = 0xEB31D82E;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateReversedTable(0xEB31D82E);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32/MPEG-2.
    /// </summary>
    public sealed class Crc32Mpeg2 : Crc
    {
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Mpeg2 class.
        /// </summary>
        public Crc32Mpeg2() : base("CRC-32/MPEG-2", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/MPEG-2", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Mpeg2(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x04C11DB7;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x04C11DB7);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32Q, CRC-32/AIXM.
    /// </summary>
    public sealed class Crc32q : Crc
    {
        private const uint INIT = 0x00000000;
        private const uint POLY = 0x814141AB;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32q class.
        /// </summary>
        public Crc32q() : base("CRC-32Q", GetEngine())
        {
        }

        internal Crc32q(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32Q", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32q(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32q(alias); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x814141AB;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x814141AB);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32/SATA.
    /// </summary>
    public sealed class Crc32Sata : Crc
    {
        private const uint INIT = 0x52325032;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Posix class.
        /// </summary>
        public Crc32Sata() : base("CRC-32/SATA", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/SATA", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Sata(); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x04C11DB7;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x04C11DB7);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-32/XFER.
    /// </summary>
    public sealed class Crc32Xfer : Crc
    {
        private const uint INIT = 0x00000000;
        private const uint POLY = 0x000000AF;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Xfer class.
        /// </summary>
        public Crc32Xfer() : base("CRC-32/XFER", GetEngine())
        {
        }

        internal Crc32Xfer(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-32/XFER", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Xfer(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc32Xfer(alias); });
        }

        private static CrcEngine32 GetEngine()
        {
            //
            // poly = 0x000000AF;
            //
            if (_table == null)
            {
                _table = CrcEngine32.GenerateTable(0x000000AF);
            }
            return new CrcEngine32(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}