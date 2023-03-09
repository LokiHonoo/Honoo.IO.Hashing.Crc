using Honoo.IO.Hashing;
using HtmlAgilityPack;
using System.Reflection;
using System.Text;

namespace Test
{
    internal static class Test2
    {
        private static readonly Random _random = new Random();
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
                Console.WriteLine($"Width={alg.Width} Refin={alg.Refin} Refout={alg.Refout} Poly={alg.Poly} Init={alg.Init} Xorout={alg.Xorout}");
                string t = string.Empty;
                bool error = false;
                foreach (var name in alg.Names)
                {
                    t = Calc(Crc.Create(name), input);
                    if (Calc(Crc.Create(name, false), input) != t)
                    {
                        error = true;
                    }
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Auto), input) != t)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding8), input) != t)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding8Table), input) != t)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding32), input) != t)
                {
                    error = true;
                }
                if (Calc(Crc.Create(alg.Width, alg.Refin, alg.Refout, alg.Poly, alg.Init, alg.Xorout, CrcCore.Sharding32Table), input) != t)
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
            crc.Update(input);
            byte[] checksum = crc.DoFinal(false);
            string a = BitConverter.ToString(checksum).Replace("-", string.Empty);
            crc.Update(input);
            string t = crc.DoFinal();
            crc.Update(input);
            crc.DoFinal(out byte b);
            string bb = Convert.ToString(b, 16).ToUpperInvariant();
            crc.Update(input);
            crc.DoFinal(out ushort s);
            string ss = Convert.ToString(s, 16).ToUpperInvariant();
            crc.Update(input);
            crc.DoFinal(out uint i);
            string ii = Convert.ToString(i, 16).ToUpperInvariant();
            crc.Update(input);
            bool truncated = crc.DoFinal(out ulong l);
            string ll = Convert.ToString((long)l, 16).ToUpperInvariant();
            Console.Write(crc.AlgorithmName.PadRight(20));
            Console.Write(crc.WithTable ? "TABLE   " : "        ");
            Console.Write(a + " ");
            Console.Write(t + " ");
            Console.Write(bb + " ");
            Console.Write(ss + " ");
            Console.Write(ii + " ");
            Console.Write(ll + " ");
            Console.WriteLine(truncated);
            bool error = false;
            if (!a.EndsWith(t)) error = true;
            if (!t.EndsWith(bb.ToString())) error = true;
            if (!t.EndsWith(ss.ToString())) error = true;
            if (!t.EndsWith(ii.ToString())) error = true;
            if (!t.EndsWith(ll.ToString())) error = true;
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