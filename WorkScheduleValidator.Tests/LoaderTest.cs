using WorkScheduleValidator.Exceptions;
using WorkScheduleValidator.Helpers;

namespace WorkScheduleValidator.Tests
{
    
    public class LoaderTest
    {
        [Fact()]
        public void LoaderTests()
        {
            var exception = Record.Exception(() => {

                ScheduleLoader scheduleLoader = new ScheduleLoader("schedule.txt", "./SchedulesFilesTests");

                var schedule = scheduleLoader.GetSchedule();
            });

            Assert.Null(exception);

            Assert.Throws<ScheduleFormatException>(() =>
            {
                ScheduleLoader scheduleLoader = new ScheduleLoader("schedule_bad_format_no_month.txt", "./SchedulesFilesTests");

                var schedule = scheduleLoader.GetSchedule();             
            });

            Assert.Throws<ScheduleFormatException>(() =>
            {
                ScheduleLoader scheduleLoader = new ScheduleLoader("schedule_bad_format_no_dash.txt", "./SchedulesFilesTests");

                var schedule = scheduleLoader.GetSchedule();
            });

            Assert.Throws<ScheduleFormatException>(() =>
            {
                ScheduleLoader scheduleLoader = new ScheduleLoader("schedule_bad_format_no_comma.txt", "./SchedulesFilesTests");

                var schedule = scheduleLoader.GetSchedule();
            });

            Assert.Throws<ScheduleArgumentException>(() =>
            {
                ScheduleLoader scheduleLoader = new ScheduleLoader("schedule_bad_argument_chars.txt", "./SchedulesFilesTests");

                var schedule = scheduleLoader.GetSchedule();
            });


            Assert.Throws<ScheduleArgumentException>(() =>
            {
                var scheduleLoader = new ScheduleLoader("schedule_bad_argument_bad_month.txt", "./SchedulesFilesTests");

                var schedule = scheduleLoader.GetSchedule();
            });

            Assert.Throws<ScheduleArgumentException>(() =>
            {
                var scheduleLoader = new ScheduleLoader("schedule_bad_argument_bad_time_period.txt", "./SchedulesFilesTests");

                var schedule = scheduleLoader.GetSchedule();
            });

            Assert.Throws<ScheduleArgumentException>(() =>
            {
                var scheduleLoader = new ScheduleLoader("schedule_bad_argument_bad_year.txt", "./SchedulesFilesTests");

                var schedule = scheduleLoader.GetSchedule();
            });

        }

    }
}
