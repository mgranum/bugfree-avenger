using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class PortfolioTests
    {
        private Portfolio portfolio;

        [TestInitialize]
        public void Setup()
        {
            portfolio = new Portfolio();
        }

        [TestMethod]
        public void CurrentlyHoldingStock_WhenStockIsNotInPortfolio_ReturnsFalse()
        {
            Assert.IsFalse(portfolio.CurrentlyHoldingStock("DUMMY"));
        }

        [TestMethod]
        public void CurrentlyHoldingStock_WhenStockIsInPortfolio_ReturnsTrue()
        {
            portfolio.BuyStock("TEST", 1.0, 1.0);

            Assert.IsTrue(portfolio.CurrentlyHoldingStock("TEST"));
        }
    }
}
