using PowerTradersIntraDayReport.Model;
using System;
using System.Collections.Generic;

namespace PowerTradersIntraDayReport.Abstractions
{
    public interface IIntraDayCsvReportWriter
    {
        void Write(IEnumerable<AggregatedPosition> positionAggregations, DateTimeOffset date);
    }
}