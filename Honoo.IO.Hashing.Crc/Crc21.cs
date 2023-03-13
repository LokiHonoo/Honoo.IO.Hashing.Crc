namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-21/CAN-FD.
    /// </summary>
    public sealed class Crc21CanFd : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc21CanFd class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc21CanFd(bool withTable = true) : base(GetEngine("CRC-21/CAN-FD", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x102899; <<(32-21) = 0x8144C800;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0x8144C800);
                }
                return new CrcEngine32(algorithmName, 21, false, false, 0x102899, 0x000000, 0x000000, _table);
            }
            else
            {
                return new CrcEngine32(algorithmName, 21, false, false, 0x102899, 0x000000, 0x000000, false);
            }
        }
    }
}