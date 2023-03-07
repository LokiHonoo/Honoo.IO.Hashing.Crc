namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-17/CAN-FD.
    /// </summary>
    public sealed class Crc17CanFd : Crc
    {
        private static uint[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc17CanFd class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc17CanFd(bool withTable = true) : base(GetEngine("CRC-17/CAN-FD", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x1685B; <<(32-17) = 0xB42D8000;
            //

            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngine32.GenerateTable(0xB42D8000);
                }
                return new CrcEngine32(algorithmName, 17, false, false, _table, 0x00000, 0x00000);
            }
            else
            {
                return new CrcEngine32(algorithmName, 17, false, false, 0x1685B, 0x00000, 0x00000);
            }
        }
    }
}