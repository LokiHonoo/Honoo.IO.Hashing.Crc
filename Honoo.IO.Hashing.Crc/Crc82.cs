namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-82/DARC.
    /// </summary>
    public sealed class Crc82Darc : Crc
    {
        private static readonly uint[] _init = new uint[3];
        private static readonly uint[] _poly = new uint[] { 0x308C, 0x01110114, 0x1440411 };
        private static readonly uint[] _reversedPoly = new uint[] { 0x22080, 0x8A00A202, 0x2200C430 };

        private static readonly uint[] _xorout = new uint[3];
        private static uint[][] _table;

        /// <summary>
        /// Initializes a new instance of the Crc82Darc class.
        /// </summary>
        /// <param name="withTable">Calculations with the table.</param>
        public Crc82Darc(bool withTable = true) : base(GetEngine("CRC-82/DARC", withTable))
        {
        }

        private static CrcEngine GetEngine(string algorithmName, bool withTable)
        {
            //
            // poly = 0x0308C0111011401440411; reverse >>(96-82) = 0x220808A00A2022200C430; (CrcEngineX 88-82, (CrcEngineX2 96-82))
            //
            if (withTable)
            {
                if (_table == null)
                {
                    _table = CrcEngineX2.GenerateReversedTable(_reversedPoly);
                }
                return new CrcEngineX2(algorithmName, 82, true, true, _table, _init, _xorout);
            }
            else
            {
                return new CrcEngineX2(algorithmName, 82, true, true, _poly, _init, _xorout);
            }
        }
    }
}