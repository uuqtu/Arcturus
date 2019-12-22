using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Opus.Model.Time
{
    public class Date : IComparable
    {
        private static Regex _TodRegex = new Regex(@"\d?\d?\d\d-\d?\d-\d?\d");

        public Date()
        {
            Init(0, 0, 0);
        }
        public Date(int year, int month, int day = 0)
        {
            Init(year, month, day);
        }
        public Date(DateTime dt)
        {
            Init(dt);
        }
        public Date(Date td)
        {
            Init(td.Year, td.Month, td.Day);
        }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public static Date Now { get { return new Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day); } }

        public bool Equals(Date other)
        {
            if (other == null) { return false; }
            return Year == other.Year && Month == other.Month && Day == other.Day;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            Date td = obj as Date;
            if (td == null) { return false; }
            else { return Equals(td); }
        }

        public double TotalDays()
        {
            DateTime PreviousDateTime = new DateTime(1970, 1, 1, 0, 0, 0);
            return (new DateTime(Year, Month, Day, 0, 0, 0).ToUniversalTime() - PreviousDateTime).TotalDays;
        }

        public void AddDays(int days)
        {
            DateTime d = this.ToDateTime();
            d.AddDays(days);
            this.Year = d.Year;
            this.Month = d.Month;
            this.Day = d.Day;
        }
        public override int GetHashCode()
        {
            DateTime PreviousDateTime = new DateTime(1970, 1, 1, 0, 0, 0);
            var stringSecsNow = (new DateTime(Year, Month, Day, 0, 0, 0).ToUniversalTime() - PreviousDateTime).TotalDays;

            return (int)Math.Abs(Math.Round(stringSecsNow)); ;
        }
        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day, 0, 0, 0);
        }
        public override string ToString()
        {
            return ToString("yyyy-MM-dd");
        }
        public string ToString(string format)
        {
            DateTime dt = new DateTime(this.Year, this.Month, this.Day, 0, 0, 0);
            return dt.ToString(format);
        }
        #region -- Static --
        /// <summary>
        /// Gibt zurück wieviele Tage unterschid herrswchen
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static int operator -(Date t1, Date t2)
        {
            var dt2 = t1.ToDateTime().Subtract(t2.ToDateTime());
            return dt2.Days;
        }

        public static bool operator !=(Date t1, Date t2)
        {
            if (ReferenceEquals(t1, t2)) { return true; }
            else if (ReferenceEquals(t1, null)) { return true; }
            else
            {
                return t1.TotalDays() != t2.TotalDays();
            }
        }
        public static bool operator !=(Date t1, DateTime dt2)
        {
            if (ReferenceEquals(t1, null)) { return false; }
            DateTime dt1 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0);
            return dt1 != dt2;
        }
        public static bool operator !=(DateTime dt1, Date t2)
        {
            if (ReferenceEquals(t2, null)) { return false; }
            DateTime dt2 = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            return dt1 != dt2;
        }
        /// <summary>
        /// Addiert ganze Tage
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static Date operator +(Date t1, TimeSpan t2)
        {
            DateTime now = DateTime.Now;
            DateTime dt1 = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            TimeSpan ts = new TimeSpan(t2.Days, 0, 0, 0, 0);
            DateTime dt2 = dt1 + ts;
            return new Date(dt2);
        }
        public static bool operator <(Date t1, Date t2)
        {
            if (ReferenceEquals(t1, t2)) { return true; }
            else if (ReferenceEquals(t1, null)) { return true; }
            else
            {
                return t1.TotalDays() < t2.TotalDays();
            }
        }
        public static bool operator <(Date t1, DateTime dt2)
        {
            if (ReferenceEquals(t1, null)) { return false; }
            DateTime dt1 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0);
            return dt1 < dt2;
        }
        public static bool operator <(DateTime dt1, Date t2)
        {
            if (ReferenceEquals(t2, null)) { return false; }
            DateTime dt2 = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            return dt1 < dt2;
        }
        public static bool operator <=(Date t1, Date t2)
        {
            if (ReferenceEquals(t1, t2)) { return true; }
            else if (ReferenceEquals(t1, null)) { return true; }
            else
            {
                if (t1 == t2) { return true; }
                return t1.TotalDays() <= t2.TotalDays();
            }
        }
        public static bool operator <=(Date t1, DateTime dt2)
        {
            if (ReferenceEquals(t1, null)) { return false; }
            DateTime dt1 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0);
            return dt1 <= dt2;
        }
        public static bool operator <=(DateTime dt1, Date t2)
        {
            if (ReferenceEquals(t2, null)) { return false; }
            DateTime dt2 = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            return dt1 <= dt2;
        }
        public static bool operator ==(Date t1, Date t2)
        {
            if (ReferenceEquals(t1, t2)) { return true; }
            else if (ReferenceEquals(t1, null)) { return true; }
            else { return t1.Equals(t2); }
        }
        public static bool operator ==(Date t1, DateTime dt2)
        {
            if (ReferenceEquals(t1, null)) { return false; }
            DateTime dt1 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0);
            return dt1 == dt2;
        }
        public static bool operator ==(DateTime dt1, Date t2)
        {
            if (ReferenceEquals(t2, null)) { return false; }
            DateTime dt2 = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            return dt1 == dt2;
        }
        public static bool operator >(Date t1, Date t2)
        {
            if (ReferenceEquals(t1, t2)) { return true; }
            else if (ReferenceEquals(t1, null)) { return true; }
            else
            {
                return t1.TotalDays() > t2.TotalDays();
            }
        }
        public static bool operator >(Date t1, DateTime dt2)
        {
            if (ReferenceEquals(t1, null)) { return false; }
            DateTime dt1 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0);
            return dt1 > dt2;
        }
        public static bool operator >(DateTime dt1, Date t2)
        {
            if (ReferenceEquals(t2, null)) { return false; }
            DateTime dt2 = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            return dt1 > dt2;
        }
        public static bool operator >=(Date t1, Date t2)
        {
            if (ReferenceEquals(t1, t2)) { return true; }
            else if (ReferenceEquals(t1, null)) { return true; }
            else
            {
                return t1.TotalDays() >= t2.TotalDays();
            }
        }
        public static bool operator >=(Date t1, DateTime dt2)
        {
            if (ReferenceEquals(t1, null)) { return false; }
            DateTime dt1 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0);
            return dt1 >= dt2;
        }
        public static bool operator >=(DateTime dt1, Date t2)
        {
            if (ReferenceEquals(t2, null)) { return false; }
            DateTime dt2 = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            return dt1 >= dt2;
        }
        /// <summary>
        /// Input examples:
        /// 14:21:17            (2pm 21min 17sec)
        /// 02:15               (2am 15min 0sec)
        /// 2:15                (2am 15min 0sec)
        /// 2/1/2017 14:21      (2pm 21min 0sec)
        /// Date=15:13:12  (3pm 13min 12sec)
        /// </summary>
        public static Date Parse(string s, bool innerCall = false)
        {

#warning Das hin und her geparse ist hier noch ziemlich schlecht(!)
            // We will parse any section of the text that matches this
            // pattern: dd:dd or dd:dd:dd where the first doublet can
            // be one or two digits for the hour.  But minute and second
            // must be two digits.
            if (!innerCall)
            {
                s = s.Replace(".", "-");

                try
                {
                    //Falls Das Datum IM Format dd-mm-yyyy kommt
                    var splittets = s.Split('-');
                    Regex r = new Regex(@"\d\d:\d\d:\d\d");
                    var ss = r.Replace(s, "");
                    s = ss;
                    splittets = s.Split('-');
                    if (splittets[2].Replace(" ", "").Length == 4)
                    {
                        _TodRegex = new Regex(@"\d\d-\d?\d-\d\d\d\d");
                        Match im = _TodRegex.Match(s.Replace(".", "-"));
                        string itext = im.Value;
                        string[] ifields = itext.Split('-');
                        if (ifields.Length < 2) { throw new ArgumentException("No valid time of day pattern found in input text"); }
                        int iyear = Convert.ToInt32(ifields[2]);
                        int imonth = Convert.ToInt32(ifields[1]);
                        int iday = ifields.Length > 2 ? Convert.ToInt32(ifields[0]) : 1;

                        return new Date(iyear, imonth, iday);
                    }
                }
                catch
                {

                }
            }
            else
            {
                _TodRegex = new Regex(@"\d?\d?\d\d-\d?\d-\d?\d");
            }
            Match m = _TodRegex.Match(s);
            string text = m.Value;
            string[] fields = text.Split('-');
            if (fields.Length < 2) { throw new ArgumentException("No valid time of day pattern found in input text"); }
            int year = Convert.ToInt32(fields[0]);
            int month = Convert.ToInt32(fields[1]);
            int day = fields.Length > 2 ? Convert.ToInt32(fields[2]) : 1;

            return new Date(year, month, day);
        }

        public static Date Parse(DateTime s)
        {
            return Parse(s.ToString("yyyy-MM-dd"), true);
        }
        #endregion

        private void Init(int year, int month, int day)
        {
            if (month < 1 || month > 12) { throw new ArgumentException("Invalid month, must be from 1 to 12."); }
            if (day < 1 || day > DateTime.DaysInMonth(year, month)) { throw new ArgumentException($"Invalid day, must be from 1 to {DateTime.DaysInMonth(year, month)} in month {month}."); }
            Year = year;
            Month = month;
            Day = day;
        }

        private void Init(DateTime dt)
        {
            Init(dt.Year, dt.Month, dt.Day);
        }

        public int CompareTo(object obj)
        {
            if (Equals(obj) == true)
                return 0;
            if (this > (Date)obj)
                return 1;
            else
                return -1;
        }
    }
}

