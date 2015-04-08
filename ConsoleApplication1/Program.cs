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
            var reader = new StreamReader(File.OpenRead(@"C:\Users\mgr\documents\visual studio 2015\Projects\ConsoleApplication1\ConsoleApplication1\stl-20150407.csv"));
            List<Tick> list01 = new List<Tick>();
            List<Tick> list05 = new List<Tick>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                DateTime timestamp;
                timestamp = DateTime.ParseExact(values[0], "yyyyMMddTHHmmss", System.Globalization.CultureInfo.InvariantCulture).ToLocalTime();

                var price = Double.Parse(values[1]);
                var datetime01 = new DateTime(timestamp.Year, timestamp.Month, timestamp.Day, timestamp.Hour, timestamp.Minute, 0);

                // Check previous
                var existingTick = list01.Find(x => x.Timestamp == datetime01);
                if (existingTick != null)
                {
                    if (price < existingTick.Low)
                    {
                        existingTick.Low = price;
                    }
                    if (price > existingTick.High)
                    {
                        existingTick.High = price;
                    }
                    existingTick.Close = price;
                }
                else
                {
                    var tick = new Tick();
                    tick.Timestamp = datetime01;
                    tick.Open = price;
                    tick.Close = price;
                    tick.High = price;
                    tick.Low = price;

                    list01.Add(tick);
                }
            }

            Console.WriteLine("List now has {0} ticks", list01.Count);

            Console.ReadKey();
        }

    }
}
