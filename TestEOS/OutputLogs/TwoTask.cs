using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Logging;

namespace TestEOS.OutputLogs
{
    public class TwoTask : BackgroundService
    {
        private readonly ILogger<TwoTask> _logger;
        private readonly OneTask _oneTask;

        public TwoTask(ILogger<TwoTask> logger, OneTask oneTask)
        {
            _logger = logger;
            _oneTask = oneTask;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                if (!_oneTask.IsRunning)
                {
                    var logFilePath = @"C:\Users\Ivan\Desktop\TestEOS\Logs\log.log";
                    var logBackupFilePath = $@"C:\Users\Ivan\Desktop\TestEOS\Logs\logs\log{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";

                    File.Move(logFilePath, logBackupFilePath);

                    _logger.LogInformation($"Лог файл был сброшен и перемещен сюда:\n{logFilePath}");

                    _oneTask.SetIsRunning (true);
                }
            }
        }
    }
}
