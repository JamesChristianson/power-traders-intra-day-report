using System;

namespace PowerTradersIntraDayReport.Abstractions
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset Now();
    }
}