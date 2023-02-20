using Honoo.IO.HashingOld;
using System.Diagnostics;
using System.Text;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string str = "1111221ADV233334444555566677788000AAAABB";
            byte[] input = Encoding.UTF8.GetBytes(str);
            //new Random().NextBytes(input);
            // Console.WriteLine($"Input string: {str}");
            Console.WriteLine();

            Test(new CRC_4_ITU(), input);
            Test(new Crc4Itu(), input);
            Test(new Crc4Itu(false), input);
            Test(new CrcCustom(4, true, true, 0x03, 0x00, 0x00), input);

            Test(new CRC_5_EPC(), input);
            Test(new Crc5Epc(), input);
            Test(new Crc5Epc(false), input);
            Test(new CrcCustom(5, false, false, 0x09, 0x09, 0x00), input);

            Test(new CRC_5_ITU(), input);
            Test(new Crc5Itu(), input);
            Test(new Crc5Itu(false), input);
            Test(new CrcCustom(5, true, true, 0x15, 0x00, 0x00), input);

            Test(new CRC_5_USB(), input);
            Test(new Crc5Usb(), input);
            Test(new Crc5Usb(false), input);
            Test(new CrcCustom(5, true, true, 0x05, 0x1F, 0x1F), input);
            Test(new CrcCustom(5, true, true, "0x05", "0x1F", "0x1F"), input);

            Test(new CRC_6_ITU(), input);
            Test(new Crc6Itu(), input);
            Test(new Crc6Itu(false), input);
            Test(new CrcCustom(6, true, true, 0x03, 0x00, 0x00), input);

            Test(new CRC_7_MMC(), input);
            Test(new Crc7Mmc(), input);
            Test(new Crc7Mmc(false), input);
            Test(new CrcCustom(7, false, false, 0x09, 0x00, 0x00), input);

            Test(new CRC_8(), input);
            Test(new Crc8(), input);
            Test(new Crc8(false), input);
            Test(new CrcCustom(8, false, false, 0x07, 0x00, 0x00), input);

            Test(new Crc8Cdma2000(), input);
            Test(new Crc8Cdma2000(false), input);
            Test(new CrcCustom(8, false, false, 0x9B, 0xFF, 0x00), input);
            Test(new CrcCustom(8, false, false, "0x9B", "0xFF", "0x00"), input);

            Test(new Crc8Ebu(), input);
            Test(new Crc8Ebu(false), input);
            Test(new CrcCustom(8, true, true, 0x1D, 0xFF, 0x00), input);

            Test(new CRC_8_ITU(), input);
            Test(new Crc8Itu(), input);
            Test(new Crc8Itu(false), input);
            Test(new CrcCustom(8, false, false, 0x07, 0x00, 0x55), input);

            Test(new CRC_8_MAXIM(), input);
            Test(new Crc8Maxim(), input);
            Test(new Crc8Maxim(false), input);
            Test(new CrcCustom(8, true, true, 0x31, 0x00, 0x00), input);

            Test(new CRC_8_ROHC(), input);
            Test(new Crc8Rohc(), input);
            Test(new Crc8Rohc(false), input);
            Test(new CrcCustom(8, true, true, 0x07, 0xFF, 0x00), input);

            Test(new Crc8Wcdma(), input);
            Test(new Crc8Wcdma(false), input);
            Test(new CrcCustom(8, true, true, 0x9B, 0x00, 0x00), input);
            Test(new CrcCustom(8, true, true, "0x9B", "0x00", "0x00"), input);

            Test(new Crc8Darc(), input);
            Test(new Crc8Darc(false), input);
            Test(new CrcCustom(8, true, true, 0x39, 0x00, 0x00), input);

            Test(new Crc8DvbS2(), input);
            Test(new Crc8DvbS2(false), input);
            Test(new CrcCustom(8, false, false, 0xD5, 0x00, 0x00), input);

            Test(new Crc8ICode(), input);
            Test(new Crc8ICode(false), input);
            Test(new CrcCustom(8, false, false, 0x1D, 0xFD, 0x00), input);

            Test(new CRC_16_CCITT(), input);
            Test(new Crc16Ccitt(), input);
            Test(new Crc16Ccitt(false), input);
            Test(new CrcCustom(16, true, true, 0x1021, 0x0000, 0x0000), input);

            Test(new CRC_16_CCITT_FALSE(), input);
            Test(new Crc16CcittFalse(), input);
            Test(new Crc16CcittFalse(false), input);
            Test(new CrcCustom(16, false, false, 0x1021, 0xFFFF, 0x0000), input);

            Test(new CRC_16_DNP(), input);
            Test(new Crc16Dnp(), input);
            Test(new Crc16Dnp(false), input);
            Test(new CrcCustom(16, true, true, 0x3D65, 0x0000, 0xFFFF), input);

            Test(new CRC_16_IBM(), input);
            Test(new Crc16Ibm(), input);
            Test(new Crc16Ibm(false), input);
            Test(new CrcCustom(16, true, true, 0x8005, 0x0000, 0x0000), input);

            Test(new CRC_16_MAXIM(), input);
            Test(new Crc16Maxim(), input);
            Test(new Crc16Maxim(false), input);
            Test(new CrcCustom(16, true, true, 0x8005, 0x0000, 0xFFFF), input);

            Test(new CRC_16_MODBUS(), input);
            Test(new Crc16Modbus(), input);
            Test(new Crc16Modbus(false), input);
            Test(new CrcCustom(16, true, true, 0x8005, 0xFFFF, 0x0000), input);

            Test(new CRC_16_USB(), input);
            Test(new Crc16Usb(), input);
            Test(new Crc16Usb(false), input);
            Test(new CrcCustom(16, true, true, 0x8005, 0xFFFF, 0xFFFF), input);

            Test(new CRC_16_X25(), input);
            Test(new Crc16X25(), input);
            Test(new Crc16X25(false), input);
            Test(new CrcCustom(16, true, true, 0x1021, 0xFFFF, 0xFFFF), input);

            Test(new CRC_16_XMODEM(), input);
            Test(new Crc16Xmodem(), input);
            Test(new Crc16Xmodem(false), input);
            Test(new CrcCustom(16, false, false, 0x1021, 0x0000, 0x0000), input);

            Test(new CRC_16_XMODEM2(), input);
            Test(new Crc16Xmodem2(), input);
            Test(new Crc16Xmodem2(false), input);
            Test(new CrcCustom(16, true, true, 0x8408, 0x0000, 0x0000), input);

            Test(new Crc16AugCcitt(), input);
            Test(new Crc16AugCcitt(false), input);
            Test(new CrcCustom(16, false, false, 0x1021, 0x1D0F, 0x0000), input);

            Test(new Crc16BuyPass(), input);
            Test(new Crc16BuyPass(false), input);
            Test(new CrcCustom(16, false, false, 0x8005, 0x0000, 0x0000), input);

            Test(new Crc16Cdma2000(), input);
            Test(new Crc16Cdma2000(false), input);
            Test(new CrcCustom(16, false, false, 0xC867, 0xFFFF, 0x0000), input);

            Test(new Crc16Dds110(), input);
            Test(new Crc16Dds110(false), input);
            Test(new CrcCustom(16, false, false, 0x8005, 0x800D, 0x0000), input);

            Test(new Crc16DectR(), input);
            Test(new Crc16DectR(false), input);
            Test(new CrcCustom(16, false, false, 0x0589, 0x0000, 0x0001), input);

            Test(new Crc16DectX(), input);
            Test(new Crc16DectX(false), input);
            Test(new CrcCustom(16, false, false, 0x0589, 0x0000, 0x0000), input);

            Test(new Crc16En13757(), input);
            Test(new Crc16En13757(false), input);
            Test(new CrcCustom(16, false, false, 0x3D65, 0x0000, 0xFFFF), input);

            Test(new Crc16Genibus(), input);
            Test(new Crc16Genibus(false), input);
            Test(new CrcCustom(16, false, false, 0x1021, 0xFFFF, 0xFFFF), input);

            Test(new Crc16Mcrf4XX(), input);
            Test(new Crc16Mcrf4XX(false), input);
            Test(new CrcCustom(16, true, true, 0x1021, 0xFFFF, 0x0000), input);

            Test(new Crc16Riello(), input);
            Test(new Crc16Riello(false), input);
            Test(new CrcCustom(16, true, true, 0x1021, 0xB2AA, 0x0000), input);

            Test(new Crc16T10Dif(), input);
            Test(new Crc16T10Dif(false), input);
            Test(new CrcCustom(16, false, false, 0x8BB7, 0x0000, 0x0000), input);

            Test(new Crc16Teledisk(), input);
            Test(new Crc16Teledisk(false), input);
            Test(new CrcCustom(16, false, false, 0xA097, 0x0000, 0x0000), input);

            Test(new Crc16Tms37157(), input);
            Test(new Crc16Tms37157(false), input);
            Test(new CrcCustom(16, true, true, 0x1021, 0x89EC, 0x0000), input);

            Test(new CrcA(), input);
            Test(new CrcA(false), input);
            Test(new CrcCustom(16, true, true, 0x1021, 0xC6C6, 0x0000), input);

            Test(new CRC_32(), input);
            Test(new Crc32(), input);
            Test(new Crc32(false), input);
            Test(new CrcCustom(32, true, true, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF), input);

            Test(new Crc32BZip2(), input);
            Test(new Crc32BZip2(false), input);
            Test(new CrcCustom(32, false, false, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF), input);
            Test(new CrcCustom(32, false, false, "0x04C11DB7", "0xFFFFFFFF", "0xFFFFFFFF"), input);

            Test(new CRC_32_C(), input);
            Test(new Crc32c(), input);
            Test(new Crc32c(false), input);
            Test(new CrcCustom(32, true, true, 0x1EDC6F41, 0xFFFFFFFF, 0xFFFFFFFF), input);

            Test(new Crc32JamCrc(), input);
            Test(new Crc32JamCrc(false), input);
            Test(new CrcCustom(32, true, true, 0x04C11DB7, 0xFFFFFFFF, 0x00000000), input);

            Test(new Crc32Posix(), input);
            Test(new Crc32Posix(false), input);
            Test(new CrcCustom(32, false, false, 0x04C11DB7, 0x00000000, 0xFFFFFFFF), input);

            Test(new Crc32Sata(), input);
            Test(new Crc32Sata(false), input);
            Test(new CrcCustom(32, false, false, 0x04C11DB7, 0x52325032, 0x00000000), input);

            Test(new Crc32Xfer(), input);
            Test(new Crc32Xfer(false), input);
            Test(new CrcCustom(32, false, false, (uint)0x000000AF, 0x00000000, 0x00000000), input);

            Test(new Crc32d(), input);
            Test(new Crc32d(false), input);
            Test(new CrcCustom(32, true, true, 0xA833982B, 0xFFFFFFFF, 0xFFFFFFFF), input);
            Test(new CrcCustom(32, true, true, "0xA833982B", "0xFFFFFFFF", "0xFFFFFFFF"), input);

            Test(new Crc32q(), input);
            Test(new Crc32q(false), input);
            Test(new CrcCustom(32, false, false, 0x814141AB, 0x00000000, 0x00000000), input);

            Test(new CRC_32_KOOPMAN(), input);
            Test(new Crc32Koopman(), input);
            Test(new Crc32Koopman(false), input);
            Test(new CrcCustom(32, true, true, 0x741B8CD7, 0xFFFFFFFF, 0xFFFFFFFF), input);

            Test(new CRC_32_MPEG2(), input);
            Test(new Crc32Mpeg2(), input);
            Test(new Crc32Mpeg2(false), input);
            Test(new CrcCustom(32, false, false, 0x04C11DB7, 0xFFFFFFFF, 0x00000000), input);

            Test(new CRC_64_ECMA(), input);
            Test(new Crc64Ecma(), input);
            Test(new Crc64Ecma(false), input);
            Test(new CrcCustom(64, true, true, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF), input);
            Test(new CrcCustom(64, true, true, "0x42F0E1EBA9EA3693", "0xFFFFFFFFFFFFFFFF", "0xFFFFFFFFFFFFFFFF"), input);

            Test(new CRC_64_ISO(), input);
            Test(new Crc64Iso(), input);
            Test(new Crc64Iso(false), input);
            Test(new CrcCustom(64, true, true, 0x000000000000001B, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF), input);

            Console.WriteLine();
            Console.WriteLine();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Crc crc = new Crc32(false);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.DoFinal(input);
            }
            stopwatch.Stop();
            Console.WriteLine("CRC-32 calc 100000 times elapsed: " + stopwatch.ElapsedMilliseconds + " ms");

            crc = new Crc32(true);
            stopwatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                crc.DoFinal(input);
            }
            stopwatch.Stop();
            Console.WriteLine("CRC-32 with table calc 100000 times elapsed: " + stopwatch.ElapsedMilliseconds + " ms");

            //crc = new CrcCustom(32, true, true, "0x04C11DB7", "0xFFFFFFFF", "0xFFFFFFFF");
            //stopwatch.Restart();
            //for (int i = 0; i < 100000; i++)
            //{
            //    crc.DoFinal(true, input);
            //}
            //stopwatch.Stop();
            //Console.WriteLine("CRC-32 binary mode calc 100000 times elapsed: " + stopwatch.ElapsedMilliseconds + " ms");

            Console.ReadKey(true);
        }

        private static void Test(CRC crc, byte[] input)
        {
            byte[] checksum = crc.DoFinalBytes(input);
            ulong l = (ulong)Honoo.IO.Binaries.GetInt64(false, checksum);
            Console.Write(crc.Name.PadRight(40));
            Console.Write(BitConverter.ToString(checksum).Replace('-', (char)0) + "   ");
            Console.WriteLine(l);
        }

        private static void Test(Crc crc, byte[] input)
        {
            byte[] checksum = crc.DoFinal(false, input);
            ulong l = (ulong)Honoo.IO.Binaries.GetInt64(false, checksum);
            object v = crc.DoFinal(input);
            Console.Write(crc.AlgorithmName.PadRight(20));
            Console.Write($"USE_TABLE {crc.UseTable}".PadRight(20));
            Console.Write(BitConverter.ToString(checksum).Replace('-', (char)0) + "   ");
            Console.Write(l + "   ");
            Console.WriteLine(v);
        }
    }
}