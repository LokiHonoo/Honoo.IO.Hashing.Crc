using Honoo.IO.Hashing;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Test
{
    internal static class Test
    {
        private static int _error = 0;

        internal static Alg[] GetAlgs()
        {
            List<Alg> algs = new();
            Stream catalogue = Assembly.GetExecutingAssembly().GetManifestResourceStream("Test.catalogue.txt")!;

            HtmlDocument document = new();
            document.Load(catalogue);
            var nodes = document.DocumentNode.SelectNodes("/html/body/h3");
            foreach (var node in nodes)
            {
                var code = node.NextSibling.NextSibling;
                if (code != null && code.Name == "p")
                {
                    string txt = code.FirstChild.InnerText;
                    Alg alg = new();
                    List<string> names = new();

                    int index1 = txt.IndexOf("width=");
                    int index2 = txt.IndexOf(" ", index1);
                    string width = txt.Substring(index1 + 6, index2 - index1 - 6);
                    alg.Width = int.Parse(width);
                    index1 = txt.IndexOf("poly=");
                    index2 = txt.IndexOf(" ", index1);
                    alg.Poly = txt.Substring(index1 + 5, index2 - index1 - 5).ToUpper().Replace('X', 'x');

                    index1 = txt.IndexOf("init=");
                    index2 = txt.IndexOf(" ", index1);
                    alg.Init = txt.Substring(index1 + 5, index2 - index1 - 5).ToUpper().Replace('X', 'x');

                    index1 = txt.IndexOf("refin=");
                    index2 = txt.IndexOf(" ", index1);
                    alg.Refin = bool.Parse(txt.AsSpan(index1 + 6, index2 - index1 - 6));

                    index1 = txt.IndexOf("refout=");
                    index2 = txt.IndexOf(" ", index1);
                    alg.Refout = bool.Parse(txt.AsSpan(index1 + 7, index2 - index1 - 7));

                    index1 = txt.IndexOf("xorout=");
                    index2 = txt.IndexOf(" ", index1);
                    alg.Xorout = txt.Substring(index1 + 7, index2 - index1 - 7).ToUpper().Replace('X', 'x');

                    index1 = txt.IndexOf("name=");
                    index2 = txt.IndexOf("\"", index1 + 7);
                    names.Add(txt.Substring(index1 + 5, index2 - index1 - 5).Trim('\"'));
                    //
                    code = node.NextSibling.NextSibling.NextSibling.NextSibling;
                    foreach (var item in code.ChildNodes)
                    {
                        if (item.InnerText.StartsWith("Alias:"))
                        {
                            txt = item.InnerText["Alias:".Length..];
                            string[] splits = txt.Split(',');
                            foreach (var split in splits)
                            {
                                names.Add(split.Trim());
                            }
                        }
                    }
                    //
                    alg.Names = names.ToArray();
                    algs.Add(alg);
                }
            }
            //
            return algs.ToArray();
        }

        internal static void Test1()
        {
            _error = 0;
            int displayLimit = 22;
            Alg[] algs = GetAlgs();
            //foreach (Alg alg in algs)
            //{
            //    Console.WriteLine(string.Join(',', alg.Names)+",");
            //}
            byte[] input = Encoding.UTF8.GetBytes("1111221ADV233334444555566677788000AAAABB");
            foreach (Alg alg in algs)
            {
                var checksums = new List<string>();
                Console.WriteLine("===================================================================================================");
                Console.WriteLine(string.Join(',', alg.Names));
                Console.WriteLine($"Width={alg.Width} Refin={alg.Refin} Refout={alg.Refout} Poly={alg.Poly} Init={alg.Init} Xorout={alg.Xorout}");
                Console.WriteLine();
                //
                foreach (var name in alg.Names)
                {
                    checksums.Add(Calc(Crc.Create(name), input, displayLimit));
                    if (name == "CRC-32")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(Calc(new Force.Crc32.Crc32Algorithm(), "Crc32.NET", input, out string hex));
                        Console.ResetColor();
                        checksums.Add(hex);
                    }
                    if (name == "CRC-32C")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(Calc(new Force.Crc32.Crc32CAlgorithm(), "Crc32c.NET", input, out string hex));
                        Console.ResetColor();
                        checksums.Add(hex);
                    }
                }
                var poly = new CrcParameter(NumericsStringFormat.Hex, alg.Poly, alg.Width);
                var init = new CrcParameter(NumericsStringFormat.Hex, alg.Init, alg.Width);
                var xorout = new CrcParameter(NumericsStringFormat.Hex, alg.Xorout, alg.Width);
                Do(alg.Width, alg.Refin, alg.Refout, poly, init, xorout, input, checksums, displayLimit);
                Console.WriteLine();
            }
            Console.WriteLine("===================================================================================================");
            Console.WriteLine("CRC-217/CUSTUM");
            Console.WriteLine();
            Do(217,
               false,
            false,
               new CrcParameter(NumericsStringFormat.Hex, "0x7204CA357EDF00742A12C562157732D9", 217),
               new CrcParameter(NumericsStringFormat.Hex, "0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF", 217),
               new CrcParameter(NumericsStringFormat.Hex, "0x00000000000000000000000000000000", 217),
               input,
               new List<string>(),
               displayLimit);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Error={_error}");
            Console.WriteLine();
        }

        private static string Calc(HashAlgorithm crc, string name, byte[] input, out string hex)
        {
            var sb = new StringBuilder();
            byte[] checksum = crc.ComputeHash(input);
            hex = BitConverter.ToString(checksum).Replace("-", string.Empty).ToLowerInvariant();
            sb.Append(name.PadRight(27));
            sb.Append("        ");
            sb.Append(hex);
            sb.Append(" ++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            return sb.ToString();
        }

        private static string Calc(Crc crc, byte[] input, int displayLimit)
        {
            crc.Update(input);
            byte[] checksum = crc.ComputeFinal(Endian.BigEndian);
            string hex = BitConverter.ToString(checksum).Replace("-", string.Empty).ToLowerInvariant();
            string sign = hex;
            if (hex.Length > displayLimit)
            {
                hex = hex.Substring(hex.Length - displayLimit, displayLimit);
            }
            foreach (byte item in input)
            {
                crc.Update(item);
            }
            string res1 = crc.ComputeFinal(NumericsStringFormat.Hex);
            if (res1.Length > displayLimit)
            {
                res1 = res1.Substring(res1.Length - displayLimit, displayLimit);
            }
            crc.Update(input);
            crc.ComputeFinal(out byte b);
            string res2 = Convert.ToString(b, 16);
            crc.Update(input);
            crc.ComputeFinal(out ushort s);
            string res3 = Convert.ToString(s, 16);
            crc.Update(input);
            crc.ComputeFinal(out uint i);
            string res4 = Convert.ToString(i, 16);
            crc.Update(input);
            crc.ComputeFinal(out ulong l);
            string res5 = Convert.ToString((long)l, 16);
            Console.Write(crc.Name.PadRight(27));
            Console.Write(crc.WithTable ? "TABLE   " : "        ");
            Console.Write(hex + " ");
            Console.Write(res1 + " ");
            Console.Write(res2 + " ");
            Console.Write(res3 + " ");
            Console.Write(res4 + " ");
            Console.WriteLine(res5);
            bool error = false;
            if (!hex.EndsWith(res1)) error = true;
            if (!hex.EndsWith(res2)) error = true;
            if (!hex.EndsWith(res3)) error = true;
            if (!hex.EndsWith(res4)) error = true;
            if (!hex.EndsWith(res5)) error = true;
            return error ? "X" : sign;
        }

        private static void Do(int width, bool refin, bool refout, CrcParameter poly, CrcParameter init, CrcParameter xorout, byte[] input, IList<string> checksums, int displayLimit)
        {
            CrcCore core;
            if (width <= 8) core = CrcCore.UInt8;
            else if (width <= 16) core = CrcCore.UInt16;
            else if (width <= 32) core = CrcCore.UInt32;
            else if (width <= 64) core = CrcCore.UInt64;
            else core = CrcCore.Sharding32;
            Crc crc = Crc.CreateBy($"CRC-{width}/CUSTUM-{core}", width, refin, refout, poly, init, xorout, true, core);
            checksums.Add(Calc(crc, input, displayLimit));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-{core}", width, refin, refout, poly, init, xorout, false, core);
            checksums.Add(Calc(crc, input, displayLimit));
            //
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding8", width, refin, refout, poly, init, xorout, true, CrcCore.Sharding8);
            checksums.Add(Calc(crc, input, displayLimit));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding8", width, refin, refout, poly, init, xorout, false, CrcCore.Sharding8);
            checksums.Add(Calc(crc, input, displayLimit));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding16", width, refin, refout, poly, init, xorout, true, CrcCore.Sharding16);
            checksums.Add(Calc(crc, input, displayLimit));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding16", width, refin, refout, poly, init, xorout, false, CrcCore.Sharding16);
            checksums.Add(Calc(crc, input, displayLimit));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding32", width, refin, refout, poly, init, xorout, true, CrcCore.Sharding32);
            checksums.Add(Calc(crc, input, displayLimit));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding32", width, refin, refout, poly, init, xorout, false, CrcCore.Sharding32);
            checksums.Add(Calc(crc, input, displayLimit));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding64", width, refin, refout, poly, init, xorout, true, CrcCore.Sharding64);
            checksums.Add(Calc(crc, input, displayLimit));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding64", width, refin, refout, poly, init, xorout, false, CrcCore.Sharding64);
            checksums.Add(Calc(crc, input, displayLimit));
            //
            if (checksums.Distinct().Count() != 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("====================================================================================");
                Console.WriteLine("====================================================================================");
                Console.ResetColor();
                _error++;
            }
        }

        internal struct Alg
        {
            public string Init;
            public string[] Names;
            public string Poly;
            public bool Refin;
            public bool Refout;
            public int Width;
            public string Xorout;
        }
    }
}