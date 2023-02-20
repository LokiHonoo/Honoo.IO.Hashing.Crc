using Honoo.IO.Hashing;
using System.Text;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string str = "11112333344445555666777788889990000AAAAABB";
            byte[] input = Encoding.UTF8.GetBytes(str);
            //new Random().NextBytes(input);
            // Console.WriteLine($"Input string: {str}");
            Console.WriteLine();

            Test(new CRC_4_ITU(), input);
            Test(new Crc4Itu(), input);
            Test(new CrcCustom(4, true, 0x03, 0x00, 0x00), input);

            Test(new CRC_5_EPC(), input);
            Test(new Crc5Epc(), input);
            Test(new CrcCustom(5, false, 0x09, 0x09, 0x00), input);

            Test(new CRC_5_ITU(), input);
            Test(new Crc5Itu(), input);
            Test(new CrcCustom(5, true, 0x15, 0x00, 0x00), input);

            Test(new CRC_5_USB(), input);
            Test(new Crc5Usb(), input);
            Test(new CrcCustom(5, true, 0x05, 0x1F, 0x1F), input);

            Test(new CRC_6_ITU(), input);
            Test(new Crc6Itu(), input);
            Test(new CrcCustom(6, true, 0x03, 0x00, 0x00), input);

            Test(new CRC_7_MMC(), input);
            Test(new Crc7Mmc(), input);
            Test(new CrcCustom(7, false, 0x09, 0x00, 0x00), input);

            Test(new CRC_8(), input);
            Test(new Crc8(), input);
            Test(new CrcCustom(8, false, 0x07, 0x00, 0x00), input);

            Test(new CRC_8_ITU(), input);
            Test(new Crc8Itu(), input);
            Test(new CrcCustom(8, false, 0x07, 0x00, 0x55), input);

            Test(new CRC_8_MAXIM(), input);
            Test(new Crc8Maxim(), input);
            Test(new CrcCustom(8, true, 0x31, 0x00, 0x00), input);

            Test(new CRC_8_ROHC(), input);
            Test(new Crc8Rohc(), input);
            Test(new CrcCustom(8, true, 0x07, 0xFF, 0x00), input);

            Test(new CRC_16_CCITT(), input);
            Test(new Crc16Ccitt(), input);
            Test(new CrcCustom(16, true, 0x1021, 0x0000, 0x0000), input);

            Test(new CRC_16_CCITT_FALSE(), input);
            Test(new Crc16CcittFalse(), input);
            Test(new CrcCustom(16, false, 0x1021, 0xFFFF, 0x0000), input);

            Test(new CRC_16_DNP(), input);
            Test(new Crc16Dnp(), input);
            Test(new CrcCustom(16, true, 0x3D65, 0x0000, 0xFFFF), input);

            Test(new CRC_16_IBM(), input);
            Test(new Crc16Ibm(), input);
            Test(new CrcCustom(16, true, 0x8005, 0x0000, 0x0000), input);

            Test(new CRC_16_MAXIM(), input);
            Test(new Crc16Maxim(), input);
            Test(new CrcCustom(16, true, 0x8005, 0x0000, 0xFFFF), input);

            Test(new CRC_16_MODBUS(), input);
            Test(new Crc16Modbus(), input);
            Test(new CrcCustom(16, true, 0x8005, 0xFFFF, 0x0000), input);

            Test(new CRC_16_USB(), input);
            Test(new Crc16Usb(), input);
            Test(new CrcCustom(16, true, 0x8005, 0xFFFF, 0xFFFF), input);

            Test(new CRC_16_X25(), input);
            Test(new Crc16X25(), input);
            Test(new CrcCustom(16, true, 0x1021, 0xFFFF, 0xFFFF), input);

            Test(new CRC_16_XMODEM(), input);
            Test(new Crc16Xmodem(), input);
            Test(new CrcCustom(16, false, 0x1021, 0x0000, 0x0000), input);

            Test(new CRC_16_XMODEM2(), input);
            Test(new Crc16Xmodem2(), input);
            Test(new CrcCustom(16, true, 0x8408, 0x0000, 0x0000), input);

            Test(new CRC_32(), input);
            Test(new Crc32(), input);
            Test(new CrcCustom(32, true, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF), input);

            Test(new CRC_32_C(), input);
            Test(new Crc32c(), input);
            Test(new CrcCustom(32, true, 0x1EDC6F41, 0xFFFFFFFF, 0xFFFFFFFF), input);

            Test(new CRC_32_KOOPMAN(), input);
            Test(new Crc32Koopman(), input);
            Test(new CrcCustom(32, true, 0x741B8CD7, 0xFFFFFFFF, 0xFFFFFFFF), input);

            Test(new CRC_32_MPEG2(), input);
            Test(new Crc32Mpeg2(), input);
            Test(new CrcCustom(32, false, 0x04C11DB7, 0xFFFFFFFF, 0x00000000), input);

            Test(new CRC_64_ECMA(), input);
            Test(new Crc64Ecma(), input);
            Test(new CrcCustom(64, true, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF), input);

            Test(new CRC_64_ISO(), input);
            Test(new Crc64Iso(), input);
            Test(new CrcCustom(64, true, 0x000000000000001B, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF), input);

            Console.ReadKey(true);
        }

        private static void Test(CRC crc, byte[] input)
        {
            byte[] checksum = crc.DoFinalBytes(input);
            Console.WriteLine($"{crc.Name,-20}{BitConverter.ToString(checksum).Replace('-', (char)0)}");
        }

        private static void Test(Crc crc, byte[] input)
        {
            byte[] checksum = crc.DoFinal(input);

            Console.WriteLine($"{crc.AlgorithmName,-20}{BitConverter.ToString(checksum).Replace('-', (char)0)}");
        }
    }
}