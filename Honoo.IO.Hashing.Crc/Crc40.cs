namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-40/GSM.
    /// </summary>
    public sealed class Crc40Gsm : Crc
    {
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc40Gsm class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc40Gsm(bool withTable = true) : base(GetEngine("CRC-40/GSM", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x0004820009; <<(64-40) = 0x4820009000000;
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine64.GenerateTable(0x4820009000000);
                }
                return new CrcEngine64(algorithmName, 40, false, false, 0x0004820009, 0x0000000000, 0xFFFFFFFFFF, _table);
            }
            else
            {
                return new CrcEngine64(algorithmName, 40, false, false, 0x0004820009, 0x0000000000, 0xFFFFFFFFFF, false);
            }
        }
    }
}