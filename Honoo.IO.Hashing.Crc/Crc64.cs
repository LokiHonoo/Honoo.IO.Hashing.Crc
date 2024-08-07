namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-64, CRC-64/ECMA-182.
    /// </summary>
    public sealed class Crc64 : Crc
    {
        private const ulong INIT = 0x0000000000000000;
        private const ulong POLY = 0x42F0E1EBA9EA3693;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 64;
        private const ulong XOROUT = 0x0000000000000000;
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64 class.
        /// </summary>
        public Crc64() : base("CRC-64", GetEngine())
        {
        }

        internal Crc64(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-64", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc64(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc64(alias); });
        }

        private static CrcEngine64 GetEngine()
        {
            //
            // poly = 0x42F0E1EBA9EA3693;
            //
            if (_table == null)
            {
                _table = CrcEngine64.GenerateTable(0x42F0E1EBA9EA3693);
            }
            return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-64/GO-ISO.
    /// </summary>
    public sealed class Crc64GoIso : Crc
    {
        private const ulong INIT = 0xFFFFFFFFFFFFFFFF;
        private const ulong POLY = 0x000000000000001B;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 64;
        private const ulong XOROUT = 0xFFFFFFFFFFFFFFFF;
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64GoIso class.
        /// </summary>
        public Crc64GoIso() : base("CRC-64/GO-ISO", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-64/GO-ISO", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc64GoIso(); });
        }

        private static CrcEngine64 GetEngine()
        {
            //
            // poly = 0x000000000000001B; reverse = 0xD800000000000000;
            //
            if (_table == null)
            {
                _table = CrcEngine64.GenerateReversedTable(0xD800000000000000);
            }
            return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-64/MS.
    /// </summary>
    public sealed class Crc64Ms : Crc
    {
        private const ulong INIT = 0xFFFFFFFFFFFFFFFF;
        private const ulong POLY = 0x259C84CBA6426349;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 64;
        private const ulong XOROUT = 0x0000000000000000;
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64Ms class.
        /// </summary>
        public Crc64Ms() : base("CRC-64/MS", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-64/MS", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc64Ms(); });
        }

        private static CrcEngine64 GetEngine()
        {
            //
            // poly = 0x259C84CBA6426349; reverse = 0x92C64265D32139A4;
            //
            if (_table == null)
            {
                _table = CrcEngine64.GenerateReversedTable(0x92C64265D32139A4);
            }
            return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-64/REDIS.
    /// </summary>
    public sealed class Crc64Redis : Crc
    {
        private const ulong INIT = 0x0000000000000000;
        private const ulong POLY = 0xAD93D23594C935A9;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 64;
        private const ulong XOROUT = 0x0000000000000000;
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64Redis class.
        /// </summary>
        public Crc64Redis() : base("CRC-64/REDIS", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-64/REDIS", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc64Redis(); });
        }

        private static CrcEngine64 GetEngine()
        {
            //
            // poly = 0xAD93D23594C935A9 ; reverse = 0x95AC9329AC4BC9B5;
            //
            if (_table == null)
            {
                _table = CrcEngine64.GenerateReversedTable(0x95AC9329AC4BC9B5);
            }
            return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-64/WE.
    /// </summary>
    public sealed class Crc64We : Crc
    {
        private const ulong INIT = 0xFFFFFFFFFFFFFFFF;
        private const ulong POLY = 0x42F0E1EBA9EA3693UL;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 64;
        private const ulong XOROUT = 0xFFFFFFFFFFFFFFFF;
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64We class.
        /// </summary>
        public Crc64We() : base("CRC-64/WE", GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-64/WE", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc64We(); });
        }

        private static CrcEngine64 GetEngine()
        {
            //
            // poly = 0x42F0E1EBA9EA3693;
            //
            if (_table == null)
            {
                _table = CrcEngine64.GenerateTable(0x42F0E1EBA9EA3693UL);
            }
            return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }

    /// <summary>
    /// CRC-64/XZ, CRC-64/GO-ECMA.
    /// </summary>
    public sealed class Crc64Xz : Crc
    {
        private const ulong INIT = 0xFFFFFFFFFFFFFFFF;
        private const ulong POLY = 0x42F0E1EBA9EA3693;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 64;
        private const ulong XOROUT = 0xFFFFFFFFFFFFFFFF;
        private static ulong[] _table;

        /// <summary>
        /// Initializes a new instance of the Crc64Xz class.
        /// </summary>
        public Crc64Xz() : base("CRC-64/XZ", GetEngine())
        {
        }

        internal Crc64Xz(string alias) : base(alias, GetEngine())
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName("CRC-64/XZ", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc64Xz(); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, () => { return new Crc64Xz(alias); });
        }

        private static CrcEngine64 GetEngine()
        {
            //
            // poly = 0x42F0E1EBA9EA3693; reverse = 0xC96C5795D7870F42;
            //
            if (_table == null)
            {
                _table = CrcEngine64.GenerateReversedTable(0xC96C5795D7870F42);
            }
            return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);
        }
    }
}