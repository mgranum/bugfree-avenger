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
            var reader = new StreamReader(File.OpenRead(@"C:\Users\mgr\Source\Repos\bugfree-avenger\ConsoleApplication1\stl-20150407.csv"));

            var stock = new Stock() { Ticker = "STL" };

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var timestamp = DateTime.ParseExact(values[0], "yyyyMMddTHHmmss", System.Globalization.CultureInfo.InvariantCulture).ToLocalTime();
                var price = Double.Parse(values[1]);

                stock.AddTrade(timestamp, price);
            }

            Console.WriteLine("List01 now has {0} ticks", stock.List01.Count);
            Console.WriteLine("List05 now has {0} ticks", stock.List05.Count);
            Console.WriteLine("List15 now has {0} ticks", stock.List15.Count);
            Console.WriteLine("O: {0}\tC: {1}\tH: {2}\tL: {3}", stock.Open, stock.Close, stock.High, stock.Low);

            Console.ReadKey();
        }

    }
}
