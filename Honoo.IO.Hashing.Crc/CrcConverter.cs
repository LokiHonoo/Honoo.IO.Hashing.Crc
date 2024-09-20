using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Crc converter.
    /// </summary>
    public sealed class CrcConverter
    {
        /// <summary>
        /// Convert binary(e.g. 0b11110000), hex(e.g. 0xFF55) to the specified format,
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static byte[] ToBytes(string input, int? truncateToWidth)
        {
            return GetBytes(input, truncateToWidth);
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000), hex(e.g. 0xFF55) to the specified format,
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="outputFormat">Specifies the type of format for output.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ToString(string input, int? truncateToWidth, NumericsStringFormat outputFormat)
        {
            switch (outputFormat)
            {
                case NumericsStringFormat.Binary: return GetBinaryString(input, truncateToWidth);
                case NumericsStringFormat.Hex: return GetHexString(input, truncateToWidth);
                default: throw new ArgumentException("Invalid NumericsStringFormat value.", nameof(outputFormat));
            }
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000), hex(e.g. 0xFF55) to the specified format,
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static ushort ToUInt16(string input, int? truncateToWidth)
        {
            byte[] bytes = GetBytes(input, truncateToWidth);
            return BEToUInt16(bytes);
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000), hex(e.g. 0xFF55) to the specified format,
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static uint ToUInt32(string input, int? truncateToWidth)
        {
            byte[] bytes = GetBytes(input, truncateToWidth);
            return BEToUInt32(bytes);
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000), hex(e.g. 0xFF55) to the specified format,
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static ulong ToUInt64(string input, int? truncateToWidth)
        {
            byte[] bytes = GetBytes(input, truncateToWidth);
            return BEToUInt64(bytes);
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000), hex(e.g. 0xFF55) to the specified format,
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static byte ToUInt8(string input, int? truncateToWidth)
        {
            byte[] bytes = GetBytes(input, truncateToWidth);
            return BEToUInt8(bytes);
        }

        internal static uint[] ToUInt32Array(string input, int? truncateToWidth)
        {
            string bin = GetBinaryString(input, truncateToWidth);
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

        internal static byte[] ToUInt8Array(string input, int? truncateToWidth)
        {
            return GetBytes(input, truncateToWidth);
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

        private static string GetBinaryString(string input, int? truncateToWidth)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"\"{nameof(input)}\" can't be null or white space.", nameof(input));
            }
            if (truncateToWidth.HasValue && truncateToWidth <= 0)
            {
                throw new ArgumentException("Invalid width. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidth));
            }
            input = input.Trim();
            StringBuilder bin = new StringBuilder();
            if (input.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase) || input.StartsWith("&h", StringComparison.InvariantCultureIgnoreCase))
            {
                for (int i = 2; i < input.Length; i++)
                {
                    bin.Append(Convert.ToString(Convert.ToByte(input[i].ToString(), 16), 2).PadLeft(4, '0'));
                }
            }
            else if (input.StartsWith("0b", StringComparison.InvariantCultureIgnoreCase))
            {
                bin.Append(input, 2, input.Length - 2);
            }
            int width = truncateToWidth.HasValue && truncateToWidth > 0 ? truncateToWidth.Value : bin.Length;
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

        private static byte[] GetBytes(string input, int? truncateToWidth)
        {
            string bin = GetBinaryString(input, truncateToWidth);
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

        private static string GetHexString(string input, int? truncateToWidth)
        {
            string bin = GetBinaryString(input, truncateToWidth);
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
            return result.ToString();
        }
    }
}