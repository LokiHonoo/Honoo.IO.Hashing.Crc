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
        private static ulong[] _tableM16x;
        private static ulong[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc64 class.
        /// </summary>
        public Crc64() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64 class.
        /// </summary>
        public Crc64(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc64(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt64Value(POLY, WIDTH), new CrcUInt64Value(INIT, WIDTH), new CrcUInt64Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc64(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt64Value(POLY, WIDTH), new CrcUInt64Value(INIT, WIDTH), new CrcUInt64Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc64(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine64Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine64M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine64(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
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
        private static ulong[] _tableM16x;
        private static ulong[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc64GoIso class.
        /// </summary>
        public Crc64GoIso() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64GoIso class.
        /// </summary>
        public Crc64GoIso(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt64Value(POLY, WIDTH), new CrcUInt64Value(INIT, WIDTH), new CrcUInt64Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc64GoIso(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x000000000000001B; reverse = 0xD800000000000000;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine64Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine64M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine64(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
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
        private static ulong[] _tableM16x;
        private static ulong[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc64Ms class.
        /// </summary>
        public Crc64Ms() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64Ms class.
        /// </summary>
        public Crc64Ms(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt64Value(POLY, WIDTH), new CrcUInt64Value(INIT, WIDTH), new CrcUInt64Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc64Ms(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x259C84CBA6426349; reverse = 0x92C64265D32139A4;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine64Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine64M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine64(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
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
        private static ulong[] _tableM16x;
        private static ulong[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc64Redis class.
        /// </summary>
        public Crc64Redis() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64Redis class.
        /// </summary>
        public Crc64Redis(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt64Value(POLY, WIDTH), new CrcUInt64Value(INIT, WIDTH), new CrcUInt64Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc64Redis(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0xAD93D23594C935A9 ; reverse = 0x95AC9329AC4BC9B5;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine64Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine64M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine64(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
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
        private static ulong[] _tableM16x;
        private static ulong[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc64We class.
        /// </summary>
        public Crc64We() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64We class.
        /// </summary>
        public Crc64We(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt64Value(POLY, WIDTH), new CrcUInt64Value(INIT, WIDTH), new CrcUInt64Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc64We(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine64Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine64M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine64(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
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
        private static ulong[] _tableM16x;
        private static ulong[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc64Xz class.
        /// </summary>
        public Crc64Xz() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc64Xz class.
        /// </summary>
        public Crc64Xz(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc64Xz(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcUInt64Value(POLY, WIDTH), new CrcUInt64Value(INIT, WIDTH), new CrcUInt64Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc64Xz(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcUInt64Value(POLY, WIDTH), new CrcUInt64Value(INIT, WIDTH), new CrcUInt64Value(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc64Xz(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x42F0E1EBA9EA3693; reverse = 0xC96C5795D7870F42;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine64Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine64M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine64M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine64(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}