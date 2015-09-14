using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWCloud.DWriteCairo;
using FontStyle = ZWCloud.DWriteCairo.FontStyle;

namespace ComHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var textFormat = DWriteCairo.CreateTextFormat("Arial", FontWeight.DWRITE_FONT_WEIGHT_NORMAL,
                FontStyle.DWRITE_FONT_STYLE_NORMAL, FontStretch.DWRITE_FONT_STRETCH_NORMAL, 32f, "en-us");
            const string s = "Hello World";
            var textLayout = DWriteCairo.CreateTextLayout(s, s.Length, textFormat, 300, 40);
        }
    }
}
