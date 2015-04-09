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

        public void AddHoldingForStock(string ticker, Holding holding)
        {
            Holdings.Add(ticker, holding);
        }

        public void SellHoldingForStock(string ticker, double price)
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
    }
}
