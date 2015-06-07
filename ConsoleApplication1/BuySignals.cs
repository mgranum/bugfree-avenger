using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class BuySignals
    {
        public bool IsUptrendLongTerm_HasDipShortTerm_IsSmashDay { get; set; }
        public bool IsMACDBreakthrough { get; set; }
        public bool IsStochastic { get; set; }
        public bool IsOutsideDay { get; set; }
        public bool IsOoops_HasDipShortTerm { get; set; }
        public bool IsGreatestSwingValue { get; set; }
        public int NumberOfSignals
        {
            get
            {
                return (IsUptrendLongTerm_HasDipShortTerm_IsSmashDay ? 1 : 0)
                    + (IsMACDBreakthrough ? 1 : 0)
                    + (IsStochastic ? 1 : 0)
                    + (IsOutsideDay ? 1 : 0)
                    + (IsOoops_HasDipShortTerm ? 1 : 0);
            }
        }
        public BuyAt BuyAt { get; set; }
        public double BuyAtSpecificPrice { get; set; }
    }

    public enum BuyAt
    {
        Open,
        SpecificPrice
    }

    public enum SellSignals
    {
        StopLoss,
        StopProfit,
        MaxDays,
        EndOfSimulation
    }
}
