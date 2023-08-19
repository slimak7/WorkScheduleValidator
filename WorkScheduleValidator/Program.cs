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

    Console.Write($"################1###############\nis valid: {validation1.isValid}\n" +
        $"number of working days: {validation1.numberOfWorkingDaysInMonth}" +
        $"\nnumber of working hours: {validation1.totalTime.totalWorkingHours}h {validation1.totalTime.totalWorkingMinutes}m\n" +
        $"max limit of hours: {validation1.maxLimitOfWorkingHours}h");

    Console.Write("\n\n");

    var validation2 = scheduleValidator.IsWorkPlannedForSunday();

    Console.Write($"################2###############\nIs work planned on any Sunday: {validation2}");
    Console.Write("\n\n");

    var validation3 = scheduleValidator.CalculateOvertime();

    Console.Write($"################3###############\nTotal overtime: {validation3.totalOvertimeHours}h {validation3.totalOvertimeMinutes}m");
    Console.Write("\n\n");

    var validation4 = scheduleValidator.Is11HoursBreak();
    Console.Write($"################4###############\nIs there min 11 hours break between two following days: {validation4}");


}
catch (ScheduleFormatException ex)
{
    Console.WriteLine(ex.Message);
}
catch (ScheduleArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
