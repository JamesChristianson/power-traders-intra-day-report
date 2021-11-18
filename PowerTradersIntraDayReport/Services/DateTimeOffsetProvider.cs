using PowerTradersIntraDayReport.Abstractions;
using System;

namespace PowerTradersIntraDayReport.Services
{
    public class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}
