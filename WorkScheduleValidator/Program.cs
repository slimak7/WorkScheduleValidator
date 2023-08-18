// See https://aka.ms/new-console-template for more information
using WorkScheduleValidator.Exceptions;
using WorkScheduleValidator.Helpers;

Console.WriteLine("Welcome to work schedule validator");

try
{
    var schedule = new ScheduleLoader("schedule_1.txt").GetSchedule();

    Console.WriteLine(schedule.GetMonthName);

    ScheduleValidator scheduleValidator = new ScheduleValidator(schedule);

    var validation1 = scheduleValidator.ValidateNumberOfWorkingHours();

    Console.Write("################1###############\nis valid: " + validation1.isValid 
        + "\nnumber of working days: " + validation1.numberOfWorkingDaysInMonth + "\nnumber of hours: " + 
        validation1.numberOfWorkingHours + "\nmax limit of working hours: " + validation1.maxNumberOfWorkingHours);

}
catch (ScheduleFormatException ex)
{
    Console.WriteLine(ex.Message);
}
