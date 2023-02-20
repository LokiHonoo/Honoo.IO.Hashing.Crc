/*
 * Copyright
 *
 * https://github.com/LokiHonoo/development-resources
 * Copyright (C) Loki Honoo 2015. All rights reserved.
 *
 * This code page is published under the terms of the MIT license.
 */

using System.Text;

namespace Honoo.IO
{
    /// <summary>
    /// 二进制对象辅助。
    /// </summary>
    public static class Binaries
    {
        #region 比较

        /// <summary>
        /// 比较字节数组。
        /// </summary>
        /// <param name="bytesA"></param>
        /// <param name="bytesB"></param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static bool Compare(byte[] bytesA, byte[] bytesB)
        {
            if (bytesA.Length == bytesB.Length)
            {
                for (int i = 0; i < bytesA.Length; i++)
                {
                    if (bytesA[i] != bytesB[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 比较字节数组。
        /// </summary>
        /// <param name="bufferA"></param>
        /// <param name="bufferB"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static bool Compare(byte[] bufferA, byte[] bufferB, int length)
        {
            if (bufferA.Length >= length && bufferB.Length >= length)
            {
                for (int i = 0; i < length; i++)
                {
                    if (bufferA[i] != bufferB[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 比较字节数组。
        /// </summary>
        /// <param name="bufferA"></param>
        /// <param name="offsetA"></param>
        /// <param name="bufferB"></param>
        /// <param name="offsetB"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static bool Compare(byte[] bufferA, int offsetA, byte[] bufferB, int offsetB, int length)
        {
            if (bufferA.Length - offsetA >= length && bufferB.Length - offsetB >= length)
            {
                for (int i = 0; i < length; i++)
                {
                    if (bufferA[offsetA + i] != bufferB[offsetB + i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion 比较

        #region 转换

        /// <summary>
        /// 将十六进制字符串转换为字节数组。字符串必须是无分隔符的表示形式。
        /// </summary>
        /// <param name="hex">无分隔符的十六进制字符串。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static byte[] GetHexBytes(string hex)
        {
            byte[] result = new byte[hex.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return result;
        }

        /// <summary>
        /// 移除指定分隔符，将十六进制字符串转换为字节数组。
        /// </summary>
        /// <param name="hex">十六进制字符串。</param>
        /// <param name="remove">要移除的分隔符。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static byte[] GetHexBytes(string hex, string remove)
        {
            return GetHexBytes(hex.Replace(remove, string.Empty));
        }

        /// <summary>
        /// 将字节数组转换为十六进制文本。
        /// </summary>
        /// <param name="bytes">要转换的字节数组。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static string GetHexString(byte[] bytes)
        {
            return GetHexString(bytes, 0, bytes.Length, string.Empty, 0, string.Empty);
        }

        /// <summary>
        /// 将字节数组转换为十六进制文本。
        /// </summary>
        /// <param name="bytes">要转换的字节数组。</param>
        /// <param name="split">指定每个字节之间的分隔符。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static string GetHexString(byte[] bytes, string split)
        {
            return GetHexString(bytes, 0, bytes.Length, split, 0, string.Empty);
        }

        /// <summary>
        /// 将字节数组转换为十六进制文本。
        /// </summary>
        /// <param name="bytes">要转换的字节数组。</param>
        /// <param name="split">指定每个字节之间的分隔符。</param>
        /// <param name="lineBreaks">达到指定的字符个数后换行。设置为 0 不换行。</param>
        /// <param name="indent">指定每行缩进的占位字符。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static string GetHexString(byte[] bytes, string split, int lineBreaks, string indent)
        {
            return GetHexString(bytes, 0, bytes.Length, split, lineBreaks, indent);
        }

        /// <summary>
        /// 将字节数组转换为十六进制文本。
        /// </summary>
        /// <param name="buffer">要转换的字节数组。</param>
        /// <param name="split">指定每个字节之间的分隔符。</param>
        /// <param name="lineBreaks">达到指定的字符个数后换行。设置为 0 不换行。</param>
        /// <param name="indent">指定每行缩进的占位字符。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static string GetHexString(byte[] buffer, int offset, int length, string split, int lineBreaks, string indent)
        {
            StringBuilder result = new StringBuilder();
            int count = 0;
            for (int i = offset; i < length; i++)
            {
                if (count == 0)
                {
                    result.Append(indent);
                }
                else if (lineBreaks > 0 && count >= lineBreaks)
                {
                    result.Append(Environment.NewLine);
                    result.Append(indent);
                    count = 0;
                }
                result.Append(buffer[i].ToString("x2"));
                if (i < length - 1)
                {
                    result.Append(split);
                }
                count++;
            }

            return result.ToString();
        }

        /// <summary>
        /// 指定字节数组大小端，根据字节数组长度（最大 4 字节）转换为对应的数值，并以 Int32 类型输出。
        /// </summary>
        /// <param name="littleEndian">指定大小端。Windows 定义 little endian，Unix 定义 big endian。</param>
        /// <param name="bytes">字节数组。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static int GetInt32(bool littleEndian, byte[] bytes)
        {
            return GetInt32(littleEndian, bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 指定字节数组大小端，根据字节数组长度（最大 4 字节）转换为对应的数值，并以 Int32 类型输出。
        /// </summary>
        /// <param name="littleEndian">指定大小端。Windows 定义 little endian，Unix 定义 big endian。</param>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static int GetInt32(bool littleEndian, byte[] buffer, int offset, int length)
        {
            if (length <= 0 || length > 4)
            {
                throw new ArgumentException("Input length must be between 1 - 4.");
            }
            int result;
            if (length == 4)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFF;
                    result |= (buffer[offset + 1] & 0xFF) << 8;
                    result |= (buffer[offset + 2] & 0xFF) << 16;
                    result |= (buffer[offset + 3] & 0xFF) << 24;
                }
                else
                {
                    result = (buffer[offset] & 0xFF) << 24;
                    result |= (buffer[offset + 1] & 0xFF) << 16;
                    result |= (buffer[offset + 2] & 0xFF) << 8;
                    result |= buffer[offset + 3] & 0xFF;
                }
            }
            else if (length == 3)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFF;
                    result |= (buffer[offset + 1] & 0xFF) << 8;
                    result |= (buffer[offset + 2] & 0xFF) << 16;
                }
                else
                {
                    result = (buffer[offset] & 0xFF) << 16;
                    result |= (buffer[offset + 1] & 0xFF) << 8;
                    result |= buffer[offset + 2] & 0xFF;
                }
            }
            else if (length == 2)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFF;
                    result |= (buffer[offset + 1] & 0xFF) << 8;
                }
                else
                {
                    result = (buffer[offset] & 0xFF) << 8;
                    result |= buffer[offset + 1] & 0xFF;
                }
            }
            else
            {
                result = buffer[offset] & 0xFF;
            }
            return result;
        }

        /// <summary>
        /// 指定字节数组大小端，根据字节数组长度（最大 8 字节）转换为对应的数值，并以 Int64 类型输出。
        /// </summary>
        /// <param name="littleEndian">指定大小端。Windows 定义 little endian，Unix 定义 big endian。</param>
        /// <param name="bytes">字节数组。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static long GetInt64(bool littleEndian, byte[] bytes)
        {
            return GetInt64(littleEndian, bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 指定字节数组大小端，根据字节数组长度（最大 8 字节）转换为对应的数值，并以 Int64 类型输出。
        /// </summary>
        /// <param name="littleEndian">指定大小端。Windows 定义 little endian，Unix 定义 big endian。</param>
        /// <param name="buffer">字节数组。</param>
        /// <param name="offset">读取的字节数组偏移量。</param>
        /// <param name="length">读取的字节数量。</param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        public static long GetInt64(bool littleEndian, byte[] buffer, int offset, int length)
        {
            if (length <= 0 || length > 8)
            {
                throw new ArgumentException("Input length must be between 1 - 8.");
            }
            long result;
            if (length == 8)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFFL;
                    result |= (buffer[offset + 1] & 0xFFL) << 8;
                    result |= (buffer[offset + 2] & 0xFFL) << 16;
                    result |= (buffer[offset + 3] & 0xFFL) << 24;
                    result |= (buffer[offset + 4] & 0xFFL) << 32;
                    result |= (buffer[offset + 5] & 0xFFL) << 40;
                    result |= (buffer[offset + 6] & 0xFFL) << 48;
                    result |= (buffer[offset + 7] & 0xFFL) << 56;
                }
                else
                {
                    result = (buffer[offset] & 0xFFL) << 56;
                    result |= (buffer[offset + 1] & 0xFFL) << 48;
                    result |= (buffer[offset + 2] & 0xFFL) << 40;
                    result |= (buffer[offset + 3] & 0xFFL) << 32;
                    result |= (buffer[offset + 4] & 0xFFL) << 24;
                    result |= (buffer[offset + 5] & 0xFFL) << 16;
                    result |= (buffer[offset + 6] & 0xFFL) << 8;
                    result |= buffer[offset + 7] & 0xFFL;
                }
            }
            else if (length == 7)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFFL;
                    result |= (buffer[offset + 1] & 0xFFL) << 8;
                    result |= (buffer[offset + 2] & 0xFFL) << 16;
                    result |= (buffer[offset + 3] & 0xFFL) << 24;
                    result |= (buffer[offset + 4] & 0xFFL) << 32;
                    result |= (buffer[offset + 5] & 0xFFL) << 40;
                    result |= (buffer[offset + 6] & 0xFFL) << 48;
                }
                else
                {
                    result = (buffer[offset] & 0xFFL) << 48;
                    result |= (buffer[offset + 1] & 0xFFL) << 40;
                    result |= (buffer[offset + 2] & 0xFFL) << 32;
                    result |= (buffer[offset + 3] & 0xFFL) << 24;
                    result |= (buffer[offset + 4] & 0xFFL) << 16;
                    result |= (buffer[offset + 5] & 0xFFL) << 8;
                    result |= buffer[offset + 6] & 0xFFL;
                }
            }
            else if (length == 6)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFFL;
                    result |= (buffer[offset + 1] & 0xFFL) << 8;
                    result |= (buffer[offset + 2] & 0xFFL) << 16;
                    result |= (buffer[offset + 3] & 0xFFL) << 24;
                    result |= (buffer[offset + 4] & 0xFFL) << 32;
                    result |= (buffer[offset + 5] & 0xFFL) << 40;
                }
                else
                {
                    result = (buffer[offset] & 0xFFL) << 40;
                    result |= (buffer[offset + 1] & 0xFFL) << 32;
                    result |= (buffer[offset + 2] & 0xFFL) << 24;
                    result |= (buffer[offset + 3] & 0xFFL) << 16;
                    result |= (buffer[offset + 4] & 0xFFL) << 8;
                    result |= buffer[offset + 5] & 0xFFL;
                }
            }
            else if (length == 5)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFFL;
                    result |= (buffer[offset + 1] & 0xFFL) << 8;
                    result |= (buffer[offset + 2] & 0xFFL) << 16;
                    result |= (buffer[offset + 3] & 0xFFL) << 24;
                    result |= (buffer[offset + 4] & 0xFFL) << 32;
                }
                else
                {
                    result = (buffer[offset] & 0xFFL) << 32;
                    result |= (buffer[offset + 1] & 0xFFL) << 24;
                    result |= (buffer[offset + 2] & 0xFFL) << 16;
                    result |= (buffer[offset + 3] & 0xFFL) << 8;
                    result |= buffer[offset + 4] & 0xFFL;
                }
            }
            else if (length == 4)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFFL;
                    result |= (buffer[offset + 1] & 0xFFL) << 8;
                    result |= (buffer[offset + 2] & 0xFFL) << 16;
                    result |= (buffer[offset + 3] & 0xFFL) << 24;
                }
                else
                {
                    result = (buffer[offset] & 0xFFL) << 24;
                    result |= (buffer[offset + 1] & 0xFFL) << 16;
                    result |= (buffer[offset + 2] & 0xFFL) << 8;
                    result |= buffer[offset + 3] & 0xFFL;
                }
            }
            else if (length == 3)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFFL;
                    result |= (buffer[offset + 1] & 0xFFL) << 8;
                    result |= (buffer[offset + 2] & 0xFFL) << 16;
                }
                else
                {
                    result = (buffer[offset] & 0xFFL) << 16;
                    result |= (buffer[offset + 1] & 0xFFL) << 8;
                    result |= buffer[offset + 2] & 0xFFL;
                }
            }
            else if (length == 2)
            {
                if (littleEndian)
                {
                    result = buffer[offset] & 0xFFL;
                    result |= (buffer[offset + 1] & 0xFFL) << 8;
                }
                else
                {
                    result = (buffer[offset] & 0xFFL) << 8;
                    result |= buffer[offset + 1] & 0xFFL;
                }
            }
            else
            {
                result = buffer[offset] & 0xFFL;
            }
            return result;
        }

        #endregion 转换
    }
}