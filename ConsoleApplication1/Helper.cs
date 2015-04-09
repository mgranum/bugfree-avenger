using System;
using System.Net;

namespace ConsoleApplication1
{
    public static class Helper
    {
        public static DateTime AdjustTime(DateTime dateTime, int timeInterval)
        {
            if (timeInterval == 1)
            {
                dateTime = dateTime.AddSeconds(-(dateTime.Second));
            }
            else if (timeInterval > 1)
            {
                dateTime = dateTime.AddMinutes(-(dateTime.Minute % timeInterval)).AddSeconds(-(dateTime.Second));
            }

            return dateTime;
            //dt = dt.AddMinutes(-(dt.Minute % 15)).AddSeconds(-dt.Second);
            //dateTime = dateTime.AddMinutes(-(dateTime))
            //throw new NotImplementedException();
        }

        public static string GetTradesForDate(string stock, DateTime date)
        {
            var datestring = string.Format("{0: yyyyMMdd}", date);
            var url = string.Format("http://www.netfonds.no/quotes/tradedump.php?date={0}&paper={1}&csv_format=csv", datestring, stock);
            return ReadDataFromUri(url);
        }

        public static string GetStockHistory(string stock)
        {
            var url = string.Format("http://www.netfonds.no/quotes/paperhistory.php?paper={0}&csv_format=csv", stock);
            return ReadDataFromUri(url);
        }

        public static string ReadDataFromUri(string url)
        {
            string result = string.Empty;
            using (WebClient client = new WebClient())
            {
                result = client.DownloadString(url);
            }
            return result;
        }
    }
}
