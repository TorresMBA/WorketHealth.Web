using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorketHealth.Services;
using WorketHealth.Services.Services.Empresa;
using WorketHealth.Web.Models;

namespace WorketHealth.Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ConsumeData consume = new ConsumeData();
            var dataDB = consume.Consumiendo();
            string pintaEnPantalla = $"Este valor viene de DataAccess: {dataDB}";

            CompanyServices prueba = new CompanyServices();
            var result = await prueba.tester();

            return View(result);
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