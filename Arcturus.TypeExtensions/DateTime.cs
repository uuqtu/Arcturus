using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcturus.TypeExtensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDate(this DateTime i)
        {
            return new DateTime(i.Year, i.Month, i.Day, 0, 0, 0, 0);
        }

        public static DateTime ToTimeOfDay(this DateTime i)
        {
            return new DateTime(1, 1, 1, i.Hour, i.Minute, i.Second, i.Millisecond);
        }

    }
}
