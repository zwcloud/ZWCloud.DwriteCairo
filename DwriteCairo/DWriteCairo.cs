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
        
        /// <summary>
        /// Create a text format object used for text layout.
        /// </summary>
        /// <param name="fontFamilyName">Name of the font family</param>
        /// <param name="fontWeight">Font weight</param>
        /// <param name="fontStyle">Font style</param>
        /// <param name="fontStretch">Font stretch</param>
        /// <param name="fontSize">Logical size of the font in DIP units. A DIP ("device-independent pixel") equals 1/96 inch.</param>
        /// <param name="localeName">Locale name(optional)</param>
        /// TODO understand the meaning of Locale name
        /// <returns> newly created text format object </returns>
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

        /// <summary>
        /// CreateTextLayout takes a string, format, and associated constraints
        /// and produces an object representing the fully analyzed
        /// and formatted result.
        /// </summary>
        /// <param name="text">The text to layout.</param>
        /// <param name="textLength">The length of the string.</param>
        /// <param name="textFormat">The format to apply to the string.</param>
        /// <param name="maxWidth">Width of the layout box.</param>
        /// <param name="maxHeight">Height of the layout box.</param>
        /// <returns>
        /// The resultant object.
        /// </returns>
        public static TextLayout CreateTextLayout(string text,
            int textLength, TextFormat textFormat, float maxWidth, float maxHeight)
        {
            var ptr = Factory.CreateTextLayout(text,
            textLength, textFormat.Handle, maxWidth, maxHeight);
            var dWriteTextLayout = new TextLayout(ptr);
            return dWriteTextLayout;
        }

        /// <summary>
        /// Show the layout
        /// </summary>
        /// <param name="context">a Cairo context</param>
        /// <param name="textLayout">the layout to show</param>
        public static void ShowLayout(Cairo.Context context,
            TextLayout textLayout)
        {
            textLayout.Show(context, Render, textLayout);
        }
    }
}