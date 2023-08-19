# WorkScheduleValidator
How to run?
- Open in Visual Studio and run but keep in mind that file schedule_1.txt is not copied every time so if You would like to make some changes to schedule You should go to file located in bin/debug/net7.0/ or bin/release/net7.0 depending on Your configuration.

--OR--

- go to appropriate folder and run from command line using dotnet CLI: project: dotnet run .\WorkScheduleValidator.csproj, tests: dotnet test.

  
Assumptions to schedule file format:
year
month
day, time range in format HH:mm - HH:mm
for example:<br />
2023<br />
8<br />
1, 8:00 - 16:00<br />
2, 8:00 - 16:00<br />
3, 8:00 - 16:00<br />
...


