﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkScheduleValidator.Exceptions;
using WorkScheduleValidator.Model;

namespace WorkScheduleValidator.Helpers
{
    public class ScheduleLoader
    {
        private const string _directory = "./";
        private string _fileName;
        public ScheduleLoader(string fileName)
        {
            _fileName = fileName;
        }

        public Schedule GetSchedule()
        {            
            using (var file = File.OpenText(Path.Combine(_directory, _fileName)))
            {
                
                int month, year;

                bool success = int.TryParse(file.ReadLine(), out year);

                if (!success)
                {
                    throw new ScheduleFormatException("Can not read the year");
                }

                success = int.TryParse(file.ReadLine(), out month);

                if (!success)
                {
                    throw new ScheduleFormatException("Can not read the month");
                }

                Schedule schedule = new Schedule(month, year);

                while (!file.EndOfStream)
                {
                    var line = file.ReadLine();

                    var parts = line.Split(",");

                    if (parts.Length > 2) 
                    {
                        throw new ScheduleFormatException("Can not determine parts indicating day and hours");
                    }

                    int day, hours;

                    success = int.TryParse(parts[0], out day);

                    if (!success) 
                    {
                        throw new ScheduleFormatException("Can not read day number");
                    }
                    
                    success = int.TryParse(parts[1], out hours);

                    if (!success)
                    {
                        throw new ScheduleFormatException("Can not read hours");
                    }

                    schedule.HoursPerDay.Add(day, hours);

                }
                
                return schedule;
            }
        }
    }
}