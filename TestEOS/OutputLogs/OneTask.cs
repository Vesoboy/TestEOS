using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Hosting;

namespace TestEOS.OutputLogs
{
    public class OneTask: BackgroundService
    {
        private readonly ILogger<OneTask> _logger;
        private Timer _timer;
        public bool IsRunning { get; private set; }

        public OneTask(ILogger<OneTask> logger)
        {
            _logger = logger;
            IsRunning = false;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                IsRunning = true;
                _logger.LogInformation("ФЗ 1 активна");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
            IsRunning = false;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Program.Logger.Info("ФЗ 1 остановлена");
            _timer?.Dispose();
            IsRunning = false;
            await base.StopAsync(stoppingToken);
        }
        public void SetIsRunning(bool value)
        {
            IsRunning = value;
        }
    }
}
