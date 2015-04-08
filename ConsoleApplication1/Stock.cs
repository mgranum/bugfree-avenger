using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Stock
    {
        public string Ticker { get; set; }
        public List<Tick> List01 { get; }
        public List<Tick> List05 { get; }
        public List<Tick> List15 { get; }

        public Stock()
        {
            List01 = new List<Tick>();
            List05 = new List<Tick>();
            List15 = new List<Tick>();
        }

        public bool AddTrade(DateTime timestamp, double price)
        {
            throw new NotImplementedException();
        }
    }
}
