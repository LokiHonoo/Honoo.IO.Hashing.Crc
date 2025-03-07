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
            int length = 7 * 1024 + 341;
            byte[] input = new byte[length];
            new Random().NextBytes(input);
            int times = 5000;
            //
            Console.WriteLine("|algorithm|core|table|table overhead|speed|");
            Console.WriteLine("|:-------:|:--:|:---:|:------------:|----:|");
            //
            Crc crc = new Crc32(CrcTableInfo.None);
            Do(crc, input, times);
            //
            crc = new Crc32();
            Do(crc, input, times);
            crc = new Crc32(CrcTableInfo.M16x);
            Do(crc, input, times);
            //
            Console.WriteLine("|-|-|-|-|-|");
            //
            var table = new CrcTable(CrcTableInfo.Standard, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Refin, CrcCore.UInt8);
            crc = Crc.CreateBy(CrcName.CRC7.Name, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Init, CrcName.CRC7.Xorout, CrcName.CRC7.Refin, CrcName.CRC7.Refout, table);
            Do(crc, input, times);
            table = new CrcTable(CrcTableInfo.Standard, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Refin, CrcCore.UInt16);
            crc = Crc.CreateBy(CrcName.CRC7.Name, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Init, CrcName.CRC7.Xorout, CrcName.CRC7.Refin, CrcName.CRC7.Refout, table);
            Do(crc, input, times);
            table = new CrcTable(CrcTableInfo.Standard, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Refin, CrcCore.UInt32);
            crc = Crc.CreateBy(CrcName.CRC7.Name, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Init, CrcName.CRC7.Xorout, CrcName.CRC7.Refin, CrcName.CRC7.Refout, table);
            Do(crc, input, times);
            table = new CrcTable(CrcTableInfo.Standard, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Refin, CrcCore.UInt64);
            crc = Crc.CreateBy(CrcName.CRC7.Name, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Init, CrcName.CRC7.Xorout, CrcName.CRC7.Refin, CrcName.CRC7.Refout, table);
            Do(crc, input, times);
            table = new CrcTable(CrcTableInfo.Standard, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Refin, CrcCore.Sharding8);
            crc = Crc.CreateBy(CrcName.CRC7.Name, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Init, CrcName.CRC7.Xorout, CrcName.CRC7.Refin, CrcName.CRC7.Refout, table);
            Do(crc, input, times);
            table = new CrcTable(CrcTableInfo.Standard, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Refin, CrcCore.Sharding16);
            crc = Crc.CreateBy(CrcName.CRC7.Name, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Init, CrcName.CRC7.Xorout, CrcName.CRC7.Refin, CrcName.CRC7.Refout, table);
            Do(crc, input, times);
            table = new CrcTable(CrcTableInfo.Standard, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Refin, CrcCore.Sharding32);
            crc = Crc.CreateBy(CrcName.CRC7.Name, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Init, CrcName.CRC7.Xorout, CrcName.CRC7.Refin, CrcName.CRC7.Refout, table);
            Do(crc, input, times);
            table = new CrcTable(CrcTableInfo.Standard, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Refin, CrcCore.Sharding64);
            crc = Crc.CreateBy(CrcName.CRC7.Name, CrcName.CRC7.Width, CrcName.CRC7.Poly, CrcName.CRC7.Init, CrcName.CRC7.Xorout, CrcName.CRC7.Refin, CrcName.CRC7.Refout, table);
            Do(crc, input, times);
            //
            Console.WriteLine("|-|-|-|-|-|");
            //
            crc = new Crc5Itu(CrcTableInfo.M16x);
            Do(crc, input, times);
            //
            crc = new Crc13bbc(CrcTableInfo.M16x);
            Do(crc, input, times);
            //
            crc = new Crc24Ble(CrcTableInfo.M16x);
            Do(crc, input, times);
            //
            crc = new Crc40Gsm(CrcTableInfo.M16x);
            Do(crc, input, times);

            //
            Console.WriteLine("|-|-|-|-|-|");
            System.IO.Hashing.Crc32 systemIOHashingCrc32 = new();
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                systemIOHashingCrc32.Append(input);
                systemIOHashingCrc32.GetCurrentHash();
            }
            _stopwatch.Stop();
            double spd = Numerics.GetSpeed((long)input.Length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numerics.SpeedKilo.MiBps, 0, out string unit);
            Console.WriteLine($"|[System.IO.Hashing.Crc32](https://www.nuget.org/packages/System.IO.Hashing/)|hardware|||" + spd + " " + unit + "|");
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
            spd = Numerics.GetSpeed((long)length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numerics.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|[Force.Crc32.Crc32Algorithm](https://github.com/force-net/Crc32.NET)|||16 KiB|" + spd + " " + unit + "|");
            //
            //
            //
            var hf = System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC32);
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                hf.ComputeHash(input);
            }
            _stopwatch.Stop();
            spd = Numerics.GetSpeed((long)length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numerics.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|[HashFunction](https://github.com/brandondahler/Data.HashFunction/)|||1 KiB|" + spd + " " + unit + "|");
            //
            //
            //
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                SHA1.HashData(input);
            }
            _stopwatch.Stop();
            spd = Numerics.GetSpeed((long)length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numerics.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|SHA1|managed|||" + spd + " " + unit + "|");
            //
            //
            //
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                SHA256.HashData(input);
            }
            _stopwatch.Stop();
            spd = Numerics.GetSpeed((long)length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numerics.SpeedKilo.MiBps, 0, out unit);
            Console.WriteLine($"|SHA256|managed|||" + spd + " " + unit + "|");
        }

        private static void Do(Crc crc, byte[] input, int times)
        {
            _stopwatch.Restart();
            for (int i = 0; i < times; i++)
            {
                crc.ComputeFinal(input);
            }
            _stopwatch.Stop();
            var tableOverhead = crc.CloneTable().Table switch
            {
                byte[] table => table.Length,
                ushort[] table => table.Length * 2,
                uint[] table => table.Length * 4,
                ulong[] table => table.Length * 8,
                _ => 0,
            };
            string overhead = tableOverhead == 0 ? string.Empty : Numerics.GetSize(tableOverhead, Numerics.SizeKilo.Auto, 0, out string unit1).ToString() + " " + unit1;
            double spd = Numerics.GetSpeed((long)input.Length * times * 1000 / _stopwatch.ElapsedMilliseconds, Numerics.SpeedKilo.MiBps, 0, out string unit2);

            Console.WriteLine($"|{crc.Name}|{crc.Core}|{crc.TableInfo}|{overhead}|{spd} {unit2}|");
        }
    }
}