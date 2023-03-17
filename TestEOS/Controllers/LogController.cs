using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Threading;
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
            return View();
        }

        public IActionResult Start() 
        {
            _hosted.StartAsync(CancellationToken.None);
            return new EmptyResult();
        }

        public IActionResult Stop()
        {
            _hosted.StopAsync(CancellationToken.None);
            return new EmptyResult();
        }

    }
}
