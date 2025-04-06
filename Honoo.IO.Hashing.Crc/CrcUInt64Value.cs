using System;
using System.Collections;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC value.
    /// </summary>
    public sealed class CrcUInt64Value : CrcValue
    {
        #region Members

        private readonly ulong _value;
        private readonly CrcValueType _valueType = CrcValueType.UInt64;
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
        /// Initializes a new instance of the CrcUInt64Value class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="truncateToWidthBits">Truncated the value to the specifies crc width. The allowed values are between 1 - 64.</param>
        public CrcUInt64Value(ulong value, int truncateToWidthBits)
        {
            if (truncateToWidthBits <= 0 || truncateToWidthBits > 64)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 64.", nameof(truncateToWidthBits));
            }
            int move = 64 - truncateToWidthBits;
            if (move > 0)
            {
                value <<= move;
                value >>= move;
            }
            _value = value;
            _width = truncateToWidthBits;
        }

        #endregion Construction

        /// <summary>
        /// Determines whether the specified <see cref="CrcUInt64Value"/> is equal to the current <see cref="CrcUInt64Value"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CrcUInt64Value other)
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
            return obj is CrcUInt64Value other && Equals(other);
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
        public override byte[] ToBytes(CrcEndian outputEndian)
        {
            int length = (int)Math.Ceiling(_width / 8d);
            byte[] output = new byte[length];
            if (outputEndian == CrcEndian.LittleEndian)
            {
                UInt64ToLE(_value, output, 0, length);
            }
            else
            {
                UInt64ToBE(_value, output, 0, length);
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
        public override int ToBytes(CrcEndian outputEndian, byte[] outputBuffer, int outputOffset)
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
                UInt64ToLE(_value, outputBuffer, outputOffset, length);
            }
            else
            {
                UInt64ToBE(_value, outputBuffer, outputOffset, length);
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
            checksum = (ushort)_value;
            return _width > 16;
        }

        /// <summary>
        /// Gets <see cref="uint"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override uint ToUInt32()
        {
            return (uint)_value;
        }

        /// <summary>
        /// Gets <see cref="uint"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public override bool ToUInt32(out uint checksum)
        {
            checksum = (uint)_value;
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
            if (other is CrcUInt64Value crcValue)
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

        private static void UInt64ToBE(ulong input, byte[] outputBuffer, int outputOffset, int outputLength)
        {
            if (outputLength == 8)
            {
                outputBuffer[outputOffset] = (byte)(input >> 56);
                outputBuffer[outputOffset + 1] = (byte)(input >> 48);
                outputBuffer[outputOffset + 2] = (byte)(input >> 40);
                outputBuffer[outputOffset + 3] = (byte)(input >> 32);
                outputBuffer[outputOffset + 4] = (byte)(input >> 24);
                outputBuffer[outputOffset + 5] = (byte)(input >> 16);
                outputBuffer[outputOffset + 6] = (byte)(input >> 8);
                outputBuffer[outputOffset + 7] = (byte)input;
            }
            else if (outputLength == 7)
            {
                outputBuffer[outputOffset] = (byte)(input >> 48);
                outputBuffer[outputOffset + 1] = (byte)(input >> 40);
                outputBuffer[outputOffset + 2] = (byte)(input >> 32);
                outputBuffer[outputOffset + 3] = (byte)(input >> 24);
                outputBuffer[outputOffset + 4] = (byte)(input >> 16);
                outputBuffer[outputOffset + 5] = (byte)(input >> 8);
                outputBuffer[outputOffset + 6] = (byte)input;
            }
            else if (outputLength == 6)
            {
                outputBuffer[outputOffset] = (byte)(input >> 40);
                outputBuffer[outputOffset + 1] = (byte)(input >> 32);
                outputBuffer[outputOffset + 2] = (byte)(input >> 24);
                outputBuffer[outputOffset + 3] = (byte)(input >> 16);
                outputBuffer[outputOffset + 4] = (byte)(input >> 8);
                outputBuffer[outputOffset + 5] = (byte)input;
            }
            else if (outputLength == 5)
            {
                outputBuffer[outputOffset] = (byte)(input >> 32);
                outputBuffer[outputOffset + 1] = (byte)(input >> 24);
                outputBuffer[outputOffset + 2] = (byte)(input >> 16);
                outputBuffer[outputOffset + 3] = (byte)(input >> 8);
                outputBuffer[outputOffset + 4] = (byte)input;
            }
            else if (outputLength == 4)
            {
                outputBuffer[outputOffset] = (byte)(input >> 24);
                outputBuffer[outputOffset + 1] = (byte)(input >> 16);
                outputBuffer[outputOffset + 2] = (byte)(input >> 8);
                outputBuffer[outputOffset + 3] = (byte)input;
            }
            else if (outputLength == 3)
            {
                outputBuffer[outputOffset] = (byte)(input >> 16);
                outputBuffer[outputOffset + 1] = (byte)(input >> 8);
                outputBuffer[outputOffset + 2] = (byte)input;
            }
            else if (outputLength == 2)
            {
                outputBuffer[outputOffset] = (byte)(input >> 8);
                outputBuffer[outputOffset + 1] = (byte)input;
            }
            else
            {
                outputBuffer[outputOffset] = (byte)input;
            }
        }

        private static void UInt64ToLE(ulong input, byte[] outputBuffer, int outputOffset, int outputLength)
        {
            if (outputLength == 8)
            {
                outputBuffer[outputOffset] = (byte)input;
                outputBuffer[outputOffset + 1] = (byte)(input >> 8);
                outputBuffer[outputOffset + 2] = (byte)(input >> 16);
                outputBuffer[outputOffset + 3] = (byte)(input >> 24);
                outputBuffer[outputOffset + 4] = (byte)(input >> 32);
                outputBuffer[outputOffset + 5] = (byte)(input >> 40);
                outputBuffer[outputOffset + 6] = (byte)(input >> 48);
                outputBuffer[outputOffset + 7] = (byte)(input >> 56);
            }
            else if (outputLength == 7)
            {
                outputBuffer[outputOffset] = (byte)input;
                outputBuffer[outputOffset + 1] = (byte)(input >> 8);
                outputBuffer[outputOffset + 2] = (byte)(input >> 16);
                outputBuffer[outputOffset + 3] = (byte)(input >> 24);
                outputBuffer[outputOffset + 4] = (byte)(input >> 32);
                outputBuffer[outputOffset + 5] = (byte)(input >> 40);
                outputBuffer[outputOffset + 6] = (byte)(input >> 48);
            }
            else if (outputLength == 6)
            {
                outputBuffer[outputOffset] = (byte)input;
                outputBuffer[outputOffset + 1] = (byte)(input >> 8);
                outputBuffer[outputOffset + 2] = (byte)(input >> 16);
                outputBuffer[outputOffset + 3] = (byte)(input >> 24);
                outputBuffer[outputOffset + 4] = (byte)(input >> 32);
                outputBuffer[outputOffset + 5] = (byte)(input >> 40);
            }
            else if (outputLength == 5)
            {
                outputBuffer[outputOffset] = (byte)input;
                outputBuffer[outputOffset + 1] = (byte)(input >> 8);
                outputBuffer[outputOffset + 2] = (byte)(input >> 16);
                outputBuffer[outputOffset + 3] = (byte)(input >> 24);
                outputBuffer[outputOffset + 4] = (byte)(input >> 32);
            }
            else if (outputLength == 4)
            {
                outputBuffer[outputOffset] = (byte)input;
                outputBuffer[outputOffset + 1] = (byte)(input >> 8);
                outputBuffer[outputOffset + 2] = (byte)(input >> 16);
                outputBuffer[outputOffset + 3] = (byte)(input >> 24);
            }
            else if (outputLength == 3)
            {
                outputBuffer[outputOffset] = (byte)input;
                outputBuffer[outputOffset + 1] = (byte)(input >> 8);
                outputBuffer[outputOffset + 2] = (byte)(input >> 16);
            }
            else if (outputLength == 2)
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