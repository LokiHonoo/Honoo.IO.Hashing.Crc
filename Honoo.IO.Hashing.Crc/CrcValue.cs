using System;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC value.
    /// </summary>
    public abstract class CrcValue : IEquatable<CrcValue>
    {
        #region Members

        /// <summary>
        /// Gets CRC value type.
        /// </summary>
        public abstract CrcValueType ValueType { get; }

        /// <summary>
        /// Gets CRC width in bits.
        /// </summary>
        public abstract int Width { get; }

        #endregion Members

        #region Construction

        /// <summary>
        /// Initializes a new instance of the CrcValue class.
        /// </summary>
        internal CrcValue()
        {
        }

        #endregion Construction

        /// <summary>
        /// Determines whether the specified <see cref="CrcValue"/> is equal to the current <see cref="CrcValue"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CrcValue other)
        {
            return EqualsProtected(other);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is CrcValue other && Equals(other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return GetHashCodeProtected();
        }

        /// <summary>
        /// Gets <see cref="string"/> as "11110000" value of converted.
        /// </summary>
        /// <returns></returns>
        public abstract string ToBits();

        /// <summary>
        /// Gets <see cref="byte"/>[] value of converted.
        /// </summary>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public abstract byte[] ToBytes(CrcEndian outputEndian);

        /// <summary>
        /// Write to output buffer and return checksum byte length.
        /// </summary>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <param name="outputBuffer">Output buffer.</param>
        /// <param name="outputOffset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public abstract int ToBytes(CrcEndian outputEndian, byte[] outputBuffer, int outputOffset);

        /// <summary>
        /// Gets <see cref="string"/> as "FF55" value of converted.
        /// </summary>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        public abstract string ToHex(CrcCaseSensitivity caseSensitivity);

        /// <summary>
        /// Method overrided. Return hex <see cref="string"/> of value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToHex(CrcCaseSensitivity.Upper);
        }

        /// <summary>
        /// Gets <see cref="ushort"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public abstract ushort ToUInt16();

        /// <summary>
        /// Gets <see cref="uint"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public abstract uint ToUInt32();

        /// <summary>
        /// Gets <see cref="ulong"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public abstract ulong ToUInt64();

        /// <summary>
        /// Gets <see cref="byte"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public abstract byte ToUInt8();

        /// <summary>
        /// Gets <see cref="byte"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public abstract bool ToUInt8(out byte checksum);

        /// <summary>
        /// Gets <see cref="ushort"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public abstract bool ToUInt16(out ushort checksum);

        /// <summary>
        /// Gets <see cref="uint"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public abstract bool ToUInt32(out uint checksum);

        /// <summary>
        /// Gets <see cref="ulong"/> value of converted. It's maybe truncated.
        /// </summary>
        /// <returns><see langword="true"/> is the value is truncated.</returns>
        /// <exception cref="Exception"></exception>
        public abstract bool ToUInt64(out ulong checksum);

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected abstract bool EqualsProtected(CrcValue other);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected abstract int GetHashCodeProtected();
    }
}