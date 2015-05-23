using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        private List<Tick> tickList;
 
        [TestInitialize]
        public void Setup()
        {
            tickList = new List<Tick>();
        }

        [TestMethod]
        public void CalculateIndicators_ListOfOneItem_CalculatesOnlySwingValue()
        {
            var tick = new Tick { Timestamp = DateTime.Now, Open = 1.0, High = 2.1, Low = 0.9, Close = 1.2 };
            tickList.Add(tick);
            tickList.CalculateIndicators();

            Assert.AreEqual(1.1, tickList[0].SwingValue);
            Assert.AreEqual(0.0, tickList[0].FastEMA);
            Assert.AreEqual(0.0, tickList[0].SlowEMA);
            Assert.AreEqual(0.0, tickList[0].MACD);
            Assert.AreEqual(0.0, tickList[0].StochasticK);
            Assert.AreEqual(0.0, tickList[0].StochasticD);
            Assert.AreEqual(0.0, tickList[0].SwingValueAverage);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfFourItems_CalculatesSwingAndAverageSwingValues()
        {
            tickList.Insert(0, new Tick { Open = 1.0, High = 2.1, Low = 0.8, Close = 1.2 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 1.0, High = 2.2, Low = 0.8, Close = 1.2 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 1.0, High = 2.3, Low = 0.8, Close = 1.2 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 1.0, High = 2.4, Low = 0.8, Close = 1.2 });
            tickList.CalculateIndicators();

            Assert.AreEqual(1.4, tickList[0].SwingValue);
            Assert.AreEqual(2.25, tickList[0].SwingValueAverage);
            Assert.AreEqual(0.0, tickList[0].FastEMA);
            Assert.AreEqual(0.0, tickList[0].SlowEMA);
            Assert.AreEqual(0.0, tickList[0].MACD);
            Assert.AreEqual(0.0, tickList[0].StochasticK);
            Assert.AreEqual(0.0, tickList[0].StochasticD);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfTwelveItems_CalculatesSwingValueAverage()
        {
            AddTwelveTicksAndCalculate();

            Assert.AreEqual(0.72, tickList[0].SwingValueAverage);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfTwelveItems_CalculatesStochasticK()
        {
            AddTwelveTicksAndCalculate();

            Assert.AreEqual(66.67, tickList[0].StochasticK);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfTwelveItems_CalculatesFastEMA()
        {
            AddTwelveTicksAndCalculate();

            Assert.AreEqual(44.1, tickList[0].FastEMA);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfTwentySixItems_CalculatesFastEMA()
        {
            AddTwentySixTicksAndCalculate();

            Assert.AreEqual(43.77, tickList[0].FastEMA);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfTwentySixItems_CalculatesStochasticD()
        {
            AddTwentySixTicksAndCalculate();

            Assert.AreEqual(9.87, tickList[0].StochasticD);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfThirtyItems_CalculatesStochasticD()
        {
            AddThirtyTicksAndCalculate();

            Assert.AreEqual(57.17, tickList[0].StochasticD);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfTwentySixItems_CalculatesSlowEMA()
        {
            AddTwentySixTicksAndCalculate();

            Assert.AreEqual(44.11, tickList[0].SlowEMA);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfThirtyItems_CalculatesSlowEMA()
        {
            AddThirtyTicksAndCalculate();

            Assert.AreEqual(44.12, tickList[0].SlowEMA);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfTwentySixItems_CalculatesMACD()
        {
            AddTwentySixTicksAndCalculate();

            Assert.AreEqual(-0.34, tickList[0].MACD);
        }

        [TestMethod]
        public void CalculateIndicators_ListOfThirtyItems_CalculatesMACD()
        {
            AddThirtyTicksAndCalculate();

            Assert.AreEqual(-0.14, tickList[0].MACD);
        }

        private void AddTwelveTicksAndCalculate()
        {
            tickList.Insert(0, new Tick { Open = 43.0, High = 43.2, Low = 42.6, Close = 42.9 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 42.6, High = 43.4, Low = 42.4, Close = 43.0 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.4, High = 44.6, Low = 43.1, Close = 44.5 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 44.6, High = 44.9, Low = 43.3, Close = 43.4 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.5, High = 44.1, Low = 43.4, Close = 43.4 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.7, High = 44.2, Low = 43.5, Close = 44.0 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 44.2, High = 45.7, Low = 44.2, Close = 45.2 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 45.1, High = 45.3, Low = 44.3, Close = 45.3 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 45.1, High = 45.2, Low = 43.8, Close = 43.8 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.5, High = 44.6, Low = 43.3, Close = 44.6 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 44.5, High = 44.9, Low = 43.9, Close = 44.5 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 44.8, High = 44.8, Low = 44.3, Close = 44.6 });
            tickList.CalculateIndicators();
        }

        private void AddTwentySixTicksAndCalculate()
        {
            AddTwelveTicksAndCalculate();

            tickList.Insert(0, new Tick { Open = 44.5, High = 44.5, Low = 43.8, Close = 44.0 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 44.0, High = 44.5, Low = 43.9, Close = 44.4 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 44.5, High = 44.9, Low = 44.4, Close = 44.4 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 44.9, High = 45.6, Low = 44.8, Close = 45.4 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 45.3, High = 45.4, Low = 44.5, Close = 45.4 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 45.2, High = 45.6, Low = 44.9, Close = 45.3 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 45.0, High = 45.2, Low = 44.9, Close = 45.0 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 44.9, High = 45.0, Low = 43.6, Close = 43.6 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.8, High = 44.2, Low = 43.2, Close = 44.0 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.6, High = 43.7, Low = 43.0, Close = 43.4 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.5, High = 43.8, Low = 43.2, Close = 43.3 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.4, High = 43.4, Low = 42.9, Close = 42.9 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.0, High = 43.6, Low = 42.9, Close = 43.3 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.7, High = 43.7, Low = 43.3, Close = 43.3 });
            tickList.CalculateIndicators();
        }

        private void AddThirtyTicksAndCalculate()
        {
            AddTwentySixTicksAndCalculate();

            tickList.Insert(0, new Tick { Open = 43.5, High = 43.7, Low = 43.3, Close = 43.4 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.4, High = 44.2, Low = 43.3, Close = 43.9 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 43.8, High = 44.7, Low = 43.8, Close = 44.3 });
            tickList.CalculateIndicators();
            tickList.Insert(0, new Tick { Open = 44.4, High = 44.8, Low = 44.1, Close = 44.8 });
            tickList.CalculateIndicators();
        }
    }
}
