﻿using System;

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
    }
}
