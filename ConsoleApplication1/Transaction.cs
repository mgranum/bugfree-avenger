using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Transaction
    {
        public string Ticker { get; set; }
        public DateTime TimeOfPurchase { get; set; }
        public double NumberOfShares { get; set; }
        public double PurchasePrice { get; set; }
        public double TotalCost { get; set; }
        public DateTime TimeOfSell { get; set; }
        public double SellPrice { get; set; }
        public double Profit { get; set; }
        public string BuySignal { get; set; }
        public string SellSignal { get; set; }
    }
}
