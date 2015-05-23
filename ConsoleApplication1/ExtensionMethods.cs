using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class ExtensionMethods
    {
        public static List<Tick> CalculateIndicators(this List<Tick> values)
        {
            if (values != null && values.Count > 0)
            {
                values.CalculateSwingValue();
                values.CalculateSwingValueAverage();
                values.CalculateStochasticK();
                values.CalculateStochasticD();
                values.CalculateFastEMA();
                values.CalculateSlowEMA();
                values.CalculateMACD();
            }
            return values;
        }

        private static List<Tick> CalculateSwingValue(this List<Tick> values)
        {
            if (values != null && values.Count > 0)
            {
                values[0].SwingValue = Math.Round(values[0].High - values[0].Open, 2);
            }

            return values;
        }

        private static List<Tick> CalculateSwingValueAverage(this List<Tick> values)
        {
            if (values != null && values.Count > 3)
            {
                var avg = values.Take(4).Average(v => v.SwingValue) * 1.8;
                values[0].SwingValueAverage = Math.Round(avg, 2);
            }
            return values;
        }

        private static List<Tick> CalculateStochasticK(this List<Tick> values)
        {
            if (values != null && values.Count > 11)
            {
                //=((F14-MIN(E3:E14))/(MAX(D3:D14)-MIN(E3:E14)))*100
                /*
                 * C=Open;D=High;E=Low;F=Close
                */
                var minimumLow = values.Take(12).Min(v => v.Low);
                var maximumHigh = values.Take(12).Max(v => v.High);
                var lastClose = values.First().Close;

                values[0].StochasticK = Math.Round((((lastClose - minimumLow) / (maximumHigh - minimumLow)) * 100.0), 2);

            }
            return values;
        }

        private static List<Tick> CalculateStochasticD(this List<Tick> values)
        {
            if (values != null && values.Count > 14)
            {
                var avg = values.Take(3).Average(v => v.StochasticK);
                values[0].StochasticD = Math.Round(avg, 2);
            }
            return values;
        }

        private static List<Tick> CalculateFastEMA(this List<Tick> values)
        {
            // =F15*(2/($K$2+1))+K14*(1-(2/($K$2+1)))
            if (values != null && values.Count > 11)
            {
                if (values.Count == 12)
                {
                    var avg = values.Average(v => v.Close);
                    values[0].FastEMA = Math.Round(avg, 2);
                }
                else
                {
                    var lastClose = values.First().Close;
                    var previousFastEMA = values[1].FastEMA;
                    var ema = lastClose * (2.0 / (12.0 + 1.0)) + previousFastEMA * (1.0 - (2.0 / (12.0 + 1.0)));

                    values[0].FastEMA = Math.Round(ema, 2);
                }
            }
            return values;
        }

        private static List<Tick> CalculateSlowEMA(this List<Tick> values)
        {
            // =F29*(2/($L$2+1))+L28*(1-(2/($L$2+1)))
            if (values != null && values.Count > 25)
            {
                if (values.Count == 26)
                {
                    var avg = values.Average(v => v.Close);
                    values[0].SlowEMA = Math.Round(avg, 2);
                }
                else
                {
                    var lastClose = values.First().Close;
                    var previousSlowEMA = values[1].SlowEMA;
                    var ema = lastClose * (2.0 / (26.0 + 1.0)) + previousSlowEMA * (1.0 - (2.0 / (26.0 + 1.0)));

                    values[0].SlowEMA = Math.Round(ema, 2);
                }
            }
            return values;
        }

        private static List<Tick> CalculateMACD(this List<Tick> values)
        {
            if (values != null && values.Count > 25)
            {
                values[0].MACD = Math.Round(values[0].FastEMA - values[0].SlowEMA, 2);
            }
            return values;
        }
    }
}
