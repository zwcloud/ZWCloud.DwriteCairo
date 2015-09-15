using System;
using System.Diagnostics;
using System.Windows.Forms;
using ZWCloud.DWriteCairo;
using FontStyle = ZWCloud.DWriteCairo.FontStyle;

namespace DWriteCairoDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var textFormat = DWriteCairo.CreateTextFormat(
                "Arial",
                FontWeight.DWRITE_FONT_WEIGHT_NORMAL,
                FontStyle.DWRITE_FONT_STYLE_NORMAL,
                FontStretch.DWRITE_FONT_STRETCH_NORMAL,
                32f);

            Debug.Assert(Math.Abs(textFormat.FontSize - 32f) < 0.0001);

            const string s = "Hello World";
            var textLayout = DWriteCairo.CreateTextLayout(s, s.Length, textFormat, 300, 40);

            //DWriteCairo.ShowLayout();
        }
    }
}
