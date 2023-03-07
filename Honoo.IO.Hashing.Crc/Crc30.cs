namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-30/CDMA.
    /// </summary>
    public sealed class Crc30Cdma : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc30Cdma class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc30Cdma(bool withTable = true) : base(GetEngine("CRC-30/CDMA", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x2030B9C7; <<(32-30) = 0x80C2E71C;
            // init = 0x3FFFFFFF; <<(32-30) = 0xFFFFFFFC;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x80C2E71C);
                }
                return new CrcEngine32(algorithmName, 30, false, false, _table, 0xFFFFFFFC, 0x3FFFFFFF);
            }
            else
            {
                return new CrcEngine32(algorithmName, 30, false, false, 0x2030B9C7, 0x3FFFFFFF, 0x3FFFFFFF);
            }
        }
    }
}