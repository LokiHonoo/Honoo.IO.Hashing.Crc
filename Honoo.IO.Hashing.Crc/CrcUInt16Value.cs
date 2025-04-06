using System;
using System.Collections;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC value.
    /// </summary>
    public sealed class CrcUInt16Value : CrcValue
    {
        #region Members

        private readonly ushort _value;
        private readonly CrcValueType _valueType = CrcValueType.UInt16;
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
        /// Initializes a new instance of the CrcUInt16Value class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="truncateToWidthBits">Truncated the value to the specifies crc width. The allowed values are between 1 - 16.</param>
        public CrcUInt16Value(ushort value, int truncateToWidthBits)
        {
            if (truncateToWidthBits <= 0 || truncateToWidthBits > 16)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 16.", nameof(truncateToWidthBits));
            }
            int move = 16 - truncateToWidthBits;
            if (move > 0)
            {
                value = (ushort)(value << move);
                value = (ushort)(value >> move);
            }
            _value = value;
            _width = truncateToWidthBits;
        }

        #endregion Construction

        /// <summary>
        /// Determines whether the specified <see cref="CrcUInt16Value"/> is equal to the current <see cref="CrcUInt16Value"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CrcUInt16Value other)
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
            return obj is CrcUInt16Value other && Equals(other);
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
        /// Gets <see cref="string"/> as "11110000" value of converted.
        /// </summary>
        /// <returns></returns>
        public override string ToBinary()
        {
            return CrcConverter.GetBinary(_value, _width);
        }

        /// <summary>
        /// Gets <see cref="BitArray"/> value of converted.
        /// </summary>
        /// <returns></returns>
        public override BitArray ToBitArray()
        {
            return CrcConverter.GetBitArray(_value, _width);
        }

        /// <summary>
        /// Gets <see cref="byte"/>[] value of converted.
        /// </summary>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override byte[] ToByteArray(CrcEndian outputEndian)
        {
            int length = (int)Math.Ceiling(_width / 8d);
            byte[] output = new byte[length];
            if (outputEndian == CrcEndian.LittleEndian)
            {
                UInt16ToLE(_value, output, 0, length);
            }
            else
            {
                UInt16ToBE(_value, output, 0, length);
            }
            return output;
        }

        /// <summary>
        /// Write to output buffer and return checksum byte length.
        /// </summary>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <param name="outputBuffer">Output buffer.</param>
        /// <param name="outputOffset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override int ToByteArray(CrcEndian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            if (outputBuffer is null)
            {
                throw new ArgumentNullException(nameof(outputBuffer));
            }
            int length = (int)Math.Ceiling(_width / 8d);
            if (outputOffset < 0 || outputOffset + length > outputBuffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(outputOffset));
            }
            if (outputEndian == CrcEndian.LittleEndian)
            {
                UInt16ToLE(_value, outputBuffer, outputOffset, length);
            }
            else
            {
                UInt16ToBE(_value, outputBuffer, outputOffset, length);
            }
            return length;
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
            return (ushort)_value;
        }

        /// <summary>
        /// Gets <see cref="ushort"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public override bool ToUInt16(out ushort checksum)
        {
            checksum = _value;
            return _width > 16;
        }

        /// <summary>
        /// Gets <see cref="uint"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override uint ToUInt32()
        {
            return _value;
        }

        /// <summary>
        /// Gets <see cref="uint"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public override bool ToUInt32(out uint checksum)
        {
            checksum = _value;
            return _width > 32;
        }

        /// <summary>
        /// Gets <see cref="ulong"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override ulong ToUInt64()
        {
            return _value;
        }

        /// <summary>
        /// Gets <see cref="ulong"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public override bool ToUInt64(out ulong checksum)
        {
            checksum = _value;
            return _width > 64;
        }

        /// <summary>
        /// Gets <see cref="byte"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override byte ToUInt8()
        {
            return (byte)_value;
        }

        /// <summary>
        /// Gets <see cref="byte"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public override bool ToUInt8(out byte checksum)
        {
            checksum = (byte)_value;
            return _width > 8;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected override bool EqualsInternal(CrcValue other)
        {
            if (other is CrcUInt16Value crcValue)
            {
                return Equals(crcValue);
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override int GetHashCodeInternal()
        {
            return GetHashCode();
        }

        private static void UInt16ToBE(ushort input, byte[] outputBuffer, int outputOffset, int outputLength)
        {
            if (outputLength == 2)
            {
                outputBuffer[outputOffset] = (byte)(input >> 8);
                outputBuffer[outputOffset + 1] = (byte)input;
            }
            else
            {
                outputBuffer[outputOffset] = (byte)input;
            }
        }

        private static void UInt16ToLE(ushort input, byte[] outputBuffer, int outputOffset, int outputLength)
        {
            if (outputLength == 2)
            {
                outputBuffer[outputOffset] = (byte)input;
                outputBuffer[outputOffset + 1] = (byte)(input >> 8);
            }
            else
            {
                outputBuffer[outputOffset] = (byte)input;
            }
        }
    }
}