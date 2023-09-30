using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorketHealth.Services;
using WorketHealth.Web.Models;

namespace WorketHealth.Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ConsumeData consume = new ConsumeData();
            var dataDB = consume.Consumiendo();
            string pintaEnPantalla = $"Este valor viene de DataAccess: {dataDB}";      

            return View((object)pintaEnPantalla);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}