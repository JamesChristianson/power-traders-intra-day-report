using NSubstitute;
using NUnit.Framework;
using PowerTradersIntraDayReport.Abstractions;
using PowerTradersIntraDayReport.Model;
using PowerTradersIntraDayReport.Services;
using PowerTradersIntraDayReport.Tests.TestHelpers;
using Serilog;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerTradersIntraDayReport.Tests
{
    public class IntraDayReportGeneratorTests
    {
        private IPowerService _powerSeriveMock;
        private IPositionAggrigator _positionAggrigatorMock;
        private ILogger _loggerMock;
        private IDateTimeOffsetProvider _dateProviderMock;
        private IIntraDayCsvReportWriter _csvWriterMock;
        private IEnumerable<PowerTrade> _testTrades;
        private IEnumerable<AggregatedPosition> _aggregatedPositions;

        private DateTimeOffset _runTime;

        [SetUp]
        public void Init()
        {
            _testTrades = new List<PowerTrade>()
            {
                PowerTradePositions.PowerTrade_1(),
                PowerTradePositions.PowerTrade_2()
            };
            _runTime = new DateTimeOffset(2021, 11, 17, 10, 55, 0, TimeSpan.Zero);
            _aggregatedPositions = PowerTradePositions.PowerTrade_Aggrigated();

            _powerSeriveMock = Substitute.For<IPowerService>();
            _powerSeriveMock.GetTradesAsync(Arg.Any<DateTime>()).Returns(_testTrades);

            _positionAggrigatorMock = Substitute.For<IPositionAggrigator>();
            _positionAggrigatorMock.Aggregate(Arg.Any<IEnumerable<PowerTrade>>()).Returns(_aggregatedPositions);


            _dateProviderMock = Substitute.For<IDateTimeOffsetProvider>();
            _dateProviderMock.Now().Returns(_runTime);

            _loggerMock = Substitute.For<ILogger>();
            _csvWriterMock = Substitute.For<IIntraDayCsvReportWriter>();
        }

        [Test]
        public async Task IntraDayReportGenerator_calls_PowerService_with_current_run_time()
        {
            var reportGenerator = new IntraDayReportGenerator(_powerSeriveMock, _positionAggrigatorMock, _loggerMock, _dateProviderMock, _csvWriterMock);

            await reportGenerator.GenerateReportAsync();

            _powerSeriveMock.Received(1).GetTradesAsync(Arg.Is<DateTime>(dt => dt.Ticks == _runTime.Date.Ticks));
        }

        [Test]
        public async Task IntraDayReportGenerator_calls_aggrigator_with_trades()
        {
            var reportGenerator = new IntraDayReportGenerator(_powerSeriveMock, _positionAggrigatorMock, _loggerMock, _dateProviderMock, _csvWriterMock);

            await reportGenerator.GenerateReportAsync();

            _positionAggrigatorMock.Received(1).Aggregate(_testTrades);
        }

        [Test]
        public async Task IntraDayReportGenerator_calls_IntraDayCsvReportWriter_with_current_run_time_and_aggregations()
        {
            var reportGenerator = new IntraDayReportGenerator(_powerSeriveMock, _positionAggrigatorMock, _loggerMock, _dateProviderMock, _csvWriterMock);

            await reportGenerator.GenerateReportAsync();

            _csvWriterMock.Received(1).Write(_aggregatedPositions, _runTime);
        }
    }
}
