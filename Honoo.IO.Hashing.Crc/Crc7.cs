namespace Honoo.IO.HashingOld
{
    /// <summary>
    /// CRC-7/MMC.
    /// </summary>
    public sealed class Crc7Mmc : Crc
    {
        private static byte[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc7Mmc class.
        /// </summary>
        /// <param name="useTable">Calculations using the table.</param>
        public Crc7Mmc(bool useTable = true) : base(GetEngine(useTable))
        {
        }

        private static CrcEngine GetEngine(bool useTable)
        {
            //
            // poly = 0x09; <<(8-7) = 0x12;
            //

            if (useTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine8.GenerateTable(0x12);
                }
                return new CrcEngine8("CRC-7/MMC", 7, false, false, _table, 0x00, 0x00);
            }
            else
            {
                return new CrcEngine8("CRC-7/MMC", 7, false, false, 0x09, 0x00, 0x00);
            }
        }
    }
}