using PowerTradersIntraDayReport.Model;
using Services;
using System.Collections.Generic;

namespace PowerTradersIntraDayReport.Abstractions
{
    public interface IPositionAggrigator
    {
        IEnumerable<AggregatedPosition> Aggregate(IEnumerable<PowerTrade> trades);
    }
}