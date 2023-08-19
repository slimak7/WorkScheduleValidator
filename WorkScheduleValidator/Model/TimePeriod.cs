using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduleValidator.Model
{
    public class TimePeriod
    {
        public TimePeriod(string startTime, string endTime)
        {
            StartTime = TimeOnly.Parse(startTime);
            EndTime = TimeOnly.Parse(endTime);
        }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
