using System;
using ZWCloud.DWriteCairo.DWrite;

namespace ZWCloud.DWriteCairo
{
    public static class DWriteCairo
    {
        private static DWriteFactory factory;
        internal static DWriteFactory Factory
        {
            get
            {
                if(factory == null)
                {
                    factory = DWriteFactory.Create();
                }
                return factory;
            }
        }

        private static DirectWriteCairoTextRenderer render;
        internal static DirectWriteCairoTextRenderer Render
        {
            get
            {
                if (render == null)
                {
                    render = DirectWriteCairoTextRenderer.Create();
                }
                return render;
            }
        }

        public static TextFormat CreateTextFormat(
            string fontFamilyName,
            FontWeight fontWeight, FontStyle fontStyle, FontStretch fontStretch,
            float fontSize,
            string localeName = "en-us")
        {
            var ptr = Factory.CreateTextFormat(fontFamilyName, IntPtr.Zero,
                fontWeight,
                fontStyle, fontStretch, fontSize,
                localeName);
            var dWriteTextFormat = new TextFormat(ptr);
            return dWriteTextFormat;
        }

        public static TextLayout CreateTextLayout(string text,
            int textLength, TextFormat textFormat, float maxWidth, float maxHeight)
        {
            var ptr = Factory.CreateTextLayout(text,
            textLength, textFormat.Handle, maxWidth, maxHeight);
            var dWriteTextLayout = new TextLayout(ptr);
            return dWriteTextLayout;
        }

        public static void ShowLayout(Cairo.Context context,
            TextLayout textLayout)
        {
            textLayout.Show(context, Render, textLayout);
        }
    }
}