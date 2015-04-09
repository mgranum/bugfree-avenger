using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class HelperTests
    {
        private DateTime timestamp;

        [TestInitialize]
        public void Setup()
        {
            timestamp = new DateTime(1999, 12, 31, 23, 59, 59);
        }

        [TestMethod]
        public void AdjustTime_WhenIntervalIsZero_TimeRemainsUnchanged()
        {
            var time = Helper.AdjustTime(timestamp, 0);
            Assert.AreEqual(timestamp, time);
        }

        [TestMethod]
        public void AdjustTime_WhenIntervalIsOne_TimeIsRoundedDownToNearestMinute()
        {
            var time = Helper.AdjustTime(timestamp, 1);
            var expected = new DateTime(1999, 12, 31, 23, 59, 0);
            Assert.AreEqual(expected, time);
        }

        [TestMethod]
        public void AdjustTime_WhenIntervalIsFive_TimeIsRoundedDownToNearestFive()
        {
            var time = Helper.AdjustTime(timestamp, 5);
            var expected = new DateTime(1999, 12, 31, 23, 55, 0);
            Assert.AreEqual(expected, time);
        }

        [TestMethod]
        public void AdjustTime_WhenIntervalIsFifteen_TimeIsRoundedDownToNearestQuarter()
        {
            var time = Helper.AdjustTime(timestamp, 15);
            var expected = new DateTime(1999, 12, 31, 23, 45, 0);
            Assert.AreEqual(expected, time);
        }

        [TestMethod]
        public void AdjustTime_WhenIntervalIsThirty_TimeIsRoundedDownToNearestHalfHour()
        {
            var time = Helper.AdjustTime(timestamp, 30);
            var expected = new DateTime(1999, 12, 31, 23, 30, 0);
            Assert.AreEqual(expected, time);
        }

        [TestMethod]
        public void AdjustTime_WhenIntervalIsSixty_TimeIsRoundedDownToNearestWholeHour()
        {
            var time = Helper.AdjustTime(timestamp, 60);
            var expected = new DateTime(1999, 12, 31, 23, 0, 0);
            Assert.AreEqual(expected, time);
        }
    }
}
