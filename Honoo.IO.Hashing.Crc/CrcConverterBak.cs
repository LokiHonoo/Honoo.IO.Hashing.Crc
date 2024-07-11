//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Numerics;
//using System.Text;

//namespace Honoo.IO.Hashing
//{
//    /// <summary>
//    /// Crc converter.
//    /// </summary>
//    public sealed class CrcConverterBak
//    {
//        /// <summary>
//        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
//        /// </summary>
//        /// <param name="inputEndian">Indicates whether the type of endian of <paramref name="inputBuffer"/>.</param>
//        /// <param name="inputBuffer">Checksum buffer bytes.</param>
//        /// <param name="inputOffset">The starting position within <paramref name="inputBuffer"/>.</param>
//        /// <param name="outputWidth">Converter to indicates width.</param>
//        /// <returns></returns>
//        /// <exception cref="Exception"/>
//        public static string ToBinaryString(Endian inputEndian, byte[] inputBuffer, int inputOffset, int outputWidth)
//        {
//            if (inputBuffer is null)
//            {
//                throw new ArgumentNullException(nameof(inputBuffer));
//            }
//            int checksumLength = (int)Math.Ceiling(outputWidth / 8d);
//            StringBuilder result = new StringBuilder();
//            if (inputEndian == Endian.LittleEndian)
//            {
//                for (int i = 0; i < checksumLength; i++)
//                {
//                    result.Append(Convert.ToString(inputBuffer[checksumLength - 1 - i + inputOffset], 2).PadLeft(8, '0'));
//                }
//            }
//            else
//            {
//                for (int i = 0; i < checksumLength; i++)
//                {
//                    result.Append(Convert.ToString(inputBuffer[i + inputOffset], 2).PadLeft(8, '0'));
//                }
//            }
//            if (result.Length > outputWidth)
//            {
//                result.Remove(0, result.Length - outputWidth);
//            }
//            else if (result.Length < outputWidth)
//            {
//                result.Insert(0, "0", outputWidth - result.Length);
//            }
//            return result.ToString();
//        }

//        /// <summary>
//        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
//        /// </summary>
//        /// <param name="inputEndian">Indicates whether the type of endian of <paramref name="inputBuffer"/>.</param>
//        /// <param name="inputBuffer">Checksum buffer bytes.</param>
//        /// <param name="inputOffset">The starting position within <paramref name="inputBuffer"/>.</param>
//        /// <param name="outputWidth">Converter to indicates width.</param>
//        /// <returns></returns>
//        /// <exception cref="Exception"/>
//        public static byte ToByte(Endian inputEndian, byte[] inputBuffer, int inputOffset, int outputWidth)
//        {
//            if (inputBuffer is null)
//            {
//                throw new ArgumentNullException(nameof(inputBuffer));
//            }
//            int rem = outputWidth % 8;
//            int truncates = rem > 0 ? 8 - rem : 0;
//            byte result;
//            if (inputEndian == Endian.LittleEndian)
//            {
//                result = inputBuffer[inputOffset];
//            }
//            else
//            {
//                int checksumLength = (int)Math.Ceiling(outputWidth / 8d);
//                result = inputBuffer[checksumLength - 1 + inputOffset];
//            }
//            if (outputWidth < 8 && truncates > 0)
//            {
//                result <<= truncates;
//                result >>= truncates;
//            }
//            return result;
//        }

