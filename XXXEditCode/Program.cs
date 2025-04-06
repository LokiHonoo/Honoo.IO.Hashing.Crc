using System.Text.RegularExpressions;

namespace XXXEditCode
{
    internal class Program
    {
        private static void Do()
        {
            var files = new List<string>();
            //var regexF = new Regex("Crc10.cs");
            var regexF = new Regex("Crc\\d+.cs");
            var regex1 = new Regex("namespace Honoo\\.IO\\.Hashing");
            var regex2 = new Regex("public (.*)\\(CrcTableInfo withTable = CrcTableInfo\\.Standard\\) : base\\(DEFAULT_NAME, GetEngine\\(withTable\\)\\)");
            var regex3 = new Regex("internal static CrcName GetAlgorithmName\\(\\)");
            var regex4 = new Regex("_table = (CrcEngine\\d+).GenerateTable(.*)");
            var regex5 = new Regex("return new (CrcEngine\\d+)\\(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table\\);");
            var regex6 = new Regex("1011000101000101101010101101000111110100001001011010110000011110");
            //
            //
            //

            string[] tmp = Directory.GetFiles("D:\\Works\\Programs\\Honoo.IO.Hashing.Crc\\Honoo.IO.Hashing.Crc");
            foreach (string t in tmp)
            {
                if (regexF.IsMatch(t))
                {
                    files.Add(t);
                }
            }
            if (files.Count > 0)
            {
                foreach (string file in files)
                {
                    var lines = new List<string>();
                    string ppp = string.Empty;
                    foreach (var line in File.ReadLines(file))
                    {
                        Match match1 = regex1.Match(line);
                        Match match2 = regex2.Match(line);
                        Match match3 = regex3.Match(line);
                        if (match1.Success)
                        {
                            //    lines.Add("using System;");
                            lines.Add(line);
                        }
                        else if (match2.Success)
                        {
                            ppp = match2.Groups[1].Value;
                            lines.Add(line);
                        }
                        else if (match3.Success)
                        {
                            lines.Add("/// <summary>");
                            lines.Add("/// Creates an instance of the algorithm.");
                            lines.Add("/// </summary>");
                            lines.Add("/// <param name=\"withTable\">Calculate with table.</param>");
                            lines.Add("/// <returns></returns>");
                            lines.Add($"public static {ppp} Create(  CrcTableInfo withTable = CrcTableInfo.Standard)");
                            lines.Add("{");
                            lines.Add($"return new {ppp}(withTable);");
                            lines.Add(" }");
                            lines.Add(line);
                        }
                        else
                        {
                            lines.Add(line);
                        }
                    }
                    //Console.WriteLine(string.Join(Environment.NewLine, lines));
                    //Console.ReadKey(true);
                    File.WriteAllLines(file, lines.ToArray());
                }
                Console.WriteLine();
                Console.WriteLine("Done.");
            }
        }

        private static void Main(string[] args)
        {
            // Do();
        }
    }
}