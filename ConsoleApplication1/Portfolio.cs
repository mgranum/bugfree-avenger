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

        public bool CurrentlyHoldingStock(string ticker)
        {
            return Holdings.ContainsKey(ticker);
        }

        public void AddHoldingForStock(string ticker, Holding holding)
        {
            Holdings.Add(ticker, holding);
        }
    }
}
