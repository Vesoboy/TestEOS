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
        private static OneTask _hosted;

        public LogController( OneTask hosted)
        {
            _hosted = hosted;
        }

        public IActionResult Index()
        {
            return RedirectToAction("index", "home");
        }

        public IActionResult Start() 
        {
            _hosted.StartAsync(CancellationToken.None);
            return View();
        }

        public IActionResult Stop()
        {
            _hosted.StopAsync(CancellationToken.None);
            return View();
        }

    }
}
