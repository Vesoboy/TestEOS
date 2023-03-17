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
                    var filePath = @"C:\Users\Ivan\Desktop\TestEOS\Logs\log.log";
                    var backupFilePath = $@"C:\Users\Ivan\Desktop\TestEOS\Logs\logs\log{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";

                    // Write the file backup information to the log
                    _logger.LogInformation($"Перемещение файла из:\n{filePath}\n" +
                        $"файл перемещен сюда:\n{backupFilePath}");

                    // Move the current log file to a backup file with a timestamp
                    File.Move(filePath, backupFilePath);

                    // Create a new empty log file
                    File.WriteAllText(filePath, string.Empty);

                    // Log that the log file was reset
                    _logger.LogInformation($"Лог файл был сброшен и перемещен:\n{filePath}");

                    _oneTask.SetIsRunning (true);
                }
            }

            //private async Task WriteLogToFileAsync(string filePath)
            //{
            //    var logContent = await File.ReadAllTextAsync(filePath);
            //    var backupFilePath = Path.Combine(_backupDirectoryPath, $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");

            //    await File.WriteAllTextAsync(backupFilePath, logContent);
            //}

            //private async Task ClearLogAsync(string filePath, string backupDirectoryPath)
            //{
            //    var backupFilePath = Path.Combine(backupDirectoryPath, $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");

            //    if (File.Exists(filePath))
            //    {
            //        File.Move(filePath, backupFilePath);
            //    }

            //    await File.WriteAllTextAsync(filePath, "");
            //}
        }

    }
}
