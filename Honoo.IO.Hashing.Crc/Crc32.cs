namespace Honoo.IO.HashingOld
{
    /// <summary>
    /// CRC-32. CRC-32/ADCCP.
    /// </summary>
    public sealed class Crc32 : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32 class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x04C11DB7; reverse = 0xEDB88320;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xEDB88320);
                }
                return new CrcEngine32("CRC-32", 32, true, true, _table, 0xFFFFFFFF, 0xFFFFFFFF);
            }
            else
            {
                return new CrcEngine32("CRC-32", 32, true, true, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-32/BZIP2.
    /// </summary>
    public sealed class Crc32BZip2 : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32BZip2 class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32BZip2(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x04C11DB7);
                }
                return new CrcEngine32("CRC-32/BZIP2", 32, false, false, _table, 0xFFFFFFFF, 0xFFFFFFFF);
            }
            else
            {
                return new CrcEngine32("CRC-32/BZIP2", 32, false, false, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-32C.
    /// </summary>
    public sealed class Crc32c : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32c class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32c(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x1EDC6F41; reverse = 0x82F63B78;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0x82F63B78);
                }
                return new CrcEngine32("CRC-32C", 32, true, true, _table, 0xFFFFFFFF, 0xFFFFFFFF);
            }
            else
            {
                return new CrcEngine32("CRC-32C", 32, true, true, 0x1EDC6F41, 0xFFFFFFFF, 0xFFFFFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-32D.
    /// </summary>
    public sealed class Crc32d : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32d class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32d(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0xA833982B; reverse = 0xD419CC15;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xD419CC15);
                }
                return new CrcEngine32("CRC-32D", 32, true, true, _table, 0xFFFFFFFF, 0xFFFFFFFF);
            }
            else
            {
                return new CrcEngine32("CRC-32D", 32, true, true, 0xA833982B, 0xFFFFFFFF, 0xFFFFFFFF);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32JamCrc(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x04C11DB7; reverse = 0xEDB88320;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xEDB88320);
                }
                return new CrcEngine32("CRC-32/JAMCRC", 32, true, true, _table, 0xFFFFFFFF, 0x00000000);
            }
            else
            {
                return new CrcEngine32("CRC-32/JAMCRC", 32, true, true, 0x04C11DB7, 0xFFFFFFFF, 0x00000000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32Koopman(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x741B8CD7; reverse = 0xEB31D82E;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateReversedTable(0xEB31D82E);
                }
                return new CrcEngine32("CRC-32/KOOPMAN", 32, true, true, _table, 0xFFFFFFFF, 0xFFFFFFFF);
            }
            else
            {
                return new CrcEngine32("CRC-32/KOOPMAN", 32, true, true, 0x741B8CD7, 0xFFFFFFFF, 0xFFFFFFFF);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32Mpeg2(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x04C11DB7);
                }
                return new CrcEngine32("CRC-32/MPEG-2", 32, false, false, _table, 0xFFFFFFFF, 0x00000000);
            }
            else
            {
                return new CrcEngine32("CRC-32/MPEG-2", 32, false, false, 0x04C11DB7, 0xFFFFFFFF, 0x00000000);
            }
        }
    }

    /// <summary>
    /// CRC-32/POSIX.
    /// </summary>
    public sealed class Crc32Posix : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32Posix class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32Posix(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x04C11DB7);
                }
                return new CrcEngine32("CRC-32/POSIX", 32, false, false, _table, 0x00000000, 0xFFFFFFFF);
            }
            else
            {
                return new CrcEngine32("CRC-32/POSIX", 32, false, false, 0x04C11DB7, 0x00000000, 0xFFFFFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-32Q.
    /// </summary>
    public sealed class Crc32q : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc32q class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32q(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x814141AB;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x814141AB);
                }
                return new CrcEngine32("CRC-32Q", 32, false, false, _table, 0x00000000, 0x00000000);
            }
            else
            {
                return new CrcEngine32("CRC-32Q", 32, false, false, 0x814141AB, 0x00000000, 0x00000000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32Sata(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x04C11DB7);
                }
                return new CrcEngine32("CRC-32/SATA", 32, false, false, _table, 0x52325032, 0x00000000);
            }
            else
            {
                return new CrcEngine32("CRC-32/SATA", 32, false, false, 0x04C11DB7, 0x52325032, 0x00000000);
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
        /// <param name="useTable">Calculations using the table.</param>
        public Crc32Xfer(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x000000AF;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x000000AF);
                }
                return new CrcEngine32("CRC-32/XFER", 32, false, false, _table, 0x00000000, 0x00000000);
            }
            else
            {
                return new CrcEngine32("CRC-32/XFER", 32, false, false, 0x000000AF, 0x00000000, 0x00000000);
            }
        }
    }
}