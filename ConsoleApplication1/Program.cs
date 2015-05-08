using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");

            var portfolio = new Portfolio();

            //var stl = new Simulator("STL.OSE", portfolio);
            //stl.Simulate();
            var yar = new Simulator("YAR.OSE", portfolio);
            yar.Simulate();

            #region
            /*var startDate = new DateTime(2015, 4, 6);
            var endDate = new DateTime(2015, 4, 10);

            foreach (DateTime today in EachDay(startDate, endDate))
            {
                //var today = new DateTime(2015, 4, 8);
                var endOfTrade = today.AddHours(16).AddMinutes(20);
                var stock = new Stock() { Ticker = "STL.OSE", Wiggle = 0.49 };
                var trades = Helper.GetTradesForDate(stock.Ticker, today);
                var noMoreTradesToday = false;

                using (StringReader read = new StringReader(trades))
                {
                    string line;
                    while ((line = read.ReadLine()) != null)
                    {
                        var values = line.Split(',');

                        if (values[0] == "time")
                        {
                            // first row -> skip
                        }
                        else
                        {
                            var timestamp = DateTime.ParseExact(values[0], "yyyyMMddTHHmmss", CultureInfo.InvariantCulture).ToLocalTime();
                            var price = double.Parse(values[1]);

                            stock.AddTrade(timestamp, price);

                            if (!noMoreTradesToday)
                            {
                                if (portfolio.CurrentlyHoldingStock(stock.Ticker) && (stock.CheckSell() || timestamp > endOfTrade))
                                {
                                    // Sell
                                    portfolio.SellStock(stock.Ticker, price);
                                }
                                else if (!portfolio.CurrentlyHoldingStock(stock.Ticker) && stock.CheckBuy() && timestamp < endOfTrade)
                                {
                                    // Buy
                                    if (portfolio.BuyStock(stock.Ticker, 100, price))
                                    {
                                        stock.EntryPrice = price;
                                    }
                                }

                                noMoreTradesToday = timestamp > endOfTrade;
                            }
                        }
                    }
                }
                
                foreach (var trans in portfolio.CompletedTransactions)
                {
                    //Console.WriteLine("{0}: {1}\t{2}\t{3}\t{4}", string.Format("{0:ddMM}",today), trans.PurchasePrice, trans.TotalCost, trans.SellPrice, trans.Profit);
                }

                //Console.WriteLine("O: {0}\tC: {1}\tH: {2}\tL: {3}", stock.Open, stock.Close, stock.High, stock.Low);
            }
            */

            #endregion

            Console.WriteLine("===================");
            Console.WriteLine("Number of trades: {0}", portfolio.CompletedTransactions.Count);
            Console.WriteLine("Number of profitable trades: {0}", portfolio.CompletedTransactions.Count(d => d.Profit > 0.0));
            Console.WriteLine("Number of loosing trades: {0}", portfolio.CompletedTransactions.Count(d => d.Profit < 0.0));
            Console.WriteLine("Greatest win: {0}", portfolio.CompletedTransactions.Max(d => d.Profit));
            Console.WriteLine("Greatest loss: {0}", portfolio.CompletedTransactions.Min(d => d.Profit));
            Console.WriteLine("Total profit: {0}", portfolio.CompletedTransactions.Sum(d => d.Profit));
            Console.WriteLine("===================");

            foreach (var item in portfolio.CompletedTransactions)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", string.Format("{0:yyyyMMdd}", item.TimeOfPurchase), string.Format("{0:0.00}", item.PurchasePrice), string.Format("{0:yyyyMMdd}", item.TimeOfSell), string.Format("{0:0.00}", item.SellPrice), string.Format("{0:0.00}", item.Profit));
            }

            Console.ReadKey();
        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
