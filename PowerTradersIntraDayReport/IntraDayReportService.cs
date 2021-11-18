using Ninject;
using PowerTradersIntraDayReport.Abstractions;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace PowerTradersIntraDayReport
{
    public partial class IntraDayReportService : ServiceBase
    {
        private readonly IIntraDayReportGenerator _reportService;
        private readonly IIntervalTaskScheduler _scheduler;


        public IntraDayReportService()
        {
            InitializeComponent();
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _reportService = kernel.Get<IIntraDayReportGenerator>();
            _scheduler = kernel.Get<IIntervalTaskScheduler>();
            _scheduler.Task = () => _reportService.GenerateReportAsync();
        }

        protected override void OnStart(string[] args)
        {
           Task.Run(() => _scheduler.ScheduleTasksAsync()); ;
        }

        public async Task RunAsync()
        {
            await _scheduler.ScheduleTasksAsync();
        }

        protected override void OnStop()
        {
        }
    }
}
