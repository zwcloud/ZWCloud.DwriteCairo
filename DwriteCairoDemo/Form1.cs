﻿using System;
using System.Diagnostics;
using System.Windows.Forms;
using Cairo;
using ZWCloud.DWriteCairo;
using FontWeight = ZWCloud.DWriteCairo.FontWeight;

namespace DWriteCairoDemo
{
    public partial class Form1 : Form
    {
        public Context Context1 { get; set; }
        public Win32Surface Surface1 { get; private set; }

        private TextLayout textLayout;

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

            Context1.SetSourceColor(new Cairo.Color(1,0,0));
            Context1.MoveTo(p1);
            Context1.LineTo(p2);
            Context1.LineTo(p3);
            Context1.LineTo(p4);
            Context1.LineTo(p1);
            Context1.ClosePath();
            Context1.Stroke();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Debug.WriteLine("Form1_Shown");

            Surface1 = new Win32Surface(this.CreateGraphics().GetHdc());
            Context1 = new Context(Surface1);

            var textFormat = DWriteCairo.CreateTextFormat(
                "Arial",
                FontWeight.Normal,
                FontStyle.Normal,
                FontStretch.Normal,
                32f);

            Debug.Assert(Math.Abs(textFormat.FontSize - 32f) < 0.0001);

            const string s = "Hello World 1213142124234252352153245345325345";
            textLayout = DWriteCairo.CreateTextLayout(s, textFormat, 300, 40);

            Debug.WriteLine("showing layout");
            Path path = DWriteCairo.RenderLayoutToCairoPath(Context1, textLayout);
            Context1.AppendPath(path);
            Context1.Fill();
            Context1.GetTarget().Flush();
        }
    }
}
