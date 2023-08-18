using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduleValidator.Model
{
    public class Schedule
    {
        private readonly DateTime _date;
        public string GetMonthName 
        { 
            get 
            {
                return _date.ToString("MMMM");
            }
        }

        public Dictionary<int, double> HoursPerDay { get; set; }

        public Schedule(int month, int year)
        {
            _date = new DateTime(year, month, 1);
            HoursPerDay = new Dictionary<int, double>();
        }

        public List<int> GetDaysByCondition(Func<DateTime, bool> condition)
        {
            List<int> days = new List<int>();

            foreach (var day in GetDaysInMonth(_date.Year, _date.Month))
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
