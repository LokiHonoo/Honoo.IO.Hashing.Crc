namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-31/PHILIPS.
    /// </summary>
    public sealed class Crc31Philips : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc31Philips class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc31Philips(bool withTable = true) : base(GetEngine("CRC-31/PHILIPS", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x04C11DB7; <<(32-31) = 0x09823B6E;
            // init = 0x7FFFFFFF; <<(32-31) = 0xFFFFFFFC;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x09823B6E);
                }
                return new CrcEngine32(algorithmName, 31, false, false, _table, 0xFFFFFFFE, 0x7FFFFFFF);
            }
            else
            {
                return new CrcEngine32(algorithmName, 31, false, false, 0x04C11DB7, 0x7FFFFFFF, 0x7FFFFFFF);
            }
        }
    }
}