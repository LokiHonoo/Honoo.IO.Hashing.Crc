using Honoo.IO;
using Honoo.IO.Hashing;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Test
{
    internal static class Speed
    {
        internal static void Test()
        {
            int length = 8 * 1024;
            byte[] input = new byte[length];
            new Random().NextBytes(input);
            int times = 10000;
            //
            Console.WriteLine("|algorithm|core|table|elapsed|");
            Console.WriteLine("|:-------:|:--:|:---:|------:|");
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
            double spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out string unit);
            Console.WriteLine($"|{crc.Name}|32 bits|table|" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|{crc.Name}|32 bits||" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|{crc.Name}|sharding 8 bits|table|" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|{crc.Name}|sharding 8 bits||" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|{crc.Name}|sharding 32 bits|table|" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|{crc.Name}|sharding 32 bits||" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|{crc.Name}|64 bits|table|" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|{crc.Name}|sharding 32 bits|table|" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|{crc.Name}|sharding 32 bits|table|" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|[CRC32](https://www.nuget.org/packages/System.IO.Hashing/)||table|" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|[CRC64](https://www.nuget.org/packages/System.IO.Hashing/)||table|" + spd + " " + unit + "|");
            //
            //
            //
            var crc32Algorithm = new Force.Crc32.Crc32Algorithm();
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc32Algorithm.ComputeHash(input);
            }
            stopwatch.Stop();
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|[Crc32.NET](https://github.com/force-net/Crc32.NET)||table|" + spd + " " + unit + "|");
            //
            //
            //
            var crc32cAlgorithm = new Force.Crc32.Crc32CAlgorithm();
            stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc32cAlgorithm.ComputeHash(input);
            }
            stopwatch.Stop();
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|[Crc32C.NET](https://github.com/force-net/Crc32.NET)||table|" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|SHA1|system||" + spd + " " + unit + "|");
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
            spd = Numeric.GetSpeed((long)length * times * 1000 / stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|SHA256|system||" + spd + " " + unit + "|");
        }
    }
}