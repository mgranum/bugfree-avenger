using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class TickTests
    {
        private DateTime timestamp;

        [TestInitialize]
        public void Setup()
        {
            timestamp = new DateTime(1999, 12, 31, 23, 59, 59);
        }

        [TestMethod]
        public void CreateTick_ConstructorWithValues_OpenCloseHighLowReturnsCorrectValue()
        {
            var tick = new Tick() { Timestamp = timestamp, Open = 1.0, Close = 2.0, High = 3.0, Low = 0.5 };

            Assert.AreEqual(1.0, tick.Open);
            Assert.AreEqual(2.0, tick.Close);
            Assert.AreEqual(3.0, tick.High);
            Assert.AreEqual(0.5, tick.Low);
        }

        [TestMethod]
        public void CreateTick_WhenOpenIsGreaterThanClose_TrendIsNegative()
        {
            var tick = new Tick() { Timestamp = timestamp, Open = 2.5, Close = 2.0, High = 3.0, Low = 0.5 };

            Assert.AreEqual(-1, tick.Trend);
        }

        [TestMethod]
        public void CreateTick_WhenCloseIsGreaterThanOpen_TrendIsPositive()
        {
            var tick = new Tick() { Timestamp = timestamp, Open = 1.0, Close = 2.0, High = 3.0, Low = 0.5 };

            Assert.AreEqual(1, tick.Trend);
        }

        [TestMethod]
        public void CreateTick_WhenOpenAndCloseAreEqual_TrendIsNeutral()
        {
            var tick = new Tick() { Timestamp = timestamp, Open = 1.0, Close = 1.0, High = 3.0, Low = 0.5 };

            Assert.AreEqual(0, tick.Trend);
        }
    }
}
