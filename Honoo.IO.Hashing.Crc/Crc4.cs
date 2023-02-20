namespace Honoo.IO.HashingOld
{
    /// <summary>
    /// CRC-4/ITU.
    /// </summary>
    public sealed class Crc4Itu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc4Itu class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc4Itu(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x03; reverse >>(8-4) = 0x0C;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x0C);
                }
                return new CrcEngine8("CRC-4/ITU", 4, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-4/ITU", 4, true, true, 0x03, 0x00, 0x00);
            }
        }
    }
}