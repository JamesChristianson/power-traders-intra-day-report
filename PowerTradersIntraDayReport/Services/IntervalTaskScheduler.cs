using PowerTradersIntraDayReport.Abstractions;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace PowerTradersIntraDayReport.Services
{
    public class IntervalTaskScheduler : IIntervalTaskScheduler
    {
        private Timer _timer;
        private readonly ILogger _logger;
        private readonly double _intervalMilliseconds;
        public Func<Task> Task { private get; set; }

        public IntervalTaskScheduler(ILogger logger, double intervalMilliseconds)
        {
            _logger = logger;
            _intervalMilliseconds = intervalMilliseconds;
        }

        public async Task ScheduleTasksAsync(bool runOnStart = true)
        {
            if (Task == null)
            {
                var message = "Unable to commence task scheduling as no task has been registered";
                _logger.Information(message);
                throw new Exception(message);
            }

            _logger.Information($"Tasks scheduled at intervals of {_intervalMilliseconds / 1000 / 60} minutes");

            if (runOnStart)
            {
                await Task();
            }

            _timer = new Timer()
            {
                Interval = _intervalMilliseconds,
            };
            _timer.Elapsed += new ElapsedEventHandler(RunTask);
            _timer.Start();
        }

        private async void RunTask(object sender, ElapsedEventArgs e)
        {
            await Task();
        }
    }
}
