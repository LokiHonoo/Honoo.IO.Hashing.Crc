namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC-32, CRC-32/ISO-HDLC, CRC-32/ADCCP, CRC-32/V-42, CRC-32/XZ, PKZIP.
    /// </summary>
    public sealed class Crc32 : Crc
    {
        private const string DEFAULT_NAME = "CRC-32";
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32 class.
        /// </summary>
        public Crc32() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32 class.
        /// </summary>
        public Crc32(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc32(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x04C11DB7; reverse = 0xEDB88320;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32/AUTOSAR.
    /// </summary>
    public sealed class Crc32Autosar : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/AUTOSAR";
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0xF4ACFB13;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32Autosar class.
        /// </summary>
        public Crc32Autosar() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32Autosar class.
        /// </summary>
        public Crc32Autosar(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Autosar(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0xF4ACFB13; reverse = 0xC8DF352F;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN   );
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN   );
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32/BZIP2, CRC-32/AAL5, CRC-32/DECT-B, B-CRC-32.
    /// </summary>
    public sealed class Crc32Bzip2 : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/BZIP2";
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32Bzip2 class.
        /// </summary>
        public Crc32Bzip2() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32Bzip2 class.
        /// </summary>
        public Crc32Bzip2(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc32Bzip2(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Bzip2(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Bzip2(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32C, CRC-32/ISCSI, CRC-32/BASE91-C, CRC-32/CASTAGNOLI, CRC-32/INTERLAKEN.
    /// </summary>
    public sealed class Crc32c : Crc
    {
        private const string DEFAULT_NAME = "CRC-32C";
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x1EDC6F41;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32c class.
        /// </summary>
        public Crc32c() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32c class.
        /// </summary>
        public Crc32c(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc32c(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32c(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32c(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x1EDC6F41; reverse = 0x82F63B78;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32/CD-ROM-EDC.
    /// </summary>
    public sealed class Crc32CdromEdc : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/CD-ROM-EDC";
        private const uint INIT = 0x00000000;
        private const uint POLY = 0x8001801B;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32CdromEdc class.
        /// </summary>
        public Crc32CdromEdc() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32CdromEdc class.
        /// </summary>
        public Crc32CdromEdc(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32CdromEdc(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x8001801B; reverse = 0xD8018001;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN   );
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32/CKSUM, CKSUM, CRC-32/POSIX.
    /// </summary>
    public sealed class Crc32Cksum : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/CKSUM";
        private const uint INIT = 0x00000000;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32Cksum class.
        /// </summary>
        public Crc32Cksum() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32Cksum class.
        /// </summary>
        public Crc32Cksum(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc32Cksum(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Cksum(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Cksum(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32D, CRC-32/BASE91-D.
    /// </summary>
    public sealed class Crc32d : Crc
    {
        private const string DEFAULT_NAME = "CRC-32D";
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0xA833982B;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32d class.
        /// </summary>
        public Crc32d() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32d class.
        /// </summary>
        public Crc32d(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc32d(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32d(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32d(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0xA833982B; reverse = 0xD419CC15;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32/JAMCRC.
    /// </summary>
    public sealed class Crc32JamCrc : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/JAMCRC";
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32JamCrc class.
        /// </summary>
        public Crc32JamCrc() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32JamCrc class.
        /// </summary>
        public Crc32JamCrc(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc32JamCrc(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32JamCrc(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32JamCrc(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x04C11DB7; reverse = 0xEDB88320;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN   );
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32/KOOPMAN.
    /// </summary>
    public sealed class Crc32Koopman : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/KOOPMAN";
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x741B8CD7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0xFFFFFFFF;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32Koopman class.
        /// </summary>
        public Crc32Koopman() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32Koopman class.
        /// </summary>
        public Crc32Koopman(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Koopman(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x741B8CD7; reverse = 0xEB31D82E;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    ///  CRC-32/MEF.
    /// </summary>
    public sealed class Crc32Mef : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/MEF";
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x741B8CD7;
        private const bool REFIN = true;
        private const bool REFOUT = true;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32Mef class.
        /// </summary>
        public Crc32Mef() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32Mef class.
        /// </summary>
        public Crc32Mef(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Mef(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x741B8CD7; reverse = 0xEB31D82E;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32/MPEG-2.
    /// </summary>
    public sealed class Crc32Mpeg2 : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/MPEG-2";
        private const uint INIT = 0xFFFFFFFF;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32Mpeg2 class.
        /// </summary>
        public Crc32Mpeg2() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32Mpeg2 class.
        /// </summary>
        public Crc32Mpeg2(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Mpeg2(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32Q, CRC-32/AIXM.
    /// </summary>
    public sealed class Crc32q : Crc
    {
        private const string DEFAULT_NAME = "CRC-32Q";
        private const uint INIT = 0x00000000;
        private const uint POLY = 0x814141AB;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32q class.
        /// </summary>
        public Crc32q() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32q class.
        /// </summary>
        public Crc32q(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc32q(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32q(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32q(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x814141AB;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32/SATA.
    /// </summary>
    public sealed class Crc32Sata : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/SATA";
        private const uint INIT = 0x52325032;
        private const uint POLY = 0x04C11DB7;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32Posix class.
        /// </summary>
        public Crc32Sata() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32Sata class.
        /// </summary>
        public Crc32Sata(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Sata(t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x04C11DB7;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }

    /// <summary>
    /// CRC-32/XFER.
    /// </summary>
    public sealed class Crc32Xfer : Crc
    {
        private const string DEFAULT_NAME = "CRC-32/XFER";
        private const uint INIT = 0x00000000;
        private const uint POLY = 0x000000AF;
        private const bool REFIN = false;
        private const bool REFOUT = false;
        private const int WIDTH = 32;
        private const uint XOROUT = 0x00000000;
        private static uint[] _tableM16x;
        private static uint[] _tableStandard;

        /// <summary>
        /// Initializes a new instance of the Crc32Xfer class.
        /// </summary>
        public Crc32Xfer() : base(DEFAULT_NAME, GetEngine(CrcTableInfo.Standard))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Crc32Xfer class.
        /// </summary>
        public Crc32Xfer(CrcTableInfo withTable) : base(DEFAULT_NAME, GetEngine(withTable))
        {
        }

        internal Crc32Xfer(string alias, CrcTableInfo withTable) : base(alias, GetEngine(withTable))
        {
        }

        internal static CrcName GetAlgorithmName()
        {
            return new CrcName(DEFAULT_NAME, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Xfer(t); });
        }

        internal static CrcName GetAlgorithmName(string alias)
        {
            return new CrcName(alias, WIDTH, new CrcParameter(POLY, WIDTH), new CrcParameter(INIT, WIDTH), new CrcParameter(XOROUT, WIDTH), REFIN, REFOUT, (t) => { return new Crc32Xfer(alias, t); });
        }

        private static CrcEngine GetEngine(CrcTableInfo withTable)
        {
            //
            // poly = 0x000000AF;
            //
            switch (withTable)
            {
                case CrcTableInfo.Standard:
                    if (_tableStandard == null)
                    {
                        _tableStandard = CrcEngine32Standard.GenerateTable(WIDTH, POLY, REFIN);
                    }
                    return new CrcEngine32Standard(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableStandard);

                case CrcTableInfo.M16x:
                    if (_tableM16x == null)
                    {
                        _tableM16x = CrcEngine32M16x.GenerateTable(WIDTH, POLY, REFIN               );
                    }
                    return new CrcEngine32M16x(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT, _tableM16x);

                default: return new CrcEngine32(WIDTH, POLY, INIT, XOROUT, REFIN, REFOUT);
            }
        }
    }
}