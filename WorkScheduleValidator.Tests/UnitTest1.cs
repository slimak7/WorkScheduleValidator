using WorkScheduleValidator.Helpers;
using WorkScheduleValidator.Model;

namespace WorkScheduleValidator.Tests
{
    public class UnitTest1
    {
        
        [Fact()]
        public void Validation1Test()
        {
            Schedule schedule = new Schedule(8, 2023, true);

            ScheduleValidator validator = new ScheduleValidator(schedule);

            var result = validator.ValidateNumberOfWorkingHours();

            Assert.True(result.isValid);

            schedule.HoursPerDay[1] = new TimePeriod("8:00", "16:01");

            result = validator.ValidateNumberOfWorkingHours();

            Assert.False(result.isValid);
        }

        [Fact()]
        public void Validation2Test()
        {
            Schedule schedule = new Schedule(8, 2023, true);

            ScheduleValidator validator = new ScheduleValidator(schedule);

            var result = validator.IsWorkPlannedForSunday();

            Assert.False(result);

            var sundays = schedule.GetDaysByCondition(x => x.DayOfWeek == DayOfWeek.Sunday);

            schedule.HoursPerDay.Add(sundays[0], new TimePeriod("8:00", "16:00"));

            result = validator.IsWorkPlannedForSunday();

            Assert.True(result);
        }

        [Fact()]
        public void Validation3Test()
        {
            Schedule schedule = new Schedule(8, 2023, true);

            ScheduleValidator validator = new ScheduleValidator(schedule);

            var result = validator.CalculateOvertime();

            Assert.True(result.totalOvertimeHours == 0 && result.totalOvertimeMinutes == 0);

            schedule.HoursPerDay[1] = new TimePeriod("8:00", "18:00");

            result = validator.CalculateOvertime();

            Assert.True(result.totalOvertimeHours == 2 && result.totalOvertimeMinutes == 0);

            var sundays = schedule.GetDaysByCondition(x => x.DayOfWeek == DayOfWeek.Sunday);

            schedule.HoursPerDay.Add(sundays[0], new TimePeriod("8:00", "10:45"));

            result = validator.CalculateOvertime();

            Assert.True(result.totalOvertimeHours == 4 && result.totalOvertimeMinutes == 45);

        }

        [Fact()]
        public void Validation4Test()
        {
            Schedule schedule = new Schedule(8, 2023, true);

            ScheduleValidator validator = new ScheduleValidator(schedule);

            var result = validator.Is11HoursBreak();

            Assert.True(result);

            schedule.HoursPerDay[1] = new TimePeriod("8:00", "21:01");

            result = validator.Is11HoursBreak();

            Assert.False(result);
        }
    }
}