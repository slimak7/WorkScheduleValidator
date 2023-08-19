using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduleValidator.Model
{
    public class Schedule
    {
        private readonly DateTime _monthYear;
        public string GetMonthName 
        { 
            get 
            {
                CultureInfo info = new CultureInfo("en-US");
                return _monthYear.ToString("MMMM", info);
            }
        }
        
        public Dictionary<int, TimePeriod> HoursPerDay { get; set; }

        public Schedule(int month, int year, bool autoFillDefaultData = false)
        {
            _monthYear = new DateTime(year, month, 1);
            HoursPerDay = new Dictionary<int, TimePeriod>();

            if (autoFillDefaultData )
            {
                int daysInMonth = DateTime.DaysInMonth(year, month);

                for (int i = 1; i <= daysInMonth; i++)
                {
                    if (new DateOnly(year, month, i).DayOfWeek != (DayOfWeek.Saturday & DayOfWeek.Sunday))
                    {
                        HoursPerDay.Add(i, new TimePeriod("8:00", "16:00"));
                    }
                }
            }
        }

        public List<int> GetDaysByCondition(Func<DateTime, bool> condition)
        {
            List<int> days = new List<int>();

            foreach (var day in GetDaysInMonth(_monthYear.Year, _monthYear.Month))
            {
                if (condition(day))
                {
                    days.Add(day.Day);
                }
            }
            return days;
        }

        private IEnumerable<DateTime> GetDaysInMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                yield return new DateTime(year, month, day);
            }
        }
    }
}
