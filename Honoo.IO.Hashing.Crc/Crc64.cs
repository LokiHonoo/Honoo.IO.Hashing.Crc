namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-64, CRC-64/ECMA-182.
    /// </summary>
    public sealed class Crc64 : Crc
    {
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64 class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc64(bool withTable = true) : base(GetEngine("CRC-64", withTable))
        {
        }

        internal Crc64(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine64.GenerateTable(0x42F0E1EBA9EA3693);
                }
                return new CrcEngine64(algorithmName, 64, false, false, 0x42F0E1EBA9EA3693, 0x0000000000000000, 0x0000000000000000, _table);
            }
            else
            {
                return new CrcEngine64(algorithmName, 64, false, false, 0x42F0E1EBA9EA3693, 0x0000000000000000, 0x0000000000000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-64/GO-ISO.
    /// </summary>
    public sealed class Crc64GoIso : Crc
    {
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64GoIso class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc64GoIso(bool withTable = true) : base(GetEngine("CRC-64/GO-ISO", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x000000000000001B; reverse = 0xD800000000000000;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine64.GenerateReversedTable(0xD800000000000000);
                }
                return new CrcEngine64(algorithmName, 64, true, true, 0x000000000000001B, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine64(algorithmName, 64, true, true, 0x000000000000001B, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, false);
            }
        }
    }

    /// <summary>
    /// CRC-64/MS.
    /// </summary>
    public sealed class Crc64Ms : Crc
    {
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64Ms class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc64Ms(bool withTable = true) : base(GetEngine("CRC-64/MS", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x259C84CBA6426349; reverse = 0x92C64265D32139A4;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine64.GenerateReversedTable(0x92C64265D32139A4);
                }
                return new CrcEngine64(algorithmName, 64, true, true, 0x259C84CBA6426349, 0xFFFFFFFFFFFFFFFF, 0x0000000000000000, _table);
            }
            else
            {
                return new CrcEngine64(algorithmName, 64, true, true, 0x259C84CBA6426349, 0xFFFFFFFFFFFFFFFF, 0x0000000000000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-64/REDIS.
    /// </summary>
    public sealed class Crc64Redis : Crc
    {
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64Redis class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc64Redis(bool withTable = true) : base(GetEngine("CRC-64/REDIS", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0xAD93D23594C935A9 ; reverse = 0x95AC9329AC4BC9B5;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine64.GenerateReversedTable(0x95AC9329AC4BC9B5);
                }
                return new CrcEngine64(algorithmName, 64, true, true, 0xAD93D23594C935A9, 0x0000000000000000, 0x0000000000000000, _table);
            }
            else
            {
                return new CrcEngine64(algorithmName, 64, true, true, 0xAD93D23594C935A9, 0x0000000000000000, 0x0000000000000000, false);
            }
        }
    }

    /// <summary>
    /// CRC-64/WE.
    /// </summary>
    public sealed class Crc64We : Crc
    {
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64We class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc64We(bool withTable = true) : base(GetEngine("CRC-64/WE", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine64.GenerateTable(0x42F0E1EBA9EA3693UL);
                }
                return new CrcEngine64(algorithmName, 64, false, false, 0x42F0E1EBA9EA3693UL, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine64(algorithmName, 64, false, false, 0x42F0E1EBA9EA3693UL, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, false);
            }
        }
    }

    /// <summary>
    /// CRC-64/XZ, CRC-64/GO-ECMA.
    /// </summary>
    public sealed class Crc64Xz : Crc
    {
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64Xz class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc64Xz(bool withTable = true) : base(GetEngine("CRC-64/XZ", withTable))
        {
        }

        internal Crc64Xz(string alias, bool withTable = true) : base(GetEngine(alias, withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693; reverse = 0xC96C5795D7870F42;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine64.GenerateReversedTable(0xC96C5795D7870F42);
                }
                return new CrcEngine64(algorithmName, 64, true, true, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine64(algorithmName, 64, true, true, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, false);
            }
        }
    }
}