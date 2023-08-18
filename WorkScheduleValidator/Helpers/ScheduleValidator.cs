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

        public bool IsWorkPlannedForSunday()
        {
            var sundays = _schedule.GetDaysByCondition(delegate (DateTime date)
            {
                return date.DayOfWeek == DayOfWeek.Sunday;
            });

            foreach (var day in sundays)
            {
                var element = _schedule.HoursPerDay[day];

                if (GetTimeDifference(element.startTime, element.endTime) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public (int totalOvertimeHours, int totalOvertimeMinutes) CalculateOvertime()
        {
            int numberOfOvertimeMinutes = 0;

            var sundays = _schedule.GetDaysByCondition(delegate (DateTime date)
            {
                return date.DayOfWeek == DayOfWeek.Sunday;
            });

            foreach (var day in sundays)
            {
                var element = _schedule.HoursPerDay[day];

                numberOfOvertimeMinutes += GetTimeDifference(element.startTime, element.endTime);                
            }

            var otherDays = _schedule.HoursPerDay.Where(x => !sundays.Contains(x.Key)).Select(x => x.Key);

            foreach (var day in otherDays)
            {
                var element = _schedule.HoursPerDay[day];

                var diff = GetTimeDifference(element.startTime, element.endTime);

                if (diff > 8 * 60)
                {
                    numberOfOvertimeMinutes += diff - (8 * 60);
                }
            }

            int totalOvertimeHours = numberOfOvertimeMinutes / 60;
            int totalOvertimeMinutes = numberOfOvertimeMinutes - (totalOvertimeHours * 60);

            return (totalOvertimeHours, totalOvertimeMinutes);

        }

        private int GetTimeDifference(TimeOnly startTime, TimeOnly endTime)
        {
            return (int)(endTime - startTime).TotalMinutes;
        }
    }
}