//        /// <summary>
//        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
//        /// </summary>
//        /// <param name="inputEndian">Indicates whether the type of endian of <paramref name="inputBuffer"/>.</param>
//        /// <param name="inputBuffer">Checksum buffer bytes.</param>
//        /// <param name="inputOffset">The starting position within <paramref name="inputBuffer"/>.</param>
//        /// <param name="outputWidth">Converter to indicates width.</param>
//        /// <returns></returns>
//        /// <exception cref="Exception"/>
//        public static string ToHexString(Endian inputEndian, byte[] inputBuffer, int inputOffset, int outputWidth)
//        {
//            if (inputBuffer is null)
//            {
//                throw new ArgumentNullException(nameof(inputBuffer));
//            }
//            int rem = outputWidth % 4;
//            int truncates = rem > 0 ? 4 - rem : 0;
//            string bin = ToBinaryString(inputEndian, inputBuffer, inputOffset, outputWidth);
//            StringBuilder result = new StringBuilder();
//            if (truncates > 0)
//            {
//                bin = bin.PadLeft(truncates, '0');
//            }
//            for (int i = 0; i < bin.Length; i += 4)
//            {
//                switch (bin.Substring(i, 4))
//                {
//                    case "0000": result.Append('0'); break;
//                    case "0001": result.Append('1'); break;
//                    case "0010": result.Append('2'); break;
//                    case "0011": result.Append('3'); break;
//                    case "0100": result.Append('4'); break;
//                    case "0101": result.Append('5'); break;
//                    case "0110": result.Append('6'); break;
//                    case "0111": result.Append('7'); break;
//                    case "1000": result.Append('8'); break;
//                    case "1001": result.Append('9'); break;
//                    case "1010": result.Append('A'); break;
//                    case "1011": result.Append('B'); break;
//                    case "1100": result.Append('C'); break;
//                    case "1101": result.Append('D'); break;
//                    case "1110": result.Append('E'); break;
//                    case "1111": result.Append('F'); break;
//                    default: break;
//                }
//            }
//            return result.ToString();
//        }

//        /// <summary>
//        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
//        /// </summary>
//        /// <param name="inputEndian">Indicates whether the type of endian of <paramref name="inputBuffer"/>.</param>
//        /// <param name="inputBuffer">Checksum buffer bytes.</param>
//        /// <param name="inputOffset">The starting position within <paramref name="inputBuffer"/>.</param>
//        /// <param name="outputWidth">Converter to indicates width.</param>
//        /// <returns></returns>
//        /// <exception cref="Exception"/>
//        public static ushort ToUInt16(Endian inputEndian, byte[] inputBuffer, int inputOffset, int outputWidth)
//        {
//            if (inputBuffer is null)
//            {
//                throw new ArgumentNullException(nameof(inputBuffer));
//            }
//            int rem = outputWidth % 8;
//            int truncates = rem > 0 ? 8 - rem : 0;
//            int checksumLength = (int)Math.Ceiling(outputWidth / 8d);
//            ushort result;
//            if (inputEndian == Endian.LittleEndian)
//            {
//                result = inputBuffer[inputOffset];
//                if (checksumLength > 1) result |= (ushort)((inputBuffer[inputOffset + 1] & 0xFF) << 8);
//            }
//            else
//            {
//                result = inputBuffer[checksumLength - 1 + inputOffset];
//                if (checksumLength > 1) result |= (ushort)((inputBuffer[checksumLength - 1 + inputOffset - 1] & 0xFF) << 8);
//            }
//            if (outputWidth < 16 && truncates > 0)
//            {
//                result <<= truncates;
//                result >>= truncates;
//            }
//            return result;
//        }

//        /// <summary>
//        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
//        /// </summary>
//        /// <param name="inputEndian">Indicates whether the type of endian of <paramref name="inputBuffer"/>.</param>
//        /// <param name="inputBuffer">Checksum buffer bytes.</param>
//        /// <param name="inputOffset">The starting position within <paramref name="inputBuffer"/>.</param>
//        /// <param name="outputWidth">Converter to indicates width.</param>
//        /// <returns></returns>
//        /// <exception cref="Exception"/>
//        public static uint ToUInt32(Endian inputEndian, byte[] inputBuffer, int inputOffset, int outputWidth)
//        {
//            if (inputBuffer is null)
//            {
//                throw new ArgumentNullException(nameof(inputBuffer));
//            }
//            int rem = outputWidth % 8;
//            int truncates = rem > 0 ? 8 - rem : 0;
//            int checksumLength = (int)Math.Ceiling(outputWidth / 8d);
//            uint result;
//            if (inputEndian == Endian.LittleEndian)
//            {
//                result = inputBuffer[inputOffset];
//                if (checksumLength > 1) result |= (inputBuffer[inputOffset + 1] & 0xFFU) << 8;
//                if (checksumLength > 2) result |= (inputBuffer[inputOffset + 2] & 0xFFU) << 16;
//                if (checksumLength > 3) result |= (inputBuffer[inputOffset + 3] & 0xFFU) << 24;
//            }
//            else
//            {
//                result = inputBuffer[checksumLength - 1 + inputOffset];
//                if (checksumLength > 1) result |= (inputBuffer[checksumLength - 1 + inputOffset - 1] & 0xFFU) << 8;
//                if (checksumLength > 2) result |= (inputBuffer[checksumLength - 1 + inputOffset - 2] & 0xFFU) << 16;
//                if (checksumLength > 3) result |= (inputBuffer[checksumLength - 1 + inputOffset - 3] & 0xFFU) << 24;
//            }
//            if (outputWidth < 32 && truncates > 0)
//            {
//                result <<= truncates;
//                result >>= truncates;
//            }
//            return result;
//        }

