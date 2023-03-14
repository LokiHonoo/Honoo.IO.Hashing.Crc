using Honoo.IO.Hashing;
using HtmlAgilityPack;
using System.Reflection;
using System.Text;

namespace Test
{
    internal static class Test
    {
        private static readonly Random _random = new();
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
            Alg[] algs = GetAlgs();
            //foreach (Alg alg in algs)
            //{
            //    Console.WriteLine(string.Join(',', alg.Names)+",");
            //}
            byte[] input = Encoding.UTF8.GetBytes("1111221ADV233334444555566677788000AAAABB");
            foreach (Alg alg in algs)
            {
                bool error = false;
                Console.WriteLine("===================================================================================================");
                Console.WriteLine(string.Join(',', alg.Names));
                Console.WriteLine($"Width={alg.Width} Refin={alg.Refin} Refout={alg.Refout} Poly={alg.Poly} Init={alg.Init} Xorout={alg.Xorout}");
                //
                Crc crc = Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Auto);
                string t = Calc(crc, input);
                CrcCore noTable;
                if (alg.Width <= 8) noTable = CrcCore.UInt8;
                else if (alg.Width <= 16) noTable = CrcCore.UInt16;
                else if (alg.Width <= 32) noTable = CrcCore.UInt32;
                else if (alg.Width <= 64) noTable = CrcCore.UInt64;
                else noTable = CrcCore.Sharding32;
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, noTable), input) != t)
                {
                    error = true;
                }
                foreach (var name in alg.Names)
                {
                    if (CrcName.TryGetAlgorithmName(name, out CrcName crcName))
                    {
                        if (crcName.Name != name)
                        {
                            error = true;
                        }
                        if (Calc(Crc.Create(crcName), input) != t)
                        {
                            error = true;
                        }
                    }
                    else
                    {
                        error = true;
                    }
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding8Table), input) != t)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding8), input) != t)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding32Table), input) != t)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding32), input) != t)
                {
                    error = true;
                }
                if (error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("===================================================================================================");
                    Console.WriteLine("===================================================================================================");
                    Console.ResetColor();
                    _error++;
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Error={_error}");
            Console.WriteLine();
        }

        private static string Calc(Crc crc, byte[] input)
        {
            crc.Update(input);
            byte[] checksum = crc.DoFinal(false);
            string a = BitConverter.ToString(checksum).Replace("-", string.Empty);
            string h = CrcUtilities.ToHexString(false, checksum, 0, crc.Width);
            string h1 = Convert.ToString(CrcUtilities.ToByte(false, checksum, 0, crc.Width), 16).ToUpperInvariant();
            string h2 = Convert.ToString(CrcUtilities.ToUInt16(false, checksum, 0, crc.Width), 16).ToUpperInvariant();
            string h3 = Convert.ToString(CrcUtilities.ToUInt32(false, checksum, 0, crc.Width), 16).ToUpperInvariant();
            string h4 = Convert.ToString((long)CrcUtilities.ToUInt64(false, checksum, 0, crc.Width), 16).ToUpperInvariant();
            crc.Update(input);
            string t = crc.DoFinal();
            crc.Update(input);
            crc.DoFinal(out byte b);
            string t1 = Convert.ToString(b, 16).ToUpperInvariant();
            crc.Update(input);
            crc.DoFinal(out ushort s);
            string t2 = Convert.ToString(s, 16).ToUpperInvariant();
            crc.Update(input);
            crc.DoFinal(out uint i);
            string t3 = Convert.ToString(i, 16).ToUpperInvariant();
            crc.Update(input);
            bool truncated = crc.DoFinal(out ulong l);
            string t4 = Convert.ToString((long)l, 16).ToUpperInvariant();
            Console.Write(crc.Name.PadRight(20));
            Console.Write(crc.WithTable ? "TABLE   " : "        ");
            Console.Write(a + " ");
            Console.Write(t + " ");
            Console.Write(t1 + " ");
            Console.Write(t2 + " ");
            Console.Write(t3 + " ");
            Console.Write(t4 + " ");
            Console.WriteLine(truncated);
            Console.Write(new string(' ', 28 + a.Length + 1));
            Console.Write(h + " ");
            Console.Write(h1 + " ");
            Console.Write(h2 + " ");
            Console.Write(h3 + " ");
            Console.WriteLine(h4 + " ");
            bool error = false;
            if (!a.EndsWith(t)) error = true;
            if (!t.EndsWith(t1.ToString())) error = true;
            if (!t.EndsWith(t2.ToString())) error = true;
            if (!t.EndsWith(t3.ToString())) error = true;
            if (!t.EndsWith(t4.ToString())) error = true;
            if (!t.EndsWith(h.ToString())) error = true;
            if (!t.EndsWith(h1.ToString())) error = true;
            if (!t.EndsWith(h2.ToString())) error = true;
            if (!t.EndsWith(h3.ToString())) error = true;
            if (!t.EndsWith(h4.ToString())) error = true;
            return error ? _random.NextDouble().ToString() : t;
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