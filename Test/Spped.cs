using Honoo.IO.Hashing;
using System.Diagnostics;
using System.Text;

namespace Test
{
    internal static class Spped
    {
        internal static void Test()
        {
            string str = "1111221ADV233334444555566677788000AAAABB";
            byte[] input = Encoding.UTF8.GetBytes(str);

            Console.WriteLine("|algorithm|core|table|times|elapsed|");
            Console.WriteLine("|:-------:|:--:|:---:|:---:|------:|");

            Stopwatch stopwatch = Stopwatch.StartNew();
            Crc crc = new Crc32(false);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.DoFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|32 bits|false|100000|" + stopwatch.ElapsedMilliseconds + " ms|");

            crc = new Crc32(true);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.DoFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|32 bits|true|100000|" + stopwatch.ElapsedMilliseconds + " ms|");

            crc = new CrcCustom(32, true, true, "0x04C11DB7", "0xFFFFFFFF", "0xFFFFFFFF", CrcCore.Sharding8);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.DoFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|sharding 8 bits|false|100000|" + stopwatch.ElapsedMilliseconds + " ms|");

            crc = new CrcCustom(32, true, true, "0x04C11DB7", "0xFFFFFFFF", "0xFFFFFFFF", CrcCore.Sharding8Table);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.DoFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|sharding 8 bits|true|100000|" + stopwatch.ElapsedMilliseconds + " ms|");

            crc = new CrcCustom(32, true, true, "0x04C11DB7", "0xFFFFFFFF", "0xFFFFFFFF", CrcCore.Sharding32);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.DoFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|sharding 32 bits|false|100000|" + stopwatch.ElapsedMilliseconds + " ms|");

            crc = new CrcCustom(32, true, true, "0x04C11DB7", "0xFFFFFFFF", "0xFFFFFFFF", CrcCore.Sharding32Table);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.DoFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|sharding 32 bits|true|100000|" + stopwatch.ElapsedMilliseconds + " ms|");

            crc = new Crc64Redis();
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.DoFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-64/REDIS|64 bits|true|100000|" + stopwatch.ElapsedMilliseconds + " ms|");

            crc = new CrcCustom(64, true, true, "0xAD93D23594C935A9", "0x0000000000000000", "0x0000000000000000", CrcCore.Sharding32Table);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.DoFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-64/REDIS|sharding 32 bits|true|100000|" + stopwatch.ElapsedMilliseconds + " ms|");
        }
    }
}