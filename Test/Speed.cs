using Honoo.IO.Hashing;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Test
{
    internal static class Speed
    {
        internal static void Test()
        {
            byte[] input = new byte[8 * 1024];
            new Random().NextBytes(input);
            int times = 10000;
            //
            Console.WriteLine($"Byte length - {input.Length}");
            Console.WriteLine();
            Console.WriteLine("|algorithm|core|table|times|elapsed|");
            Console.WriteLine("|:-------:|:--:|:---:|:---:|------:|");
            //
            //
            //
            Crc crc = new Crc32();
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(out uint _);
            }
            stopwatch.Stop();
            Console.WriteLine($"|{crc.Name}|32 bits|table|{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name,
                                  CrcName.CRC32.Width,
                                  CrcName.CRC32.Refin,
                                  CrcName.CRC32.Refout,
                                  CrcName.CRC32.Poly.ToUInt32(),
                                  CrcName.CRC32.Init.ToUInt32(),
                                  CrcName.CRC32.Xorout.ToUInt32(),
                                  false);
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(out uint _);
            }
            stopwatch.Stop();
            Console.WriteLine($"|{crc.Name}|32 bits||{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name,
                               CrcName.CRC32.Width,
                               CrcName.CRC32.Refin,
                               CrcName.CRC32.Refout,
                               CrcName.CRC32.Poly.ToHexString(),
                               CrcName.CRC32.Init.ToHexString(),
                               CrcName.CRC32.Xorout.ToHexString(),
                               CrcCore.Sharding8Table);
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(out uint _);
            }
            stopwatch.Stop();
            Console.WriteLine($"|{crc.Name}|sharding 8 bits|table|{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name,
                               CrcName.CRC32.Width,
                               CrcName.CRC32.Refin,
                               CrcName.CRC32.Refout,
                               CrcName.CRC32.Poly.ToHexString(),
                               CrcName.CRC32.Init.ToHexString(),
                               CrcName.CRC32.Xorout.ToHexString(),
                               CrcCore.Sharding8);
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(out uint _);
            }
            stopwatch.Stop();
            Console.WriteLine($"|{crc.Name}|sharding 8 bits||{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name,
                               CrcName.CRC32.Width,
                               CrcName.CRC32.Refin,
                               CrcName.CRC32.Refout,
                               CrcName.CRC32.Poly.ToHexString(),
                               CrcName.CRC32.Init.ToHexString(),
                               CrcName.CRC32.Xorout.ToHexString(),
                               CrcCore.Sharding32Table);
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(out uint _);
            }
            stopwatch.Stop();
            Console.WriteLine($"|{crc.Name}|sharding 32 bits|table|{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name,
                               CrcName.CRC32.Width,
                               CrcName.CRC32.Refin,
                               CrcName.CRC32.Refout,
                               CrcName.CRC32.Poly.ToHexString(),
                               CrcName.CRC32.Init.ToHexString(),
                               CrcName.CRC32.Xorout.ToHexString(),
                               CrcCore.Sharding32);
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(out uint _);
            }
            stopwatch.Stop();
            Console.WriteLine($"|{crc.Name}|sharding 32 bits||{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = new Crc64Redis();
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(out uint _);
            }
            stopwatch.Stop();
            Console.WriteLine($"|{crc.Name}|64 bits|table|{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy(CrcName.CRC64_REDIS.Name,
                               CrcName.CRC64_REDIS.Width,
                               CrcName.CRC64_REDIS.Refin,
                               CrcName.CRC64_REDIS.Refin,
                               CrcName.CRC64_REDIS.Poly.ToString(),
                               CrcName.CRC64_REDIS.Init.ToString(),
                               CrcName.CRC64_REDIS.Xorout.ToString(),
                               CrcCore.Sharding32Table);
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(out ulong _);
            }
            stopwatch.Stop();
            Console.WriteLine($"|{crc.Name}|sharding 32 bits|table|{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            crc = Crc.CreateBy("CRC-217/CUSTOM",
                               217,
                               true,
                               true,
                               "0x7204CA357EDF00742A12C562157732D9",
                               "0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF",
                               "0x00000000000000000000000000000000",
                               CrcCore.Sharding32Table);
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(NumericsStringFormat.Hex);
            }
            stopwatch.Stop();
            Console.WriteLine($"|{crc.Name}|sharding 32 bits|table|{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            System.IO.Hashing.Crc32 crc32 = new();
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc32.Append(input);
                crc32.GetCurrentHash();
            }
            stopwatch.Stop();
            Console.WriteLine($"|CRC32|system|table|{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            System.IO.Hashing.Crc64 crc64 = new();
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc64.Append(input);
                crc64.GetCurrentHash();
            }
            stopwatch.Stop();
            Console.WriteLine($"|CRC64|system|table|{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            SHA1 sha1 = SHA1.Create();
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                sha1.ComputeHash(input);
            }
            stopwatch.Stop();
            Console.WriteLine($"|SHA1|system||{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
            //
            //
            //
            SHA256 sha256 = SHA256.Create();
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                sha256.ComputeHash(input);
            }
            stopwatch.Stop();
            Console.WriteLine($"|SHA256|system||{times}|" + stopwatch.ElapsedMilliseconds + " ms|");
        }
    }
}