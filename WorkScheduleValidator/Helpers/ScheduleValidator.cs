using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkScheduleValidator.Model;

namespace WorkScheduleValidator.Helpers
{
    public class ScheduleValidator
    {
        private Schedule _schedule;

        public ScheduleValidator(Schedule schedule)
        {
            _schedule = schedule;
        }

        public (bool isValid, double numberOfWorkingHours, double maxNumberOfWorkingHours, int numberOfWorkingDaysInMonth) ValidateNumberOfWorkingHours()
        {
            var workingDays = _schedule.GetDaysByCondition(delegate (DateTime date)
            {
                return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
            });

            int numberOfWorkingDaysInMonth = workingDays.Count;

            double numberOfWorkingHours = 0;
            foreach (var day in workingDays)
            {
                numberOfWorkingHours += _schedule.HoursPerDay[day];
            }

            double maxNumberOfWorkingHours = 8 * numberOfWorkingDaysInMonth;

            return (numberOfWorkingHours <= maxNumberOfWorkingHours, numberOfWorkingHours, maxNumberOfWorkingHours, numberOfWorkingDaysInMonth);
        }
    }
}
