using System;
using System.Diagnostics;
using System.Windows.Forms;
using Cairo;
using ZWCloud.DWriteCairo;
using FontWeight = ZWCloud.DWriteCairo.FontWeight;

namespace DwriteCairoDemo_DoubleBuffered
{
    public partial class Form1 : Form
    {
        public Context FontContext { get; set; }
        public Win32Surface Win32Surface { get; private set; }

        public Context BackContext { get; set; }
        public ImageSurface ImageSurface { get; private set; }

        private TextLayout textLayout;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.WriteLine("OnPaint");

            //draw something on the back surface

            var p1 = new PointD(10, 10);
            var p2 = new PointD(100, 10);
            var p3 = new PointD(100, 100);
            var p4 = new PointD(10, 100);

            BackContext.SetSourceColor(new Cairo.Color(1,0,0));
            BackContext.MoveTo(p1);
            BackContext.LineTo(p2);
            BackContext.LineTo(p3);
            BackContext.LineTo(p4);
            BackContext.LineTo(p1);
            BackContext.ClosePath();
            BackContext.Stroke();

            BackContext.SetSourceColor(new Cairo.Color(0, 1, 0));
            BackContext.MoveTo(new PointD(p3.X + 10, p3.Y + 10));
            DWriteCairo.ShowLayout(BackContext, textLayout);

            //copy back surface to font surface
            SwapBuffer();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Debug.WriteLine("Form1_Shown");

            Win32Surface = new Win32Surface(this.CreateGraphics().GetHdc());
            FontContext = new Context(Win32Surface);

            //CRITICAL: Format of Win32Surface and ImageSurface must be identical!

            ImageSurface = new ImageSurface(Format.Rgb24, ClientSize.Width, ClientSize.Height);
            BackContext = new Context(ImageSurface);

            //Clear Surface2
            BackContext.SetSourceColor(new Color(1,1,1));
            BackContext.Operator = Operator.Source;
            BackContext.Paint();
            BackContext.Operator = Operator.Over;

            var textFormat = DWriteCairo.CreateTextFormat(
                "Arial",
                FontWeight.Normal,
                FontStyle.Normal,
                FontStretch.Normal,
                32f);
            
            Debug.Assert(Math.Abs(textFormat.FontSize - 32f) < 0.0001);
            
            const string s = "Hello World";
            textLayout = DWriteCairo.CreateTextLayout(s, textFormat, 300, 40);

        }

        /// <summary>
        /// Paint ImageSurface to Win32Surface
        /// </summary>
        private void SwapBuffer()
        {
            FontContext.SetSourceSurface(ImageSurface, 0, 0);
            FontContext.Paint();
        }

    }
}
