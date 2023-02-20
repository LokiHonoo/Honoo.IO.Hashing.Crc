namespace Honoo.IO.HashingOld
{
    /// <summary>
    /// CRC-64/ECMA, CRC-64/XZ.
    /// </summary>
    public sealed class Crc64Ecma : Crc
    {
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64Ecma class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc64Ecma(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693; reverse = 0xC96C5795D7870F42;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine64.GenerateReversedTable(0xC96C5795D7870F42);
                }
                return new CrcEngine64("CRC-64/ECMA", 64, true, true, _table, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF);
            }
            else
            {
                return new CrcEngine64("CRC-64/ECMA", 64, true, true, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF);
            }
        }
    }

    /// <summary>
    /// CRC-64/ISO.
    /// </summary>
    public sealed class Crc64Iso : Crc
    {
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64Iso class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc64Iso(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x000000000000001B; reverse = 0xD800000000000000;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine64.GenerateReversedTable(0xD800000000000000);
                }
                return new CrcEngine64("CRC-64/ISO", 64, true, true, _table, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF);
            }
            else
            {
                return new CrcEngine64("CRC-64/ISO", 64, true, true, 0x000000000000001B, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF);
            }
        }
    }
}