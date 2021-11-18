using PowerTradersIntraDayReport.Abstractions;
using Serilog;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PowerTradersIntraDayReport.Services
{
    public class IntraDayReportGenerator : IIntraDayReportGenerator
    {
        private readonly IPowerService _powerService;
        private readonly IPositionAggrigator _positionAggrigator;
        private readonly ILogger _logger;
        private readonly IDateTimeOffsetProvider _dateProvider;
        private readonly IIntraDayCsvReportWriter _csvWriter;

        public IntraDayReportGenerator(
            IPowerService powerService,
            IPositionAggrigator positionAggrigator,
            ILogger logger,
            IDateTimeOffsetProvider dateProvider,
            IIntraDayCsvReportWriter csvWriter)
        {
            _powerService = powerService;
            _positionAggrigator = positionAggrigator;
            _logger = logger;
            _dateProvider = dateProvider;
            _csvWriter = csvWriter;
        }

        public async Task GenerateReportAsync()
        {
            try
            {
                var runTime = _dateProvider.Now();
                _logger.Information($"Generating intra day report at {runTime: dd-MM-yyyy mm:ss}");

                var trades = await GetTradesAsync(runTime);
                var positionAggregations = _positionAggrigator.Aggregate(trades);
                _csvWriter.Write(positionAggregations, runTime);
                _logger.Information("Report written to csv");

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An exception was encounted while running the intra day report");
            }
        }

        public async Task<IEnumerable<PowerTrade>> GetTradesAsync(DateTimeOffset date)
        {
            _logger.Information("Requesting trades");
            var trades = await _powerService.GetTradesAsync(date.Date);
            _logger.Information($"{trades.Count()} trades returned");
            return trades;
        }
    }
}
