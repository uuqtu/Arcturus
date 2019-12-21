using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcturus.Time.Weekend
{
    public static class WeekendDays
    {
        public static List<DateTime> GetWeekendDays(int year)
        {
            var weekends = GetDaysBetween(new DateTime(1, 1, year), new DateTime(31, 12, year))
                .Where(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);

            return weekends.ToList();
        }

        public static List<DateTime> GetWeekendDays(DateTime start, DateTime end)
        {
            var weekends = GetDaysBetween(start, end)
                .Where(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);

            return weekends.ToList();
        }

        public static IEnumerable<DateTime> GetDaysBetween(DateTime start, DateTime end)
        {
            for (DateTime i = start; i < end; i = i.AddDays(1))
            {
                yield return i;
            }
        }


    }
}
