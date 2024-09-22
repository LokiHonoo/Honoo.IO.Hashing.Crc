namespace XXXEditCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var files = new List<string>();
            //var regex1 = new Regex("Crc\\d+.cs");
            //var regex2 = new Regex("public .* base\\(\"(.*)\", GetEngine\\(\\)\\)");
            //var regex3 = new Regex("return new CrcName\\(\".*\", WIDTH, REFIN, REFOUT, POLY, INIT, XOROUT");
            //string[] tmp = Directory.GetFiles("D:\\Works\\Programs\\Honoo.IO.Hashing.Crc\\Honoo.IO.Hashing.Crc");
            //foreach (string t in tmp)
            //{
            //    if (regex1.IsMatch(t))
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
            //        foreach (var line in File.ReadLines(file))
            //        {
            //            Match match2 = regex2.Match(line);
            //            if (match2.Success)
            //            {
            //                name = match2.Groups[1].Value;
            //                lines.Insert(lines.Count - 1 - 3, $"private const string DEFAULT_NAME = \"{name}\";");

            //                lines.Add(line.Replace($"\"{name}\"", "DEFAULT_NAME"));
            //            }
            //            else if (regex3.IsMatch(line))
            //            {
            //                lines.Add(line.Replace($"\"{name}\"", "DEFAULT_NAME"));
            //            }
            //            else
            //            {
            //                lines.Add(line);
            //            }
            //        }
            //        File.WriteAllLines(file, lines.ToArray());
            //        // Console.WriteLine(string.Join(Environment.NewLine,lines));
            //        // Console.ReadKey(true);
            //    }
            //}
        }
    }
}