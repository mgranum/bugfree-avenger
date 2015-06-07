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
        private Portfolio portfolio;
        private string ticker;
        private int today;
        private int yesterday;
        private double numberOfShares = 100.0;
        private double amountPerTrade = 2500.0;
        private int numberOfHoldingDaysPerTrade = 4;

        public Simulator(string input, Portfolio portf)
        {
            ticker = input;
            history = new List<Tick>();
            portfolio = portf;
        }

        public void Simulate()
        {
            ReadHistory();

            for (today = 0; today < history.Count; today++)
            {
                if (today <= 30)
                {
                    continue;
                }
                yesterday = today - 1;

                var signals = CheckBuySignals();
                if (signals.NumberOfSignals > 0)
                {
                    // Buy
                    switch (signals.BuyAt)
                    {
                        case BuyAt.Open:
                            numberOfShares = amountPerTrade / history[today].Open;
                            portfolio.BuyStock(ticker, numberOfShares, history[today].Open, history[today].Timestamp, signals);
                            break;
                        case BuyAt.SpecificPrice:
                            if (signals.BuyAtSpecificPrice > history[today].Low && signals.BuyAtSpecificPrice < history[today].High)
                            {
                                numberOfShares = amountPerTrade / signals.BuyAtSpecificPrice;
                                portfolio.BuyStock(ticker, numberOfShares, signals.BuyAtSpecificPrice, history[today].Timestamp, signals);
                            }
                            break;
                        default:
                            break;
                    }
                    
                }

                var holdings = portfolio.GetHoldings(ticker);
                if (holdings == null || holdings.Count == 0)
                {
                    continue;
                }
                for (int n = holdings.Count - 1; n >= 0; n--)
                {
                    var holding = holdings[n];

                    if (today == history.Count - 1)
                    {
                        // Last day in history - get out!
                        portfolio.SellStock(ticker, holding.TimeOfPurchase, history[today].Close);
                    }
                    else if (numberOfHoldingDaysPerTrade > 0 && history[today - numberOfHoldingDaysPerTrade].Timestamp == holding.TimeOfPurchase)
                    {
                        if (!portfolio.SellStockAtStopLoss(ticker, holding.TimeOfPurchase, history[today].Low, history[today].Timestamp))
                        {
                            portfolio.SellStock(ticker, holding.TimeOfPurchase, history[today].Close, history[today].Timestamp, "MD");
                        }
                    }
                    else
                    {
                        if (!portfolio.SellStockAtStopLoss(ticker, holding.TimeOfPurchase, history[today].Low, history[today].Timestamp))
                        {
                            portfolio.SellStockAtStopProfit(ticker, holding.TimeOfPurchase, history[today].Low, history[today].High, history[today].Timestamp);

                        }
                    }
                }

            }
        }

        private void ReadHistory()
        {
            var data = Helper.GetStockHistory(ticker);
            var tmpHistory = new List<Tick>();

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

                        tmpHistory.Insert(0, tick);
                    }
                }
            }
            foreach (var item in tmpHistory)
            {
                history.Insert(0, item);
                history.CalculateIndicators();
            }
            history = history.OrderBy(v => v.Timestamp).ToList();

        }

        private BuySignals CheckBuySignals()
        {
            //return IsUptrendLongTerm() && HasDipShortTerm() && IsStochastic();
            if (history[today].Timestamp == new DateTime(2011, 9, 7, 0, 0, 0))
            {
                var ugh = true;
            }
            var isGreatestSwingValue = IsGreatestSwingValue();
            if (isGreatestSwingValue.Item1 == true && isGreatestSwingValue.Item2 > 0.0)
            {
                return new BuySignals
                {
                    IsGreatestSwingValue = true,
                    BuyAt = BuyAt.SpecificPrice,
                    BuyAtSpecificPrice = isGreatestSwingValue.Item2
                };
            } 

            return new BuySignals
            {
                IsUptrendLongTerm_HasDipShortTerm_IsSmashDay = IsUptrendLongTerm() && HasDipShortTerm() && IsSmashDay(),
                IsMACDBreakthrough = IsMACDBreakthrough(),
                IsStochastic = IsStochastic(),
                IsOutsideDay = IsOutsideDay(),
                IsOoops_HasDipShortTerm = IsOoops() && HasDipShortTerm(),
                BuyAt = BuyAt.Open
            };
        }

        private bool IsUptrendLongTerm()
        {
            return (history[yesterday].Open > history[yesterday - 29].Close);
        }

        private bool HasDipShortTerm()
        {
            return (history[yesterday].Close < history[yesterday - 9].Close);
        }

        private bool IsSmashDay()
        {
            return (history[yesterday].Close < history[yesterday - 1].Low);
        }

        private bool IsMACDBreakthrough()
        {
            return (history[yesterday].MACD_Histogram > 0.0 && history[yesterday - 1].MACD_Histogram < 0.0 && history[yesterday - 2].MACD_Histogram < 0.0);
        }

        private bool IsStochastic()
        {
            // =AND(G202>H202;G201<H201;G200<H200;G202<80;H202<80)
            // G=%K, H=%D
            if (history[yesterday].StochasticK > history[yesterday].StochasticD)
            {
                if (history[yesterday - 1].StochasticK < history[yesterday - 1].StochasticD)
                {
                    if (history[yesterday - 2].StochasticK < history[yesterday - 2].StochasticD)
                    {
                        if (history[yesterday].StochasticK < 80 && history[yesterday].StochasticD < 80)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsOutsideDay()
        {
            // C=Open, D=High, E=Low, F=Close
            // =IF(AND(L493>L492>L491;D493>MAX(D490:D492);E493<MIN(E490:E492);F493<C493);"YES";"")
            var highestHigh = history.Skip(yesterday-3).Take(3).Max(v => v.High);
            var lowestLow = history.Skip(yesterday-3).Take(3).Min(v => v.Low);
            return (history[yesterday].High > highestHigh && history[yesterday].Low < lowestLow && history[yesterday].Close < history[yesterday].Open);
        }

        private bool IsOoops()
        {
            // C=Open, D=High, E=Low
            // =IF(AND(C2361<E2360;D2361>E2360;K2360<K2359);"YES";"")
            return (history[yesterday].Open < history[yesterday - 1].Low && history[today].High > history[yesterday - 1].Low);
        }

        private Tuple<bool, double> IsGreatestSwingValue()
        {
            // =AND(D18>(C18+O17);F17<F12)
            // C=Open, D=High, E=Low, F=Close, O=SwingValueAverage
            // BuyAt Open + SwingValueAverage
            bool buy = false;
            double price = 0.0;
            if (history[yesterday].Close < history[yesterday - 5].Close)
            {
                if (history[today].High > (history[today].Open + history[yesterday].SwingValueAverage))
                {
                    buy = true;
                    price = history[today].Open + history[yesterday].SwingValueAverage;
                }
            }
            return Tuple.Create(buy, price); 
        }
    }
}
