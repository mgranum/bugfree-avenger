using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Portfolio
    {
        public Dictionary<string, Holding> Holdings { get; set; }
        public List<Transaction> CompletedTransactions { get; set; }

        public Portfolio()
        {
            Holdings = new Dictionary<string, Holding>();
            CompletedTransactions = new List<Transaction>();
        }

        public bool CurrentlyHoldingStock(string ticker)
        {
            return Holdings.ContainsKey(ticker);
        }

        public bool BuyStock(string ticker, double numberOfShares, double price)
        {
            try
            {
                // Do the actual trade on the stock exchange

                var holding = new Holding() {
                    Ticker = ticker,
                    NumberOfShares = numberOfShares,
                    TimeOfPurchase = DateTime.Now,
                    Price = price,
                    TotalCost = (price * numberOfShares),
                    StopLoss = calculatePriceMinusPercentage(price, 2.5),
                    StopProfit = calculatePriceMinusPercentage(price, 2.5)
                };
                Holdings.Add(ticker, holding);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool BuyStock(string ticker, double numberOfShares, double price, DateTime simulationDate)
        {
            try
            {
                // Do the actual trade on the stock exchange

                var holding = new Holding()
                {
                    Ticker = ticker,
                    NumberOfShares = numberOfShares,
                    TimeOfPurchase = simulationDate,
                    Price = price,
                    TotalCost = (price * numberOfShares),
                    StopLoss = calculatePriceMinusPercentage(price, 2.5),
                    StopProfit = calculatePriceMinusPercentage(price, 2.5)
                };
                Holdings.Add(ticker, holding);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SellStock(string ticker, double price)
        {
            var holding = Holdings[ticker];
            var transaction = new Transaction()
            {
                Ticker = holding.Ticker,
                NumberOfShares = holding.NumberOfShares,
                PurchasePrice = holding.Price,
                TimeOfPurchase = holding.TimeOfPurchase,
                TotalCost = holding.TotalCost,
                TimeOfSell = DateTime.Now,
                SellPrice = price,
                Profit = (holding.NumberOfShares * price) - holding.TotalCost
            };
            CompletedTransactions.Add(transaction);
            Holdings.Remove(ticker);
        }

        public void SellStock(string ticker, double price, DateTime simulationDate)
        {
            var holding = Holdings[ticker];
            var transaction = new Transaction()
            {
                Ticker = holding.Ticker,
                NumberOfShares = holding.NumberOfShares,
                PurchasePrice = holding.Price,
                TimeOfPurchase = holding.TimeOfPurchase,
                TotalCost = holding.TotalCost,
                TimeOfSell = simulationDate,
                SellPrice = price,
                Profit = (holding.NumberOfShares * price) - holding.TotalCost
            };
            CompletedTransactions.Add(transaction);
            Holdings.Remove(ticker);
        }


        public bool SellStockAtStopLoss(string ticker, double currentPrice, DateTime simulationDate)
        {
            var holding = Holdings[ticker];

            if (currentPrice < holding.StopLoss)
            {
                SellStock(ticker, holding.StopLoss, simulationDate);
                return true;
            }
            return false;
        }

        public bool SellStockAtStopProfit(string ticker, double currentPrice, DateTime simulationDate)
        {
            var holding = Holdings[ticker];

            if (currentPrice <= holding.StopProfit)
            {
                SellStock(ticker, currentPrice, simulationDate);
                return true;
            }

            UpdateStopProfit(ticker, currentPrice);
            return false;
        }

        public void UpdateStopProfit(string ticker, double currentHigh)
        {
            var holding = Holdings[ticker];

            // Beregn ny StopProfit
            var newStopProfit = calculatePriceMinusPercentage(currentHigh, 2.5);
            if (newStopProfit > holding.StopProfit)
            {
                holding.StopProfit = newStopProfit;
            }
        }

        private double calculatePriceMinusPercentage(double price, double percentage)
        {
            return price - (price * percentage / 100);
        }
    }
}
