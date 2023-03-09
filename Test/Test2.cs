using Honoo.IO.Hashing;
using HtmlAgilityPack;
using System.Reflection;
using System.Text;

namespace Test
{
    internal static class Test2
    {
        private static int _error = 0;

        internal static Alg[] GetAlgs()
        {
            List<Alg> algs = new List<Alg>();
            Stream catalogue = Assembly.GetExecutingAssembly().GetManifestResourceStream("Test.catalogue.txt")!;

            HtmlDocument document = new HtmlDocument();
            document.Load(catalogue);
            var nodes = document.DocumentNode.SelectNodes("/html/body/h3");
            foreach (var node in nodes)
            {
                var code = node.NextSibling.NextSibling;
                if (code != null && code.Name == "p")
                {
                    string txt = code.FirstChild.InnerText;
                    Alg alg = new Alg();
                    List<string> names = new List<string>();

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
                    alg.Refin = bool.Parse(txt.Substring(index1 + 6, index2 - index1 - 6));

                    index1 = txt.IndexOf("refout=");
                    index2 = txt.IndexOf(" ", index1);
                    alg.Refout = bool.Parse(txt.Substring(index1 + 7, index2 - index1 - 7));

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
                            txt = item.InnerText.Substring("Alias:".Length, item.InnerText.Length - "Alias:".Length);
                            string[] splits = txt.Split(',');
                            names.AddRange(splits);
                        }
                    }
                    //
                    alg.Names = names.ToArray();
                    algs.Add(alg);
                }
            }
            return algs.ToArray();
        }

        internal static void Test()
        {
            _error = 0;
            Alg[] algs = GetAlgs();
            //foreach (Alg alg in algs)
            //{
            //    Console.WriteLine(string.Join(',', alg.Names)+",");
            //}
            string str = "1111221ADV233334444555566677788000AAAABB";
            byte[] input = Encoding.UTF8.GetBytes(str);
            foreach (Alg alg in algs)
            {
                Console.WriteLine("===================================================================================================");
                Console.WriteLine(string.Join(',', alg.Names));
                if (alg.Width <= 64)
                {
                    Console.WriteLine($"Width={alg.Width} Refin={alg.Refin} Refout={alg.Refout} Poly={alg.Poly} Init={alg.Init} Xorout={alg.Xorout}");
                }
                else
                {
                    Console.WriteLine($"Width={alg.Width} Refin={alg.Refin} Refout={alg.Refout}");
                    Console.WriteLine($"Poly={alg.Poly} Init={alg.Init} Xorout={alg.Xorout}");
                }
                string v = string.Empty;
                bool error = false;
                foreach (var name in alg.Names)
                {
                    v = Calc(Crc.Create(name), input);
                    if (Calc(Crc.Create(name, false), input) != v)
                    {
                        error = true;
                    }
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Auto), input) != v)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding8), input) != v)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding8Table), input) != v)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding32), input) != v)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding32Table), input) != v)
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
            Console.WriteLine($"Error {_error}");
        }

        private static string Calc(Crc crc, byte[] input)
        {
            byte[] checksum = crc.DoFinal(false, input);
            string l = CrcUtilities.ToHex(false, checksum, 0, (int)Math.Ceiling(crc.ChecksumSize / 4d));
            string v = crc.DoFinal(input);
            Console.Write(crc.AlgorithmName.PadRight(20));
            Console.Write($"WITH_TABLE {crc.WithTable}".PadRight(20));
            Console.Write(BitConverter.ToString(checksum).Replace('-', (char)0) + "   ");
            Console.Write(v + "   ");
            Console.WriteLine(l);
            return v;
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