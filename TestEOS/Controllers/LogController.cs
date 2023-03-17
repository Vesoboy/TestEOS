using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TestEOS.OutputLogs;

namespace TestEOS.Controllers
{
    public class LogController : Controller
    {

        private readonly ILogger<LogController> _logger;
        private static IHostedService _hosted;

        public LogController(ILogger<LogController> logger, IHostedService hosted)
        {
            _logger = logger;
            _hosted = hosted;
        }

        public IActionResult Index()
        {
            return RedirectToAction("index", "home");
        }

        public IActionResult Start() 
        {
            //var oneTaskLog = HttpContext.RequestServices.GetService<IHostedService>();
            //oneTaskLog.StartAsync(HttpContext.RequestAborted);

            
            _hosted.StartAsync(CancellationToken.None);
            return View();
        }

        public IActionResult Stop()
        {
            //var oneTaskLog = HttpContext.RequestServices.GetService<IHostedService>();
            //oneTaskLog.StartAsync(HttpContext.RequestAborted);

            
            _hosted.StopAsync(CancellationToken.None);
            return View();
        }

    }
}
