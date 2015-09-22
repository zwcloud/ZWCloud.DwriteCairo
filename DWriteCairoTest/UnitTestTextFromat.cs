using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZWCloud.DWriteCairo;


namespace DWriteCairoTest
{
    [TestClass]
    public class UnitTestTextFromat
    {
        [TestMethod]
        public void TestTextFormat()
        {
            const string fontFamilyName = "SimSun";
            const FontWeight fontWeight = FontWeight.Bold;
            const FontStyle fontStyle = FontStyle.Normal;
            const FontStretch fontStretch = FontStretch.Normal;
            const float fontSize = 32f;

            var textFormat = DWriteCairo.CreateTextFormat(fontFamilyName, fontWeight, fontStyle, fontStretch, fontSize);

            Assert.IsNotNull(textFormat, "TextFormat creating failed.");

            Assert.AreEqual(fontFamilyName, textFormat.FontFamilyName);
            Assert.AreEqual(fontWeight, textFormat.FontWeight);
            Assert.AreEqual(fontStyle, textFormat.FontStyle);
            Assert.AreEqual(fontStretch, textFormat.FontStretch);
            Assert.AreEqual(fontSize, textFormat.FontSize);

            textFormat.TextAlignment = TextAlignment.Center;

            Assert.AreEqual(textFormat.TextAlignment, TextAlignment.Center);
        }
    }
}
