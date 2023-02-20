using System;
using System.Security.Cryptography;

namespace Honoo.IO.HashingOld
{
    /// <summary>
    /// Represents the abstract base class from which all implementations of crc algorithms must inherit.
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
        /// Gets checksum size bits.
        /// </summary>
        public int ChecksumSize => _engine.ChecksumSize;

        /// <summary>
        /// Gets a value indicating whether the calculations using the table.
        /// </summary>
        public bool UseTable => _engine.UseTable;

        #endregion Properties

        #region Construction

        internal Crc(CrcEngine engine)
        {
            _engine = engine;
        }

        protected Crc()
        {
        }

        #endregion Construction

        /// <summary>
        /// Creates an instance of the specified implementation of a Crc algorithm.
        /// </summary>
        /// <param name="algorithmName">Crc algorithm name.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Crc Create(string algorithmName)
        {
            if (string.IsNullOrWhiteSpace(algorithmName))
            {
                throw new CryptographicException("Unsupported algorithm name.");
            }
            algorithmName = algorithmName.Replace('_', '-').Replace('/', '-').ToUpperInvariant();
            switch (algorithmName)
            {
                case "CRC-4-ITU": case "CRC4-ITU": case "CRC4ITU": return new Crc4Itu();
                case "CRC-5-EPC": case "CRC5-EPC": case "CRC5EPC": return new Crc5Epc();
                case "CRC-5-ITU": case "CRC5-ITU": case "CRC5ITU": return new Crc5Itu();
                case "CRC-5-USB": case "CRC5-USB": case "CRC5USB": return new Crc5Usb();
                case "CRC-6-ITU": case "CRC6-ITU": case "CRC6ITU": return new Crc6Itu();
                case "CRC-7-MMC": case "CRC7-MMC": case "CRC7MMC": return new Crc7Mmc();
                case "CRC-8": case "CRC8": return new Crc8();
                case "CRC-8-CDMA2000": case "CRC8-CDMA2000": case "CRC8CDMA2000": return new Crc8Cdma2000();
                case "CRC-8-DARC": case "CRC8-DARC": case "CRC8DARC": return new Crc8Darc();
                case "CRC-8-DVB-S2":
                case "CRC8-DVB-S2":
                case "CRC8DVB-S2":
                case "CRC-8DVB-S2":
                case "CRC-8DVBS2":
                case "CRC8DVBS2": return new Crc8DvbS2();
                case "CRC-8-EBU": case "CRC8-EBU": case "CRC8EBU": return new Crc8Ebu();
                case "CRC-8-I-CODE": case "CRC8-I-CODE": case "CRC8I-CODE": case "CRC8ICODE": return new Crc8ICode();
                case "CRC-8-ITU":
                case "CRC8-ITU":
                case "CRC8ITU":
                case "CRC-8-ATM":
                case "CRC8-ATM":
                case "CRC8ATM": return new Crc8Itu();
                case "CRC-8-MAXIM":
                case "CRC8-MAXIM":
                case "CRC8MAXIM":
                case "DOW-CRC":
                case "DOWCRC":
                case "CRC-8-IBUTTON":
                case "CRC8-IBUTTON":
                case "CRC8IBUTTON": return new Crc8Maxim();
                case "CRC-8-ROHC": case "CRC8-ROHC": case "CRC8ROHC": return new Crc8Rohc();
                case "CRC-A": case "CRCA": return new CrcA();
                case "CRC-16-AUG-CCITT":
                case "CRC16-AUG-CCITT":
                case "CRC16AUG-CCITT":
                case "CRC-16AUG-CCITT":
                case "CRC-16AUGCCITT":
                case "CRC16AUGCCITT": return new Crc16AugCcitt();
                case "CRC-16-BUYPASS": case "CRC16-BUYPASS": case "CRC16BUYPASS": return new Crc16BuyPass();
                case "CRC-16-CCITT":
                case "CRC16-CCITT":
                case "CRC16CCITT":
                case "CRC-16-KERMIT":
                case "CRC16-KERMIT":
                case "CRC16KERMIT": return new Crc16Ccitt();
                case "CRC-16-CCITT-FALSE":
                case "CRC16-CCITT-FALSE":
                case "CRC16CCITT-FALSE":
                case "CRC-16CCITT-FALSE":
                case "CRC-16CCITTFALSE":
                case "CRC16CCITTFALSE": return new Crc16CcittFalse();
                case "CRC-16-CDMA2000": case "CRC16-CDMA2000": case "CRC16CDMA2000": return new Crc16Cdma2000();
                case "CRC-16-DECT-R":
                case "CRC16-DECT-R":
                case "CRC16DECT-R":
                case "CRC-16DECT-R":
                case "CRC-16DECTR":
                case "CRC16DECTR": return new Crc16DectR();
                case "CRC-16-DECT-X":
                case "CRC16-DECT-X":
                case "CRC16DECT-X":
                case "CRC-16DECT-X":
                case "CRC-16DECTX":
                case "CRC16DECTX": return new Crc16DectX();
                case "CRC-16-DNP": case "CRC16-DNP": case "CRC16DNP": return new Crc16Dnp();
                case "CRC-16-DDS-110":
                case "CRC16-DDS-110":
                case "CRC16DDS-110":
                case "CRC-16DDS-110":
                case "CRC-16DDS110":
                case "CRC16DDS110": return new Crc16Dds110();
                case "CRC-16-EN-13757":
                case "CRC16-EN-13757":
                case "CRC16EN-13757":
                case "CRC-16EN-13757":
                case "CRC-16CEN13757":
                case "CRC16EN13757": return new Crc16En13757();
                case "CRC-16-GENIBUS": case "CRC16-GENIBUS": case "CRC16GENIBUS": return new Crc16Genibus();
                case "CRC-16-IBM":
                case "CRC16-IBM":
                case "CRC16IBM":
                case "CRC-16-ARC":
                case "CRC16-ARC":
                case "CRC16ARC":
                case "CRC-16-LHA":
                case "CRC16-LHA":
                case "CRC16LHA": return new Crc16Ibm();
                case "CRC-16-MAXIM": case "CRC16-MAXIM": case "CRC16MAXIM": return new Crc16Maxim();
                case "CRC-16-MCRF4XX": case "CRC16-MCRF4XX": case "CRC16MCRF4XX": return new Crc16Mcrf4XX();
                case "CRC-16-MODBUS": case "CRC16-MODBUS": case "CRC16MODBUS": return new Crc16Modbus();
                case "CRC-16-RIELLO": case "CRC16-RIELLO": case "CRC16RIELLO": return new Crc16Riello();
                case "CRC-16-T10-DIF":
                case "CRC16-T10-DIF":
                case "CRC16T10-DIF":
                case "CRC-16T10-DIF":
                case "CRC-16T10DIF":
                case "CRC16T10DIF": return new Crc16T10Dif();
                case "CRC-16-TELEDISK": case "CRC16-TELEDISK": case "CRC16TELEDISK": return new Crc16Teledisk();
                case "CRC-16-TMS37157": case "CRC16-TMS37157": case "CRC16TMS37157": return new Crc16Tms37157();
                case "CRC-16-USB": case "CRC16-USB": case "CRC16USB": return new Crc16Usb();
                case "CRC-16-X-25":
                case "CRC16-X-25":
                case "CRC16X-25":
                case "CRC-16-X25":
                case "CRC16-X25":
                case "CRC16X25": return new Crc16X25();
                case "CRC-16-XMODEM":
                case "CRC16-XMODEM":
                case "CRC16XMODEM":
                case "CRC-16-ZMODEM":
                case "CRC16-ZMODEM":
                case "CRC16ZMODEM":
                case "CRC-16-ACORN":
                case "CRC16-ACORN":
                case "CRC16ACORN": return new Crc16Xmodem();
                case "CRC-16-XMODEM2": case "CRC16-XMODEM2": case "CRC16XMODEM2": return new Crc16Xmodem2();
                case "CRC-32": case "CRC32": case "CRC-32-ADCCP": case "CRC32-ADCCP": case "CRC32ADCCP": return new Crc32();
                case "CRC-32-C": case "CRC32-C": case "CRC32C": return new Crc32c();
                case "CRC-32-D": case "CRC32-D": case "CRC32D": return new Crc32d();
                case "CRC-32-BZIP2": case "CRC32-BZIP2": case "CRC32BZIP2": return new Crc32BZip2();
                case "CRC-32-JAMCRC": case "CRC32-JAMCRC": case "CRC32JAMCRC": return new Crc32JamCrc();
                case "CRC-32-KOOPMAN": case "CRC32-KOOPMAN": case "CRC32KOOPMAN": return new Crc32Koopman();
                case "CRC-32-MPEG-2":
                case "CRC32-MPEG-2":
                case "CRC32MPEG-2":
                case "CRC-32-MPEG2":
                case "CRC32-MPEG2":
                case "CRC32MPEG2": return new Crc32Mpeg2();
                case "CRC-32-POSIX": case "CRC32-POSIX": case "CRC32POSIX": return new Crc32Posix();
                case "CRC-32-Q": case "CRC32-Q": case "CRC32Q": return new Crc32q();
                case "CRC-32-SATA": case "CRC32-SATA": case "CRC32SATA": return new Crc32Sata();
                case "CRC-32-XFER": case "CRC32-XFER": case "CRC32XFER": return new Crc32Xfer();
                case "CRC-64-ECMA":
                case "CR-64-ECMA":
                case "CRC64ECMA":
                case "CRC-64-XZ":
                case "CR-64-XZ":
                case "CRC64XZ": return new Crc64Ecma();
                case "CRC-64-ISO": case "CRC64-ISO": case "CRC64ISO": return new Crc64Iso();
                default: throw new CryptographicException("Unsupported algorithm name.");
            }
        }

