using System;
using System.Security.Cryptography;

namespace Honoo.IO.Hashing
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

        #endregion Properties

        #region Construction

        internal Crc(CrcEngine engine)
        {
            _engine = engine;
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
                case "CRC-16-CCITT":
                case "CRC16-CCITT":
                case "CRC16CCITT":
                case "CRC-16-KERMIT":
                case "CRC16-KERMIT":
                case "CRC16KERMIT": return new Crc16Ccitt();
                case "CRC-16-CCITT-FALSE":
                case "CRC16-CCITT-FALSE":
                case "CRC16CCITT-FALSE":
                case "CRC16CCITTFALSE": return new Crc16CcittFalse();
                case "CRC-16-DNP": case "CRC16-DNP": case "CRC16DNP": return new Crc16Dnp();
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
                case "CRC-16-MODBUS": case "CRC16-MODBUS": case "CRC16MODBUS": return new Crc16Modbus();
                case "CRC-16-USB": case "CRC16-USB": case "CRC16USB": return new Crc16Usb();
                case "CRC-16-X25": case "CRC16-X25": case "CRC16X25": return new Crc16X25();
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
                case "CRC-32":
                case "CRC32":
                case "CRC-32-ADCCP":
                case "CRC32-ADCCP":
                case "CRC32ADCCP": return new Crc32();
                case "CRC-32-C": case "CRC32-C": case "CRC32C": return new Crc32c();
                case "CRC-32-KOOPMAN": case "CRC32-KOOPMAN": case "CRC32KOOPMAN": return new Crc32Koopman();
                case "CRC-32-MPEG-2":
                case "CRC32-MPEG-2":
                case "CRC32MPEG-2":
                case "CRC-32-MPEG2":
                case "CRC32-MPEG2":
                case "CRC32MPEG2": return new Crc32Mpeg2();
                case "CRC-64-ECMA": case "CR-64-ECMA": case "CRC64ECMA": return new Crc64Ecma();
                case "CRC-64-ISO": case "CRC64-ISO": case "CRC64ISO": return new Crc64Iso();
                default: throw new CryptographicException("Unsupported algorithm name.");
            }
        }

        /// <summary>
        /// Compute input crc value.
        /// </summary>
        /// <returns></returns>
        public byte[] DoFinal()
        {
            return _engine.DoFinal();
        }

        /// <summary>
        /// Compute input crc value.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte[] DoFinal(byte[] input)
        {
            return DoFinal(input, 0, input.Length);
        }

        /// <summary>
        /// Compute input crc value.
        /// </summary>
        /// <param name="buffer">Input buffer.</param>
        /// <param name="offset">Read start offset from buffer.</param>
        /// <param name="length">Read length from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte[] DoFinal(byte[] buffer, int offset, int length)
        {
            Update(buffer, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Reset.
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
            _engine.Update(buffer, offset, length);
        }
    }
}