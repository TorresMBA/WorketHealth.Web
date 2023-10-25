using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models;
using WorketHealth.Services.Services.Empresa;

namespace WorketHealth.Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUsuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUsuario> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUsuario> userManager , SignInManager<AppUsuario> signInManager, RoleManager<IdentityRole> roleManager)
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


                //Creacion de Usuarios
                await CreateUserAndAssignRole(_userManager, "Administrador", "Administrador@example.com", "Qwer@123?", "Administrador");
                await CreateUserAndAssignRole(_userManager, "Desarrollador1", "Desarrollador1@example.com", "Qwer@123?", "Administrador");
                await CreateUserAndAssignRole(_userManager, "Desarrollador2", "Desarrollador2@example.com", "Qwer@123?", "Administrador");


            }
            if (!await _roleManager.RoleExistsAsync("Desarrollador"))
            {
                //Creacion de rol administrador
                await _roleManager.CreateAsync(new IdentityRole("Desarrollador"));
            }
            if (!await _roleManager.RoleExistsAsync("Visitante"))
            {
                //Creacion de rol administrador
                await _roleManager.CreateAsync(new IdentityRole("Visitante"));
            }
            if (!await _roleManager.RoleExistsAsync("Registrado"))
            {
                //Creacion de rol usuario registrado
                await _roleManager.CreateAsync(new IdentityRole("Registrado"));
            }

            return View();
        }
        private async Task CreateUserAndAssignRole(UserManager<AppUsuario> userManager, string username, string email, string password, string roleName)
        {
            if (userManager.FindByNameAsync(username).Result == null)
            {
                // Crear usuario
                var user = new AppUsuario
                {
                    UserName = username.Replace(" ", ""),
                    Email = email
                };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    // Asignar el rol al usuario
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Models.AccesoViewModel accViewModel)
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
            return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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