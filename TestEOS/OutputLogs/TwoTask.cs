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
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                if (!_oneTask.IsRunning)
                {
                    var date = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";

                    string logPath = Path.Combine("C:", "Users", "Ivan", "Desktop", "TestEOS", "TestEOS", "Logs");
                    var logFilePath = Path.Combine(logPath, "log.log");

                    string logPathBackup = Path.Combine("C:", "Users", "Ivan", "Desktop", "TestEOS", "TestEOS", "Logs", "logs");
                    var logBackupFilePath = Path.Combine(logPathBackup, $"log{date}.log");  

                    File.Move(logFilePath, logBackupFilePath);

					_logger.LogInformation($@"Лог файл был сброшен и перемещен сюда:{"\n"+ logPathBackup + "\n"}Под именем {"\n"}log{date}.log");

                    _oneTask.SetIsRunning (true);
                }
            }
        }
    }
}
