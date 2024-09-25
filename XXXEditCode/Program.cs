namespace XXXEditCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var files = new List<string>();
            //var regexF = new Regex("Crc\\d+.cs");
            //var regex1 = new Regex("public (Crc.*)\\(\\) : base\\(DEFAULT_NAME, GetEngine\\(\\)\\)");
            //var regex2 = new Regex("internal .*\\(string alias\\) : base\\(alias, GetEngine\\(\\)\\)");
            //var regex3 = new Regex("\\(\\) => { return new .*\\(\\);");
            //var regex4 = new Regex("\\(\\) => { return new .*\\(alias\\);");
            //var regex5 = new Regex("private static (CrcEngine.*) GetEngine\\(\\)");
            //var regex6 = new Regex("if \\(_table == null\\)");
            //var regex7 = new Regex("return new .*\\(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table\\);");
            //string[] tmp = Directory.GetFiles("D:\\Works\\Programs\\Honoo.IO.Hashing.Crc\\Honoo.IO.Hashing.Crc");
            //foreach (string t in tmp)
            //{
            //    if (regexF.IsMatch(t))
            //    {
            //        files.Add(t);
            //    }
            //}
            //if (files.Count > 0)
            //{
            //    foreach (string file in files)
            //    {
            //        var lines = new List<string>();
            //        string name = string.Empty;
            //        string eng = string.Empty;
            //        foreach (var line in File.ReadLines(file))
            //        {
            //            Match match1 = regex1.Match(line);
            //            Match match5 = regex5.Match(line);
            //            if (match1.Success)
            //            {
            //                name = match1.Groups[1].Value;
            //                lines.Add(line.Replace($"DEFAULT_NAME, GetEngine())", "DEFAULT_NAME, GetEngine(CrcTable.Standard))"));
            //                lines.Add("{");
            //                lines.Add("}");
            //                lines.Add("/// <summary>");
            //                lines.Add($"/// Initializes a new instance of the {name} class.");
            //                lines.Add("/// </summary>");
            //                lines.Add($"public {name}(CrcTable withTable) : base(DEFAULT_NAME, GetEngine(withTable))");
            //            }
            //            else if (regex2.IsMatch(line))
            //            {
            //                lines.Add($"internal {name}(string alias, CrcTable withTable) : base(alias, GetEngine(withTable))");
            //            }
            //            else if (regex3.IsMatch(line))
            //            {
            //                lines.Add(line.Replace("() =>", "(t) =>").Replace("(); });", "(t); });"));
            //            }
            //            else if (regex4.IsMatch(line))
            //            {
            //                lines.Add(line.Replace("() =>", "(t) =>").Replace("(alias); });", "(alias, t); });"));
            //            }
            //            else if (match5.Success)
            //            {
            //                eng = match5.Groups[1].Value;
            //                lines.Add(line.Replace("GetEngine()", "GetEngine(CrcTable withTable)"));
            //            }
            //            else if (regex6.IsMatch(line))
            //            {
            //                lines.Add("switch (withTable)");
            //                lines.Add("{");
            //                lines.Add("case CrcTable.Standard:");
            //                lines.Add(line);
            //            }
            //            else if (regex7.IsMatch(line))
            //            {
            //                lines.Add($"return new {eng}(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, _table);");
            //                lines.Add("");
            //                lines.Add($"//case CrcTable.M16x: return new {eng}M16x(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT);");
            //                lines.Add("");
            //                lines.Add($"case CrcTable.None:  default: return new {eng}(WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT, withTable);");
            //                lines.Add("}");
            //            }
            //            else
            //            {
            //                lines.Add(line);
            //            }
            //        }
            //        //Console.WriteLine(string.Join(Environment.NewLine, lines));
            //        //Console.ReadKey(true);
            //        File.WriteAllLines(file, lines.ToArray());
            //    }
            //}
        }
    }
}