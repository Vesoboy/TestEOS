using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Hosting;

namespace TestEOS.OutputLogs
{
    public class OneTask: BackgroundService//, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<OneTask> _logger;
        private Timer _timer;

        public OneTask(ILogger<OneTask> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("ФЗ 1 активна");

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

        }


        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            //_timer = null;//?.Change(Timeout.Infinite, 0);
            
            Program.Logger.Info("ФЗ 1 остановлена");

            _timer?.Dispose();
            await base.StopAsync(stoppingToken);

        }

    }
}
