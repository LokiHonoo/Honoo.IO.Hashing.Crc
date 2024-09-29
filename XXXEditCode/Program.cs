using System.Text.RegularExpressions;

namespace XXXEditCode
{
    internal class Program
    {
        private static void Do()
        {
            var files = new List<string>();
            var regexF = new Regex("Crc\\d+.cs");
            var regex1 = new Regex("default: return new (CrcEngine\\d+)\\(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable\\);");
            var regex2 = new Regex(" private static (uint\\[\\]) _table;");
            var regex3 = new Regex("if \\(_table == null\\)");
            var regex4 = new Regex("_table = (CrcEngine\\d+).GenerateTable(.*)");
            var regex5 = new Regex("return new (CrcEngine\\d+)\\(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table\\);");
            var regex6 = new Regex("case CrcTableInfo.M16x: return new (CrcEngine\\d+M16x)\\(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, null\\);");
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
                        Match match4 = regex4.Match(line);
                        Match match5 = regex5.Match(line);
                        Match match6 = regex6.Match(line);

                        if (match1.Success)
                        {
                            string m1 = match1.Groups[1].Value;
                            lines.Add($"default: return new {m1}(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);");
                        }
                        else if (match2.Success)
                        {
                            string m1 = match2.Groups[1].Value;
                            lines.Add($"private static {m1} _tableStandard;");
                            lines.Add($"private static {m1} _tableM16x;");
                        }
                        else if (match3.Success)
                        {
                            lines.Add("if (_tableStandard == null)");
                        }
                        else if (match4.Success)
                        {
                            string m1 = match4.Groups[1].Value;
                            ppp = match4.Groups[2].Value;
                            lines.Add($"_tableStandard = {m1}Standard.GenerateTable{ppp}");
                        }
                        else if (match5.Success)
                        {
                            string m1 = match5.Groups[1].Value;
                            lines.Add($"return new {m1}Standard(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableStandard);");
                        }
                        else if (match6.Success)
                        {
                            string m1 = match6.Groups[1].Value;
                            lines.Add($"case CrcTableInfo.M16x:");
                            lines.Add("if (_tableM16x == null)");
                            lines.Add("{");
                            lines.Add($"_tableM16x = {m1}.GenerateTable{ppp}");
                            lines.Add("}");
                            lines.Add($"return new {m1}(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _tableM16x);");
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
            }
        }

        private static void Main(string[] args)
        {
            //Do();
        }
    }
}