using System;

namespace PowerTradersIntraDayReport.Abstractions
{
    public interface IReportPathProvider
    {
        string GetPath(DateTimeOffset date);
    }
}