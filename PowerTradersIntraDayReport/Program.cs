using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace PowerTradersIntraDayReport
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static async Task Main(string[] args)
        {
            var reportService = new IntraDayReportService();

            if (Environment.UserInteractive)
            {
                Console.WriteLine("Running Intra Day Report Service");
                var ct = new CancellationTokenSource();
                Console.CancelKeyPress += (s, e) =>
                {
                    Console.WriteLine("Canceling...");
                    ct.Cancel();
                    e.Cancel = true;
                };

                await reportService.RunAsync();
                while (!ct.IsCancellationRequested)
                {
                    await Task.Delay(1000);
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                       reportService
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}