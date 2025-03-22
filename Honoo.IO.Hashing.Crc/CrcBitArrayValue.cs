using System;
using System.Collections;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC value.
    /// </summary>
    public sealed class CrcBitArrayValue : CrcValue
    {
        #region Members

        private readonly BitArray _value;
        private readonly CrcValueType _valueType = CrcValueType.BitArray;
        private readonly int _width;

        /// <summary>
        /// Gets CRC value type.
        /// </summary>
        public override CrcValueType ValueType => _valueType;

        /// <summary>
        /// Gets CRC width in bits.
        /// </summary>
        public override int Width => _width;

        #endregion Members

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CrcBitArrayValue class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="truncateToWidthBits">Truncated the value to the specifies crc width. The allowed values are more than 0.</param>
        public CrcBitArrayValue(BitArray value, int truncateToWidthBits)
        {
            if (truncateToWidthBits <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0.", nameof(truncateToWidthBits));
            }
            _value = CrcConverter.GetBitArray(value, truncateToWidthBits);
            _width = truncateToWidthBits;
        }

        #endregion Construction

        /// <summary>
        /// Determines whether the specified <see cref="CrcBitArrayValue"/> is equal to the current <see cref="CrcBitArrayValue"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CrcBitArrayValue other)
        {
            return other != null && _valueType == other._valueType && _width == other._width && _value == other._value;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is CrcBitArrayValue other && Equals(other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _valueType.GetHashCode() ^ _width.GetHashCode() ^ _value.GetHashCode();
        }

        /// <summary>
        /// Gets <see cref="BitArray"/> value of converted.
        /// </summary>
        /// <returns></returns>
        public override BitArray ToBitArray()
        {
            return (BitArray)_value.Clone();
        }

        /// <summary>
        /// Gets <see cref="string"/> as "11110000" value of converted.
        /// </summary>
        /// <returns></returns>
        public override string ToBits()
        {
            return CrcConverter.GetBits(_value, _width);
        }

        /// <summary>
        /// Gets <see cref="byte"/>[] value of converted.
        /// </summary>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override byte[] ToBytes(CrcEndian outputEndian)
        {
            return CrcConverter.GetBytes(_value, _width, outputEndian);
        }

        /// <summary>
        /// Write to output buffer and return checksum byte length.
        /// </summary>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <param name="outputBuffer">Output buffer.</param>
        /// <param name="outputOffset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override int ToBytes(CrcEndian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            return CrcConverter.GetBytes(_value, _width, outputEndian, outputBuffer, outputOffset);
        }

        /// <summary>
        /// Gets <see cref="string"/> as "FF55" value of converted.
        /// </summary>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        public override string ToHex(CrcCaseSensitivity caseSensitivity)
        {
            return CrcConverter.GetHex(_value, _width, caseSensitivity);
        }

        /// <summary>
        /// Gets <see cref="ushort"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override ushort ToUInt16()
        {
            return CrcConverter.GetUInt16(_value, _width);
        }

        /// <summary>
        /// Gets <see cref="ushort"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public override bool ToUInt16(out ushort checksum)
        {
            checksum = CrcConverter.GetUInt16(_value, _width);
            return _width > 16;
        }

        /// <summary>
        /// Gets <see cref="uint"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override uint ToUInt32()
        {
            return CrcConverter.GetUInt32(_value, _width);
        }

        /// <summary>
        /// Gets <see cref="uint"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public override bool ToUInt32(out uint checksum)
        {
            checksum = CrcConverter.GetUInt32(_value, _width);
            return _width > 32;
        }

        /// <summary>
        /// Gets <see cref="ulong"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override ulong ToUInt64()
        {
            return CrcConverter.GetUInt64(_value, _width);
        }

        /// <summary>
        /// Gets <see cref="ulong"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public override bool ToUInt64(out ulong checksum)
        {
            checksum = CrcConverter.GetUInt64(_value, _width);
            return _width > 64;
        }

        /// <summary>
        /// Gets <see cref="byte"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override byte ToUInt8()
        {
            return CrcConverter.GetUInt8(_value, _width);
        }

        /// <summary>
        /// Gets <see cref="byte"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public override bool ToUInt8(out byte checksum)
        {
            checksum = CrcConverter.GetUInt8(_value, _width);
            return _width > 8;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected override bool EqualsProtected(CrcValue other)
        {
            if (other is CrcBitArrayValue crcValue)
            {
                return Equals(crcValue);
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override int GetHashCodeProtected()
        {
            return GetHashCode();
        }
    }
}