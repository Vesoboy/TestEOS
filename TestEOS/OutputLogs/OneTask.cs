using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Hosting;

namespace TestEOS.OutputLogs
{
    public class OneTask: IHostedService//, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<OneTask> _logger;
        private Timer _timer;

        public OneTask(ILogger<OneTask> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));
            Program.Logger.Info("ФЗ 1 активно");
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            //_timer = null;//?.Change(Timeout.Infinite, 0);
            
            Program.Logger.Info("ФЗ 1 остановлено");
            //_timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
            return Task.CompletedTask;
        }

        //public void Dispose()
        //{
        //    _timer?.Dispose();
        //}
    }
}
