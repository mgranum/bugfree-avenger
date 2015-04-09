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

            var stock = new Stock() { Ticker = "STL.OSE", Wiggle = 0.49 };
            var trades = Helper.GetTradesForDate(stock.Ticker, new DateTime(2015, 4, 9));
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

                        if (portfolio.CurrentlyHoldingStock(stock.Ticker) && stock.CheckSell())
                        {
                            // Sell
                            portfolio.SellHoldingForStock(stock.Ticker, price);
                        }
                        else if (!portfolio.CurrentlyHoldingStock(stock.Ticker) && stock.CheckBuy())
                        {
                            // Buy
                            var holding = new Holding() { Ticker = stock.Ticker, NumberOfShares = 100, TimeOfPurchase = DateTime.Now, Price = price, TotalCost = (price * 100) };
                            portfolio.AddHoldingForStock(stock.Ticker, holding);
                        }
                    }
                }
            }

            foreach (var trans in portfolio.CompletedTransactions)
            {
                Console.WriteLine("{0}: {1}\t{2}\t{3}\t{4}", trans.TimeOfPurchase, trans.PurchasePrice, trans.TotalCost, trans.SellPrice, trans.Profit);
            }

            Console.WriteLine("===================");
            Console.WriteLine("Total profit: {0}", portfolio.CompletedTransactions.Sum(d => d.Profit));
            Console.WriteLine("===================");


            Console.WriteLine("List01 now has {0} ticks", stock.List01.Count);
            Console.WriteLine("List05 now has {0} ticks", stock.List05.Count);
            Console.WriteLine("List15 now has {0} ticks", stock.List15.Count);
            Console.WriteLine("O: {0}\tC: {1}\tH: {2}\tL: {3}", stock.Open, stock.Close, stock.High, stock.Low);

            Console.ReadKey();
        }

    }
}
