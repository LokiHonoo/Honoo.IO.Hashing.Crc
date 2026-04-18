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
            var regex1 = new Regex("private static (.*)\\[] _tableM16x;");
            var regex2 = new Regex("private static (.*)\\[] _tableStandard;");
            var regex3 = new Regex("case CrcTableInfo.Standard:");
            var regex4 = new Regex("case CrcTableInfo.M16x:");
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
                    var lines = File.ReadLines(file).ToArray();
                    var result = new List<string>();
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var line = lines[i];
                        Match match1 = regex1.Match(line);
                        Match match2 = regex2.Match(line);
                        Match match3 = regex3.Match(line);
                        Match match4 = regex4.Match(line);
                        if (match1.Success)
                        {
                            //    lines.Add("using System;");
                        }
                        else if (match2.Success)
                        {
                            // ppp = match2.Groups[1].Value;
                        }
                        else if (match3.Success)
                        {
                            i += 3;
                            string ppp = lines[i].Trim().Trim(';').Replace("_tableStandard = ", string.Empty);
                            i += 2;
                            string ppp2 = lines[i].Trim().Replace("_tableStandard", ppp);
                            result.Add(line + ppp2);
                            i++;
                        }
                        else if (match4.Success)
                        {
                            i += 3;
                            string ppp = lines[i].Trim().Trim(';').Replace("_tableM16x = ", string.Empty);
                            i += 2;
                            string ppp2 = lines[i].Trim().Replace("_tableM16x", ppp);
                            result.Add(line + ppp2);
                            i++;
                        }
                        else
                        {
                            result.Add(line);
                        }
                    }
                    //Console.WriteLine(string.Join(Environment.NewLine, lines));
                    //Console.ReadKey(true);
                    File.WriteAllLines(file, result.ToArray());
                }
                Console.WriteLine();
                Console.WriteLine("Done.");
            }
        }

        private static void Main(string[] args)
        {
            //Do();
        }
    }
}