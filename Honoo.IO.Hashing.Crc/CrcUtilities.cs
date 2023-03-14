using System;
using System.Text;

namespace Honoo.IO.Hashing
{
    /// <summary>
    /// Crc utilities.
    /// </summary>
    public sealed class CrcUtilities
    {
        /// <summary>
        /// Convert checksum/poly/init/xorout hex string to the bytes, Truncate bits form header if the input length is greater than width bits.
        /// </summary>
        /// <param name="inputHex">Input hex string.</param>
        /// <param name="width">Width bits.</param>
        /// <returns></returns>
        public static byte[] GetBytes(string inputHex, int width)
        {
            int rem = width % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            byte[] result = new byte[(int)Math.Ceiling(width / 8d)];
            StringBuilder bin = new StringBuilder();
            if (inputHex.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase) || inputHex.StartsWith("&h", StringComparison.InvariantCultureIgnoreCase))
            {
                inputHex = inputHex.Substring(2, inputHex.Length - 2).Replace("_", null).Replace("-", null);
            }
            else
            {
                inputHex = inputHex.Replace("_", null).Replace("-", null);
            }
            for (int i = 0; i < inputHex.Length; i++)
            {
                bin.Append(Convert.ToString(Convert.ToByte(inputHex[i].ToString(), 16), 2).PadLeft(4, '0'));
            }
            if (bin.Length > width)
            {
                bin.Remove(0, bin.Length - width);
            }
            else if (bin.Length < width)
            {
                bin.Insert(0, "0", width - bin.Length);
            }
            if (truncates > 0)
            {
                bin.Insert(0, "0", truncates);
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(bin.ToString(i * 8, 8), 2);
            }
            return result;
        }

        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of <paramref name="buffer"/>.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="width">Width bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static byte ToByte(bool littleEndian, byte[] buffer, int startIndex, int width)
        {
            int rem = width % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            byte result;
            if (littleEndian)
            {
                result = buffer[startIndex];
            }
            else
            {
                int checksumLength = (int)Math.Ceiling(width / 8d);
                result = buffer[checksumLength - 1 + startIndex];
            }
            if (width < 8 && truncates > 0)
            {
                result <<= truncates;
                result >>= truncates;
            }
            return result;
        }

        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of <paramref name="buffer"/>.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="width">Width bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static string ToHexString(bool littleEndian, byte[] buffer, int startIndex, int width)
        {
            int rem = width % 4;
            int truncates = rem > 0 ? 4 - rem : 0;
            int checksumLength = (int)Math.Ceiling(width / 8d);
            StringBuilder bin = new StringBuilder();
            if (littleEndian)
            {
                for (int i = 0; i < checksumLength; i++)
                {
                    bin.Append(Convert.ToString(buffer[checksumLength - 1 - i + startIndex], 2).PadLeft(8, '0'));
                }
            }
            else
            {
                for (int i = 0; i < checksumLength; i++)
                {
                    bin.Append(Convert.ToString(buffer[i + startIndex], 2).PadLeft(8, '0'));
                }
            }
            if (bin.Length > width)
            {
                bin.Remove(0, bin.Length - width);
            }
            else if (bin.Length < width)
            {
                bin.Insert(0, "0", width - bin.Length);
            }
            if (truncates > 0)
            {
                bin.Insert(0, "0", truncates);
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bin.Length; i += 4)
            {
                switch (bin.ToString(i, 4))
                {
                    case "0000": result.Append("0"); break;
                    case "0001": result.Append("1"); break;
                    case "0010": result.Append("2"); break;
                    case "0011": result.Append("3"); break;
                    case "0100": result.Append("4"); break;
                    case "0101": result.Append("5"); break;
                    case "0110": result.Append("6"); break;
                    case "0111": result.Append("7"); break;
                    case "1000": result.Append("8"); break;
                    case "1001": result.Append("9"); break;
                    case "1010": result.Append("A"); break;
                    case "1011": result.Append("B"); break;
                    case "1100": result.Append("C"); break;
                    case "1101": result.Append("D"); break;
                    case "1110": result.Append("E"); break;
                    case "1111": result.Append("F"); break;
                    default: break;
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of <paramref name="buffer"/>.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="width">Width bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static ushort ToUInt16(bool littleEndian, byte[] buffer, int startIndex, int width)
        {
            int rem = width % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            int checksumLength = (int)Math.Ceiling(width / 8d);
            ushort result;
            if (littleEndian)
            {
                result = buffer[startIndex];
                if (checksumLength > 1) result |= (ushort)((buffer[startIndex + 1] & 0xFF) << 8);
            }
            else
            {
                result = buffer[checksumLength - 1 + startIndex];
                if (checksumLength > 1) result |= (ushort)((buffer[checksumLength - 1 + startIndex - 1] & 0xFF) << 8);
            }
            if (width < 16 && truncates > 0)
            {
                result <<= truncates;
                result >>= truncates;
            }
            return result;
        }

        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of <paramref name="buffer"/>.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="width">Width bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static uint ToUInt32(bool littleEndian, byte[] buffer, int startIndex, int width)
        {
            int rem = width % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            int checksumLength = (int)Math.Ceiling(width / 8d);
            uint result;
            if (littleEndian)
            {
                result = buffer[startIndex];
                if (checksumLength > 1) result |= (buffer[startIndex + 1] & 0xFFU) << 8;
                if (checksumLength > 2) result |= (buffer[startIndex + 2] & 0xFFU) << 16;
                if (checksumLength > 3) result |= (buffer[startIndex + 3] & 0xFFU) << 24;
            }
            else
            {
                result = buffer[checksumLength - 1 + startIndex];
                if (checksumLength > 1) result |= (buffer[checksumLength - 1 + startIndex - 1] & 0xFFU) << 8;
                if (checksumLength > 2) result |= (buffer[checksumLength - 1 + startIndex - 2] & 0xFFU) << 16;
                if (checksumLength > 3) result |= (buffer[checksumLength - 1 + startIndex - 3] & 0xFFU) << 24;
            }
            if (width < 32 && truncates > 0)
            {
                result <<= truncates;
                result >>= truncates;
            }
            return result;
        }

        /// <summary>
        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the type of endian of <paramref name="buffer"/>.</param>
        /// <param name="buffer">Checksum buffer bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="buffer"/>.</param>
        /// <param name="width">Width bits.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static ulong ToUInt64(bool littleEndian, byte[] buffer, int startIndex, int width)
        {
            int rem = width % 8;
            int truncates = rem > 0 ? 8 - rem : 0;
            int checksumLength = (int)Math.Ceiling(width / 8d);
            ulong result;
            if (littleEndian)
            {
                result = buffer[startIndex];
                if (checksumLength > 1) result |= (buffer[startIndex + 1] & 0xFFUL) << 8;
                if (checksumLength > 2) result |= (buffer[startIndex + 2] & 0xFFUL) << 16;
                if (checksumLength > 3) result |= (buffer[startIndex + 3] & 0xFFUL) << 24;
                if (checksumLength > 4) result |= (buffer[startIndex + 4] & 0xFFUL) << 32;
                if (checksumLength > 5) result |= (buffer[startIndex + 5] & 0xFFUL) << 40;
                if (checksumLength > 6) result |= (buffer[startIndex + 6] & 0xFFUL) << 48;
                if (checksumLength > 7) result |= (buffer[startIndex + 7] & 0xFFUL) << 56;
            }
            else
            {
                result = buffer[checksumLength - 1 + startIndex];
                if (checksumLength > 1) result |= (buffer[checksumLength - 1 + startIndex - 1] & 0xFFUL) << 8;
                if (checksumLength > 2) result |= (buffer[checksumLength - 1 + startIndex - 2] & 0xFFUL) << 16;
                if (checksumLength > 3) result |= (buffer[checksumLength - 1 + startIndex - 3] & 0xFFUL) << 24;
                if (checksumLength > 4) result |= (buffer[checksumLength - 1 + startIndex - 4] & 0xFFUL) << 32;
                if (checksumLength > 5) result |= (buffer[checksumLength - 1 + startIndex - 5] & 0xFFUL) << 40;
                if (checksumLength > 6) result |= (buffer[checksumLength - 1 + startIndex - 6] & 0xFFUL) << 48;
                if (checksumLength > 7) result |= (buffer[checksumLength - 1 + startIndex - 7] & 0xFFUL) << 56;
            }
            if (width < 64 && truncates > 0)
            {
                result <<= truncates;
                result >>= truncates;
            }
            return result;
        }
    }
}