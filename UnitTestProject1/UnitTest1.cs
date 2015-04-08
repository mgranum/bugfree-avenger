using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private DateTime timestamp;

        [TestInitialize]
        public void Setup()
        {
            timestamp = new DateTime(1999, 12, 31, 23, 59, 59);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var tick = new Tick() { Timestamp = timestamp, Open = 1.0, Close = 2.0, High = 3.0, Low = 0.5 };

            Assert.AreEqual(2.0, tick.Close);
        }

        [TestMethod]
        public void CreateTick_WhenIntervalIsZero_TimeRemainsUnchanged()
        {
            var tick = Helper.CreateTick(timestamp, 1.0, 0);
            Assert.AreEqual(timestamp, tick.Timestamp);
        }

        [TestMethod]
        public void CreateTick_WhenIntervalIsOne_TimeIsRoundedDownToNearestMinute()
        {
            var tick = Helper.CreateTick(timestamp, 1.0, 1);
            var expected = new DateTime(1999, 12, 31, 23, 59, 0);
            Assert.AreEqual(expected, tick.Timestamp);
        }

        [TestMethod]
        public void CreateTick_WhenIntervalIsFive_TimeIsRoundedDownToNearestFive()
        {
            var tick = Helper.CreateTick(timestamp, 1.0, 5);
            var expected = new DateTime(1999, 12, 31, 23, 55, 0);
            Assert.AreEqual(expected, tick.Timestamp);
        }

        [TestMethod]
        public void CreateTick_WhenIntervalIsFifteen_TimeIsRoundedDownToNearestQuarter()
        {
            var tick = Helper.CreateTick(timestamp, 1.0, 15);
            var expected = new DateTime(1999, 12, 31, 23, 45, 0);
            Assert.AreEqual(expected, tick.Timestamp);
        }

        [TestMethod]
        public void CreateTick_WhenIntervalIsThirty_TimeIsRoundedDownToNearestHalfHour()
        {
            var tick = Helper.CreateTick(timestamp, 1.0, 30);
            var expected = new DateTime(1999, 12, 31, 23, 30, 0);
            Assert.AreEqual(expected, tick.Timestamp);
        }

        [TestMethod]
        public void CreateTick_WhenIntervalIsSixty_TimeIsRoundedDownToNearestWholeHour()
        {
            var tick = Helper.CreateTick(timestamp, 1.0, 60);
            var expected = new DateTime(1999, 12, 31, 23, 0, 0);
            Assert.AreEqual(expected, tick.Timestamp);
        }
    }
}