//        /// <summary>
//        /// Convert checksum to the specified format, Truncate bits form header if checksum length is greater than target format.
//        /// </summary>
//        /// <param name="inputEndian">Indicates whether the type of endian of <paramref name="inputBuffer"/>.</param>
//        /// <param name="inputBuffer">Checksum buffer bytes.</param>
//        /// <param name="inputOffset">The starting position within <paramref name="inputBuffer"/>.</param>
//        /// <param name="outputWidth">Converter to indicates width.</param>
//        /// <returns></returns>
//        /// <exception cref="Exception"/>
//        public static ulong ToUInt64(Endian inputEndian, byte[] inputBuffer, int inputOffset, int outputWidth)
//        {
//            if (inputBuffer is null)
//            {
//                throw new ArgumentNullException(nameof(inputBuffer));
//            }
//            int rem = outputWidth % 8;
//            int truncates = rem > 0 ? 8 - rem : 0;
//            int checksumLength = (int)Math.Ceiling(outputWidth / 8d);
//            ulong result;
//            if (inputEndian == Endian.LittleEndian)
//            {
//                result = inputBuffer[inputOffset];
//                if (checksumLength > 1) result |= (inputBuffer[inputOffset + 1] & 0xFFUL) << 8;
//                if (checksumLength > 2) result |= (inputBuffer[inputOffset + 2] & 0xFFUL) << 16;
//                if (checksumLength > 3) result |= (inputBuffer[inputOffset + 3] & 0xFFUL) << 24;
//                if (checksumLength > 4) result |= (inputBuffer[inputOffset + 4] & 0xFFUL) << 32;
//                if (checksumLength > 5) result |= (inputBuffer[inputOffset + 5] & 0xFFUL) << 40;
//                if (checksumLength > 6) result |= (inputBuffer[inputOffset + 6] & 0xFFUL) << 48;
//                if (checksumLength > 7) result |= (inputBuffer[inputOffset + 7] & 0xFFUL) << 56;
//            }
//            else
//            {
//                result = inputBuffer[checksumLength - 1 + inputOffset];
//                if (checksumLength > 1) result |= (inputBuffer[checksumLength - 1 + inputOffset - 1] & 0xFFUL) << 8;
//                if (checksumLength > 2) result |= (inputBuffer[checksumLength - 1 + inputOffset - 2] & 0xFFUL) << 16;
//                if (checksumLength > 3) result |= (inputBuffer[checksumLength - 1 + inputOffset - 3] & 0xFFUL) << 24;
//                if (checksumLength > 4) result |= (inputBuffer[checksumLength - 1 + inputOffset - 4] & 0xFFUL) << 32;
//                if (checksumLength > 5) result |= (inputBuffer[checksumLength - 1 + inputOffset - 5] & 0xFFUL) << 40;
//                if (checksumLength > 6) result |= (inputBuffer[checksumLength - 1 + inputOffset - 6] & 0xFFUL) << 48;
//                if (checksumLength > 7) result |= (inputBuffer[checksumLength - 1 + inputOffset - 7] & 0xFFUL) << 56;
//            }
//            if (outputWidth < 64 && truncates > 0)
//            {
//                result <<= truncates;
//                result >>= truncates;
//            }
//            return result;
//        }
//    }
//}