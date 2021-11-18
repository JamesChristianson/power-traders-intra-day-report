using Ninject.Modules;
using PowerTradersIntraDayReport.Abstractions;
using PowerTradersIntraDayReport.Services;
using Serilog;
using Services;
using System;
using System.Configuration;

namespace PowerTradersIntraDayReport.Modules
{
    public class AppModule : NinjectModule
    {
        public override void Load()
        {
                Log.Logger = new LoggerConfiguration()
                .WriteTo
                .File(ConfigurationManager.AppSettings["LogsPath"])
                .CreateLogger();

            Bind<ILogger>().ToMethod(context => Log.Logger);
            Bind<IReportPathProvider>().ToMethod(context => new ReportPathProvider(ConfigurationManager.AppSettings["ReportsPath"]));
            Bind<IPositionAggrigator>().To<PositionAggrigator>();
            Bind<IIntraDayReportGenerator>().To<IntraDayReportGenerator>();
            Bind<IDateTimeOffsetProvider>().To<DateTimeOffsetProvider>();
            Bind<IIntraDayCsvReportWriter>().To<IntraDayCsvReportWriter>();
            Bind<IPowerService>().To<PowerService>();
            Bind<ITimePeriodConverter>().To<TimePeriodConverter>();
            Bind<IIntervalTaskScheduler>().ToConstructor(context => new IntervalTaskScheduler(context.Inject<ILogger>(), Convert.ToInt32(ConfigurationManager.AppSettings["ScheduleIntervalMinutes"]) * 1000 * 60));
        }
    }
}
