using PowerTradersIntraDayReport.Abstractions;
using System;

namespace PowerTradersIntraDayReport.Services
{
    public class TimePeriodConverter : ITimePeriodConverter
    {
        public string ToTimePeriod(int period)
        {
            return DateTime.Today.AddHours(period - 2).ToString("HH:mm");
        }
    }
}
