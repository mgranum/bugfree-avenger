using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Simulator
    {
        private List<Tick> history;
        private List<Transaction> transactions;
        private Portfolio portfolio;
        private string ticker;
        private int historyItem;
        private bool currentlyHolding;
        private double numberOfShares = 100.0;
        private int numberOfHoldingDaysPerTrade = -5;
        private int sellAtHistoryItem = 0;

        public Simulator(string input, Portfolio portf)
        {
            ticker = input;
            history = new List<Tick>();
            portfolio = portf;
        }

        public void Simulate()
        {
            ReadHistory();

            for(var i=0; i < history.Count; i++)
            {
                if (i <= 30)
                {
                    continue;
                }
                historyItem = i - 1;
                if (!portfolio.CurrentlyHoldingStock(ticker))
                {
                    if (CheckBuySignals())
                    {
                        // Buy
                        portfolio.BuyStock(ticker, numberOfShares, history[i].Open, history[i].Timestamp);
                        if (numberOfHoldingDaysPerTrade > 0)
                        {
                            sellAtHistoryItem = i + numberOfHoldingDaysPerTrade;
                        }
                    }
                }

                if (portfolio.CurrentlyHoldingStock(ticker))
                {
                    if (i == history.Count -1)
                    {
                        // Last day in history - get out!
                        portfolio.SellStock(ticker, history[i].Close); 
                    }
                    else if (i == sellAtHistoryItem)
                    {
                        if (!portfolio.SellStockAtStopLoss(ticker, history[i].Low, history[i].Timestamp))
                        {
                            portfolio.SellStock(ticker, history[i].Close, history[i].Timestamp);
                        }
                        sellAtHistoryItem = 0;
                    }
                    else
                    {
                        if (portfolio.SellStockAtStopLoss(ticker, history[i].Low, history[i].Timestamp))
                        {
                            sellAtHistoryItem = 0;
                        }
                        else if (portfolio.SellStockAtStopProfit(ticker, history[i].Low, history[i].Timestamp))
                        {
                            sellAtHistoryItem = 0;
                        }
                    }
                }
            }
        }

        private void ReadHistory()
        {
            var data = Helper.GetStockHistory(ticker);

            using (StringReader read = new StringReader(data))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    var values = line.Split(',');

                    if (values[0] == "quote_date")
                    {
                        // first row -> skip
                    }
                    else
                    {
                        var timestamp = DateTime.ParseExact(values[0], "yyyyMMdd", CultureInfo.InvariantCulture).ToLocalTime();
                        var open = double.Parse(values[3]);
                        var high = double.Parse(values[4]);
                        var low = double.Parse(values[5]);
                        var close = double.Parse(values[6]);
                        var tick = new Tick() { Timestamp = timestamp, Open = open, High = high, Low = low, Close = close };

                        history.Insert(0, tick);
                    }
                }
            }
        }

        private bool CheckBuySignals()
        {
            return IsUptrendLongTerm() && HasDipShortTerm() && IsSmashDay();
        }

        private bool IsUptrendLongTerm()
        {
            return (history[historyItem].Open > history[historyItem - 30].Close);
        }

        private bool HasDipShortTerm()
        {
            return (history[historyItem].Close < history[historyItem - 9].Close);
        }

        private bool IsSmashDay()
        {
            return (history[historyItem].Close < history[historyItem - 1].Low);
        }

        private bool IsMacdBreakthrough()
        {
            //var fastEma = 
            return false;
        }
    }
}
