using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZWCloud.DWriteCairo;

namespace DWriteCairoTest
{
    [TestClass]
    public class UnitTestTextLayout
    {
        [TestMethod]
        public void TestTextLayout()
        {
            /* Creating */
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

            /* Property */
            maxWidth = 100;
            maxHeight = 120;
            textLayout.Width = maxWidth;
            Assert.AreEqual(maxWidth, textLayout.Width);

            textLayout.Height = maxHeight;
            Assert.AreEqual(maxHeight, textLayout.Height);

        }
    }
}
