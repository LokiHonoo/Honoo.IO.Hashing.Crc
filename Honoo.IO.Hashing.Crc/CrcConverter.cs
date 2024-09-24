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
        /// Convert binary(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input string to the specifies crc width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static byte[] ToBytes(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            return GetBytes(inputFormat, input, truncateToWidthBits);
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input string to the specifies crc width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <param name="outputFormat">Specifies the type of format for output.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ToString(CrcStringFormat inputFormat, string input, int? truncateToWidthBits, CrcStringFormat outputFormat)
        {
            switch (outputFormat)
            {
                case CrcStringFormat.Binary: return GetBinaryString(inputFormat, input, truncateToWidthBits);
                case CrcStringFormat.Hex: return GetHexString(inputFormat, input, truncateToWidthBits);
                default: throw new ArgumentException("Invalid crc string format.", nameof(outputFormat));
            }
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input string to the specifies crc width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static ushort ToUInt16(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits);
            return BEToUInt16(bytes);
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input string to the specifies crc width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static uint ToUInt32(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits);
            return BEToUInt32(bytes);
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input string to the specifies crc width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static ulong ToUInt64(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits);
            return BEToUInt64(bytes);
        }

        /// <summary>
        /// Convert binary(e.g. 0b11110000 or 11110000), hex(e.g. 0xFF55 or FF55) to the specified format,
        /// </summary>
        /// <param name="inputFormat">Specifies the type of format for input string.</param>
        /// <param name="input">Input string.</param>
        /// <param name="truncateToWidthBits">Truncated the input string to the specifies crc width in bits. The allowed values are more than 0 or set 'null' to not truncated.</param>
        /// <returns></returns>
        public static byte ToUInt8(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            byte[] bytes = GetBytes(inputFormat, input, truncateToWidthBits);
            return BEToUInt8(bytes);
        }

        internal static ushort[] ToUInt16Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string bin = GetBinaryString(inputFormat, input, truncateToWidthBits);
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

        internal static uint[] ToUInt32Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string bin = GetBinaryString(inputFormat, input, truncateToWidthBits);
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

        internal static ulong[] ToUInt64Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string bin = GetBinaryString(inputFormat, input, truncateToWidthBits);
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

        internal static byte[] ToUInt8Array(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            return GetBytes(inputFormat, input, truncateToWidthBits);
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

        private static string GetBinaryString(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"\"{nameof(input)}\" can't be null or white space.", nameof(input));
            }
            if (truncateToWidthBits.HasValue && truncateToWidthBits <= 0)
            {
                throw new ArgumentException("Invalid width. The allowed values are more than 0 or set 'null' to not truncated.", nameof(truncateToWidthBits));
            }
            input = input.Trim();
            StringBuilder bin = new StringBuilder();
            switch (inputFormat)
            {
                case CrcStringFormat.Binary:
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

                default: throw new ArgumentException("Invalid crc string format.", nameof(inputFormat));
            }
            int width = truncateToWidthBits.HasValue && truncateToWidthBits > 0 ? truncateToWidthBits.Value : bin.Length;
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

        private static byte[] GetBytes(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string bin = GetBinaryString(inputFormat, input, truncateToWidthBits);
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

        private static string GetHexString(CrcStringFormat inputFormat, string input, int? truncateToWidthBits)
        {
            string bin = GetBinaryString(inputFormat, input, truncateToWidthBits);
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