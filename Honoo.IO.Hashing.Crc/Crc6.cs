namespace Honoo.IO.HashingOld
{
    /// <summary>
    /// CRC-6/ITU.
    /// </summary>
    public sealed class Crc6Itu : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc6Itu class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc6Itu(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x03; reverse >>(8-6) = 0x30;
            //
            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateReversedTable(0x30);
                }
                return new CrcEngine8("CRC-6/ITU", 6, true, true, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-6/ITU", 6, true, true, 0x03, 0x00, 0x00);
            }
        }
    }
}