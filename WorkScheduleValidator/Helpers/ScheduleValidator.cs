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

        public (bool isValid, (int totalWorkingHours, int totalWorkingMinutes) totalTime, int maxLimitOfWorkingHours, int numberOfWorkingDaysInMonth) ValidateNumberOfWorkingHours()
        {
            var workingDays = _schedule.GetDaysByCondition(delegate (DateTime date)
            {
                return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
            });

            int numberOfWorkingDaysInMonth = workingDays.Count;

            int numberOfMinutes = 0;

            foreach (var day in workingDays)
            {
                var element = _schedule.HoursPerDay[day];

                numberOfMinutes += GetTimeDifference(element.startTime, element.endTime);
            }

            int maxNumberOfHours = 8 * numberOfWorkingDaysInMonth;

            int totalWorkingHours = numberOfMinutes / 60;
            int totalWorkingMinutes = numberOfMinutes - (totalWorkingHours * 60);

            return (numberOfMinutes <= maxNumberOfHours * 60, (totalWorkingHours, totalWorkingMinutes), maxNumberOfHours, numberOfWorkingDaysInMonth);
        }

        private int GetTimeDifference(TimeOnly startTime, TimeOnly endTime)
        {
            return (int)(endTime - startTime).TotalMinutes;
        }
    }
}
