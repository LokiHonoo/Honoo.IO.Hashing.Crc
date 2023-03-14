using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("D:\\Works\\Programs\\Honoo.IO.Hashing.Crc\\Honoo.IO.Hashing.Crc", "crc*.*");
            foreach (var file in files)
            {
                bool ex = false;
                List<string> lines = new List<string>(File.ReadAllLines(file));
                for (int i = lines.Count - 1; i >= 0; i--)
                {
                    if (lines[i].IndexOf("_table = CrcEngine") >= 0)
                    {
                        lines.RemoveAt(i + 1);
                        lines.RemoveAt(i - 1);
                        lines.RemoveAt(i - 1 - 1);
                        i-=3;
                        ex = true;
                    }
                }
                if (ex) {
                    File.WriteAllLines(file, lines.ToArray());

                }
            }
            //StringBuilder result = new StringBuilder();
            //string[] lines = this.textBox1.Lines;
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    string line = lines[i].Trim();
            //    if (line.Length > 0)
            //    {
            //        if (line.IndexOf("_table;") >= 0)
            //        {
            //            result.AppendLine("private const int CHECKSUM_SIZE = <%width%>;");
            //            result.AppendLine("private const byte POLY = <%poly%>;");
            //            result.AppendLine("private const byte INIT = <%init%>;");
            //            result.AppendLine("private const byte XOROUT = <%xorout%>;");
            //            result.AppendLine("private const bool REFIN = <%refin%>;");
            //            result.AppendLine("private const bool REFOUT = <%refout%>;");
            //            result.AppendLine(line);
            //        }
            //        else if (line.IndexOf("base(GetEngine(alias))") >= 0)
            //        {
            //            result.AppendLine(line.Replace("base(GetEngine(alias))", "base(alias , GetEngine())"));
            //        }
            //        else if (line.IndexOf("base(GetEngine(") >= 0)
            //        {
            //            result.AppendLine(line.Replace("base(GetEngine(", "base(").Replace("))", ",GetEngine())"));
            //        }
            //        else if (line.IndexOf("_table = CrcEngine") >= 0)
            //        {
            //            result.AppendLine("lock (_table) {");
            //            result.AppendLine(line);
            //            result.AppendLine("}");
            //        }
            //        else if (line.IndexOf("return new CrcName(") >= 0)
            //        {
            //            result.AppendLine(line.Replace(", () => { return new", ",CHECKSUM_SIZE,REFIN,REFOUT ,POLY,INIT ,XOROUT, () => { return new"));
            //        }
            //        else if (line.IndexOf("private static CrcEngine GetEngine(string algorithmName)") >= 0)
            //        {
            //            result.AppendLine("private static CrcEngine GetEngine()");
            //        }
            //        else if (line.IndexOf("return new CrcEngine") >= 0)
            //        {
            //            string[] splits = line.Split(',');
            //            result.Replace("<%width%>", splits[1]);
            //            result.Replace("<%refin%>", splits[2]);
            //            result.Replace("<%refout%>", splits[3]);
            //            result.Replace("<%poly%>", splits[4]);
            //            result.Replace("<%init%>", splits[5]);
            //            result.Replace("<%xorout%>", splits[6]);
            //            result.AppendLine("return new CrcEngine8(CHECKSUM_SIZE,REFIN,REFOUT ,POLY,INIT ,XOROUT,_table); ");
            //        }
            //        else
            //        {
            //            result.AppendLine(line);
            //        }
            //    }
            //}
            //Clipboard.SetText(result.ToString());
        }

        private string DDD(string[] lines, ref int lineIndex)
        {
            StringBuilder result = new StringBuilder();

            while (lineIndex < lines.Length)
            {
                string line = lines[lineIndex].Trim();
                lineIndex++;
                if (line.Length > 0)
                {
                    if (line.IndexOf("return new CrcName(") >= 0)
                    {
                        result.AppendLine(line.Replace(", () => { return new", ",CHECKSUM_SIZE,REFIN,REFOUT ,POLY,INIT ,XOROUT, () => { return new"));
                    }
                    else if (line.IndexOf("return new CrcEngine") >= 0)
                    {
                        string[] splits = line.Split(',');
                        result.Replace("<%width%>", splits[1]);
                        result.Replace("<%refin%>", splits[2]);
                        result.Replace("<%refout%>", splits[3]);
                        result.Replace("<%poly%>", splits[4]);
                        result.Replace("<%init%>", splits[5]);
                        result.Replace("<%xorout%>", splits[6]);
                        result.AppendLine("return new CrcEngine16(algorithmName,CHECKSUM_SIZE,REFIN,REFOUT ,POLY,INIT ,XOROUT,_table); ");
                        break;
                    }
                    else
                    {
                        result.AppendLine(line);
                    }
                }
            }
            return result.ToString();
        }
    }
}