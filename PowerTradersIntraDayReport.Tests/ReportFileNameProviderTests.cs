using NUnit.Framework;
using System;
using PowerTradersIntraDayReport.Services;

namespace PowerTradersIntraDayReport.Tests
{
    public class ReportFileNameProviderTests
    {
        [Test]
        public void ReportFileNameProvider_Generates_Correct_Full_Path()
        {
            var fileNameProvider = new ReportPathProvider("C:\\temp\\reports\\");
            var path = fileNameProvider.GetPath(new DateTimeOffset(2021, 11, 17, 10, 55, 0, TimeSpan.Zero));

            var expectedFilePath = "C:\\temp\\reports\\PowerPosition_20211117_1055.csv";

            Assert.AreEqual(expectedFilePath, path);
        }
    }
}