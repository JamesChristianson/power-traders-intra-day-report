using NUnit.Framework;
using PowerTradersIntraDayReport.Services;
using PowerTradersIntraDayReport.Tests.TestHelpers;
using Services;
using System.Collections.Generic;
using System.Linq;

namespace PowerTradersIntraDayReport.Tests
{
    public class PositionAggrigatorTests
    {
        [Test]
        public void PositionAggrigator_aggregates_trades()
        {
            var aggregator = new PositionAggrigator(new TimePeriodConverter());

            //Trades as per Challenge Requirements document
            var trades = new List<PowerTrade>()
            {
                PowerTradePositions.PowerTrade_1(),
                PowerTradePositions.PowerTrade_2()
            };

            var aggregatedPositions = aggregator.Aggregate(trades);

            //Results as per Challenge Requirements document
            var expected = PowerTradePositions.PowerTrade_Aggrigated();

            CollectionAssert.AreEqual(expected.Select(ap => ap.Period), aggregatedPositions.Select(ap => ap.Period));
            CollectionAssert.AreEqual(expected.Select(ap => ap.Volume), aggregatedPositions.Select(ap => ap.Volume));
        }
    }
}