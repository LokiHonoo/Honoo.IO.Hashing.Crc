using System;
using System.Collections;
using System.Text;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC converter.
    /// </summary>
    public sealed class CrcConverter
    {
        /// <summary>
        /// Convert binary(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBinary(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"\"{nameof(input)}\" can't be null or white space.", nameof(input));
            }
            if (truncateToWidthBits.HasValue && truncateToWidthBits <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            input = input.Trim();
            StringBuilder binary = new StringBuilder();
            switch (inputFormat)
            {
                case CrcStringFormat.Binary:
                    if (input.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
                    {
                        binary.Append(input, 2, input.Length - 2);
                    }
                    else
                    {
                        binary.Append(input);
                    }
                    break;

                case CrcStringFormat.Hex:
                    if (input.StartsWith("0x", StringComparison.OrdinalIgnoreCase) || input.StartsWith("&h", StringComparison.OrdinalIgnoreCase))
                    {
                        for (int i = 2; i < input.Length; i++)
                        {
                            binary.Append(Convert.ToString(Convert.ToByte(input[i].ToString(), 16), 2).PadLeft(4, '0'));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < input.Length; i++)
                        {
                            binary.Append(Convert.ToString(Convert.ToByte(input[i].ToString(), 16), 2).PadLeft(4, '0'));
                        }
                    }
                    break;

                default: throw new ArgumentException("Invalid CRC string format.", nameof(inputFormat));
            }
            int width = truncateToWidthBits ?? binary.Length;
            if (binary.Length > width)
            {
                binary.Remove(0, binary.Length - width);
            }
            else if (binary.Length < width)
            {
                binary.Insert(0, "0", width - binary.Length);
            }
            return binary.ToString();
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBinary(BitArray input, int? truncateToWidthBits)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (truncateToWidthBits.HasValue && truncateToWidthBits <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            StringBuilder binary = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                binary.Append(input.Get(i) ? '1' : '0');
            }
            int width = truncateToWidthBits ?? binary.Length;
            if (binary.Length > width)
            {
                binary.Remove(0, binary.Length - width);
            }
            else if (binary.Length < width)
            {
                binary.Insert(0, "0", width - binary.Length);
            }
            return binary.ToString();
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 8 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBinary(byte input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 8))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 8 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            string result = Convert.ToString(input, 2).PadLeft(8, '0');
            int width = truncateToWidthBits ?? result.Length;
            if (result.Length > width)
            {
                result = result.Substring(result.Length - width, width);
            }
            return result;
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 16 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBinary(ushort input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 16))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 16 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            string result = Convert.ToString(input, 2).PadLeft(16, '0');
            int width = truncateToWidthBits ?? result.Length;
            if (result.Length > width)
            {
                result = result.Substring(result.Length - width, width);
            }
            return result;
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 32 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBinary(uint input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 32))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 32 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            string result = Convert.ToString(input, 2).PadLeft(32, '0');
            int width = truncateToWidthBits ?? result.Length;
            if (result.Length > width)
            {
                result = result.Substring(result.Length - width, width);
            }
            return result;
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 64 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBinary(ulong input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 64))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 64 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            string result = Convert.ToString((uint)(input >> 32), 2).PadLeft(32, '0') + Convert.ToString((uint)input, 2).PadLeft(32, '0');
            int width = truncateToWidthBits ?? result.Length;
            if (result.Length > width)
            {
                result = result.Substring(result.Length - width, width);
            }
            return result;
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static BitArray GetBitArray(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"\"{nameof(input)}\" can't be null or white space.", nameof(input));
            }
            if (truncateToWidthBits.HasValue && truncateToWidthBits <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            input = input.Trim();
            BitArray bits;
            switch (inputFormat)
            {
                case CrcStringFormat.Binary:
                    if (input.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
                    {
                        bits = new BitArray(input.Length - 2);
                        for (int i = 2; i < input.Length; i++)
                        {
                            bits.Set(i - 2, input[i] == '1');
                        }
                    }
                    else
                    {
                        bits = new BitArray(input.Length);
                        for (int i = 0; i < input.Length; i++)
                        {
                            bits.Set(i, input[i] == '1');
                        }
                    }
                    break;

                case CrcStringFormat.Hex:
                    if (input.StartsWith("0x", StringComparison.OrdinalIgnoreCase) || input.StartsWith("&h", StringComparison.OrdinalIgnoreCase))
                    {
                        bits = new BitArray((input.Length - 2) * 4);
                        for (int i = 2; i < input.Length; i++)
                        {
                            byte b = Convert.ToByte(input[i].ToString(), 16);
                            bits.Set((i - 2) * 4, ((b >> 3) & 0x1) == 0x1);
                            bits.Set((i - 2) * 4 + 1, ((b >> 2) & 0x1) == 0x1);
                            bits.Set((i - 2) * 4 + 2, ((b >> 1) & 0x1) == 0x1);
                            bits.Set((i - 2) * 4 + 3, (b & 0x1) == 0x1);
                        }
                    }
                    else
                    {
                        bits = new BitArray(input.Length * 4);
                        for (int i = 0; i < input.Length; i++)
                        {
                            byte b = Convert.ToByte(input[i].ToString(), 16);
                            bits.Set(i * 4, ((b >> 3) & 0x1) == 0x1);
                            bits.Set(i * 4 + 1, ((b >> 2) & 0x1) == 0x1);
                            bits.Set(i * 4 + 2, ((b >> 1) & 0x1) == 0x1);
                            bits.Set(i * 4 + 3, (b & 0x1) == 0x1);
                        }
                    }
                    break;

                default: throw new ArgumentException("Invalid CRC string format.", nameof(inputFormat));
            }
            int width = truncateToWidthBits ?? bits.Count;
            if (width == bits.Count)
            {
                var result = new BitArray(width);
                int length = Math.Min(width, bits.Count);
                for (int i = 0; i < length; i++)
                {
                    result.Set(result.Count - 1 - i, bits.Get(bits.Count - 1 - i));
                }
                return result;
            }
            else
            {
                return bits;
            }
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static BitArray GetBitArray(BitArray input, int? truncateToWidthBits)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (truncateToWidthBits.HasValue && truncateToWidthBits <= 0)
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            int width = truncateToWidthBits ?? input.Count;
            if (width == input.Count)
            {
                var result = new BitArray(width);
                int length = Math.Min(width, input.Count);
                for (int i = 0; i < length; i++)
                {
                    result.Set(result.Count - 1 - i, input.Get(input.Count - 1 - i));
                }
                return result;
            }
            else
            {
                return new BitArray(input);
            }
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 8 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static BitArray GetBitArray(byte input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 8))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 8 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            int width = truncateToWidthBits ?? 8;
            var result = new BitArray(width);
            for (int i = 0; i < width; i++)
            {
                result.Set(result.Count - 1 - i, ((input >> i) & 0x1) == 0x1);
            }
            return result;
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 16 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static BitArray GetBitArray(ushort input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 16))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 16 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            int width = truncateToWidthBits ?? 16;
            var result = new BitArray(width);
            for (int i = 0; i < width; i++)
            {
                result.Set(result.Count - 1 - i, ((input >> i) & 0x1) == 0x1);
            }
            return result;
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 32 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static BitArray GetBitArray(uint input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 32))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 32 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            int width = truncateToWidthBits ?? 32;
            var result = new BitArray(width);
            for (int i = 0; i < width; i++)
            {
                result.Set(result.Count - 1 - i, ((input >> i) & 0x1) == 0x1);
            }
            return result;
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 64 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static BitArray GetBitArray(ulong input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 64))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 64 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            int width = truncateToWidthBits ?? 64;
            var result = new BitArray(width);
            for (int i = 0; i < width; i++)
            {
                result.Set(result.Count - 1 - i, ((input >> i) & 0x1) == 0x1);
            }
            return result;
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static byte[] GetBytes(CrcStringFormat inputFormat, string input, int? truncateToWidthBits, CrcEndian outputEndian)
        {
            string binary = GetBinary(inputFormat, input, truncateToWidthBits);
            int rem = binary.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                binary = binary.PadLeft(binary.Length + truncates, '0');
            }
            int length = binary.Length / 8;
            byte[] result = new byte[length];
            if (outputEndian == CrcEndian.LittleEndian)
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    result[i] = Convert.ToByte(binary.Substring(i * 8, 8), 2);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    result[i] = Convert.ToByte(binary.Substring(i * 8, 8), 2);
                }
            }
            return result;
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <param name="outputBuffer">Output buffer.</param>
        /// <param name="outputOffset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int GetBytes(CrcStringFormat inputFormat, string input, int? truncateToWidthBits, CrcEndian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            if (outputBuffer is null)
            {
                throw new ArgumentNullException(nameof(outputBuffer));
            }
            if (outputOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(outputOffset));
            }
            string binary = GetBinary(inputFormat, input, truncateToWidthBits);
            int rem = binary.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                binary = binary.PadLeft(binary.Length + truncates, '0');
            }
            int length = binary.Length / 8;
            if (outputOffset + length > outputBuffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(outputOffset));
            }
            if (outputEndian == CrcEndian.LittleEndian)
            {
                for (int i = length + outputOffset - 1; i >= outputOffset; i--)
                {
                    outputBuffer[i] = Convert.ToByte(binary.Substring(i * 8, 8), 2);
                }
            }
            else
            {
                for (int i = outputOffset; i < length + outputOffset; i++)
                {
                    outputBuffer[i] = Convert.ToByte(binary.Substring(i * 8, 8), 2);
                }
            }
            return length;
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static byte[] GetBytes(BitArray input, int? truncateToWidthBits, CrcEndian outputEndian)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            BitArray bits = GetBitArray(input, truncateToWidthBits);
            int rem = bits.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                BitArray tmp = new BitArray(bits.Length + truncates);
                for (int i = 0; i < bits.Length; i++)
                {
                    tmp.Set(tmp.Count - 1 - i, bits.Get(bits.Count - 1 - i));
                }
                bits = tmp;
            }
            int length = bits.Length / 8;
            byte[] result = new byte[length];
            if (outputEndian == CrcEndian.LittleEndian)
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    byte b = (byte)((bits[i * 8] ? 1 : 0) << 7);
                    b |= (byte)((bits[i * 8 + 1] ? 1 : 0) << 6);
                    b |= (byte)((bits[i * 8 + 2] ? 1 : 0) << 5);
                    b |= (byte)((bits[i * 8 + 3] ? 1 : 0) << 4);
                    b |= (byte)((bits[i * 8 + 4] ? 1 : 0) << 3);
                    b |= (byte)((bits[i * 8 + 5] ? 1 : 0) << 2);
                    b |= (byte)((bits[i * 8 + 6] ? 1 : 0) << 1);
                    b |= (byte)(bits[i * 8 + 7] ? 1 : 0);
                    result[i] = b;
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    byte b = (byte)((bits[i * 8] ? 1 : 0) << 7);
                    b |= (byte)((bits[i * 8 + 1] ? 1 : 0) << 6);
                    b |= (byte)((bits[i * 8 + 2] ? 1 : 0) << 5);
                    b |= (byte)((bits[i * 8 + 3] ? 1 : 0) << 4);
                    b |= (byte)((bits[i * 8 + 4] ? 1 : 0) << 3);
                    b |= (byte)((bits[i * 8 + 5] ? 1 : 0) << 2);
                    b |= (byte)((bits[i * 8 + 6] ? 1 : 0) << 1);
                    b |= (byte)(bits[i * 8 + 7] ? 1 : 0);
                    result[i] = b;
                }
            }
            return result;
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <param name="outputBuffer">Output buffer.</param>
        /// <param name="outputOffset">Write start offset from buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int GetBytes(BitArray input, int? truncateToWidthBits, CrcEndian outputEndian, byte[] outputBuffer, int outputOffset)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (outputBuffer is null)
            {
                throw new ArgumentNullException(nameof(outputBuffer));
            }
            if (outputOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(outputOffset));
            }
            BitArray bits = GetBitArray(input, truncateToWidthBits);
            int rem = bits.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                BitArray tmp = new BitArray(bits.Length + truncates);
                for (int i = 0; i < bits.Length; i++)
                {
                    tmp.Set(tmp.Count - 1 - i, bits.Get(bits.Count - 1 - i));
                }
                bits = tmp;
            }
            int length = bits.Length / 8;
            if (outputOffset + length > outputBuffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(outputOffset));
            }
            if (outputEndian == CrcEndian.LittleEndian)
            {
                for (int i = length + outputOffset - 1; i >= outputOffset; i--)
                {
                    byte b = (byte)((bits[i * 8] ? 1 : 0) << 7);
                    b |= (byte)((bits[i * 8 + 1] ? 1 : 0) << 6);
                    b |= (byte)((bits[i * 8 + 2] ? 1 : 0) << 5);
                    b |= (byte)((bits[i * 8 + 3] ? 1 : 0) << 4);
                    b |= (byte)((bits[i * 8 + 4] ? 1 : 0) << 3);
                    b |= (byte)((bits[i * 8 + 5] ? 1 : 0) << 2);
                    b |= (byte)((bits[i * 8 + 6] ? 1 : 0) << 1);
                    b |= (byte)(bits[i * 8 + 7] ? 1 : 0);
                    outputBuffer[i] = b;
                }
            }
            else
            {
                for (int i = outputOffset; i < length + outputOffset; i++)
                {
                    byte b = (byte)((bits[i * 8] ? 1 : 0) << 7);
                    b |= (byte)((bits[i * 8 + 1] ? 1 : 0) << 6);
                    b |= (byte)((bits[i * 8 + 2] ? 1 : 0) << 5);
                    b |= (byte)((bits[i * 8 + 3] ? 1 : 0) << 4);
                    b |= (byte)((bits[i * 8 + 4] ? 1 : 0) << 3);
                    b |= (byte)((bits[i * 8 + 5] ? 1 : 0) << 2);
                    b |= (byte)((bits[i * 8 + 6] ? 1 : 0) << 1);
                    b |= (byte)(bits[i * 8 + 7] ? 1 : 0);
                    outputBuffer[i] = b;
                }
            }
            return length;
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(CrcStringFormat inputFormat, string input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            string binary = GetBinary(inputFormat, input, truncateToWidthBits);
            int rem = binary.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                binary = binary.PadLeft(binary.Length + truncates, '0');
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < binary.Length; i += 8)
            {
                byte b = Convert.ToByte(binary.Substring(i, 8), 2);
                result.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }
            int length = truncateToWidthBits.HasValue ? (int)Math.Ceiling(truncateToWidthBits.Value / 4d) : result.Length;
            if (result.Length > length)
            {
                result.Remove(0, result.Length - length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result.ToString() : result.ToString().ToUpperInvariant();
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(BitArray input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            BitArray bits = GetBitArray(input, truncateToWidthBits);
            int rem = bits.Length % 4;
            int truncates = rem > 0 ? 4 - rem : 0;
            if (truncates > 0)
            {
                BitArray tmp = new BitArray(bits.Length + truncates);
                for (int i = 0; i < bits.Length; i++)
                {
                    tmp.Set(tmp.Count - 1 - i, bits.Get(bits.Count - 1 - i));
                }
                bits = tmp;
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bits.Length; i += 4)
            {
                byte b = (byte)((bits[i] ? 1 : 0) << 3);
                b |= (byte)((bits[i + 1] ? 1 : 0) << 2);
                b |= (byte)((bits[i + 2] ? 1 : 0) << 1);
                b |= (byte)(bits[i + 3] ? 1 : 0);
                result.Append(Convert.ToString(b, 16));
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result.ToString() : result.ToString().ToUpperInvariant();
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 8 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(byte input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 8))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 8 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            string result = Convert.ToString(input, 16).PadLeft(2, '0');
            int length = truncateToWidthBits.HasValue ? (int)Math.Ceiling(truncateToWidthBits.Value / 4d) : result.Length;
            if (result.Length > length)
            {
                result = result.Substring(result.Length - length, length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result : result.ToUpperInvariant();
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 16 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(ushort input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 16))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 16 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            string result = Convert.ToString(input, 16).PadLeft(4, '0');
            int length = truncateToWidthBits.HasValue ? (int)Math.Ceiling(truncateToWidthBits.Value / 4d) : result.Length;
            if (result.Length > length)
            {
                result = result.Substring(result.Length - length, length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result : result.ToUpperInvariant();
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 32 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(uint input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 32))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 32 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            string result = Convert.ToString(input, 16).PadLeft(8, '0');
            int length = truncateToWidthBits.HasValue ? (int)Math.Ceiling(truncateToWidthBits.Value / 4d) : result.Length;
            if (result.Length > length)
            {
                result = result.Substring(result.Length - length, length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result : result.ToUpperInvariant();
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are between 1 - 64 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(ulong input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 64))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 1 - 64 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            string result = Convert.ToString((uint)(input >> 32), 16).PadLeft(8, '0') + Convert.ToString((uint)input, 16).PadLeft(8, '0');
            int length = truncateToWidthBits.HasValue ? (int)Math.Ceiling(truncateToWidthBits.Value / 4d) : result.Length;
            if (result.Length > length)
            {
                result = result.Substring(result.Length - length, length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result : result.ToUpperInvariant();
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ushort GetUInt16(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt16(bytes);
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ushort GetUInt16(BitArray input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt16(bytes);
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static uint GetUInt32(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt32(bytes);
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static uint GetUInt32(BitArray input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt32(bytes);
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ulong GetUInt64(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt64(bytes);
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ulong GetUInt64(BitArray input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt64(bytes);
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static byte GetUInt8(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt8(bytes);
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input value.</param>
        /// <param name="truncateToWidthBits">Truncated the input to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static byte GetUInt8(BitArray input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt8(bytes);
        }

        internal static string GetHex(byte[] input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 16).PadLeft(2, '0'));
            }
            int length = truncateToWidthBits.HasValue ? (int)Math.Ceiling(truncateToWidthBits.Value / 4d) : result.Length;
            if (result.Length > length)
            {
                result.Remove(0, result.Length - length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result.ToString() : result.ToString().ToUpperInvariant();
        }

        internal static string GetHex(ushort[] input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 16).PadLeft(4, '0'));
            }
            int length = truncateToWidthBits.HasValue ? (int)Math.Ceiling(truncateToWidthBits.Value / 4d) : result.Length;
            if (result.Length > length)
            {
                result.Remove(0, result.Length - length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result.ToString() : result.ToString().ToUpperInvariant();
        }

        internal static string GetHex(uint[] input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString(input[i], 16).PadLeft(8, '0'));
            }
            int length = truncateToWidthBits.HasValue ? (int)Math.Ceiling(truncateToWidthBits.Value / 4d) : result.Length;
            if (result.Length > length)
            {
                result.Remove(0, result.Length - length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result.ToString() : result.ToString().ToUpperInvariant();
        }

        internal static string GetHex(ulong[] input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                result.Append(Convert.ToString((uint)(input[i] >> 32), 16).PadLeft(8, '0') + Convert.ToString((uint)input[i], 16).PadLeft(8, '0'));
            }
            int length = truncateToWidthBits.HasValue ? (int)Math.Ceiling(truncateToWidthBits.Value / 4d) : result.Length;
            if (result.Length > length)
            {
                result.Remove(0, result.Length - length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result.ToString() : result.ToString().ToUpperInvariant();
        }

        internal static ushort[] GetUInt16Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string binary = GetBinary(inputFormat, input, truncateToWidthBits);
            int rem = binary.Length % 16;
            int truncates = rem > 0 ? 16 - rem : 0;
            if (truncates > 0)
            {
                binary = binary.PadLeft(binary.Length + truncates, '0');
            }
            ushort[] result = new ushort[binary.Length / 16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToUInt16(binary.Substring(i * 16, 16), 2);
            }
            return result;
        }

        internal static uint[] GetUInt32Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string binary = GetBinary(inputFormat, input, truncateToWidthBits);
            int rem = binary.Length % 32;
            int truncates = rem > 0 ? 32 - rem : 0;
            if (truncates > 0)
            {
                binary = binary.PadLeft(binary.Length + truncates, '0');
            }
            uint[] result = new uint[binary.Length / 32];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToUInt32(binary.Substring(i * 32, 32), 2);
            }
            return result;
        }

        internal static ulong[] GetUInt64Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string binary = GetBinary(inputFormat, input, truncateToWidthBits);
            int rem = binary.Length % 64;
            int truncates = rem > 0 ? 64 - rem : 0;
            if (truncates > 0)
            {
                binary = binary.PadLeft(binary.Length + truncates, '0');
            }
            ulong[] result = new ulong[binary.Length / 64];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToUInt64(binary.Substring(i * 64, 64), 2);
            }
            return result;
        }

        internal static byte[] GetUInt8Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string binary = GetBinary(inputFormat, input, truncateToWidthBits);
            int rem = binary.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                binary = binary.PadLeft(binary.Length + truncates, '0');
            }
            byte[] result = new byte[binary.Length / 8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(binary.Substring(i * 8, 8), 2);
            }
            return result;
        }

        private static ushort BEToUInt16(byte[] input)
        {
            ushort result = 0;
            int length = Math.Min(input.Length, 2);
            for (int i = 0; i < length; i++)
            {
                result |= (ushort)((input[input.Length - 1 - i] & 0xFF) << (8 * i));
            }
            return result;
        }

        private static uint BEToUInt32(byte[] input)
        {
            uint result = 0;
            int length = Math.Min(input.Length, 4);
            for (int i = 0; i < length; i++)
            {
                result |= (input[input.Length - 1 - i] & 0xFFU) << (8 * i);
            }
            return result;
        }

        private static ulong BEToUInt64(byte[] input)
        {
            ulong result = 0;
            int length = Math.Min(input.Length, 8);
            for (int i = 0; i < length; i++)
            {
                result |= (input[input.Length - 1 - i] & 0xFFUL) << (8 * i);
            }
            return result;
        }

        private static byte BEToUInt8(byte[] input)
        {
            return input[input.Length - 1];
        }
    }
}