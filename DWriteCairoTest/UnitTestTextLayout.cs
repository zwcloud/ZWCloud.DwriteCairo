using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZWCloud.DWriteCairo;


namespace DWriteCairoTest
{
    [TestClass]
    public class UnitTestDWriteCairo
    {
        [TestMethod]
        public void CreateTextFormat()
        {
            const string fontFamilyName = "SimSun";
            const FontWeight fontWeight = FontWeight.Bold;
            const FontStyle fontStyle = FontStyle.Normal;
            const FontStretch fontStretch = FontStretch.Normal;
            const float fontSize = 32f;

            var textFormat = DWriteCairo.CreateTextFormat(fontFamilyName, fontWeight, fontStyle, fontStretch, fontSize);

            Assert.IsNotNull(textFormat, "TextFormat creating failed.");
        }

        [TestMethod]
        public void TestTextFormatApi()
        {
            const string fontFamilyName = "SimSun";
            const FontWeight fontWeight = FontWeight.Bold;
            const FontStyle fontStyle = FontStyle.Normal;
            const FontStretch fontStretch = FontStretch.Normal;
            const float fontSize = 32f;

            var textFormat = DWriteCairo.CreateTextFormat(fontFamilyName, fontWeight, fontStyle, fontStretch, fontSize);


            Assert.AreEqual(fontSize, textFormat.FontSize);
        }
    }
}
