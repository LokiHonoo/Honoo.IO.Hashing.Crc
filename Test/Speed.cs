using Honoo.IO;
using Honoo.IO.Hashing;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Test
{
    internal static class Speed
    {
        private static readonly Stopwatch _stopwatch = new();

        internal static void Test()
        {
            int length = 8 * 1024;
            byte[] input = new byte[length];
            new Random().NextBytes(input);
            int times = 5000;
            //
            Console.WriteLine("|algorithm|core|table overhead|speed|");
            Console.WriteLine("|:-------:|:--:|:------------:|----:|");
            //
            Crc crc = new Crc32();
            Do(crc, input, times);
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name, CrcName.CRC32.Width, CrcName.CRC32.Refin, CrcName.CRC32.Refout, CrcName.CRC32.Poly.ToUInt32(), CrcName.CRC32.Init.ToUInt32(), CrcName.CRC32.Xorout.ToUInt32(), false);
            Do(crc, input, times);
            //
            crc = new Crc64Redis();
            Do(crc, input, times);
            //
            crc = Crc.CreateBy(CrcName.CRC64_REDIS.Name, CrcName.CRC64_REDIS.Width, CrcName.CRC64_REDIS.Refin, CrcName.CRC64_REDIS.Refout, CrcName.CRC64_REDIS.Poly.ToUInt64(), CrcName.CRC64_REDIS.Init.ToUInt64(), CrcName.CRC64_REDIS.Xorout.ToUInt64(), false);
            Do(crc, input, times);
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name, CrcName.CRC32.Width, CrcName.CRC32.Refin, CrcName.CRC32.Refout, CrcName.CRC32.Poly, CrcName.CRC32.Init, CrcName.CRC32.Xorout, true, CrcCore.Sharding8);
            Do(crc, input, times);
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name, CrcName.CRC32.Width, CrcName.CRC32.Refin, CrcName.CRC32.Refout, CrcName.CRC32.Poly, CrcName.CRC32.Init, CrcName.CRC32.Xorout, true, CrcCore.Sharding16);
            Do(crc, input, times);
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name, CrcName.CRC32.Width, CrcName.CRC32.Refin, CrcName.CRC32.Refout, CrcName.CRC32.Poly, CrcName.CRC32.Init, CrcName.CRC32.Xorout, true, CrcCore.Sharding32);
            Do(crc, input, times);
            //
            crc = Crc.CreateBy(CrcName.CRC32.Name, CrcName.CRC32.Width, CrcName.CRC32.Refin, CrcName.CRC32.Refout, CrcName.CRC32.Poly, CrcName.CRC32.Init, CrcName.CRC32.Xorout, true, CrcCore.Sharding64);
            Do(crc, input, times);
            //
            crc = Crc.CreateBy("CRC-217/CUSTOM",
                               217,
                               true,
                               true,
                               new CrcParameter(CrcStringFormat.Hex, "0x7204CA357EDF00742A12C562157732D9", 217),
                               new CrcParameter(CrcStringFormat.Hex, "0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF", 217),
                              new CrcParameter(CrcStringFormat.Hex, "0x00000000000000000000000000000000", 217),
                               true,
                               CrcCore.Sharding64);
            Do(crc, input, times);
            //
            //
            //
            System.IO.Hashing.Crc32 systemIOHashingCrc32 = new();
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                systemIOHashingCrc32.Append(input);
                systemIOHashingCrc32.GetCurrentHash();
            }
            _stopwatch.Stop();
            double spd = Numeric.GetSpeed((long)input.Length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out string unit);
            Console.WriteLine($"|[System.IO.Hashing.Crc32](https://www.nuget.org/packages/System.IO.Hashing/)||1 KiB|" + spd + " " + unit + "|");
            //
            //
            //
            var k4os = new K4os.Hash.Crc.Crc32();
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                k4os.Update(input);
                k4os.Digest();
            }
            _stopwatch.Stop();
            spd = Numeric.GetSpeed((long)length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|[K4os.Hash.Crc.Crc32](https://github.com/MiloszKrajewski/K4os.Hash.Crc)||1 KiB|" + spd + " " + unit + "|");
            //
            //
            //
            var forceCrc32 = new Force.Crc32.Crc32Algorithm();
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                forceCrc32.ComputeHash(input);
            }
            _stopwatch.Stop();
            spd = Numeric.GetSpeed((long)length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|[Force.Crc32.Crc32Algorithm](https://github.com/force-net/Crc32.NET)||16 KiB|" + spd + " " + unit + "|");
            //
            //
            //
            SHA1 sha1 = SHA1.Create();
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                sha1.ComputeHash(input);
            }
            _stopwatch.Stop();
            spd = Numeric.GetSpeed((long)length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|SHA1|system||" + spd + " " + unit + "|");
            //
            //
            //
            SHA256 sha256 = SHA256.Create();
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                sha256.ComputeHash(input);
            }
            _stopwatch.Stop();
            spd = Numeric.GetSpeed((long)length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|SHA256|system||" + spd + " " + unit + "|");
        }

        private static void Do(Crc crc, byte[] input, int times)
        {
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.Update(input);
                crc.ComputeFinal(out uint _);
            }
            _stopwatch.Stop();
            object obj = crc.CloneTable();
            var tableOverhead = obj switch
            {
                byte[] table => table.Length,
                ushort[] table => table.Length * 2,
                uint[] table => table.Length * 4,
                ulong[] table => table.Length * 8,
                byte[][] table => GetTableOverhead(table),
                ushort[][] table => GetTableOverhead(table),
                uint[][] table => GetTableOverhead(table),
                ulong[][] table => GetTableOverhead(table),
                _ => 0,
            };
            string overhead = tableOverhead == 0 ? string.Empty : Numeric.GetSize(tableOverhead, Numeric.SizeKilo.Auto, 0, out string unit1).ToString() + " " + unit1;
            double spd = Numeric.GetSpeed((long)input.Length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numeric.SpeedKilo.MiBps, 0, out string unit2);

            Console.WriteLine($"|{crc.Name}|{crc.Core}|{overhead}|{spd} {unit2}|");
        }

        private static int GetTableOverhead(byte[][] table)
        {
            int count = 0;
            foreach (var row in table)
            {
                foreach (var col in row)
                {
                    count++;
                }
            }
            return count;
        }

        private static int GetTableOverhead(ushort[][] table)
        {
            int count = 0;
            foreach (var row in table)
            {
                foreach (var col in row)
                {
                    count++;
                }
            }
            return count * 2;
        }

        private static int GetTableOverhead(uint[][] table)
        {
            int count = 0;
            foreach (var row in table)
            {
                foreach (var col in row)
                {
                    count++;
                }
            }
            return count * 4;
        }

        private static int GetTableOverhead(ulong[][] table)
        {
            int count = 0;
            foreach (var row in table)
            {
                foreach (var col in row)
                {
                    count++;
                }
            }
            return count * 8;
        }
    }
}