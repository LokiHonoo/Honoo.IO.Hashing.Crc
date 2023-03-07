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
            Stopwatch stopwatch = Stopwatch.StartNew();
            Crc crc = new Crc64Xz(false);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.DoFinal(input);
            }
            stopwatch.Stop();
            Console.WriteLine("CRC-64 calc 100000 times elapsed: " + stopwatch.ElapsedMilliseconds + " ms");

            crc = new Crc64Xz(true);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.DoFinal(input);
            }
            stopwatch.Stop();
            Console.WriteLine("CRC-64 with table calc 100000 times elapsed: " + stopwatch.ElapsedMilliseconds + " ms");

            crc = new CrcCustom(64, true, true, "0x42F0E1EBA9EA3693", "0xFFFFFFFFFFFFFFFF", "0xFFFFFFFFFFFFFFFF", CrcCore.Sharding8);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.DoFinal(true, input);
            }
            stopwatch.Stop();
            Console.WriteLine("CRC-64 Sharding8 core calc 100000 times elapsed: " + stopwatch.ElapsedMilliseconds + " ms");

            crc = new CrcCustom(64, true, true, "0x42F0E1EBA9EA3693", "0xFFFFFFFFFFFFFFFF", "0xFFFFFFFFFFFFFFFF", CrcCore.Sharding8Table);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.DoFinal(true, input);
            }
            stopwatch.Stop();
            Console.WriteLine("CRC-64 Sharding8 core with table calc 100000 times elapsed: " + stopwatch.ElapsedMilliseconds + " ms");

            crc = new CrcCustom(64, true, true, "0x42F0E1EBA9EA3693", "0xFFFFFFFFFFFFFFFF", "0xFFFFFFFFFFFFFFFF", CrcCore.Sharding32);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.DoFinal(true, input);
            }
            stopwatch.Stop();
            Console.WriteLine("CRC-64 Sharding32 core calc 100000 times elapsed: " + stopwatch.ElapsedMilliseconds + " ms");

            crc = new CrcCustom(64, true, true, "0x42F0E1EBA9EA3693", "0xFFFFFFFFFFFFFFFF", "0xFFFFFFFFFFFFFFFF", CrcCore.Sharding32Table);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.DoFinal(true, input);
            }
            stopwatch.Stop();
            Console.WriteLine("CRC-64 Sharding32 core with table calc 100000 times elapsed: " + stopwatch.ElapsedMilliseconds + " ms");
        }
    }
}