using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tick = new Tick() { Timestamp = new DateTime(1999, 12, 31, 23, 59, 59), Open = 1.0, Close = 2.0, High = 3.0, Low = 0.5 };

            Assert.AreEqual(2.0, tick.Close);
        }
    }
}
