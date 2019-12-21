using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcturus.Time.Years
{
    public static class YearsSinceDate
    {
        public static List<int> GetYears(DateTime date)
        {
            List<int> years = new List<int>();

            for (DateTime a = date; a < DateTime.Now; a = a.AddYears(1))
            {
                if (a.Year <= DateTime.Now.Year)
                    years.Add(a.Year);
            }

            return years;
        }

    }
}
