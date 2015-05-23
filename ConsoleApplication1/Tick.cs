using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Tick
    {
        public DateTime Timestamp { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public int Trend {
            get
            {
                if (Close > Open)
                {
                    return 1;
                }
                else if (Open > Close)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public double FastEMA { get; set; }
        public double SlowEMA { get; set; }
        public double MACD { get; set; }
        public double StochasticK { get; set; }
        public double StochasticD { get; set; }
        public double SwingValueAverage { get; set; }
    }
}
