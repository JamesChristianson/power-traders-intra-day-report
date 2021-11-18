# power-traders-intra-day-report
Petroineos Development Challenge

## Features
- Implemented as a Windows service using .Net Frameworkcan 4.7 and can also be run as a console app
- Dependency injection using Ninject
- Unit testing using NUnit and NSubstitute
- Logging using serilog (logs configured to write to file)
- Interface driven design for testable, maintainable code

## Getting Started
- This app can be installed as a Windows service in the normal way. 
- For testing this app may be run using the console by changing the project type to "console application". The console app will run until cancelled (Ctrl + c)

### Configuration 
- LogsPath: location of log file
- ReportsPath: location of reports
- ScheduleIntervalMinutes: interval of reporting in minutes

## Coding Notes
Testing would be more extensive in a real application
