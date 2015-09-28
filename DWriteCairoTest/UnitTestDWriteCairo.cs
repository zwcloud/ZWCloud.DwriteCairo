using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZWCloud.DWriteCairo;

namespace DWriteCairoTest
{
    [TestClass]
    public class UnitTestDWriteCairo
    {
        [TestMethod]
        public void TestAll()
        {
            var textFormat = DWriteCairo.CreateTextFormat("Simsun", FontWeight.Bold, FontStyle.Normal, FontStretch.Normal, 16f);
            Assert.IsNotNull(textFormat);
            var textLayout = DWriteCairo.CreateTextLayout("1234asdf.@#$^!Hello! 你好！ こんにちは 안녕하세요", textFormat, 180, 60);
            Assert.IsNotNull(textLayout);
        }
    }
}
