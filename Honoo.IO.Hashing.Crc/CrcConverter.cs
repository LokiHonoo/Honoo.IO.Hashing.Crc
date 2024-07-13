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
        /// Convert Binary(e.g. 0b11110000), Hex(e.g. 0xFFFF) to the specified format.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width.</param>
        /// <returns></returns>
        public static byte[] ToBytes(string input, int? truncateToWidth = null)
        {
            string bin = ToBinary(input, false, truncateToWidth);
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

        /// <summary>
        /// Convert binary(e.g. 0b11110000), hex(e.g. 0xFFFF) to the specified format,
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="outputFormat">Specifies the type of format for output.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ToString(string input, StringFormat outputFormat, int? truncateToWidth = null)
        {
            switch (outputFormat)
            {
                case StringFormat.Binary: return ToBinary(input, false, truncateToWidth);
                case StringFormat.BinaryWithPrefix: return ToBinary(input, true, truncateToWidth);
                case StringFormat.Hex: return ToHex(input, false, truncateToWidth);
                case StringFormat.HexWithPrefix: return ToHex(input, true, truncateToWidth);
                default: throw new ArgumentException("Invalid StringFormat value.", nameof(outputFormat));
            }
        }

        /// <summary>
        /// Convert Binary(e.g. 0b11110000), Hex(e.g. 0xFFFF) to the specified format.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width.</param>
        /// <returns></returns>
        public static ushort ToUInt16(string input, int? truncateToWidth = null)
        {
            byte[] bytes = ToBytes(input, truncateToWidth);
            return BEToUInt16(bytes);
        }

        /// <summary>
        /// Convert Binary(e.g. 0b11110000), Hex(e.g. 0xFFFF) to the specified format.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width.</param>
        /// <returns></returns>
        public static uint ToUInt32(string input, int? truncateToWidth = null)
        {
            byte[] bytes = ToBytes(input, truncateToWidth);
            return BEToUInt32(bytes);
        }

        /// <summary>
        /// Convert Binary(e.g. 0b11110000), Hex(e.g. 0xFFFF) to the specified format.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width.</param>
        /// <returns></returns>
        public static ulong ToUInt64(string input, int? truncateToWidth = null)
        {
            byte[] bytes = ToBytes(input, truncateToWidth);
            return BEToUInt64(bytes);
        }

        /// <summary>
        /// Convert Binary(e.g. 0b11110000), Hex(e.g. 0xFFFF) to the specified format.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidth">Truncated the input string to the specifies width.</param>
        /// <returns></returns>
        public static byte ToUInt8(string input, int? truncateToWidth = null)
        {
            byte[] bytes = ToBytes(input, truncateToWidth);
            return BEToUInt8(bytes);
        }

        internal static uint[] GenerateSharding32Value(string input, int? truncateToWidth = null)
        {
            string bin = ToBinary(input, false, truncateToWidth);
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

        internal static byte[] GenerateSharding8Value(string input, int? truncateToWidth = null)
        {
            return ToBytes(input, truncateToWidth);
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

        private static string ToBinary(string input, bool withPrefix, int? truncateToWidth)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"\"{nameof(input)}\" can't be null or white space.", nameof(input));
            }
            if (truncateToWidth <= 0)
            {
                throw new ArgumentException("Invalid checkcum width. The allowed values are more than 0.", nameof(truncateToWidth));
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
            int width = truncateToWidth > 0 ? truncateToWidth.Value : bin.Length;
            if (bin.Length > width)
            {
                bin.Remove(0, bin.Length - width);
            }
            else if (bin.Length < width)
            {
                bin.Insert(0, "0", width - bin.Length);
            }
            return withPrefix ? "0b" + bin.ToString() : bin.ToString();
        }

        private static string ToHex(string input, bool withPrefix, int? truncateToWidth)
        {
            byte[] bytes = ToBytes(input, truncateToWidth);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(Convert.ToString(bytes[i], 16).PadLeft(2, '0'));
            }
            return withPrefix ? "0x" + result.ToString() : result.ToString();
        }
    }
}