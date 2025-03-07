using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// CRC converter.
    /// </summary>
    public sealed class CrcConverter
    {
        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBits(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
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
            StringBuilder bin = new StringBuilder();
            switch (inputFormat)
            {
                case CrcStringFormat.Bits:
                    if (input.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
                    {
                        bin.Append(input, 2, input.Length - 2);
                    }
                    else
                    {
                        bin.Append(input);
                    }
                    break;

                case CrcStringFormat.Hex:
                    if (input.StartsWith("0x", StringComparison.OrdinalIgnoreCase) || input.StartsWith("&h", StringComparison.OrdinalIgnoreCase))
                    {
                        for (int i = 2; i < input.Length; i++)
                        {
                            bin.Append(Convert.ToString(Convert.ToByte(input[i].ToString(), 16), 2).PadLeft(4, '0'));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < input.Length; i++)
                        {
                            bin.Append(Convert.ToString(Convert.ToByte(input[i].ToString(), 16), 2).PadLeft(4, '0'));
                        }
                    }
                    break;

                default: throw new ArgumentException("Invalid CRC string format.", nameof(inputFormat));
            }
            int width = truncateToWidthBits ?? bin.Length;
            if (bin.Length > width)
            {
                bin.Remove(0, bin.Length - width);
            }
            else if (bin.Length < width)
            {
                bin.Insert(0, "0", width - bin.Length);
            }
            return bin.ToString();
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are between 0 - 8 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBits(byte input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 8))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8 or set 'null' to not truncated.", nameof(truncateToWidthBits));
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
        /// <param name="input">Input.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are between 0 - 16 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBits(ushort input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 16))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16 or set 'null' to not truncated.", nameof(truncateToWidthBits));
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
        /// <param name="input">Input.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are between 0 - 32 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBits(uint input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 32))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 32 or set 'null' to not truncated.", nameof(truncateToWidthBits));
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
        /// <param name="input">Input.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are between 0 - 64 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetBits(ulong input, int? truncateToWidthBits)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 64))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64 or set 'null' to not truncated.", nameof(truncateToWidthBits));
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
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="outputEndian">Specifies the type of endian for output.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static byte[] GetBytes(CrcStringFormat inputFormat, string input, int? truncateToWidthBits, CrcEndian outputEndian)
        {
            string bin = GetBits(inputFormat, input, truncateToWidthBits);
            int rem = bin.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                bin = bin.PadLeft(bin.Length + truncates, '0');
            }
            int length = bin.Length / 8;
            byte[] result = new byte[length];
            if (outputEndian == CrcEndian.LittleEndian)
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    result[i] = Convert.ToByte(bin.Substring(i * 8, 8), 2);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    result[i] = Convert.ToByte(bin.Substring(i * 8, 8), 2);
                }
            }
            return result;
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
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
            string bin = GetBits(inputFormat, input, truncateToWidthBits);
            int rem = bin.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                bin = bin.PadLeft(bin.Length + truncates, '0');
            }
            int length = bin.Length / 8;
            if (outputEndian == CrcEndian.LittleEndian)
            {
                for (int i = length + outputOffset - 1; i >= outputOffset; i--)
                {
                    outputBuffer[i] = Convert.ToByte(bin.Substring(i * 8, 8), 2);
                }
            }
            else
            {
                for (int i = outputOffset; i < length + outputOffset; i++)
                {
                    outputBuffer[i] = Convert.ToByte(bin.Substring(i * 8, 8), 2);
                }
            }
            return length;
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(CrcStringFormat inputFormat, string input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            string bin = GetBits(inputFormat, input, truncateToWidthBits);
            int rem = bin.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                bin = bin.PadLeft(bin.Length + truncates, '0');
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bin.Length; i += 8)
            {
                byte b = Convert.ToByte(bin.Substring(i, 8), 2);
                result.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result.ToString() : result.ToString().ToUpperInvariant();
        }

        /// <summary>
        /// Convert to the specified format,
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are between 0 - 8 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(byte input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 8))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 8 or set 'null' to not truncated.", nameof(truncateToWidthBits));
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
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are between 0 - 16 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(ushort input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 16))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 16 or set 'null' to not truncated.", nameof(truncateToWidthBits));
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
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are between 0 - 32 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(uint input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 32))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 32 or set 'null' to not truncated.", nameof(truncateToWidthBits));
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
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are between 0 - 64 or set 'null' to not truncated.</param>
        /// <param name="caseSensitivity">Hex case sensitivity.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetHex(ulong input, int? truncateToWidthBits, CrcCaseSensitivity caseSensitivity)
        {
            if (truncateToWidthBits.HasValue && (truncateToWidthBits <= 0 || truncateToWidthBits > 64))
            {
                throw new ArgumentException("Invalid width bits. The allowed values are between 0 - 64 or set 'null' to not truncated.", nameof(truncateToWidthBits));
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
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ushort GetUInt16(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt16(bytes);
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static uint GetUInt32(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt32(bytes);
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ulong GetUInt64(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits, CrcEndian.BigEndian);
            return BEToUInt64(bytes);
        }

        /// <summary>
        /// Convert bits(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the string to the specifies CRC width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static byte GetUInt8(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits, CrcEndian.BigEndian);
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
                result = result.Remove(0, result.Length - length);
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
                result = result.Remove(0, result.Length - length);
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
                result = result.Remove(0, result.Length - length);
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
                result = result.Remove(0, result.Length - length);
            }
            return caseSensitivity == CrcCaseSensitivity.Lower ? result.ToString() : result.ToString().ToUpperInvariant();
        }

        internal static ushort[] GetUInt16Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string bin = GetBits(inputFormat, input, truncateToWidthBits);
            int rem = bin.Length % 16;
            int truncates = rem > 0 ? 16 - rem : 0;
            if (truncates > 0)
            {
                bin = bin.PadLeft(bin.Length + truncates, '0');
            }
            ushort[] result = new ushort[bin.Length / 16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToUInt16(bin.Substring(i * 16, 16), 2);
            }
            return result;
        }

        internal static uint[] GetUInt32Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string bin = GetBits(inputFormat, input, truncateToWidthBits);
            int rem = bin.Length % 32;
            int truncates = rem > 0 ? 32 - rem : 0;
            if (truncates > 0)
            {
                bin = bin.PadLeft(bin.Length + truncates, '0');
            }
            uint[] result = new uint[bin.Length / 32];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToUInt32(bin.Substring(i * 32, 32), 2);
            }
            return result;
        }

        internal static ulong[] GetUInt64Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string bin = GetBits(inputFormat, input, truncateToWidthBits);
            int rem = bin.Length % 64;
            int truncates = rem > 0 ? 64 - rem : 0;
            if (truncates > 0)
            {
                bin = bin.PadLeft(bin.Length + truncates, '0');
            }
            ulong[] result = new ulong[bin.Length / 64];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToUInt64(bin.Substring(i * 64, 64), 2);
            }
            return result;
        }

        internal static byte[] GetUInt8Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string bin = GetBits(inputFormat, input, truncateToWidthBits);
            int rem = bin.Length % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            if (truncates > 0)
            {
                bin = bin.PadLeft(bin.Length + truncates, '0');
            }
            byte[] result = new byte[bin.Length / 8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(bin.Substring(i * 8, 8), 2);
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