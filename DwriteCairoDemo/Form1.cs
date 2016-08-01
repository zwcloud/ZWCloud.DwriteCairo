using System;
using System.Diagnostics;
using System.Windows.Forms;
using Cairo;
using ZWCloud.DWriteCairo;
using FontWeight = ZWCloud.DWriteCairo.FontWeight;

namespace DWriteCairoDemo
{
    public partial class Form1 : Form
    {
        private Context context;
        private Win32Surface surface;
        private TextLayout textLayout;
        private TextFormat textFormat;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.WriteLine("OnPaint");

            base.OnPaint(e);

            var p1 = new PointD(10, 10);
            var p2 = new PointD(100, 10);
            var p3 = new PointD(100, 100);
            var p4 = new PointD(10, 100);

            context.SetSourceColor(new Cairo.Color(1,0,0));
            context.MoveTo(p1);
            context.LineTo(p2);
            context.LineTo(p3);
            context.LineTo(p4);
            context.LineTo(p1);
            context.ClosePath();
            context.Stroke();
        }

        const string s = "dummy0";//"Hello!你好！";//"こんにちは 안녕하세요";
        System.Drawing.RectangleF textRect;

        private void Form1_Shown(object sender, EventArgs e)
        {
            Debug.WriteLine("Form1_Shown");

            surface = new Win32Surface(this.CreateGraphics().GetHdc());
            context = new Context(surface);

            textFormat = DWriteCairo.CreateTextFormat(
                "Consolas",
                FontWeight.Normal,
                FontStyle.Normal,
                FontStretch.Normal,
                12);

            textFormat.TextAlignment = TextAlignment.Center;
            
            float left, top, width, height;

            // get actual size of the text
            var measureLayout = DWriteCairo.CreateTextLayout(s, textFormat, 4096, 4096);
            measureLayout.GetRect(out left, out top, out width, out height);
            measureLayout.Dispose();

            // build text context against the size and format
            textLayout = DWriteCairo.CreateTextLayout(s, textFormat, (int)Math.Ceiling(width), (int)Math.Ceiling(height));

            Debug.WriteLine("showing layout");
            Path path = DWriteCairo.RenderLayoutToCairoPath(context, textLayout);
            context.AppendPath(path);
            context.Fill();

            textLayout.GetRect(out left, out top, out width, out height);
            textRect = new System.Drawing.RectangleF(left, top, width, height);
            context.Rectangle(left, top, width, height);
            context.Stroke();

            context.GetTarget().Flush();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if(textLayout == null)
            {
                return;
            }
            var mousePosition = PointToClient(MousePosition);
            bool isInside;
            var caretIndex = textLayout.XyToIndex(mousePosition.X, mousePosition.Y, out isInside);

            if (!isInside && caretIndex == s.Length - 1)
            {
                ++caretIndex;
            }
            MessageBox.Show(string.Format("Mouse Clicked at {0},{1}: character index is {2}. Text rect: {3}", mousePosition.X,
                mousePosition.Y, caretIndex, textRect));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            textLayout.Dispose();
            textFormat.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            surface.Dispose();
            context.Dispose();
        }
    }
}
