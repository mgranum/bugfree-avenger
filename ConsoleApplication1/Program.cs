using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");

            var portfolio = new Portfolio();

            var stock = new Stock() { Ticker = "STL.OSE" };
            var trades = Helper.GetTradesForDate(stock.Ticker, new DateTime(2015, 4, 7));
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
                    }
                }
            }

            Console.WriteLine("List01 now has {0} ticks", stock.List01.Count);
            Console.WriteLine("List05 now has {0} ticks", stock.List05.Count);
            Console.WriteLine("List15 now has {0} ticks", stock.List15.Count);
            Console.WriteLine("O: {0}\tC: {1}\tH: {2}\tL: {3}", stock.Open, stock.Close, stock.High, stock.Low);

            Console.ReadKey();
        }

    }
}
