using System;
using System.Globalization;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC Parameter.
    /// </summary>
    public sealed class CrcParameter
    {
        #region Enum

        internal enum ValueType
        { UInt8, UInt16, UInt32, UInt64, HexString }

        #endregion Enum

        #region Properties

        private readonly object _value;
        private readonly ValueType _valueType;

        #endregion Properties

        #region Construction

        internal CrcParameter(object value)
        {
            _value = value;
            switch (value)
            {
                case byte _: _valueType = ValueType.UInt8; break;
                case ushort _: _valueType = ValueType.UInt16; break;
                case uint _: _valueType = ValueType.UInt32; break;
                case ulong _: _valueType = ValueType.UInt64; break;
                default: _valueType = ValueType.HexString; break;
            }
        }

        #endregion Construction

        /// <summary>
        /// Determines whether the specified <see cref="CrcParameter"/> is equal to the current <see cref="CrcParameter"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CrcParameter other)
        {
            return _valueType == other._valueType & _value == other._value;
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
                case ValueType.HexString: return byte.Parse((string)_value, NumberStyles.HexNumber);
                default: throw new Exception($"CRC parameter's type is {_value.GetType()}.");
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
                case ValueType.UInt8: return Convert.ToString((byte)_value, 16).ToUpperInvariant();
                case ValueType.UInt16: return Convert.ToString((ushort)_value, 16).ToUpperInvariant();
                case ValueType.UInt32: return Convert.ToString((uint)_value, 16).ToUpperInvariant();
                case ValueType.UInt64: return Convert.ToString((long)(ulong)_value, 16).ToUpperInvariant();
                case ValueType.HexString: return (string)_value;
                default: throw new Exception($"CRC parameter's type is {_value.GetType()}.");
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
                case ValueType.HexString: return ushort.Parse((string)_value, NumberStyles.HexNumber);
                default: throw new Exception($"CRC parameter's type is {_value.GetType()}.");
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
                case ValueType.HexString: return uint.Parse((string)_value, NumberStyles.HexNumber);
                default: throw new Exception($"CRC parameter's type is {_value.GetType()}.");
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
                case ValueType.HexString: return ulong.Parse((string)_value, NumberStyles.HexNumber);
                default: throw new Exception($"CRC parameter's type is {_value.GetType()}.");
            }
        }
    }
}