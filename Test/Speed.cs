using Honoo.IO.Hashing;
using System.Diagnostics;
using System.Text;

namespace Test
{
    internal static class Speed
    {
        internal static void Test()
        {
            string str = "1111221ADV233334444555566677788000AAAABB";
            byte[] input = Encoding.UTF8.GetBytes(str);
            //
            Console.WriteLine("|algorithm|core|table|times|elapsed|");
            Console.WriteLine("|:-------:|:--:|:---:|:---:|------:|");
            //
            //
            //
            Stopwatch stopwatch = Stopwatch.StartNew();
            Crc crc = Crc.CreateBy(CrcName.CRC32.Width,
                                 CrcName.CRC32.Refin,
                                 CrcName.CRC32.Refout,
                                 CrcName.CRC32.Poly.ToUInt32(),
                                 CrcName.CRC32.Init.ToUInt32(),
                                 CrcName.CRC32.Xorout.ToUInt32(),
                                 false);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.ComputeFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|32 bits||100000|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = new Crc32();
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.ComputeFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|32 bits|table|100000|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC32.Width,
                             CrcName.CRC32.Refin,
                             CrcName.CRC32.Refout,
                             CrcName.CRC32.Poly.ToHexString(),
                             CrcName.CRC32.Init.ToHexString(),
                             CrcName.CRC32.Xorout.ToHexString(),
                             CrcCore.Sharding8);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.ComputeFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|sharding 8 bits||100000|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC32.Width,
                             CrcName.CRC32.Refin,
                             CrcName.CRC32.Refout,
                             CrcName.CRC32.Poly.ToHexString(),
                             CrcName.CRC32.Init.ToHexString(),
                             CrcName.CRC32.Xorout.ToHexString(),
                             CrcCore.Sharding8Table);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.ComputeFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|sharding 8 bits|table|100000|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC32.Width,
                             CrcName.CRC32.Refin,
                             CrcName.CRC32.Refout,
                             CrcName.CRC32.Poly.ToHexString(),
                             CrcName.CRC32.Init.ToHexString(),
                             CrcName.CRC32.Xorout.ToHexString(),
                             CrcCore.Sharding32);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.ComputeFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|sharding 32 bits||100000|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC32.Width,
                             CrcName.CRC32.Refin,
                             CrcName.CRC32.Refout,
                             CrcName.CRC32.Poly.ToHexString(),
                             CrcName.CRC32.Init.ToHexString(),
                             CrcName.CRC32.Xorout.ToHexString(),
                             CrcCore.Sharding32Table);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.ComputeFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-32|sharding 32 bits|table|100000|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = new Crc64Redis();
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.ComputeFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-64/REDIS|64 bits|table|100000|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC64_REDIS.Width,
                             CrcName.CRC64_REDIS.Refin,
                             CrcName.CRC64_REDIS.Refin,
                             CrcName.CRC64_REDIS.Poly.ToString(),
                             CrcName.CRC64_REDIS.Init.ToString(),
                             CrcName.CRC64_REDIS.Xorout.ToString(),
                             CrcCore.Sharding32Table);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.ComputeFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CRC-64/REDIS|sharding 32 bits|table|100000|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(217,
                             true,
                             true,
                             "0x7204CA357EDF00742A12C562157732D9",
                             "0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF",
                             "0x00000000000000000000000000000000",
                             CrcCore.Sharding32Table);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.Update(input);
                crc.ComputeFinal();
            }
            stopwatch.Stop();
            Console.WriteLine("|CUSTUM CRC-217|sharding 32 bits|table|100000|" + stopwatch.ElapsedMilliseconds + " ms|");
        }
    }
}