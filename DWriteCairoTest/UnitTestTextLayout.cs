using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZWCloud.DWriteCairo;
using System.Diagnostics;

namespace DWriteCairoTest
{
    [TestClass]
    public class UnitTestTextLayout
    {
        [TestMethod]
        public void TestTextLayout()
        {
            /* create */
            const string fontFamilyName = "SimSun";
            const FontWeight fontWeight = FontWeight.Bold;
            const FontStyle fontStyle = FontStyle.Normal;
            const FontStretch fontStretch = FontStretch.Normal;
            const float fontSize = 32f;
            var textFormat = DWriteCairo.CreateTextFormat(fontFamilyName, fontWeight, fontStyle, fontStretch, fontSize);
            float maxWidth = 200;
            float maxHeight = 80;

            var textLayout = DWriteCairo.CreateTextLayout("Hello! 你好！ こんにちは 안녕하세요", textFormat, maxWidth, maxHeight);

            Assert.IsNotNull(textLayout, "TextLayout creating failed.");
            Assert.AreEqual(maxWidth, textLayout.Width);
            Assert.AreEqual(maxHeight, textLayout.Height);

            /* read and write property */
            textLayout.Width = 100;
            Assert.AreEqual(100, textLayout.Width);

            textLayout.Height = 120;
            Assert.AreEqual(120, textLayout.Height);

            textLayout.FontWeight = FontWeight.Bold;
            Assert.AreEqual(FontWeight.Bold, textLayout.FontWeight);
            textLayout.FontWeight = FontWeight.Normal;
            Assert.AreEqual(FontWeight.Normal, textLayout.FontWeight);

            textLayout.FontStyle = FontStyle.Italic;
            Assert.AreEqual(FontStyle.Italic, textLayout.FontStyle);

            textLayout.FontStyle = FontStyle.Normal;
            Assert.AreEqual(FontStyle.Normal, textLayout.FontStyle);

            textLayout.FontStretch = FontStretch.Medium;
            Assert.AreEqual(FontStretch.Medium, textLayout.FontStretch);

            textLayout.FontStretch = FontStretch.Normal;
            Assert.AreEqual(FontStretch.Normal, textLayout.FontStretch);

            Debug.WriteLine(textLayout.FontFamilyName);
            textLayout.FontFamilyName = "Courier New";
            Assert.AreEqual("Courier New", textLayout.FontFamilyName);

            textLayout.FontFamilyName = "Simsun";
            Assert.AreEqual("simsun", textLayout.FontFamilyName);
        }
    }
}
