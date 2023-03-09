using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Represents the abstract base class from which all implementations of crc algorithms must inherit.
    /// <br/>Catalogue of parametrised CRC algorithms: https://reveng.sourceforge.io/crc-catalogue/all.htm .
    /// </summary>
    public abstract class Crc
    {
        #region Properties

        private readonly CrcEngine _engine;

        /// <summary>
        /// Gets algorithm name.
        /// </summary>
        public string AlgorithmName => _engine.AlgorithmName;

        /// <summary>
        /// Gets checksum byte length.
        /// </summary>
        public int ChecksumLength => _engine.ChecksumLength;

        /// <summary>
        /// Gets checksum size bits.
        /// </summary>
        public int ChecksumSize => _engine.ChecksumSize;

        /// <summary>
        /// Gets a value indicating whether the calculations with the table.
        /// </summary>
        public bool WithTable => _engine.WithTable;

        #endregion Properties

        #region Construction

        internal Crc(CrcEngine engine)
        {
            _engine = engine;
        }

        #endregion Construction

        /// <summary>
        /// Creates an instance of the algorithm by algorithm name.
        /// </summary>
        /// <param name="algorithmName">Crc algorithm name.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Crc Create(string algorithmName, bool withTable = true)
        {
            if (string.IsNullOrWhiteSpace(algorithmName))
            {
                throw new ArgumentException("Unsupported algorithm name.", nameof(algorithmName));
            }
            algorithmName = algorithmName.Replace("_", null).Replace("-", null).Replace("/", null).Replace("\\", null).Trim().ToUpperInvariant();
            switch (algorithmName)
            {
                case "CRC3GSM": return new Crc3Gsm(withTable);
                case "CRC3ROHC": return new Crc3Rohc(withTable);
                case "CRC4ITU": return new Crc4Itu(withTable);
                case "CRC4G704": return new Crc4Itu("CRC-4/G-704", withTable);
                case "CRC4INTERLAKEN": return new Crc4Interlaken(withTable);
                case "CRC5EPC": return new Crc5Epc(withTable);
                case "CRC5EPCC1G2": return new Crc5Epc("CRC-5/EPC-C1G2", withTable);
                case "CRC5ITU": return new Crc5Itu(withTable);
                case "CRC5G704": return new Crc5Itu("CRC-5/G-704", withTable);
                case "CRC5USB": return new Crc5Usb(withTable);
                case "CRC6CDMA2000A": return new Crc6Cdma2000A(withTable);
                case "CRC6CDMA2000B": return new Crc6Cdma2000B(withTable);
                case "CRC6DARC": return new Crc6Darc(withTable);
                case "CRC6GSM": return new Crc6Gsm(withTable);
                case "CRC6ITU": return new Crc6Itu(withTable);
                case "CRC6G704": return new Crc6Itu("CRC-6/G-704", withTable);
                case "CRC7": return new Crc7(withTable);
                case "CRC7MMC": return new Crc7("CRC-7/MMC", withTable);
                case "CRC7ROHC": return new Crc7Rohc(withTable);
                case "CRC7UMTS": return new Crc7Umts(withTable);
                case "CRC8": return new Crc8(withTable);
                case "CRC8SMBUS": return new Crc8("CRC-8/SMBUS", withTable);
                case "CRC8OPENSAFETY": return new Crc8Opensafety(withTable);
                case "CRC8SAEJ1850": return new Crc8SaeJ1850(withTable);
                case "CRC8WCDMA": return new Crc8Wcdma(withTable);
                case "CRC8BLUETOOTH": return new Crc8Bluetooth(withTable);
                case "CRC8AUTOSAR": return new Crc8Autosar(withTable);
                case "CRC8CDMA2000": return new Crc8Cdma2000(withTable);
                case "CRC8NRSC5": return new Crc8Nrsc5(withTable);
                case "CRC8LTE": return new Crc8Lte(withTable);
                case "CRC8DARC": return new Crc8Darc(withTable);
                case "CRC8DVBS2": return new Crc8DvbS2(withTable);
                case "CRC8HITAG": return new Crc8Hitag(withTable);
                case "CRC8GSMA": return new Crc8GsmA(withTable);
                case "CRC8GSMB": return new Crc8GsmB(withTable);
                case "CRC8EBU": return new Crc8Ebu(withTable);
                case "CRC8TECH3250": return new Crc8Ebu("CRC-8/TECH-3250", withTable);
                case "CRC8AES": return new Crc8Ebu("CRC-8/AES", withTable);
                case "CRC8ICODE": return new Crc8ICode(withTable);
                case "CRC8MIFAREMAD": return new Crc8MifareMad(withTable);
                case "CRC8ITU": return new Crc8Itu(withTable);
                case "CRC8I4321": return new Crc8Itu("CRC-8/I-432-1", withTable);
                case "CRC8ATM": return new Crc8Itu("CRC-8/ATM", withTable);
                case "CRC8MAXIM": return new Crc8Maxim(withTable);
                case "CRC8MAXIMDOW": return new Crc8Maxim("CRC-8/MAXIM-DOW", withTable);
                case "DOWCRC": return new Crc8Maxim("DOW-CRC", withTable);
                case "CRC8ROHC": return new Crc8Rohc(withTable);
                case "CRC10": return new Crc10(withTable);
                case "CRC10ATM": return new Crc10("CRC-10/ATM", withTable);
                case "CRC10I610": return new Crc10("CRC-10/I-610", withTable);
                case "CRC10CDMA2000": return new Crc10Cdma2000(withTable);
                case "CRC10GSM": return new Crc10Gsm(withTable);
                case "CRC11": return new Crc11(withTable);
                case "CRC11FLEXRAY": return new Crc11("CRC-11/FLEXRAY", withTable);
                case "CRC11UMTS": return new Crc11Umts(withTable);
                case "CRC12CDMA2000": return new Crc12Cdma2000(withTable);
                case "CRC12DECT": return new Crc12Dect(withTable);
                case "XCRC12": return new Crc12Dect("X-CRC-12", withTable);
                case "CRC12GSM": return new Crc12Gsm(withTable);
                case "CRC12UMTS": return new Crc12Umts(withTable);
                case "CRC123GPP": return new Crc12Umts("CRC-12/3GPP", withTable);
                case "CRC13BBC": return new Crc13bbc(withTable);
                case "CRC14DARC": return new Crc14Darc(withTable);
                case "CRC14GSM": return new Crc14Gsm(withTable);
                case "CRC15": return new Crc15(withTable);
                case "CRC15CAN": return new Crc15("CRC-15/CAN", withTable);
                case "CRC15MPT1327": return new Crc15Mpt1327(withTable);
                case "CRC16": return new Crc16(withTable);
                case "CRC16ARC": return new Crc16("CRC-16/ARC", withTable);
                case "ARC": return new Crc16("ARC", withTable);
                case "CRC16LHA": return new Crc16("CRC-16/LHA", withTable);
                case "CRCIBM": return new Crc16("CRC-IBM", withTable);
                case "CRCA": return new CrcA(withTable);
                case "CRC16CMS": return new Crc16Cms(withTable);
                case "CRC16ISOIEC144433A": return new CrcA("CRC-16/ISO-IEC-14443-3-A", withTable);
                case "CRC16SPIFUJITSU": return new Crc16SpiFujitsu(withTable);
                case "CRC16AUGCCITT": return new Crc16SpiFujitsu("CRC-16/AUG-CCITT", withTable);
                case "CRC16UMTS": return new Crc16Umts(withTable);
                case "CRC16BUYPASS": return new Crc16Umts("CRC-16/BUYPASS", withTable);
                case "CRC16VERIFONE": return new Crc16Umts("CRC-16/VERIFONE", withTable);
                case "CRC16CCITT": return new Crc16Ccitt(withTable);
                case "CRCCCITT": return new Crc16Ccitt("CRC-CCITT", withTable);
                case "CRC16CCITTTRUE": return new Crc16Ccitt("CRC-16/CCITT-TRUE", withTable);
                case "CRC16KERMIT": return new Crc16Ccitt("CRC-16/KERMIT", withTable);
                case "KERMIT": return new Crc16Ccitt("KERMIT", withTable);
                case "CRC16BLUETOOTH": return new Crc16Ccitt("CRC-16/BLUETOOTH", withTable);
                case "CRC16V41LSB": return new Crc16Ccitt("CRC-16/V-41-LSB", withTable);
                case "CRC16CCITTFALSE": return new Crc16CcittFalse(withTable);
                case "CRC16IBM3740": return new Crc16CcittFalse("CRC-16/IBM-3740", withTable);
                case "CRC16AUTOSAR": return new Crc16CcittFalse("CRC-16/AUTOSAR", withTable);
                case "CRC16CDMA2000": return new Crc16Cdma2000(withTable);
                case "CRC16DECTR": return new Crc16DectR(withTable);
                case "RCRC16": return new Crc16DectR("R-CRC-16", withTable);
                case "CRC16DECTX": return new Crc16DectX(withTable);
                case "XCRC16": return new Crc16DectX("X-CRC-16", withTable);
                case "CRC16DNP": return new Crc16Dnp(withTable);
                case "CRC16DDS110": return new Crc16Dds110(withTable);
                case "CRC16EN13757": return new Crc16En13757(withTable);
                case "CRC16GENIBUS": return new Crc16Genibus(withTable);
                case "CRC16DARC": return new Crc16Genibus("CRC-16/DARC", withTable);
                case "CRC16EPC": return new Crc16Genibus("CRC-16/EPC", withTable);
                case "CRC16EPCC1G2": return new Crc16Genibus("CRC-16/EPC-C1G2", withTable);
                case "CRC16ICODE": return new Crc16Genibus("CRC-16/I-CODE", withTable);
                case "CRC16M17": return new Crc16M17(withTable);
                case "CRC16GSM": return new Crc16Gsm(withTable);
                case "CRC16LJ1200": return new Crc16Lj1200(withTable);
                case "CRC16MAXIM": return new Crc16Maxim(withTable);
                case "CRC16MAXIMDOW": return new Crc16Maxim("CRC-16/MAXIM-DOW", withTable);
                case "CRC16MCRF4XX": return new Crc16Mcrf4XX(withTable);
                case "CRC16MODBUS": return new Crc16Modbus(withTable);
                case "MODBUS": return new Crc16Modbus("MODBUS", withTable);
                case "CRC16PROFIBUS": return new Crc16Profibus(withTable);
                case "CRC16IEC611582": return new Crc16Profibus("CRC-16/IEC-61158-2", withTable);
                case "CRC16NRSC5": return new Crc16Nrsc5(withTable);
                case "CRC16OPENSAFETYA": return new Crc16OpensafetyA(withTable);
                case "CRC16OPENSAFETYB": return new Crc16OpensafetyB(withTable);
                case "CRC16RIELLO": return new Crc16Riello(withTable);
                case "CRC16T10DIF": return new Crc16T10Dif(withTable);
                case "CRC16TELEDISK": return new Crc16Teledisk(withTable);
                case "CRC16TMS37157": return new Crc16Tms37157(withTable);
                case "CRC16USB": return new Crc16Usb(withTable);
                case "CRC16X25": return new Crc16X25(withTable);
                case "X25": return new Crc16X25("X-25", withTable);
                case "CRC16IBMSDLC": return new Crc16X25("CRC-16/IBM-SDLC", withTable);
                case "CRC16ISOHDLC": return new Crc16X25("CRC-16/ISO-HDLC", withTable);
                case "CRC16ISOIEC144433B": return new Crc16X25("CRC-16/ISO-IEC-14443-3-B", withTable);
                case "CRCB": return new Crc16X25("CRC-B", withTable);
                case "CRC16XMODEM": return new Crc16Xmodem(withTable);
                case "XMODEM": return new Crc16Xmodem("XMODEM", withTable);
                case "CRC16ZMODEM": return new Crc16Xmodem("CRC-16/ZMODEM", withTable);
                case "ZMODEM": return new Crc16Xmodem("ZMODEM", withTable);
                case "CRC16ACORN": return new Crc16Xmodem("CRC-16/ACORN", withTable);
                case "CRC16LTE": return new Crc16Xmodem("CRC-16/LTE", withTable);
                case "CRC16V41MSB": return new Crc16Xmodem("CRC-16/V-41-MSB", withTable);
                case "CRC16XMODEM2": return new Crc16Xmodem2(withTable);
                case "CRC17CANFD": return new Crc17CanFd(withTable);
                case "CRC21CANFD": return new Crc21CanFd(withTable);
                case "CRC24": return new Crc24(withTable);
                case "CRC24OPENPGP": return new Crc24("CRC-24/OPENPGP", withTable);
                case "CRC24BLE": return new Crc24Ble(withTable);
                case "CRC24INTERLAKEN": return new Crc24Interlaken(withTable);
                case "CRC24FLEXRAYA": return new Crc24FlexrayA(withTable);
                case "CRC24FLEXRAYB": return new Crc24FlexrayB(withTable);
                case "CRC24LTEA": return new Crc24LteA(withTable);
                case "CRC24LTEB": return new Crc24LteB(withTable);
                case "CRC24OS9": return new Crc24Os9(withTable);
                case "CRC30CDMA": return new Crc30Cdma(withTable);
                case "CRC31PHILIPS": return new Crc31Philips(withTable);
                case "CRC32": return new Crc32(withTable);
                case "CRC32ISOHDLC": return new Crc32("CRC-32/ISO-HDLC", withTable);
                case "CRC32V42": return new Crc32("CRC-32/V-42", withTable);
                case "CRC32XZ": return new Crc32("CRC-32/XZ", withTable);
                case "PKZIP": return new Crc32("PKZIP", withTable);
                case "CRC32ADCCP": return new Crc32("CRC-32/ADCCP", withTable);
                case "CRC32AUTOSAR": return new Crc32Autosar(withTable);
                case "CRC32C": return new Crc32c(withTable);
                case "CRC32ISCSI": return new Crc32c("CRC-32/ISCSI", withTable);
                case "CRC32BASE91C": return new Crc32c("CRC-32/BASE91-C", withTable);
                case "CRC32CASTAGNOLI": return new Crc32c("CRC-32/CASTAGNOLI", withTable);
                case "CRC32INTERLAKEN": return new Crc32c("CRC-32/INTERLAKEN", withTable);
                case "CRC32CDROMEDC": return new Crc32CdromEdc(withTable);
                case "CRC32D": return new Crc32d(withTable);
                case "CRC32BASE91D": return new Crc32d("CRC-32/BASE91-D", withTable);
                case "CRC32BZIP2": return new Crc32Bzip2(withTable);
                case "CRC32AAL5": return new Crc32Bzip2("CRC-32/AAL5", withTable);
                case "CRC32DECTB": return new Crc32Bzip2("CRC-32/DECT-B", withTable);
                case "BCRC32": return new Crc32Bzip2("B-CRC-32", withTable);
                case "CRC32JAMCRC": return new Crc32JamCrc(withTable);
                case "JAMCRC": return new Crc32JamCrc("JAMCRC", withTable);
                case "CRC32KOOPMAN": return new Crc32Koopman(withTable);
                case "CRC32MEF": return new Crc32Mef(withTable);
                case "CRC32MPEG2": return new Crc32Mpeg2(withTable);
                case "CRC32CKSUM": return new Crc32Cksum(withTable);
                case "CKSUM": return new Crc32Cksum("CKSUM", withTable);
                case "CRC32POSIX": return new Crc32Cksum("CRC-32/POSIX", withTable);
                case "CRC32Q": return new Crc32q(withTable);
                case "CRC32AIXM": return new Crc32q("CRC-32/AIXM", withTable);
                case "CRC32SATA": return new Crc32Sata(withTable);
                case "CRC32XFER": return new Crc32Xfer(withTable);
                case "XFER": return new Crc32Xfer("XFER", withTable);
                case "CRC40GSM": return new Crc40Gsm(withTable);
                case "CRC64": return new Crc64(withTable);
                case "CRC64ECMA182": return new Crc64("CRC-64/ECMA-182", withTable);
                case "CRC64GOISO": return new Crc64GoIso(withTable);
                case "CRC64MS": return new Crc64Ms(withTable);
                case "CRC64REDIS": return new Crc64Redis(withTable);
                case "CRC64WE": return new Crc64We(withTable);
                case "CRC64XZ": return new Crc64Xz(withTable);
                case "CRC64GOECMA": return new Crc64Xz("CRC-64/GO-ECMA", withTable);
                case "CRC82DARC": return new Crc82Darc(withTable);
                default: throw new ArgumentException("Unsupported algorithm name.", nameof(algorithmName));
            }
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 8.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int checksumSize, bool refin, bool refout, byte poly, byte init, byte xorout, bool withTable = true)
        {
            return new CrcCustom(checksumSize, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 16.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int checksumSize, bool refin, bool refout, ushort poly, ushort init, ushort xorout, bool withTable = true)
        {
            return new CrcCustom(checksumSize, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 32.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int checksumSize, bool refin, bool refout, uint poly, uint init, uint xorout, bool withTable = true)
        {
            return new CrcCustom(checksumSize, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are between 0 - 64.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="poly">Polynomials value.</param>
        /// <param name="init">Initialization value.</param>
        /// <param name="xorout">Output xor value.</param>
        /// <param name="withTable">Calculations with the table.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int checksumSize, bool refin, bool refout, ulong poly, ulong init, ulong xorout, bool withTable = true)
        {
            return new CrcCustom(checksumSize, refin, refout, poly, init, xorout, withTable);
        }

        /// <summary>
        /// Creates an instance of the algorithm by custom parameters.
        /// </summary>
        /// <param name="checksumSize">Checkum size bits. The allowed values are more than 0.</param>
        /// <param name="refin">Reflects input value.</param>
        /// <param name="refout">Reflects output value.</param>
        /// <param name="polyHex">Polynomials value hex string.</param>
        /// <param name="initHex">Initialization value hex string.</param>
        /// <param name="xoroutHex">Output xor value hex string.</param>
        /// <param name="core">Use the specified CRC calculation core.</param>
        /// <exception cref="Exception"></exception>
        public static Crc Create(int checksumSize, bool refin, bool refout, string polyHex, string initHex, string xoroutHex, CrcCore core = CrcCore.Auto)
        {
            return new CrcCustom(checksumSize, refin, refout, polyHex, initHex, xoroutHex, core);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator. The return value is "Hex String".
        /// </summary>
        /// <returns></returns>
        public string DoFinal()
        {
            return _engine.DoFinal();
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// </summary>
        /// <param name="littleEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        public byte[] DoFinal(bool littleEndian)
        {
            return _engine.DoFinal(littleEndian);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Write to output buffer and return checksum byte length.
        /// </summary>
        /// <param name="littleEndian">Specifies the type of endian for output.</param>
        /// <param name="output">Output buffer.</param>
        /// <param name="offset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int DoFinal(bool littleEndian, byte[] output, int offset)
        {
            return _engine.DoFinal(littleEndian, output, offset);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a value indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool DoFinal(out byte checksum)
        {
            return _engine.DoFinal(out checksum);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a value indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool DoFinal(out ushort checksum)
        {
            return _engine.DoFinal(out checksum);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a value indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool DoFinal(out uint checksum)
        {
            return _engine.DoFinal(out checksum);
        }

        /// <summary>
        /// Calculates checksum and reset the calculator.
        /// <br/>Output checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// Return a value indicating whether the checksum is truncated.
        /// </summary>
        /// <param name="checksum">Checksum value.</param>
        /// <returns></returns>
        public bool DoFinal(out ulong checksum)
        {
            return _engine.DoFinal(out checksum);
        }

        /// <summary>
        /// Reset calculator.
        /// </summary>
        public void Reset()
        {
            _engine.Reset();
        }

        /// <summary>
        /// Update input.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <exception cref="Exception"></exception>
        public void Update(byte input)
        {
            _engine.Update(input);
        }

        /// <summary>
        /// Update input.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <exception cref="Exception"></exception>
        public void Update(byte[] input)
        {
            Update(input, 0, input.Length);
        }

        /// <summary>
        /// Update input.
        /// </summary>
        /// <param name="buffer">Input buffer.</param>
        /// <param name="offset">Read start offset from buffer.</param>
        /// <param name="length">Read length from buffer.</param>
        /// <exception cref="Exception"></exception>
        public void Update(byte[] buffer, int offset, int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            for (int i = offset; i < offset + length; i++)
            {
                Update(buffer[i]);
            }
        }
    }
}