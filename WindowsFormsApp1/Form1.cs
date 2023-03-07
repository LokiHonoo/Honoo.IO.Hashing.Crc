using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //   CRC-24/LTE-B
        //width = 24 poly=0x800063 init=0x000000 refin=false refout=false xorout=0x000000
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = textBox1.Text;
            int index1 = 0;
            int index2 = 0;
            index1 = txt.IndexOf("width=");
            index2 = txt.IndexOf(" ", index1);
            string width = txt.Substring(index1 + 6, index2 - index1 - 6);

            index1 = txt.IndexOf("poly=");
            index2 = txt.IndexOf(" ", index1);
            string poly = txt.Substring(index1 + 5, index2 - index1 - 5).ToUpper().Replace('X', 'x');

            index1 = txt.IndexOf("init=");
            index2 = txt.IndexOf(" ", index1);
            string init = txt.Substring(index1 + 5, index2 - index1 - 5).ToUpper().Replace('X', 'x');

            index1 = txt.IndexOf("refin=");
            index2 = txt.IndexOf(" ", index1);
            string refin = txt.Substring(index1 + 6, index2 - index1 - 6);

            index1 = txt.IndexOf("refout=");
            index2 = txt.IndexOf(" ", index1);
            string refout = txt.Substring(index1 + 7, index2 - index1 - 7);

            index1 = txt.IndexOf("xorout=");
            index2 = txt.IndexOf(" ", index1);
            string xorout = txt.Substring(index1 + 7, index2 - index1 - 7).ToUpper().Replace('X', 'x');

            index1 = txt.IndexOf("name=");
            index2 = txt.IndexOf("\"", index1 + 7);
            string name = txt.Substring(index1 + 5, index2 - index1 - 5).Trim('\"');
            string cla = name.ToLower().Replace("/", "").Replace("-", "").Replace("crc", "Crc");
            string ou = Properties.Resources.Templ;
            ou = ou.Replace("<%name%>", name)
                .Replace("<%class%>", cla)
                .Replace("<%gta%>", refin == "true" ? "GenerateReversedTable" : "GenerateTable")
                .Replace("<%width%>", width)
                .Replace("<%poly%>", poly)
                .Replace("<%init%>", init)
                .Replace("<%refin%>", refin)
                .Replace("<%refout%>", refout)
                .Replace("<%xorout%>", xorout);
            this.textBox2.Text = ou;

            string ou2 = Properties.Resources.templ2;
            ou2 = ou2.Replace("<%name%>", name)
    .Replace("<%class%>", cla)
    .Replace("<%width%>", width)
    .Replace("<%poly%>", poly)
    .Replace("<%init%>", init)
    .Replace("<%refin%>", refin)
    .Replace("<%refout%>", refout)
    .Replace("<%xorout%>", xorout);
            this.textBox2.Text += Environment.NewLine;
            this.textBox2.Text += Environment.NewLine;
            this.textBox2.Text += Environment.NewLine;
            this.textBox2.Text += ou2;
        }
    }
}