using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorketHealth.Services;
using WorketHealth.Services.Services.Empresa;
using WorketHealth.Web.Models;

namespace WorketHealth.Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var dataDB = 123;
            string pintaEnPantalla = $"Este valor viene de DataAccess: {dataDB}";

            CompanyServices prueba = new CompanyServices();
            var result = await prueba.tester();

            return View(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // Para la cracion de los roles
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                //Creacion de rol administrador
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));
            }
            if (!await _roleManager.RoleExistsAsync("Registrado"))
            {
                //Creacion de rol usuario registrado
                await _roleManager.CreateAsync(new IdentityRole("Registrado"));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccesoViewModel accViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(accViewModel.Email);

                if(user == null)
                {
                    ModelState.AddModelError(String.Empty, "Acceso Invalido");
                    return View(accViewModel);
                }

                var resultado = await _signInManager.PasswordSignInAsync(user, accViewModel.Password, accViewModel.RememberMe, lockoutOnFailure: true);                

                if (resultado.Succeeded)
                {
                    // Usuario autenticado correctamente
                    // Ahora, obtén los roles asignados al usuario
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Count > 0)
                    {
                        // Aquí puedes usar los roles de alguna manera
                        // Puedes almacenarlos en una variable o mostrarlos en la vista, por ejemplo
                        TempData["UserRole"] = string.Join(", ", roles);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if (resultado.IsLockedOut)
                {
                    return View("Bloqueado");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Acceso Invalido");
                    return View(accViewModel);
                }
            }
            return View(accViewModel);
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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Bloquedo(string returnurl =null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            return View();
        }
    }
}