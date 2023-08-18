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
        + "\nnumber of working days: " + validation1.numberOfWorkingDaysInMonth + "\nworking time: " + 
        validation1.totalTime.totalWorkingHours + "h " + validation1.totalTime.totalWorkingMinutes + "m" + "\nmax limit of working time: " + validation1.maxLimitOfWorkingHours + "h");

}
catch (ScheduleFormatException ex)
{
    Console.WriteLine(ex.Message);
}
