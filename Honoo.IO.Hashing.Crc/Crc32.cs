namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-32, CRC-32/ISO-HDLC, CRC-32/ADCCP, CRC-32/V-42, CRC-32/XZ, PKZIP.
    /// </summary>
    public sealed class Crc32 : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32(bool withTable = true) : base(GetEngine("CRC-32", withTable))
        {
        }

        internal Crc32(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x04C11DB7; reverse = 0xEDB88320;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xEDB88320);
                }
                return new CrcEngine32(algorithmName, 32, true, true, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, true, true, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF, false);
            }
        }
    }

    /// <summary>
    /// CRC-32/AUTOSAR.
    /// </summary>
    public sealed class Crc32Autosar : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Autosar class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32Autosar(bool withTable = true) : base(GetEngine("CRC-32/AUTOSAR", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0xF4ACFB13; reverse = 0xC8DF352F;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xC8DF352F);
                }
                return new CrcEngine32(algorithmName, 32, true, true, 0xF4ACFB13, 0xFFFFFFFF, 0xFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, true, true, 0xF4ACFB13, 0xFFFFFFFF, 0xFFFFFFFF, false);
            }
        }
    }

    /// <summary>
    /// CRC-32/BZIP2, CRC-32/AAL5, CRC-32/DECT-B, B-CRC-32.
    /// </summary>
    public sealed class Crc32Bzip2 : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Bzip2 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32Bzip2(bool withTable = true) : base(GetEngine("CRC-32/BZIP2", withTable))
        {
        }

        internal Crc32Bzip2(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x04C11DB7);
                }
                return new CrcEngine32(algorithmName, 32, false, false, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, false, false, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF, false);
            }
        }
    }

    /// <summary>
    /// CRC-32C, CRC-32/ISCSI, CRC-32/BASE91-C, CRC-32/CASTAGNOLI, CRC-32/INTERLAKEN.
    /// </summary>
    public sealed class Crc32c : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32c class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32c(bool withTable = true) : base(GetEngine("CRC-32C", withTable))
        {
        }

        internal Crc32c(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1EDC6F41; reverse = 0x82F63B78;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0x82F63B78);
                }
                return new CrcEngine32(algorithmName, 32, true, true, 0x1EDC6F41, 0xFFFFFFFF, 0xFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, true, true, 0x1EDC6F41, 0xFFFFFFFF, 0xFFFFFFFF, false);
            }
        }
    }

    /// <summary>
    /// CRC-32/CD-ROM-EDC.
    /// </summary>
    public sealed class Crc32CdromEdc : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32CdromEdc class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32CdromEdc(bool withTable = true) : base(GetEngine("CRC-32/CD-ROM-EDC", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x8001801B; reverse = 0xD8018001;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xD8018001);
                }
                return new CrcEngine32(algorithmName, 32, true, true, 0x8001801B, 0x00000000, 0x00000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, true, true, 0x8001801B, 0x00000000, 0x00000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-32/CKSUM, CKSUM, CRC-32/POSIX.
    /// </summary>
    public sealed class Crc32Cksum : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Cksum class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32Cksum(bool withTable = true) : base(GetEngine("CRC-32/CKSUM", withTable))
        {
        }

        internal Crc32Cksum(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x04C11DB7);
                }
                return new CrcEngine32(algorithmName, 32, false, false, 0x04C11DB7, 0x00000000, 0xFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, false, false, 0x04C11DB7, 0x00000000, 0xFFFFFFFF, false);
            }
        }
    }

    /// <summary>
    /// CRC-32D, CRC-32/BASE91-D.
    /// </summary>
    public sealed class Crc32d : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32d class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32d(bool withTable = true) : base(GetEngine("CRC-32D", withTable))
        {
        }

        internal Crc32d(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0xA833982B; reverse = 0xD419CC15;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xD419CC15);
                }
                return new CrcEngine32(algorithmName, 32, true, true, 0xA833982B, 0xFFFFFFFF, 0xFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, true, true, 0xA833982B, 0xFFFFFFFF, 0xFFFFFFFF, false);
            }
        }
    }

    /// <summary>
    /// CRC-32/JAMCRC.
    /// </summary>
    public sealed class Crc32JamCrc : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32JamCrc class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32JamCrc(bool withTable = true) : base(GetEngine("CRC-32/JAMCRC", withTable))
        {
        }

        internal Crc32JamCrc(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x04C11DB7; reverse = 0xEDB88320;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xEDB88320);
                }
                return new CrcEngine32(algorithmName, 32, true, true, 0x04C11DB7, 0xFFFFFFFF, 0x00000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, true, true, 0x04C11DB7, 0xFFFFFFFF, 0x00000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-32/KOOPMAN.
    /// </summary>
    public sealed class Crc32Koopman : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Koopman class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32Koopman(bool withTable = true) : base(GetEngine("CRC-32/KOOPMAN", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x741B8CD7; reverse = 0xEB31D82E;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xEB31D82E);
                }
                return new CrcEngine32(algorithmName, 32, true, true, 0x741B8CD7, 0xFFFFFFFF, 0xFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, true, true, 0x741B8CD7, 0xFFFFFFFF, 0xFFFFFFFF, false);
            }
        }
    }

    /// <summary>
    ///  CRC-32/MEF.
    /// </summary>
    public sealed class Crc32Mef : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Mef class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32Mef(bool withTable = true) : base(GetEngine("CRC-32/MEF", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x741B8CD7; reverse = 0xEB31D82E;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xEB31D82E);
                }
                return new CrcEngine32(algorithmName, 32, true, true, 0x741B8CD7, 0xFFFFFFFF, 0x00000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, true, true, 0x741B8CD7, 0xFFFFFFFF, 0x00000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-32/MPEG-2.
    /// </summary>
    public sealed class Crc32Mpeg2 : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Mpeg2 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32Mpeg2(bool withTable = true) : base(GetEngine("CRC-32/MPEG-2", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x04C11DB7);
                }
                return new CrcEngine32(algorithmName, 32, false, false, 0x04C11DB7, 0xFFFFFFFF, 0x00000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, false, false, 0x04C11DB7, 0xFFFFFFFF, 0x00000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-32Q, CRC-32/AIXM.
    /// </summary>
    public sealed class Crc32q : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32q class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32q(bool withTable = true) : base(GetEngine("CRC-32Q", withTable))
        {
        }

        internal Crc32q(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x814141AB;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x814141AB);
                }
                return new CrcEngine32(algorithmName, 32, false, false, 0x814141AB, 0x00000000, 0x00000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, false, false, 0x814141AB, 0x00000000, 0x00000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-32/SATA.
    /// </summary>
    public sealed class Crc32Sata : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Posix class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32Sata(bool withTable = true) : base(GetEngine("CRC-32/SATA", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x04C11DB7);
                }
                return new CrcEngine32(algorithmName, 32, false, false, 0x04C11DB7, 0x52325032, 0x00000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, false, false, 0x04C11DB7, 0x52325032, 0x00000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-32/XFER.
    /// </summary>
    public sealed class Crc32Xfer : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Xfer class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc32Xfer(bool withTable = true) : base(GetEngine("CRC-32/XFER", withTable))
        {
        }

        internal Crc32Xfer(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x000000AF;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x000000AF);
                }
                return new CrcEngine32(algorithmName, 32, false, false, 0x000000AF, 0x00000000, 0x00000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 32, false, false, 0x000000AF, 0x00000000, 0x00000000, false);
            }
        }
    }
}