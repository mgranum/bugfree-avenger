using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class StockTests
    {
        private Stock stock;
        private DateTime timestamp, other_timestamp;
        private DateTime dt01, dt05, dt15;

        [TestInitialize]
        public void Setup()
        {
            stock = new Stock();
            timestamp = new DateTime(1999, 12, 31, 23, 59, 59);
            other_timestamp = timestamp.AddSeconds(2);
            dt01 = new DateTime(1999, 12, 31, 23, 59, 0);
            dt05 = new DateTime(1999, 12, 31, 23, 55, 0);
            dt15 = new DateTime(1999, 12, 31, 23, 45, 0);
        }

        [TestMethod]
        public void Constructor_ListsAreNotNull()
        {
            Assert.IsNotNull(stock.List01);
            Assert.IsNotNull(stock.List05);
            Assert.IsNotNull(stock.List15);
        }

        [TestMethod]
        public void AddTrade_WhenAddingOneTrade_ListsContainOneItem()
        {
            stock.AddTrade(timestamp, 1.0);
            Assert.AreEqual(1, stock.List01.Count);
            Assert.AreEqual(1, stock.List05.Count);
            Assert.AreEqual(1, stock.List15.Count);
        }

        [TestMethod]
        public void AddTrade_WhenAddingOneTrade_TimesAreAdjustedInLists()
        {
            stock.AddTrade(timestamp, 1.0);
            Assert.AreEqual(dt01, stock.List01[0].Timestamp);
            Assert.AreEqual(dt05, stock.List05[0].Timestamp);
            Assert.AreEqual(dt15, stock.List15[0].Timestamp);
        }

        [TestMethod]
        public void AddTrade_WhenAddingOneTrade_AllAmountsAreIdenticalInLists()
        {
            stock.AddTrade(timestamp, 1.23);
            Assert.AreEqual(1.23, stock.List01[0].Open);
            Assert.AreEqual(1.23, stock.List01[0].Close);
            Assert.AreEqual(1.23, stock.List01[0].High);
            Assert.AreEqual(1.23, stock.List01[0].Low);

            Assert.AreEqual(1.23, stock.List05[0].Open);
            Assert.AreEqual(1.23, stock.List05[0].Close);
            Assert.AreEqual(1.23, stock.List05[0].High);
            Assert.AreEqual(1.23, stock.List05[0].Low);

            Assert.AreEqual(1.23, stock.List15[0].Open);
            Assert.AreEqual(1.23, stock.List15[0].Close);
            Assert.AreEqual(1.23, stock.List15[0].High);
            Assert.AreEqual(1.23, stock.List15[0].Low);
        }

        [TestMethod]
        public void AddTrade_WhenAddingTwoTradesWithSameTime_ListJustContainsOneItem()
        {
            stock.AddTrade(timestamp, 1.0);
            stock.AddTrade(timestamp, 1.0);
            Assert.AreEqual(1, stock.List01.Count);
            Assert.AreEqual(1, stock.List05.Count);
            Assert.AreEqual(1, stock.List15.Count);
        }

        [TestMethod]
        public void AddTrade_WhenAddingTwoTradesWithSameTime_ListContainsOneItemWithCorrectValues()
        {
            stock.AddTrade(timestamp, 2.34);
            stock.AddTrade(timestamp, 1.23);
            Assert.AreEqual(2.34, stock.List01[0].High);
            Assert.AreEqual(1.23, stock.List01[0].Low);
            Assert.AreEqual(2.34, stock.List01[0].Open);
            Assert.AreEqual(1.23, stock.List01[0].Close);

            Assert.AreEqual(2.34, stock.List05[0].High);
            Assert.AreEqual(1.23, stock.List05[0].Low);
            Assert.AreEqual(2.34, stock.List05[0].Open);
            Assert.AreEqual(1.23, stock.List05[0].Close);

            Assert.AreEqual(2.34, stock.List15[0].High);
            Assert.AreEqual(1.23, stock.List15[0].Low);
            Assert.AreEqual(2.34, stock.List15[0].Open);
            Assert.AreEqual(1.23, stock.List15[0].Close);

        }

        [TestMethod]
        public void GetHigh_WhenAddedTwoTrades_ReturnsCorrectPrice()
        {
            stock.AddTrade(timestamp, 2.34);
            stock.AddTrade(other_timestamp, 1.23);
            Assert.AreEqual(2.34, stock.High);
        }

        [TestMethod]
        public void GetLow_WhenAddedTwoTrades_ReturnsCorrectPrice()
        {
            stock.AddTrade(timestamp, 2.34);
            stock.AddTrade(other_timestamp, 1.23);
            Assert.AreEqual(1.23, stock.Low);
        }

        [TestMethod]
        public void GetOpen_WhenAddedTwoTrades_ReturnsCorrectPrice()
        {
            stock.AddTrade(timestamp, 2.34);
            stock.AddTrade(other_timestamp, 1.23);
            Assert.AreEqual(2.34, stock.Open);
        }

        [TestMethod]
        public void GetClose_WhenAddedTwoTrades_ReturnsCorrectPrice()
        {
            stock.AddTrade(timestamp, 2.34);
            stock.AddTrade(other_timestamp, 1.23);
            Assert.AreEqual(1.23, stock.Close);
        }

        [TestCleanup]
        public void Teardown()
        {

        }
    }
}
