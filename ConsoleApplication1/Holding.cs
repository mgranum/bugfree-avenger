using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Holding
    {
        public string Ticker { get; set; }
        public DateTime TimeOfPurchase { get; set; }
        public double NumberOfShares { get; set; }
        public double Price { get; set; }
        public double TotalCost { get; set; }
    }
}