        /// <summary>
        /// Compute input crc value and reset calculator. The return value may be "Byte", "UInt16", "UInt32", "UInt64", "Hex String".
        /// </summary>
        /// <returns></returns>
        public object DoFinal()
        {
            return _engine.DoFinal();
        }

        /// <summary>
        /// Compute input crc value and reset calculator. The return value may be "Byte", "UInt16", "UInt32", "UInt64", "Hex String".
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public object DoFinal(byte input)
        {
            Update(input);
            return DoFinal();
        }

        /// <summary>
        /// Compute input crc value and reset calculator. The return value may be "Byte", "UInt16", "UInt32", "UInt64", "Hex String".
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public object DoFinal(byte[] input)
        {
            Update(input);
            return DoFinal();
        }

        /// <summary>
        /// Compute input crc value and reset calculator. The return value may be "Byte", "UInt16", "UInt32", "UInt64", "Hex String".
        /// </summary>
        /// <param name="buffer">Input buffer.</param>
        /// <param name="offset">Read start offset from buffer.</param>
        /// <param name="length">Read length from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public object DoFinal(byte[] buffer, int offset, int length)
        {
            Update(buffer, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Compute input crc value and reset calculator.
        /// </summary>
        /// <param name="littleEndian">Output little endian bytes.</param>
        /// <returns></returns>
        public byte[] DoFinal(bool littleEndian)
        {
            return _engine.DoFinal(littleEndian);
        }

        /// <summary>
        /// Compute input crc value and reset calculator.
        /// </summary>
        /// <param name="littleEndian">Output little endian bytes.</param>
        /// <param name="input">Input.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte[] DoFinal(bool littleEndian, byte input)
        {
            Update(input);
            return DoFinal(littleEndian);
        }

        /// <summary>
        /// Compute input crc value and reset calculator.
        /// </summary>
        /// <param name="littleEndian">Output little endian bytes.</param>
        /// <param name="input">Input.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte[] DoFinal(bool littleEndian, byte[] input)
        {
            Update(input);
            return DoFinal(littleEndian);
        }

        /// <summary>
        /// Compute input crc value and reset calculator.
        /// </summary>
        /// <param name="littleEndian">Output little endian bytes.</param>
        /// <param name="buffer">Input buffer.</param>
        /// <param name="offset">Read start offset from buffer.</param>
        /// <param name="length">Read length from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte[] DoFinal(bool littleEndian, byte[] buffer, int offset, int length)
        {
            Update(buffer, offset, length);
            return DoFinal(littleEndian);
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