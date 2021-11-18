using System.Threading.Tasks;

namespace PowerTradersIntraDayReport.Abstractions
{
    public interface IIntraDayReportGenerator
    {
        Task GenerateReportAsync();
    }
}