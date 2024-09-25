namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-64, CRC-64/ECMA-182.
    /// </summary>
    public sealed class Crc64 : Crc
    {
        private const string DEFAULT_NAME = "CRC-64";
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
        public Crc64() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64 class.
        /// </summary>
        public Crc64(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc64(string alias, CrcTable withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc64(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc64(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine64.GenerateTable(0x42F0E1EBA9EA3693);
                    }
                    return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine64M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-64/GO-ISO.
    /// </summary>
    public sealed class Crc64GoIso : Crc
    {
        private const string DEFAULT_NAME = "CRC-64/GO-ISO";
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
        public Crc64GoIso() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64GoIso class.
        /// </summary>
        public Crc64GoIso(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc64GoIso(t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x000000000000001B; reverse = 0xD800000000000000;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine64.GenerateTableRef(0xD800000000000000);
                    }
                    return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine64M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-64/MS.
    /// </summary>
    public sealed class Crc64Ms : Crc
    {
        private const string DEFAULT_NAME = "CRC-64/MS";
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
        public Crc64Ms() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64Ms class.
        /// </summary>
        public Crc64Ms(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc64Ms(t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x259C84CBA6426349; reverse = 0x92C64265D32139A4;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine64.GenerateTableRef(0x92C64265D32139A4);
                    }
                    return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine64M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-64/REDIS.
    /// </summary>
    public sealed class Crc64Redis : Crc
    {
        private const string DEFAULT_NAME = "CRC-64/REDIS";
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
        public Crc64Redis() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64Redis class.
        /// </summary>
        public Crc64Redis(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc64Redis(t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0xAD93D23594C935A9 ; reverse = 0x95AC9329AC4BC9B5;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine64.GenerateTableRef(0x95AC9329AC4BC9B5);
                    }
                    return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine64M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-64/WE.
    /// </summary>
    public sealed class Crc64We : Crc
    {
        private const string DEFAULT_NAME = "CRC-64/WE";
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
        public Crc64We() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64We class.
        /// </summary>
        public Crc64We(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc64We(t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine64.GenerateTable(0x42F0E1EBA9EA3693UL);
                    }
                    return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine64M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }

    /// <summary>
    /// CRC-64/XZ, CRC-64/GO-ECMA.
    /// </summary>
    public sealed class Crc64Xz : Crc
    {
        private const string DEFAULT_NAME = "CRC-64/XZ";
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
        public Crc64Xz() : base(DEFAULT_NAME, GetEngine(CrcTable.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64Xz class.
        /// </summary>
        public Crc64Xz(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc64Xz(string alias, CrcTable withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc64Xz(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, REFIN, REFOUT, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), (t) => { return new Crc64Xz(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTable withTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693; reverse = 0xC96C5795D7870F42;
            //
            switch (withTable)
            {
                case CrcTable.Standard:
                    if (_table == null)
                    {
                        _table = CrcEngine64.GenerateTableRef(0xC96C5795D7870F42);
                    }
                    return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);

                case CrcTable.M16x: return new CrcEngine64M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);

                case CrcTable.None: default: return new CrcEngine64(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);
            }
        }
    }
}