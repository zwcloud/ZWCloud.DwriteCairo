using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Cairo;
using ZWCloud.DWriteCairo;
using ZWCloud.DWriteCairo.DWrite;
using ZWCloud.DWriteCairo;
using ZWCloud.DWriteCairo.DWrite;
using FontStyle = ZWCloud.DWriteCairo.FontStyle;
using FontWeight = ZWCloud.DWriteCairo.FontWeight;
using Graphics = System.Drawing.Graphics;

namespace DWriteCairoDemo
{
    public partial class Form1 : Form
    {
        public Graphics Graphics1 { get; private set; }
        public Context Context1 { get; set; }
        public Win32Surface Surface1 { get; private set; }

        private bool ready;
        private TextLayout textLayout;

        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDC(IntPtr hWnd);

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
            textLayout = DWriteCairo.CreateTextLayout(s, s.Length, textFormat, 300, 40);
            ready = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var p1 = new PointD(10, 10);
            var p2 = new PointD(100, 10);
            var p3 = new PointD(100, 100);
            var p4 = new PointD(10, 100);

            Context1.SetSourceColor(new Cairo.Color(1,0,0));
            Context1.MoveTo(p1);
            Context1.LineTo(p2);
            Context1.LineTo(p3);
            Context1.LineTo(p4);
            Context1.LineTo(p1);
            Context1.ClosePath();
            Context1.Stroke();

            if (ready)
            {
                Debug.WriteLine("showing layout");
                DWriteCairo.ShowLayout(Context1, textLayout);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Surface1 = new Win32Surface(this.CreateGraphics().GetHdc());
            Context1 = new Context(Surface1);
            Context1.Antialias = Antialias.Subpixel;
        }
    }
}
