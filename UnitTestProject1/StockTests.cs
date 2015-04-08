using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class StockTests
    {
        private Stock stock;
        private DateTime timestamp;

        [TestInitialize]
        public void Setup()
        {
            stock = new Stock();
            timestamp = new DateTime(1999, 12, 31, 23, 59, 59);
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
        }
    }
}
