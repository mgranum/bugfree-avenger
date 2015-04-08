using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Stock
    {
        private List<Tick> list01;
        private List<Tick> list05;
        private List<Tick> list15;

        public string Ticker { get; set; }
        public List<Tick> List05 { get { return list05; } }
        public List<Tick> List15 { get { return list15; } }
        public List<Tick> List01
        {
            get { return list01; }
        }


        public Stock()
        {
            list01 = new List<Tick>();
            list05 = new List<Tick>();
            list15 = new List<Tick>();
        }

        public void AddTrade(DateTime timestamp, double price)
        {
            var dt01 = Helper.AdjustTime(timestamp, 1);
            AddOrUpdateTick(ref list01, dt01, price);

            var dt05 = Helper.AdjustTime(timestamp, 5);
            AddOrUpdateTick(ref list05, dt05, price);

            var dt15 = Helper.AdjustTime(timestamp, 15);
            AddOrUpdateTick(ref list15, dt15, price);
        }

        public void AddOrUpdateTick(ref List<Tick> list, DateTime dateTime, double price)
        {
            var existingTick = list.Find(x => x.Timestamp == dateTime);
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
                tick.Timestamp = dateTime;
                tick.Open = price;
                tick.Close = price;
                tick.High = price;
                tick.Low = price;

                list.Add(tick);
            }
        }
    }
}
