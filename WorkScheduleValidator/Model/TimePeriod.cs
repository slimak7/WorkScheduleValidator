using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkScheduleValidator.Exceptions;

namespace WorkScheduleValidator.Model
{
    public class TimePeriod
    {
        public TimePeriod(string startTime, string endTime)
        {
            try
            {

                StartTime = TimeOnly.Parse(startTime);
                EndTime = TimeOnly.Parse(endTime);

                if (StartTime > EndTime)
                {
                    throw new ScheduleArgumentException($"Improper time period: Start time({startTime}) can not be after End time({endTime})");
                }
            }
            catch (FormatException ex)
            {
                throw new ScheduleArgumentException(ex.Message);
            }
        }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
