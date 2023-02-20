/*
 * Copyright
 *
 * https://github.com/LokiHonoo/development-resources
 * Copyright (C) Loki Honoo 2015. All rights reserved.
 *
 * This code page is published under the terms of the MIT license.
 */

using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC algorithm helper.
    /// </summary>
    public static class CRCHelper
    {
        /// <summary>
        /// Try get CRC algorithm from mechanism.
        /// </summary>
        /// <param name="mechanism">CRC algorithm mechanism.</param>
        /// <param name="algorithm">CRC algorithm.</param>
        /// <returns></returns>
        public static bool TryGetAlgorithm(string mechanism, out CRC algorithm)
        {
            if (string.IsNullOrWhiteSpace(mechanism))
            {
                algorithm = null;
                return false;
            }
            mechanism = mechanism.Replace('_', '-').Replace('/', '-').ToUpperInvariant();
            switch (mechanism)
            {
                case "CRC-4-ITU": case "CRC4-ITU": case "CRC4ITU": algorithm = new CRC_4_ITU(); return true;
                case "CRC-5-EPC": case "CRC5-EPC": case "CRC5EPC": algorithm = new CRC_5_EPC(); return true;
                case "CRC-5-ITU": case "CRC5-ITU": case "CRC5ITU": algorithm = new CRC_5_ITU(); return true;
                case "CRC-5-USB": case "CRC5-USB": case "CRC5USB": algorithm = new CRC_5_USB(); return true;
                case "CRC-6-ITU": case "CRC6-ITU": case "CRC6ITU": algorithm = new CRC_6_ITU(); return true;
                case "CRC-7-MMC": case "CRC7-MMC": case "CRC7MMC": algorithm = new CRC_7_MMC(); return true;
                case "CRC-8": case "CRC8": algorithm = new CRC_8(); return true;
                case "CRC-8-ITU":
                case "CRC8-ITU":
                case "CRC8ITU":
                case "CRC-8-ATM":
                case "CRC8-ATM":
                case "CRC8ATM": algorithm = new CRC_8_ITU(); return true;
                case "CRC-8-MAXIM":
                case "CRC8-MAXIM":
                case "CRC8MAXIM":
                case "DOW-CRC":
                case "DOWCRC":
                case "CRC-8-IBUTTON":
                case "CRC8-IBUTTON":
                case "CRC8IBUTTON": algorithm = new CRC_8_MAXIM(); return true;
                case "CRC-8-ROHC": case "CRC8-ROHC": case "CRC8ROHC": algorithm = new CRC_8_ROHC(); return true;
                case "CRC-16-CCITT":
                case "CRC16-CCITT":
                case "CRC16CCITT":
                case "CRC-16-KERMIT":
                case "CRC16-KERMIT":
                case "CRC16KERMIT": algorithm = new CRC_16_CCITT(); return true;
                case "CRC-16-CCITT-FALSE":
                case "CRC16-CCITT-FALSE":
                case "CRC16CCITT-FALSE":
                case "CRC16CCITTFALSE": algorithm = new CRC_16_CCITT_FALSE(); return true;
                case "CRC-16-DNP": case "CRC16-DNP": case "CRC16DNP": algorithm = new CRC_16_DNP(); return true;
                case "CRC-16-IBM":
                case "CRC16-IBM":
                case "CRC16IBM":
                case "CRC-16-ARC":
                case "CRC16-ARC":
                case "CRC16ARC":
                case "CRC-16-LHA":
                case "CRC16-LHA":
                case "CRC16LHA": algorithm = new CRC_16_IBM(); return true;
                case "CRC-16-MAXIM": case "CRC16-MAXIM": case "CRC16MAXIM": algorithm = new CRC_16_MAXIM(); return true;
                case "CRC-16-MODBUS": case "CRC16-MODBUS": case "CRC16MODBUS": algorithm = new CRC_16_MODBUS(); return true;
                case "CRC-16-USB": case "CRC16-USB": case "CRC16USB": algorithm = new CRC_16_USB(); return true;
                case "CRC-16-X25": case "CRC16-X25": case "CRC16X25": algorithm = new CRC_16_X25(); return true;
                case "CRC-16-XMODEM":
                case "CRC16-XMODEM":
                case "CRC16XMODEM":
                case "CRC-16-ZMODEM":
                case "CRC16-ZMODEM":
                case "CRC16ZMODEM":
                case "CRC-16-ACORN":
                case "CRC16-ACORN":
                case "CRC16ACORN": algorithm = new CRC_16_XMODEM(); return true;
                case "CRC-16-XMODEM2": case "CRC16-XMODEM2": case "CRC16XMODEM2": algorithm = new CRC_16_XMODEM2(); return true;
                case "CRC-32":
                case "CRC32":
                case "CRC-32-ADCCP":
                case "CRC32-ADCCP":
                case "CRC32ADCCP": algorithm = new CRC_32(); return true;
                case "CRC-32-C": case "CRC32-C": case "CRC32C": algorithm = new CRC_32_C(); return true;
                case "CRC-32-KOOPMAN": case "CRC32-KOOPMAN": case "CRC32KOOPMAN": algorithm = new CRC_32_KOOPMAN(); return true;
                case "CRC-32-MPEG-2":
                case "CRC32-MPEG-2":
                case "CRC32MPEG-2":
                case "CRC-32-MPEG2":
                case "CRC32-MPEG2":
                case "CRC32MPEG2": algorithm = new CRC_32_MPEG2(); return true;
                case "CRC-64-ECMA": case "CR-64-ECMA": case "CRC64ECMA": algorithm = new CRC_64_ECMA(); return true;
                case "CRC-64-ISO": case "CRC64-ISO": case "CRC64ISO": algorithm = new CRC_64_ISO(); return true;
                default: algorithm = null; return false;
            }
        }
    }

    #region Root

    /// <summary>
    /// CRC algorithm.
    /// </summary>
    public abstract class CRC : IDisposable, IEquatable<CRC>
    {
        #region Properties

        private readonly int _checksumSize;
        private readonly string _name;
        private readonly int _width;

        /// <summary>
        /// Gets checksum size bits.
        /// </summary>
        public int ChecksumSize => _checksumSize;

        /// <summary>
        /// Gets algorithm name.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Gets width bits.
        /// </summary>
        public int Width => _width;

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CRC class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="checksumSize"></param>
        protected CRC(string name, int width, int checksumSize)
        {
            _name = name;
            _width = width;
            _checksumSize = checksumSize;
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        ~CRC()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        #endregion Construction

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        public abstract byte[] DoFinalBytes();

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] DoFinalBytes(byte[] bytes)
        {
            return DoFinalBytes(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] DoFinalBytes(byte[] bytes, int offset, int length)
        {
            Update(bytes, offset, length);
            return DoFinalBytes();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CRC other)
        {
            return _name.Equals(other._name);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals((CRC)obj);
        }

        /// <summary>
        /// Returns the hash code for this object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Return algorithm name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        public void Update(byte[] bytes)
        {
            Update(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public abstract void Update(byte[] bytes, int offset, int length);
    }

    #endregion Root

    #region CRC-4

    /// <summary>
    /// CRC-4.
    /// </summary>
    public abstract class CRC_4 : CRC
    {
        #region Properties

        private readonly byte _init;
        private readonly byte _poly;
        private readonly bool _reflect;
        private readonly byte _xorout;
        private byte _crc;
        private bool _disposed;

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CRC_4 class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reflect"></param>
        /// <param name="poly"></param>
        /// <param name="init"></param>
        /// <param name="xorout"></param>
        protected CRC_4(string name, bool reflect, byte poly, byte init, byte xorout) : base(name, 4, 8)
        {
            _reflect = reflect;
            _poly = poly;
            _init = init;
            _xorout = xorout;
            //
            _crc = init;
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // _table = null;
                }
                _disposed = true;
            }
        }

        #endregion Construction

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <returns></returns>
        public byte DoFinal()
        {
            byte crc = (byte)(_crc ^ _xorout);
            if (!_reflect)
            {
                crc = (byte)(crc >> 4);
            }
            _crc = _init;
            return crc;
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes)
        {
            return DoFinal(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes, int offset, int length)
        {
            Update(bytes, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        public override byte[] DoFinalBytes()
        {
            byte crc = DoFinal();
            return new byte[] { crc };
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public override void Reset()
        {
            _crc = _init;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public override void Update(byte[] bytes, int offset, int length)
        {
            if (_reflect)
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 1) == 1)
                        {
                            _crc = (byte)((_crc >> 1) ^ _poly);
                        }
                        else
                        {
                            _crc >>= 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 0x80) == 0x80)
                        {
                            _crc = (byte)((_crc << 1) ^ _poly);
                        }
                        else
                        {
                            _crc <<= 1;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// CRC-4-ITU. POLY=0x03, INIT=0x00, REFIN=true, REFOUT=true, XOROUT=0x00.
    /// <para/>(reverse POLY)>>(8-4).
    /// </summary>
    public sealed class CRC_4_ITU : CRC_4
    {
        /// <summary>
        /// Initializes a new instance of the CRC_4_ITU class.
        /// </summary>
        public CRC_4_ITU() : base("CRC-4-ITU", true, 0x0C, 0x00, 0x00)
        {
        }
    }

    #endregion CRC-4

    #region CRC-5

    /// <summary>
    /// CRC-5.
    /// </summary>
    public abstract class CRC_5 : CRC
    {
        #region Properties

        private readonly byte _init;
        private readonly byte _poly;
        private readonly bool _reflect;
        private readonly byte _xorout;
        private byte _crc;
        private bool _disposed;

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CRC_5 class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reflect"></param>
        /// <param name="poly"></param>
        /// <param name="init"></param>
        /// <param name="xorout"></param>
        protected CRC_5(string name, bool reflect, byte poly, byte init, byte xorout) : base(name, 5, 8)
        {
            _reflect = reflect;
            _poly = poly;
            _init = init;
            _xorout = xorout;
            //
            _crc = init;
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // _table = null;
                }
                _disposed = true;
            }
        }

        #endregion Construction

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <returns></returns>
        public byte DoFinal()
        {
            byte crc = (byte)(_crc ^ _xorout);
            if (!_reflect)
            {
                crc = (byte)(crc >> 3);
            }
            _crc = _init;
            return crc;
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes)
        {
            return DoFinal(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes, int offset, int length)
        {
            Update(bytes, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        public override byte[] DoFinalBytes()
        {
            byte crc = DoFinal();
            return new byte[] { crc };
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public override void Reset()
        {
            _crc = _init;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public override void Update(byte[] bytes, int offset, int length)
        {
            if (_reflect)
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 1) == 1)
                        {
                            _crc = (byte)((_crc >> 1) ^ _poly);
                        }
                        else
                        {
                            _crc >>= 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 0x80) == 0x80)
                        {
                            _crc = (byte)((_crc << 1) ^ _poly);
                        }
                        else
                        {
                            _crc <<= 1;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// CRC-5-EPC. POLY=0x09, INIT=0x09, REFIN=false, REFOUT=false, XOROUT=0x00.
    /// <para/>POLY<<(8-5).
    /// <para/>INIT<<(8-5).
    /// </summary>
    public sealed class CRC_5_EPC : CRC_5
    {
        /// <summary>
        /// Initializes a new instance of the CRC_5_EPC class.
        /// </summary>
        public CRC_5_EPC() : base("CRC-5-EPC", false, 0x48, 0x48, 0x00)
        {
        }
    }

    /// <summary>
    /// CRC-5-ITU. POLY=0x15, INIT=0x00, REFIN=true, REFOUT=true, XOROUT=0x00.
    /// <para/>(reverse POLY)>>(8-5).
    /// </summary>
    public sealed class CRC_5_ITU : CRC_5
    {
        /// <summary>
        /// Initializes a new instance of the CRC_5-ITU class.
        /// </summary>
        public CRC_5_ITU() : base("CRC-5-ITU", true, 0x15, 0x00, 0x00)
        {
        }
    }

    /// <summary>
    /// CRC-5-USB. POLY=0x05, INIT=0x1F, REFIN=true, REFOUT=true, XOROUT=0x1F.
    /// <para/>(reverse POLY)>>(8-5).
    /// <para/>INIT<<(8-5).
    /// </summary>
    public sealed class CRC_5_USB : CRC_5
    {
        /// <summary>
        /// Initializes a new instance of the CRC_5_USB class.
        /// </summary>
        public CRC_5_USB() : base("CRC-5-USB", true, 0x14, 0x1F, 0x1F)
        {
        }
    }

    #endregion CRC-5

    #region CRC-6

    /// <summary>
    /// CRC-6.
    /// </summary>
    public abstract class CRC_6 : CRC
    {
        #region Properties

        private readonly byte _init;
        private readonly byte _poly;
        private readonly bool _reflect;
        private readonly byte _xorout;
        private byte _crc;
        private bool _disposed;

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CRC_6 class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reflect"></param>
        /// <param name="poly"></param>
        /// <param name="init"></param>
        /// <param name="xorout"></param>
        protected CRC_6(string name, bool reflect, byte poly, byte init, byte xorout) : base(name, 6, 8)
        {
            _reflect = reflect;
            _poly = poly;
            _init = init;
            _xorout = xorout;
            //
            _crc = init;
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // _table = null;
                }
                _disposed = true;
            }
        }

        #endregion Construction

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <returns></returns>
        public byte DoFinal()
        {
            byte crc = (byte)(_crc ^ _xorout);
            if (!_reflect)
            {
                crc = (byte)(crc >> 2);
            }
            _crc = _init;
            return crc;
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes)
        {
            return DoFinal(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes, int offset, int length)
        {
            Update(bytes, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        public override byte[] DoFinalBytes()
        {
            byte crc = DoFinal();
            return new byte[] { crc };
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public override void Reset()
        {
            _crc = _init;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public override void Update(byte[] bytes, int offset, int length)
        {
            if (_reflect)
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 1) == 1)
                        {
                            _crc = (byte)((_crc >> 1) ^ _poly);
                        }
                        else
                        {
                            _crc >>= 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 0x80) == 0x80)
                        {
                            _crc = (byte)((_crc << 1) ^ _poly);
                        }
                        else
                        {
                            _crc <<= 1;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// CRC-6-ITU. POLY=0x03, INIT=0x00, REFIN=true, REFOUT=true, XOROUT=0x00.
    /// <para/>(reverse POLY)>>(8-6).
    /// </summary>
    public sealed class CRC_6_ITU : CRC_6
    {
        /// <summary>
        /// Initializes a new instance of the CRC_6_ITU class.
        /// </summary>
        public CRC_6_ITU() : base("CRC-6-ITU", true, 0x30, 0x00, 0x00)
        {
        }
    }

    #endregion CRC-6

    #region CRC-7

    /// <summary>
    /// CRC-7.
    /// </summary>
    public abstract class CRC_7 : CRC
    {
        #region Properties

        private readonly byte _init;
        private readonly byte _poly;
        private readonly bool _reflect;
        private readonly byte _xorout;
        private byte _crc;
        private bool _disposed;

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CRC_7 class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reflect"></param>
        /// <param name="poly"></param>
        /// <param name="init"></param>
        /// <param name="xorout"></param>
        protected CRC_7(string name, bool reflect, byte poly, byte init, byte xorout) : base(name, 7, 8)
        {
            _reflect = reflect;
            _poly = poly;
            _init = init;
            _xorout = xorout;
            //
            _crc = init;
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // _table = null;
                }
                _disposed = true;
            }
        }

        #endregion Construction

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <returns></returns>
        public byte DoFinal()
        {
            byte crc = (byte)(_crc ^ _xorout);
            if (!_reflect)
            {
                crc = (byte)(crc >> 1);
            }
            _crc = _init;
            return crc;
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes)
        {
            return DoFinal(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes, int offset, int length)
        {
            Update(bytes, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        public override byte[] DoFinalBytes()
        {
            byte crc = DoFinal();
            return new byte[] { crc };
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public override void Reset()
        {
            _crc = _init;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public override void Update(byte[] bytes, int offset, int length)
        {
            if (_reflect)
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 1) == 1)
                        {
                            _crc = (byte)((_crc >> 1) ^ _poly);
                        }
                        else
                        {
                            _crc >>= 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 0x80) == 0x80)
                        {
                            _crc = (byte)((_crc << 1) ^ _poly);
                        }
                        else
                        {
                            _crc <<= 1;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// CRC-7-MMC. POLY=0x09, INIT=0x00, REFIN=false, REFOUT=false, XOROUT=0x00.
    /// <para/>POLY<<(8-7).
    /// </summary>
    public sealed class CRC_7_MMC : CRC_7
    {
        /// <summary>
        /// Initializes a new instance of the CRC_7_MMC class.
        /// </summary>
        public CRC_7_MMC() : base("CRC-7-MMC", false, 0x12, 0x00, 0x00)
        {
        }
    }

    #endregion CRC-7

    #region CRC-8

    /// <summary>
    /// CRC-8.
    /// </summary>
    public class CRC_8 : CRC
    {
        #region Properties

        private readonly byte _init;
        private readonly byte _poly;
        private readonly bool _reflect;
        private readonly byte _xorout;
        private byte _crc;
        private bool _disposed;

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CRC_8 class.
        /// </summary>
        public CRC_8() : base("CRC-8", 8, 8)
        {
            _reflect = false;
            _poly = 0x07;
            _init = 0x00;
            _xorout = 0x00;
            //
            _crc = _init;
        }

        /// <summary>
        /// Initializes a new instance of the CRC_8 class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reflect"></param>
        /// <param name="poly"></param>
        /// <param name="init"></param>
        /// <param name="xorout"></param>
        protected CRC_8(string name, bool reflect, byte poly, byte init, byte xorout) : base(name, 8, 8)
        {
            _reflect = reflect;
            _poly = poly;
            _init = init;
            _xorout = xorout;
            //
            _crc = init;
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // _table = null;
                }
                _disposed = true;
            }
        }

        #endregion Construction

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <returns></returns>
        public byte DoFinal()
        {
            byte crc = (byte)(_crc ^ _xorout);
            _crc = _init;
            return crc;
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes)
        {
            return DoFinal(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte DoFinal(byte[] bytes, int offset, int length)
        {
            Update(bytes, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        public override byte[] DoFinalBytes()
        {
            byte crc = DoFinal();
            return new byte[] { crc };
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public override void Reset()
        {
            _crc = _init;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public override void Update(byte[] bytes, int offset, int length)
        {
            if (_reflect)
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 1) == 1)
                        {
                            _crc = (byte)((_crc >> 1) ^ _poly);
                        }
                        else
                        {
                            _crc >>= 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 0x80) == 0x80)
                        {
                            _crc = (byte)((_crc << 1) ^ _poly);
                        }
                        else
                        {
                            _crc <<= 1;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// CRC-8-ITU. CRC-8-ATM. POLY=0x07, INIT=0x00, REFIN=false, REFOUT=false, XOROUT=0x55.
    /// </summary>
    public sealed class CRC_8_ITU : CRC_8
    {
        /// <summary>
        /// Initializes a new instance of the CRC_8_ITU class.
        /// </summary>
        public CRC_8_ITU() : base("CRC-8-ITU", false, 0x07, 0x00, 0x55)
        {
        }
    }

    /// <summary>
    /// CRC-8-MAXIM. DOW-CRC. CRC-8-IBUTTON. POLY=0x31, INIT=0x00, REFIN=true, REFOUT=true, XOROUT=0x00.
    /// <para/>reverse POLY.
    /// </summary>
    public sealed class CRC_8_MAXIM : CRC_8
    {
        /// <summary>
        /// Initializes a new instance of the CRC_8_MAXIM class.
        /// </summary>
        public CRC_8_MAXIM() : base("CRC-8-MAXIM", true, 0x8C, 0x00, 0x00)
        {
        }
    }

    /// <summary>
    /// CRC-8-ROHC. POLY=0x07, INIT=0xFF, REFIN=true, REFOUT=true, XOROUT=0x00.
    /// <para/>reverse POLY.
    /// </summary>
    public sealed class CRC_8_ROHC : CRC_8
    {
        /// <summary>
        /// Initializes a new instance of the CRC_8_ROHC class.
        /// </summary>
        public CRC_8_ROHC() : base("CRC-8-ROHC", true, 0xE0, 0xFF, 0x00)
        {
        }
    }

    #endregion CRC-8

    #region CRC-16

    /// <summary>
    /// CRC-16.
    /// </summary>
    public abstract class CRC_16 : CRC
    {
        #region Properties

        private readonly ushort _init;
        private readonly ushort _poly;
        private readonly bool _reflect;
        private readonly ushort _xorout;
        private ushort _crc;
        private bool _disposed;

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CRC_16 class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reflect"></param>
        /// <param name="poly"></param>
        /// <param name="init"></param>
        /// <param name="xorout"></param>
        protected CRC_16(string name, bool reflect, ushort poly, ushort init, ushort xorout) : base(name, 16, 16)
        {
            _reflect = reflect;
            _poly = poly;
            _init = init;
            _xorout = xorout;
            //
            _crc = init;
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // _table = null;
                }
                _disposed = true;
            }
        }

        #endregion Construction

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <returns></returns>
        public ushort DoFinal()
        {
            ushort crc = (ushort)(_crc ^ _xorout);
            crc = (ushort)(crc << 8 | crc >> 8);
            _crc = _init;
            return crc;
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public ushort DoFinal(byte[] bytes)
        {
            return DoFinal(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public ushort DoFinal(byte[] bytes, int offset, int length)
        {
            Update(bytes, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        public override byte[] DoFinalBytes()
        {
            ushort crc = DoFinal();
            return BitConverter.GetBytes(crc);
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public override void Reset()
        {
            _crc = _init;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public override void Update(byte[] bytes, int offset, int length)
        {
            if (_reflect)
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 1) == 1)
                        {
                            _crc = (ushort)((_crc >> 1) ^ _poly);
                        }
                        else
                        {
                            _crc >>= 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= (ushort)(bytes[i] << 8);
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 0x8000) == 0x8000)
                        {
                            _crc = (ushort)((_crc << 1) ^ _poly);
                        }
                        else
                        {
                            _crc <<= 1;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// CRC-16-CCITT. CRC-16-KERMIT. POLY=0x1021, INIT=0x0000, REFIN=true, REFOUT=true, XOROUT=0x0000.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_16_CCITT : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_CCITT class.
        /// </summary>
        public CRC_16_CCITT() : base("CRC-16-CCITT", true, 0x8408, 0x0000, 0x0000)
        {
        }
    }

    /// <summary>
    /// CRC-16-CCITT-FALSE. POLY=0x1021, INIT=0xFFFF, REFIN=false, REFOUT=false, XOROUT=0x0000.
    /// </summary>
    public sealed class CRC_16_CCITT_FALSE : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_CCITT_FALSE class.
        /// </summary>
        public CRC_16_CCITT_FALSE() : base("CRC-16-CCITT-FALSE", false, 0x1021, 0xFFFF, 0x0000)
        {
        }
    }

    /// <summary>
    /// CRC-16-DNP. POLY=0x3D65, INIT=0x0000, REFIN=true, REFOUT=true, XOROUT=0xFFFF.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_16_DNP : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_DNP class.
        /// </summary>
        public CRC_16_DNP() : base("CRC-16-DNP", true, 0xA6BC, 0x0000, 0xFFFF)
        {
        }
    }

    /// <summary>
    /// CRC-16-IBM. CRC-16-ARC. CRC-16-LHA. POLY=0x8005, INIT=0x0000, REFIN=true, REFOUT=true, XOROUT=0x0000.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_16_IBM : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_IBM class.
        /// </summary>
        public CRC_16_IBM() : base("CRC-16-IBM", true, 0xA001, 0x0000, 0x0000)
        {
        }
    }

    /// <summary>
    /// CRC-16-MAXIM. POLY=0x8005, INIT=0x0000, REFIN=true, REFOUT=true, XOROUT=0xFFFF.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_16_MAXIM : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_MAXIM class.
        /// </summary>
        public CRC_16_MAXIM() : base("CRC-16-MAXIM", true, 0xA001, 0x0000, 0xFFFF)
        {
        }
    }

    /// <summary>
    /// CRC-16-MODBUS. POLY=0x8005, INIT=0xFFFF, REFIN=true, REFOUT=true, XOROUT=0x0000.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_16_MODBUS : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_MODBUS class.
        /// </summary>
        public CRC_16_MODBUS() : base("CRC-16-MODBUS", true, 0xA001, 0xFFFF, 0x0000)
        {
        }
    }

    /// <summary>
    /// CRC-16-USB. POLY=0x8005, INIT=0xFFFF, REFIN=true, REFOUT=true, XOROUT=0xFFFF.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_16_USB : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_USB class.
        /// </summary>
        public CRC_16_USB() : base("CRC-16-USB", true, 0xA001, 0xFFFF, 0xFFFF)
        {
        }
    }

    /// <summary>
    /// CRC-16-X25. POLY=0x1021, INIT=0xFFFF, REFIN=true, REFOUT=true, XOROUT=0xFFFF.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_16_X25 : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_X25 class.
        /// </summary>
        public CRC_16_X25() : base("CRC-16-X25", true, 0x8408, 0xFFFF, 0xFFFF)
        {
        }
    }

    /// <summary>
    /// CRC-16-XMODEM. CRC-16-ZMODEM. CRC-16-ACORN. POLY=0x1021, INIT=0x0000, REFIN=false, REFOUT=false, XOROUT=0x0000.
    /// </summary>
    public sealed class CRC_16_XMODEM : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_XMODEM class.
        /// </summary>
        public CRC_16_XMODEM() : base("CRC-16-XMODEM", false, 0x1021, 0x0000, 0x0000)
        {
        }
    }

    /// <summary>
    /// CRC-16-XMODEM2. POLY=0x8408, INIT=0x0000, REFIN=true, REFOUT=true, XOROUT=0x0000.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_16_XMODEM2 : CRC_16
    {
        /// <summary>
        /// Initializes a new instance of the CRC_16_XMODEM2 class.
        /// </summary>
        public CRC_16_XMODEM2() : base("CRC-16-XMODEM2", true, 0x1021, 0x0000, 0x0000)
        {
        }
    }

    #endregion CRC-16

    #region CRC-32

    /// <summary>
    /// CRC-32. CRC-32-ADCCP.
    /// </summary>
    public class CRC_32 : CRC
    {
        #region Properties

        private readonly uint _init;
        private readonly uint _poly;
        private readonly bool _reflect;
        private readonly uint _xorout;
        private uint _crc;
        private bool _disposed;

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CRC_32 class.
        /// </summary>
        public CRC_32() : base("CRC-32", 32, 32)
        {
            _reflect = true;
            _poly = 0xEDB88320; // reverse 0x04C11DB7
            _init = 0xFFFFFFFF;
            _xorout = 0xFFFFFFFF;
            //
            _crc = _init;
        }

        /// <summary>
        /// Initializes a new instance of the CRC_32 class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reflect"></param>
        /// <param name="poly"></param>
        /// <param name="init"></param>
        /// <param name="xorout"></param>
        protected CRC_32(string name, bool reflect, uint poly, uint init, uint xorout) : base(name, 32, 32)
        {
            _reflect = reflect;
            _poly = poly;
            _init = init;
            _xorout = xorout;
            //
            _crc = init;
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // _table = null;
                }
                _disposed = true;
            }
        }

        #endregion Construction

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <returns></returns>
        public uint DoFinal()
        {
            uint crc = _crc ^ _xorout;
            crc = crc << 24 | (crc & 0xFF00) << 8 | (crc >> 8) & 0xFF00 | crc >> 24;
            _crc = _init;
            return crc;
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public uint DoFinal(byte[] bytes)
        {
            return DoFinal(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public uint DoFinal(byte[] bytes, int offset, int length)
        {
            Update(bytes, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        public override byte[] DoFinalBytes()
        {
            uint crc = DoFinal();
            return BitConverter.GetBytes(crc);
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public override void Reset()
        {
            _crc = _init;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public override void Update(byte[] bytes, int offset, int length)
        {
            if (_reflect)
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 1) == 1)
                        {
                            _crc = (_crc >> 1) ^ _poly;
                        }
                        else
                        {
                            _crc >>= 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= (uint)(bytes[i] << 24);
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 0x80000000) == 0x80000000)
                        {
                            _crc = (_crc << 1) ^ _poly;
                        }
                        else
                        {
                            _crc <<= 1;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// CRC-32-C. POLY=0x1EDC6F41, INIT=0xFFFFFFFF, REFIN=true, REFOUT=true, XOROUT=0xFFFFFFFF.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_32_C : CRC_32
    {
        /// <summary>
        /// Initializes a new instance of the CRC_32_C class.
        /// </summary>
        public CRC_32_C() : base("CRC-32-C", true, 0x82F63B78, 0xFFFFFFFF, 0xFFFFFFFF)
        {
        }
    }

    /// <summary>
    /// CRC-32-KOOPMAN. POLY=0x741B8CD7, INIT=0xFFFFFFFF, REFIN=true, REFOUT=true, XOROUT=0xFFFFFFFF.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_32_KOOPMAN : CRC_32
    {
        /// <summary>
        /// Initializes a new instance of the CRC_32_KOOPMAN class.
        /// </summary>
        public CRC_32_KOOPMAN() : base("CRC-32-KOOPMAN", true, 0xEB31D82E, 0xFFFFFFFF, 0xFFFFFFFF)
        {
        }
    }

    /// <summary>
    /// CRC-32-MPEG2. POLY=0x04C11DB7, INIT=0xFFFFFFFF, REFIN=false, REFOUT=false, XOROUT=0x00000000.
    /// </summary>
    public sealed class CRC_32_MPEG2 : CRC_32
    {
        /// <summary>
        /// Initializes a new instance of the CRC_32_MPEG2 class.
        /// </summary>
        public CRC_32_MPEG2() : base("CRC-32-MPEG2", false, 0x04C11DB7, 0xFFFFFFFF, 0x00000000)
        {
        }
    }

    #endregion CRC-32

    #region CRC-64

    /// <summary>
    /// CRC-64.
    /// </summary>
    public abstract class CRC_64 : CRC
    {
        #region Properties

        private readonly ulong _init;
        private readonly ulong _poly;
        private readonly bool _reflect;
        private readonly ulong _xorout;
        private ulong _crc;
        private bool _disposed;

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CRC_64 class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reflect"></param>
        /// <param name="poly"></param>
        /// <param name="init"></param>
        /// <param name="xorout"></param>
        protected CRC_64(string name, bool reflect, ulong poly, ulong init, ulong xorout) : base(name, 64, 64)
        {
            _reflect = reflect;
            _poly = poly;
            _init = init;
            _xorout = xorout;
            //
            _crc = init;
        }

        /// <summary>
        /// Releases resources at the instance.
        /// </summary>
        /// <param name="disposing">Releases unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // _table = null;
                }
                _disposed = true;
            }
        }

        #endregion Construction

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <returns></returns>
        public ulong DoFinal()
        {
            ulong crc = _crc ^ _xorout;
            crc = crc << 56
                | (crc & 0xFF00) << 40
                | (crc & 0xFF0000) << 24
                | (crc & 0xFF000000) << 8
                | (crc >> 8) & 0xFF000000
                | (crc >> 24) & 0xFF0000
                | (crc >> 40) & 0xFF00
                | crc >> 56;
            _crc = _init;
            return crc;
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public ulong DoFinal(byte[] bytes)
        {
            return DoFinal(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public ulong DoFinal(byte[] bytes, int offset, int length)
        {
            Update(bytes, offset, length);
            return DoFinal();
        }

        /// <summary>
        /// Gets final crc value.
        /// </summary>
        public override byte[] DoFinalBytes()
        {
            ulong crc = DoFinal();
            return BitConverter.GetBytes(crc);
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public override void Reset()
        {
            _crc = _init;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public override void Update(byte[] bytes, int offset, int length)
        {
            if (_reflect)
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= bytes[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 1) == 1)
                        {
                            _crc = (_crc >> 1) ^ _poly;
                        }
                        else
                        {
                            _crc >>= 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < offset + length; i++)
                {
                    _crc ^= (ulong)(bytes[i] << 56);
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_crc & 0x8000000000000000) == 0x8000000000000000)
                        {
                            _crc = (_crc << 1) ^ _poly;
                        }
                        else
                        {
                            _crc <<= 1;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// CRC-64-ECMA. POLY=0x42F0E1EBA9EA3693, INIT=0xFFFFFFFFFFFFFFFF, REFIN=true, REFOUT=true, XOROUT=0xFFFFFFFFFFFFFFFF.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_64_ECMA : CRC_64
    {
        /// <summary>
        /// Initializes a new instance of the CRC_64_ECMA class.
        /// </summary>
        public CRC_64_ECMA() : base("CRC-64-ECMA", true, 0xC96C5795D7870F42, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)
        {
        }
    }

    /// <summary>
    /// CRC-64-ISO. POLY=0x000000000000001B, INIT=0xFFFFFFFFFFFFFFFF, REFIN=true, REFOUT=true, XOROUT=0xFFFFFFFFFFFFFFFF.
    /// <para/>(reverse POLY).
    /// </summary>
    public sealed class CRC_64_ISO : CRC_64
    {
        /// <summary>
        /// Initializes a new instance of the CRC_64_ISO class.
        /// </summary>
        public CRC_64_ISO() : base("CRC-64-ISO", true, 0xD800000000000000, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)
        {
        }
    }

    #endregion CRC-64
}