using Honoo.IO.Hashing;
using HtmlAgilityPack;
using System;
using System.Collections;
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
            List<Alg> algs = [];
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
                    var names = new List<string>();

                    int index1 = txt.IndexOf("width=");
                    int index2 = txt.IndexOf(' ', index1);
                    string width = txt.Substring(index1 + 6, index2 - index1 - 6);
                    alg.Width = int.Parse(width);
                    index1 = txt.IndexOf("poly=");
                    index2 = txt.IndexOf(' ', index1);
                    alg.Poly = txt.Substring(index1 + 5, index2 - index1 - 5).ToUpper().Replace('X', 'x');

                    index1 = txt.IndexOf("init=");
                    index2 = txt.IndexOf(' ', index1);
                    alg.Init = txt.Substring(index1 + 5, index2 - index1 - 5).ToUpper().Replace('X', 'x');

                    index1 = txt.IndexOf("refin=");
                    index2 = txt.IndexOf(' ', index1);
                    alg.Refin = bool.Parse(txt.AsSpan(index1 + 6, index2 - index1 - 6));

                    index1 = txt.IndexOf("refout=");
                    index2 = txt.IndexOf(' ', index1);
                    alg.Refout = bool.Parse(txt.AsSpan(index1 + 7, index2 - index1 - 7));

                    index1 = txt.IndexOf("xorout=");
                    index2 = txt.IndexOf(' ', index1);
                    alg.Xorout = txt.Substring(index1 + 7, index2 - index1 - 7).ToUpper().Replace('X', 'x');

                    index1 = txt.IndexOf("name=");
                    index2 = txt.IndexOf('\"', index1 + 7);
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
                    alg.Names = [.. names];
                    algs.Add(alg);
                }
            }
            //
            return [.. algs];
        }

        internal static void Test1()
        {
            _error = 0;
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
                Console.WriteLine($"Width={alg.Width} Poly={alg.Poly} Init={alg.Init} Xorout={alg.Xorout} Refin={alg.Refin} Refout={alg.Refout}");
                Console.WriteLine();
                //
                foreach (var name in alg.Names)
                {
                    checksums.Add(Calc(Crc.Create(name), input));
                    string other = DoTT(name, input);
                    if (!string.IsNullOrEmpty(other))
                    {
                        checksums.Add(other);
                    }
                }
                var poly = new CrcHexValue(alg.Poly, alg.Width);
                var init = new CrcHexValue(alg.Init, alg.Width);
                var xorout = new CrcHexValue(alg.Xorout, alg.Width);
                Do1(alg.Width, poly, init, xorout, alg.Refin, alg.Refout, input, checksums);
                Console.WriteLine();
            }
            Console.WriteLine("===================================================================================================");
            Do2(7, new CrcHexValue("0x9", 7), new CrcHexValue("0xF", 7), new CrcHexValue("0x0", 7), false, false, input, []);
            Console.WriteLine();
            Console.WriteLine("===================================================================================================");
            Do2(5, new CrcHexValue("0x9", 5), new CrcHexValue("0xF", 5), new CrcHexValue("0x0", 5), true, true, input, []);
            Console.WriteLine();
            Console.WriteLine("===================================================================================================");
            Do3(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC7_ROHC), CrcName.CRC7_ROHC, input);
            Console.WriteLine();
            Console.WriteLine("===================================================================================================");
            Do3(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC14_DARC), CrcName.CRC14_DARC, input);
            Console.WriteLine();
            Console.WriteLine("===================================================================================================");
            Do3(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC24_FLEXRAYA), CrcName.CRC24_FLEXRAY_A, input);
            Console.WriteLine();
            Console.WriteLine("===================================================================================================");
            Do3(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC40_GSM), CrcName.CRC40_GSM, input);
            Console.WriteLine();
            Console.WriteLine("===================================================================================================");
            Do3(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC64_XZ), CrcName.CRC64_XZ, input);
            Console.WriteLine();
            Console.WriteLine("===================================================================================================");
            Do4(input);
            Console.WriteLine();
            //
            //
            //
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Error={_error}");
            Console.WriteLine();
        }

        private static string Calc(System.Data.HashFunction.CRC.ICRC crc, string name, byte[] input, out string hex)
        {
            byte[] input2 = new byte[input.Length - 2];
            Buffer.BlockCopy(input, 0, input2, 0, input2.Length);
            var sb = new StringBuilder();
            var hv = crc.ComputeHash(input2);
            byte[] checksum = hv.Hash;
            for (int i = checksum.Length - 1; i >= 0; i--)
            {
                sb.Append(Convert.ToString(checksum[i], 16).PadLeft(2, '0'));
            }
            hex = CrcConverter.GetHex(CrcStringFormat.Hex, sb.ToString(), crc.HashSizeInBits, CrcCaseSensitivity.Lower);
            sb.Clear();
            sb.Append(name.PadRight(27));
            sb.Append("         ");

            sb.Append(hex);
            sb.Append(' ');
            sb.Append(hv.AsHexString());
            sb.Append(" +++++++++++++++++++++++++++");
            return sb.ToString();
        }

        private static string Calc(HashAlgorithm crc, string name, byte[] input, out string hex)
        {
            var sb = new StringBuilder();
            byte[] checksum = crc.ComputeHash(input, 0, input.Length - 2);
            hex = BitConverter.ToString(checksum).Replace("-", string.Empty);
            hex = CrcConverter.GetHex(CrcStringFormat.Hex, hex, crc.HashSize, CrcCaseSensitivity.Lower);
            sb.Append(name.PadRight(27));
            sb.Append("         ");
            sb.Append(hex);
            sb.Append(" +++++++++++++++++++++++++++++++++++++++++++++++++");
            return sb.ToString();
        }

        private static string Calc(System.IO.Hashing.Crc64 crc, string name, byte[] input, out string hex)
        {
            byte[] input2 = new byte[input.Length - 2];
            Buffer.BlockCopy(input, 0, input2, 0, input2.Length);
            var sb = new StringBuilder();
            crc.Append(input2);
            byte[] checksum = crc.GetCurrentHash();
            hex = BitConverter.ToString(checksum).Replace("-", string.Empty);
            hex = CrcConverter.GetHex(CrcStringFormat.Hex, hex, crc.HashLengthInBytes * 8, CrcCaseSensitivity.Lower);
            sb.Append(name.PadRight(27));
            sb.Append("         ");
            sb.Append(hex);
            sb.Append(" +++++++++++++++++++++++++++++++++++++++++++++++++");
            return sb.ToString();
        }

        private static string Calc(Crc crc, byte[] input)
        {
            byte[] checksum1 = crc.ComputeFinal(input, 0, input.Length - 2).ToBytes(CrcEndian.BigEndian);
            string hexU = BitConverter.ToString(checksum1).Replace("-", string.Empty);
            byte[] checksum2 = new byte[crc.ChecksumByteLength];
            crc.ComputeFinal(input, 0, input.Length - 2).ToBytes(CrcEndian.BigEndian, checksum2, 0);
            string hexL = BitConverter.ToString(checksum2).Replace("-", string.Empty).ToLowerInvariant();
            for (int k = 0; k < input.Length - 2; k++)
            {
                crc.Update(input[k]);
            }
            string res1 = crc.ComputeFinal().ToHex(CrcCaseSensitivity.Lower);
            string res2 = Convert.ToString(crc.ComputeFinal(input, 0, input.Length - 2).ToUInt8(), 16);
            string res3 = Convert.ToString(crc.ComputeFinal(input, 0, input.Length - 2).ToUInt16(), 16);
            string res4 = Convert.ToString(crc.ComputeFinal(input, 0, input.Length - 2).ToUInt32(), 16);
            string res5 = Convert.ToString((long)crc.ComputeFinal(input, 0, input.Length - 2).ToUInt64(), 16);
            string table = crc.TableInfo switch
            {
                CrcTableInfo.Standard => "Standard ",
                CrcTableInfo.M16x => "M16x     ",
                _ => "         ",
            };
            Console.Write(crc.Name.PadRight(27));
            Console.Write(table);
            Console.Write(hexU + " ");
            Console.Write(res1 + " ");
            Console.Write(res2 + " ");
            Console.Write(res3 + " ");
            Console.Write(res4 + " ");
            Console.WriteLine(res5);
            bool error = !hexU.Equals(hexL.ToUpperInvariant());
            if (!hexL.EndsWith(res1)) error = true;
            if (!hexL.EndsWith(res2)) error = true;
            if (!hexL.EndsWith(res3)) error = true;
            if (!hexL.EndsWith(res4)) error = true;
            if (!hexL.EndsWith(res5)) error = true;
            return error ? "X" : res1;
        }

        private static void Do1(int width, CrcValue poly, CrcValue init, CrcValue xorout, bool refin, bool refout, byte[] input, List<string> checksums)
        {
            CrcCore core;
            if (width <= 8) core = CrcCore.UInt8;
            else if (width <= 16) core = CrcCore.UInt16;
            else if (width <= 32) core = CrcCore.UInt32;
            else if (width <= 64) core = CrcCore.UInt64;
            else core = CrcCore.Sharding32;
            Crc crc = Crc.CreateBy($"CRC-{width}/CUSTUM-{core}", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.Standard, width, poly, refin, core));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-{core}", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.M16x, width, poly, refin, core));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-{core}", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.None, width, poly, refin, core));
            checksums.Add(Calc(crc, input));
            //
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding8", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.Standard, width, poly, refin, CrcCore.Sharding8));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding8", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.None, width, poly, refin, CrcCore.Sharding8));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding16", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.Standard, width, poly, refin, CrcCore.Sharding16));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding16", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.None, width, poly, refin, CrcCore.Sharding16));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding32", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.Standard, width, poly, refin, CrcCore.Sharding32));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding32", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.None, width, poly, refin, CrcCore.Sharding32));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding64", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.Standard, width, poly, refin, CrcCore.Sharding64));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{width}/CUSTUM-Sharding64", width, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.None, width, poly, refin, CrcCore.Sharding64));
            checksums.Add(Calc(crc, input));
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

        private static void Do2(int widthMax8, CrcValue poly, CrcValue init, CrcValue xorout, bool refin, bool refout, byte[] input, List<string> checksums)
        {
            Crc crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt8", widthMax8, poly.ToUInt8(), init.ToUInt8(), xorout.ToUInt8(), refin, refout, new CrcTable(CrcTableInfo.Standard, widthMax8, poly.ToUInt8(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt8", widthMax8, poly.ToUInt8(), init.ToUInt8(), xorout.ToUInt8(), refin, refout, new CrcTable(CrcTableInfo.M16x, widthMax8, poly.ToUInt8(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt8", widthMax8, poly.ToUInt8(), init.ToUInt8(), xorout.ToUInt8(), refin, refout, new CrcTable(CrcTableInfo.None, widthMax8, poly.ToUInt8(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt16", widthMax8, poly.ToUInt16(), init.ToUInt16(), xorout.ToUInt16(), refin, refout, new CrcTable(CrcTableInfo.Standard, widthMax8, poly.ToUInt16(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt16", widthMax8, poly.ToUInt16(), init.ToUInt16(), xorout.ToUInt16(), refin, refout, new CrcTable(CrcTableInfo.M16x, widthMax8, poly.ToUInt16(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt16", widthMax8, poly.ToUInt16(), init.ToUInt16(), xorout.ToUInt16(), refin, refout, new CrcTable(CrcTableInfo.None, widthMax8, poly.ToUInt16(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt32", widthMax8, poly.ToUInt32(), init.ToUInt32(), xorout.ToUInt32(), refin, refout, new CrcTable(CrcTableInfo.Standard, widthMax8, poly.ToUInt32(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt32", widthMax8, poly.ToUInt32(), init.ToUInt32(), xorout.ToUInt32(), refin, refout, new CrcTable(CrcTableInfo.M16x, widthMax8, poly.ToUInt32(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt32", widthMax8, poly.ToUInt32(), init.ToUInt32(), xorout.ToUInt32(), refin, refout, new CrcTable(CrcTableInfo.None, widthMax8, poly.ToUInt32(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt64", widthMax8, poly.ToUInt64(), init.ToUInt64(), xorout.ToUInt64(), refin, refout, new CrcTable(CrcTableInfo.Standard, widthMax8, poly.ToUInt64(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt64", widthMax8, poly.ToUInt64(), init.ToUInt64(), xorout.ToUInt64(), refin, refout, new CrcTable(CrcTableInfo.M16x, widthMax8, poly.ToUInt64(), refin));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-UInt64", widthMax8, poly.ToUInt64(), init.ToUInt64(), xorout.ToUInt64(), refin, refout, new CrcTable(CrcTableInfo.None, widthMax8, poly.ToUInt64(), refin));
            checksums.Add(Calc(crc, input));

            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-Sharding8", widthMax8, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.Standard, widthMax8, poly, refin, CrcCore.Sharding8));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-Sharding8", widthMax8, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.None, widthMax8, poly, refin, CrcCore.Sharding8));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-Sharding16", widthMax8, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.Standard, widthMax8, poly, refin, CrcCore.Sharding16));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-Sharding16", widthMax8, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.None, widthMax8, poly, refin, CrcCore.Sharding16));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-Sharding32", widthMax8, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.Standard, widthMax8, poly, refin, CrcCore.Sharding32));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-Sharding32", widthMax8, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.None, widthMax8, poly, refin, CrcCore.Sharding32));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-Sharding64", widthMax8, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.Standard, widthMax8, poly, refin, CrcCore.Sharding64));
            checksums.Add(Calc(crc, input));
            crc = Crc.CreateBy($"CRC-{widthMax8}/CUSTUM-Sharding64", widthMax8, poly, init, xorout, refin, refout, new CrcTable(CrcTableInfo.None, widthMax8, poly, refin, CrcCore.Sharding64));
            checksums.Add(Calc(crc, input));
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

        private static void Do3(System.Data.HashFunction.CRC.ICRC hf, CrcName cn, byte[] input)
        {
            Console.WriteLine(cn.Name);
            byte[] input2 = new byte[input.Length - 2];
            Buffer.BlockCopy(input, 0, input2, 0, input2.Length);
            List<string> checksums1 = new List<string>();
            List<string> checksums2 = new List<string>();
            var sb = new StringBuilder();
            var hv = hf.ComputeHash(input2);
            BitArray ba1 = hv.AsBitArray();
            var bb1 = new List<bool>();
            for (int i = ba1.Length - 1; i >= 0; i--)
            {
                bb1.Add(ba1.Get(i));
            }
            ba1 = new BitArray(bb1.ToArray());
            for (int i = 0; i < ba1.Length; i++)
            {
                sb.Append(ba1.Get(i) ? '1' : '0');
            }
            string res1 = sb.ToString();
            Console.WriteLine(res1);
            checksums1.Add(res1);
            sb.Clear();
            for (int i = hv.Hash.Length - 1; i >= 0; i--)
            {
                sb.Append(Convert.ToString(hv.Hash[i], 16).PadLeft(2, '0'));
            }
            string res1h = sb.ToString();
            Console.WriteLine(res1h);
            checksums2.Add(res1h);
            //
            //
            //
            var crc = Crc.Create(cn);
            BitArray ba2 = crc.ComputeFinal(input2).ToBitArray();
            string res2 = CrcConverter.GetBits(ba2, null);
            Console.WriteLine(res2);
            checksums1.Add(res2);
            string res2h = crc.ComputeFinal(input2).ToHex(CrcCaseSensitivity.Lower);
            Console.WriteLine(res2h);
            checksums2.Add(res2h);
            crc = Crc.CreateBy(
                cn.Name,
                cn.Width,
                new CrcBitsValue(cn.Poly.ToBits(), cn.Width),
                new CrcBitArrayValue(cn.Init.ToBitArray(), cn.Width),
                new CrcHexValue(cn.Xorout.ToHex(CrcCaseSensitivity.Lower), cn.Width),
                cn.Refin,
                cn.Refout,
                new CrcTable(CrcTableInfo.Standard, cn.Width, cn.Poly, cn.Refin, CrcCore.Sharding32)
                );
            string res3 = crc.ComputeFinal(input2).ToBits();
            Console.WriteLine(res3);
            checksums1.Add(res3);
            string res3h = crc.ComputeFinal(input2).ToHex(CrcCaseSensitivity.Lower);
            Console.WriteLine(res3h);
            checksums2.Add(res3h);
            //
            if (checksums1.Distinct().Count() != 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("====================================================================================");
                Console.WriteLine("====================================================================================");
                Console.ResetColor();
                _error++;
            }
            if (checksums2.Distinct().Count() != 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("====================================================================================");
                Console.WriteLine("====================================================================================");
                Console.ResetColor();
                _error++;
            }
        }

        private static void Do4(byte[] input)
        {
            var a = new BitArray(input);
            a.Length = 31;
            var b = new CrcBitArrayValue(a, 30);
            var c = new CrcHexValue(b.ToHex(CrcCaseSensitivity.Upper), 30);
            var d = new CrcBitsValue(c.ToBits(), 30);
            for (int i = 1; i < a.Count; i++)
            {
                Console.Write(a[i] ? 1 : 0);
            }
            Console.WriteLine();
            Console.WriteLine(b.ToBits());
            Console.WriteLine(c.ToBits());
            Console.WriteLine(d.ToBits());

            Console.WriteLine(CrcConverter.GetHex(a, 30, CrcCaseSensitivity.Upper));
            Console.WriteLine(BitConverter.ToString(b.ToBytes(CrcEndian.BigEndian)).Replace("-", ""));
            Console.WriteLine(c.ToHex(CrcCaseSensitivity.Upper));
        }

        private static string DoTT(string name, byte[] input)
        {
            if (name == "CRC-5/USB")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Calc(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC5_USB), "HashFunction", input, out string hex));
                Console.ResetColor();
                return hex;
            }
            if (name == "CRC-6/CDMA2000-A")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Calc(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC6_CDMA2000A), "HashFunction", input, out string hex));
                Console.ResetColor();
                return hex;
            }
            if (name == "CRC-12/CDMA2000")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Calc(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC12_CDMA2000), "HashFunction", input, out string hex));
                Console.ResetColor();
                return hex;
            }
            if (name == "CRC-14/DARC")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Calc(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC14_DARC), "HashFunction", input, out string hex));
                Console.ResetColor();
                return hex;
            }
            if (name == "CRC-31/PHILIPS")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Calc(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC31_PHILIPS), "HashFunction", input, out string hex));
                Console.ResetColor();
                return hex;
            }
            if (name == "CRC-32")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Calc(new Force.Crc32.Crc32Algorithm(), "Crc32c.NET", input, out string hex));
                Console.ResetColor();
                return hex;
            }
            if (name == "CRC-40/GSM")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Calc(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC40_GSM), "HashFunction", input, out string hex));
                Console.ResetColor();
                return hex;
            }
            if (name == "CRC-64")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Calc(new System.IO.Hashing.Crc64(), "System.IO.Hashing.Crc64", input, out string hex));
                Console.ResetColor();
                return hex;
            }
            if (name == "CRC-64/XZ")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Calc(System.Data.HashFunction.CRC.CRCFactory.Instance.Create(System.Data.HashFunction.CRC.CRCConfig.CRC64_XZ), "HashFunction", input, out string hex));
                Console.ResetColor();
                return hex;
            }
            return string.Empty;
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