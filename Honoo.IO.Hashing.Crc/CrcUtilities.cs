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
        /// Specify the type of endian, convert to the numeric value according to the bytes.
        /// </summary>
        /// <param name="littleEndian">Indicates whether the input bytes is little endian.</param>
        /// <param name="input">An array of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="input"/>.</param>
        /// <param name="outputLength">Truncate to the length from the output string header.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static string ToHex(bool littleEndian, byte[] input, int startIndex, int outputLength)
        {
            StringBuilder result = new StringBuilder();
            int length = input.Length + startIndex;
            if (littleEndian)
            {
                for (int i = 0; i < length; i++)
                {
                    result.Append(Convert.ToString(input[length - 1 - i + startIndex], 16).PadLeft(2, '0'));
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    result.Append(Convert.ToString(input[i + startIndex], 16).PadLeft(2, '0'));
                }
            }
            if (result.Length > outputLength)
            {
                return result.ToString(result.Length - outputLength, outputLength).ToUpperInvariant();
            }
            else
            {
                return result.ToString().ToUpperInvariant();
            }
        }

        /// <summary>
        /// Specify the type of endian, convert to the numeric value according to the bytes (read to end, and max to 2 bytes).
        /// </summary>
        /// <param name="littleEndian">Indicates whether the input bytes is little endian.</param>
        /// <param name="input">An array of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="input"/>.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static ushort ToUInt16(bool littleEndian, byte[] input, int startIndex)
        {
            ushort result = 0;
            int length = Math.Min(input.Length + startIndex, 2);
            if (littleEndian)
            {
                for (int i = 0; i < length; i++)
                {
                    result |= (ushort)((input[i + startIndex] & 0xFF) << (8 * i));
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    result |= (ushort)((input[length - 1 - i + startIndex] & 0xFF) << (8 * i));
                }
            }
            return result;
        }

        /// <summary>
        /// Specify the type of endian, convert to the numeric value according to the bytes (read to end, and max to 4 bytes).
        /// </summary>
        /// <param name="littleEndian">Indicates whether the input bytes is little endian.</param>
        /// <param name="input">An array of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="input"/>.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static uint ToUInt32(bool littleEndian, byte[] input, int startIndex)
        {
            uint result = 0;
            int length = Math.Min(input.Length + startIndex, 4);
            if (littleEndian)
            {
                for (int i = 0; i < length; i++)
                {
                    result |= (input[i + startIndex] & 0xFFU) << (8 * i);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    result |= (input[length - 1 - i + startIndex] & 0xFFU) << (8 * i);
                }
            }
            return result;
        }

        /// <summary>
        /// Specify the type of endian, convert to the numeric value according to the bytes (read to end, and max to 8 bytes).
        /// </summary>
        /// <param name="littleEndian">Indicates whether the input bytes is little endian.</param>
        /// <param name="input">An array of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="input"/>.</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public static ulong ToUInt64(bool littleEndian, byte[] input, int startIndex)
        {
            ulong result = 0;
            int length = Math.Min(input.Length + startIndex, 8);
            if (littleEndian)
            {
                for (int i = 0; i < length; i++)
                {
                    result |= (input[i + startIndex] & 0xFFUL) << (8 * i);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    result |= (input[length - 1 - i + startIndex] & 0xFFUL) << (8 * i);
                }
            }

            return result;
        }
    }
}