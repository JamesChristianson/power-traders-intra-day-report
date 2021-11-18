using System;
using System.Threading.Tasks;

namespace PowerTradersIntraDayReport.Abstractions
{
    public interface IIntervalTaskScheduler
    {
        Func<Task> Task { set; }

        Task ScheduleTasksAsync(bool runOnStart = true);
    }
}