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

                numberOfMinutes += GetTimeDifference(element.StartTime, element.EndTime);
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

                TimePeriod value;
                bool success = _schedule.HoursPerDay.TryGetValue(day, out value);

                if (success && GetTimeDifference(value.StartTime, value.EndTime) != 0)
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
                TimePeriod value;
                var success = _schedule.HoursPerDay.TryGetValue(day, out value);

                if (success)
                {
                    numberOfOvertimeMinutes += GetTimeDifference(value.StartTime, value.EndTime);
                }
            }

            var otherDays = _schedule.HoursPerDay.Where(x => !sundays.Contains(x.Key)).Select(x => x.Key);

            foreach (var day in otherDays)
            {
                TimePeriod value;
                var success = _schedule.HoursPerDay.TryGetValue(day, out value);

                if (success)
                {
                    var diff = GetTimeDifference(value.StartTime, value.EndTime);

                    if (diff > 8 * 60)
                    {
                        numberOfOvertimeMinutes += diff - (8 * 60);
                    }
                }
            }

            int totalOvertimeHours = numberOfOvertimeMinutes / 60;
            int totalOvertimeMinutes = numberOfOvertimeMinutes - (totalOvertimeHours * 60);

            return (totalOvertimeHours, totalOvertimeMinutes);

        }

        public bool Is11HoursBreak()
        {
            for (int i = 1; i < _schedule.HoursPerDay.Count - 1; i++)
            {
                TimePeriod day, nextDay;

                var success = _schedule.HoursPerDay.TryGetValue(i, out day);

                if (!success)
                {
                    continue;
                }

                success = _schedule.HoursPerDay.TryGetValue(i + 1, out nextDay);

                if (!success)
                {
                    continue;
                }

                if ((GetTimeDifference(day.EndTime, new TimeOnly().AddHours(24)) + 
                    GetTimeDifference(new TimeOnly(), nextDay.StartTime)) < 11 * 60) 
                { 
                    return false; 
                }
            }
            return true;
        }

        private int GetTimeDifference(TimeOnly startTime, TimeOnly endTime)
        {
            return (int)(endTime - startTime).TotalMinutes;
        }
    }
}
