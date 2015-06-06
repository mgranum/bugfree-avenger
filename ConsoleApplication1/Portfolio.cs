using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Portfolio
    {
        public Dictionary<string, List<Holding>> Holdings { get; set; }
        public List<Transaction> CompletedTransactions { get; set; }

        public Portfolio()
        {
            Holdings = new Dictionary<string, List<Holding>>();
            CompletedTransactions = new List<Transaction>();
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

                AddHolding(ticker, holding);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AddHolding(string ticker, Holding holding)
        {
            if (Holdings.ContainsKey(ticker))
            {
                Holdings.FirstOrDefault(v => v.Key == ticker).Value.Add(holding);
            }
            else
            {
                var holdingsList = new List<Holding>();
                holdingsList.Add(holding);
                Holdings.Add(ticker, holdingsList);
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
                    StopProfit = 0.0
                };
                AddHolding(ticker, holding);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SellStock(string ticker, DateTime timeOfPurchase, double price)
        {
            var holding = GetHolding(ticker, timeOfPurchase);
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
            RemoveHolding(ticker, holding.TimeOfPurchase);
        }

        public void SellStock(string ticker, DateTime timeOfPurchase, double price, DateTime simulationDate, string sellSignal)
        {
            var holding = GetHolding(ticker, timeOfPurchase);
            var transaction = new Transaction()
            {
                Ticker = holding.Ticker,
                NumberOfShares = holding.NumberOfShares,
                PurchasePrice = holding.Price,
                TimeOfPurchase = holding.TimeOfPurchase,
                TotalCost = holding.TotalCost,
                TimeOfSell = simulationDate,
                SellPrice = price,
                SellSignal = sellSignal,
                Profit = (holding.NumberOfShares * price) - holding.TotalCost
            };
            CompletedTransactions.Add(transaction);
            RemoveHolding(ticker, holding.TimeOfPurchase);
        }


        public bool SellStockAtStopLoss(string ticker, DateTime timeOfPurchase, double currentPrice, DateTime simulationDate)
        {
            var holding = GetHolding(ticker, timeOfPurchase);

            if (currentPrice < holding.StopLoss)
            {
                SellStock(ticker, timeOfPurchase, holding.StopLoss, simulationDate, "SL");
                return true;
            }
            return false;
        }

        public bool SellStockAtStopProfit(string ticker, DateTime timeOfPurchase, double currentPrice, DateTime simulationDate)
        {
            var holding = GetHolding(ticker, timeOfPurchase);

            if (currentPrice <= holding.StopProfit)
            {
                SellStock(ticker, timeOfPurchase, currentPrice, simulationDate, "SP");
                return true;
            }

            UpdateStopProfit(ticker, timeOfPurchase, currentPrice);
            return false;
        }

        public void UpdateStopProfit(string ticker, DateTime timeOfPurchase, double currentHigh)
        {
            var holding = GetHolding(ticker, timeOfPurchase);

            if (holding.StopProfit > 0.0 && currentHigh > calculatePricePlusPercentage(holding.Price, 2.5))
            {
                // Beregn ny StopProfit
                var newStopProfit = calculatePriceMinusPercentage(currentHigh, 2.5);
                if (newStopProfit > holding.StopProfit)
                {
                    holding.StopProfit = newStopProfit;
                }
            }
        }

        private double calculatePriceMinusPercentage(double price, double percentage)
        {
            return price - (price * percentage / 100);
        }

        private double calculatePricePlusPercentage(double price, double percentage)
        {
            return price + (price * percentage / 100);
        }


        public List<Holding> GetHoldings(string ticker)
        {
            if (Holdings.ContainsKey(ticker))
            {
                if (Holdings.First(v => v.Key == ticker).Value.Any())
                {
                    return Holdings.First(v => v.Key == ticker).Value;
                }
            }
            return null;
        }

        private Holding GetHolding(string ticker, DateTime timeOfPurchase)
        {
            var holdingsList = GetHoldings(ticker);
            if (holdingsList.Any())
            {
                return holdingsList.First(v => v.TimeOfPurchase == timeOfPurchase);
            }
            return null;
        }

        private void RemoveHolding(string ticker, DateTime timeOfPurchase)
        {
            var holdingsList = GetHoldings(ticker);
            if (holdingsList.Any())
            {
                holdingsList.RemoveAll(v => v.TimeOfPurchase == timeOfPurchase);
            }
        }
    }
}
