namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-13/BBC.
    /// </summary>
    public sealed class Crc13bbc : Crc
    {
        private static ushort[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc13bbc class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc13bbc(bool withTable = true) : base(GetEngine("CRC-13/BBC", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1CF5; <<(16-13) = 0xE7A8;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine16.GenerateTable(0xE7A8);
                }
                return new CrcEngine16(algorithmName, 13, false, false, _table, 0x0000, 0x0000);
            }
            else
            {
                return new CrcEngine16(algorithmName, 13, false, false, 0x1CF5, 0x0000, 0x0000);
            }
        }
    }
}