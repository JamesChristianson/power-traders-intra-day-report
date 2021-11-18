using PowerTradersIntraDayReport.Abstractions;
using System;
using System.IO;

namespace PowerTradersIntraDayReport.Services
{
    public class ReportPathProvider : IReportPathProvider
    {
        private readonly string _filePath;

        public ReportPathProvider(string filePath)
        {
            _filePath = filePath;
        }

        public string GetPath(DateTimeOffset date)
        {
            return Path.Combine(_filePath, $"PowerPosition_{date:yyyyMMdd_HHmm}.csv");
        }
    }
}
