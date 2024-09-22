using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC Parameter.
    /// </summary>
    public sealed class CrcParameter : IEquatable<CrcParameter>
    {
        #region Enum

        private enum ValueType
        { UInt8, UInt16, UInt32, UInt64, BinaryString, HexString }

        #endregion Enum

        #region Members

        private readonly object _value;
        private readonly ValueType _valueType;
        private readonly int _width;

        /// <summary>
        /// Gets crc width in bits.
        /// </summary>
        public int Width => _width;

        #endregion Members

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CrcParameter class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="width">Truncated the value to the specifies crc width.</param>
        public CrcParameter(byte value, int width)
        {
            _valueType = ValueType.UInt8;
            value = (byte)(value << width);
            value = (byte)(value >> width);
            _value = value;
            _width = width;
        }

        /// <summary>
        /// Initializes a new instance of the CrcParameter class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="width">Truncated the value to the specifies crc width.</param>
        public CrcParameter(ushort value, int width)
        {
            _valueType = ValueType.UInt16;
            value = (ushort)(value << width);
            value = (ushort)(value >> width);
            _value = value;
            _width = width;
        }

        /// <summary>
        /// Initializes a new instance of the CrcParameter class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="width">Truncated the value to the specifies crc width.</param>
        public CrcParameter(uint value, int width)
        {
            _valueType = ValueType.UInt32;
            value <<= width;
            value >>= width;
            _value = value;
            _width = width;
        }

        /// <summary>
        /// Initializes a new instance of the CrcParameter class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="width">Truncated the value to the specifies crc width.</param>
        public CrcParameter(ulong value, int width)
        {
            _valueType = ValueType.UInt64;
            value <<= width;
            value >>= width;
            _value = value;
            _width = width;
        }

        /// <summary>
        /// Initializes a new instance of the CrcParameter class.
        /// </summary>
        /// <param name="format">Specifies the type of format for value.</param>
        /// <param name="value">Value string. String must binary(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55).</param>
        /// <param name="width">Truncated the value to the specifies crc width.</param>
        /// <exception cref="Exception"></exception>
        public CrcParameter(NumericsStringFormat format, string value, int width)
        {
            switch (format)
            {
                case NumericsStringFormat.Binary: _valueType = ValueType.BinaryString; break;
                case NumericsStringFormat.Hex: _valueType = ValueType.HexString; break;
                default: throw new ArgumentException("Invalid NumericsStringFormat value.", nameof(format));
            }
            _value = CrcConverter.ToString(format, value, width, format);
            _width = width;
        }

        #endregion Construction

        /// <summary>
        /// Determines whether the specified <see cref="CrcParameter"/> is equal to the current <see cref="CrcParameter"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CrcParameter other)
        {
            return other != null && _valueType == other._valueType && _value == other._value;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is CrcParameter other && Equals(other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _valueType.GetHashCode() ^ _value.GetHashCode();
        }

        /// <summary>
        /// Gets binary <see cref="string"/> value of converted.
        /// </summary>
        /// <returns></returns>
        public string ToBinaryString()
        {
            switch (_valueType)
            {
                case ValueType.UInt8: return Convert.ToString((byte)_value, 2);
                case ValueType.UInt16: return Convert.ToString((ushort)_value, 2);
                case ValueType.UInt32: return Convert.ToString((uint)_value, 2);
                case ValueType.UInt64: return Convert.ToString((long)(ulong)_value, 2);
                case ValueType.BinaryString: return (string)_value;
                case ValueType.HexString: return CrcConverter.ToString(NumericsStringFormat.Hex, (string)_value, null, NumericsStringFormat.Binary);
                default: throw new ArithmeticException($"CRC parameter's type is {_value.GetType()}.");
            }
        }

        /// <summary>
        /// Gets <see cref="byte"/> value. Throw <see cref="Exception"/> if value's type is not <see cref="byte"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte ToByte()
        {
            switch (_valueType)
            {
                case ValueType.UInt8: return (byte)_value;
                default: throw new ArithmeticException($"CRC parameter's type is {_value.GetType()}.");
            }
        }

        /// <summary>
        /// Gets hex <see cref="string"/> value of converted.
        /// </summary>
        /// <returns></returns>
        public string ToHexString()
        {
            switch (_valueType)
            {
                case ValueType.UInt8: return Convert.ToString((byte)_value, 16);
                case ValueType.UInt16: return Convert.ToString((ushort)_value, 16);
                case ValueType.UInt32: return Convert.ToString((uint)_value, 16);
                case ValueType.UInt64: return Convert.ToString((long)(ulong)_value, 16);
                case ValueType.BinaryString: return CrcConverter.ToString(NumericsStringFormat.Binary, (string)_value, null, NumericsStringFormat.Hex);
                case ValueType.HexString: return (string)_value;
                default: throw new ArithmeticException($"CRC parameter's type is {_value.GetType()}.");
            }
        }

        /// <summary>
        /// Method overrided. Return hex <see cref="string"/> of value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToHexString();
        }

        /// <summary>
        /// Gets <see cref="ushort"/> value. Throw <see cref="Exception"/> if value's type is not <see cref="ushort"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ushort ToUInt16()
        {
            switch (_valueType)
            {
                case ValueType.UInt16: return (ushort)_value;
                default: throw new ArithmeticException($"CRC parameter's type is {_value.GetType()}.");
            }
        }

        /// <summary>
        /// Gets <see cref="uint"/> value. Throw <see cref="Exception"/> if value's type is not <see cref="uint"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public uint ToUInt32()
        {
            switch (_valueType)
            {
                case ValueType.UInt32: return (uint)_value;
                default: throw new ArithmeticException($"CRC parameter's type is {_value.GetType()}.");
            }
        }

        /// <summary>
        /// Gets <see cref="ulong"/> value. Throw <see cref="Exception"/> if value's type is not <see cref="ulong"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ulong ToUInt64()
        {
            switch (_valueType)
            {
                case ValueType.UInt64: return (ulong)_value;
                default: throw new ArithmeticException($"CRC parameter's type is {_value.GetType()}.");
            }
        }
    }
}