using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZWCloud.DWriteCairo;
using System.Windows.Forms;

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
            textFormat.TextAlignment = TextAlignment.Leading;

            var text = "Hello! 你好！ こんにちは 안녕하세요";
            int maxWidth = 200;
            int maxHeight = 40;

            var textLayout = DWriteCairo.CreateTextLayout(text, textFormat, maxWidth, maxHeight);

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

            textLayout.FontFamilyName = "Courier New";
            Assert.AreEqual("Courier New", textLayout.FontFamilyName);

            textLayout.FontFamilyName = "Simsun";
            Assert.AreEqual("Simsun", textLayout.FontFamilyName);
        }

        [TestMethod]
        public void TestTextLayoutXYToIndex()
        {
            const string fontFamilyName = "SimSun";
            const FontWeight fontWeight = FontWeight.Bold;
            const FontStyle fontStyle = FontStyle.Normal;
            const FontStretch fontStretch = FontStretch.Normal;
            const float fontSize = 32f;
            var textFormat = DWriteCairo.CreateTextFormat(fontFamilyName, fontWeight, fontStyle, fontStretch, fontSize);
            textFormat.TextAlignment = TextAlignment.Leading;

            var text = "Hello! 你好！ こんにちは 안녕하세요";
            int maxWidth = 200;
            int maxHeight = 40;

            var textLayout = DWriteCairo.CreateTextLayout(text, textFormat, maxWidth, maxHeight);
            bool isInside;
            var caretIndex = textLayout.XyToIndex(0f, 0f, out isInside);
            if (!isInside && caretIndex == text.Length - 1)
            {
                ++caretIndex;
            }
            Assert.AreEqual(0u, caretIndex);

            //Make sure this point(1000f, 1000f) is off the bottom right of the text layout box.
            caretIndex = textLayout.XyToIndex(1000f, 1000f, out isInside);
            if (!isInside && caretIndex == text.Length - 1)
            {
                ++caretIndex;
            }
            Assert.AreEqual((uint)text.Length, caretIndex);

            MessageBox.Show("All test passed.\nNow run DWriteCairoDemo and click on the text to check if the XyToIndex works well.");
        }

        [TestMethod]
        public void TestTextLayoutIndexToXY()
        {
            const string fontFamilyName = "SimSun";
            const FontWeight fontWeight = FontWeight.Bold;
            const FontStyle fontStyle = FontStyle.Normal;
            const FontStretch fontStretch = FontStretch.Normal;
            const float fontSize = 32f;
            var textFormat = DWriteCairo.CreateTextFormat(fontFamilyName, fontWeight, fontStyle, fontStretch, fontSize);
            textFormat.TextAlignment = TextAlignment.Leading;

            var text = "Hello! 你好！ こんにちは 안녕하세요";
            int maxWidth = 200;
            int maxHeight = 40;

            var textLayout = DWriteCairo.CreateTextLayout(text, textFormat, maxWidth, maxHeight);
            float x, y, height;
            uint characterIndex = 1;
            textLayout.IndexToXY(characterIndex, false, out x, out y, out height);
            Debug.WriteLine("Character index: {0}, point: ({1},{2}), height: {3}", characterIndex, x, y, height);
        }

    }
}
