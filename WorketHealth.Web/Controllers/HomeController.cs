using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models;
using WorketHealth.DataAccess.Models.Test;
using WorketHealth.Domain.Interfaces.Fecha;
using WorketHealth.Services.Services.Empresa;

namespace WorketHealth.Web.Controllers {
    public class HomeController : Controller {
        private readonly WorketHealthContext _dbContext;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUsuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUsuario> _signInManager;
        private readonly IMesService _mesService;
        private readonly IAnhoService _anhoService;

        public HomeController(WorketHealthContext dbContext, ILogger<HomeController> logger, UserManager<AppUsuario> userManager , SignInManager<AppUsuario> signInManager, RoleManager<IdentityRole> roleManager, IMesService mesService, IAnhoService anhoService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mesService = mesService;
            _anhoService = anhoService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Anhos = _anhoService.GetAnhos();
            ViewBag.Meses = _mesService.GetMeses();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // Para la cracion de los roles
         //  // if (!await _roleManager.RoleExistsAsync("Administrador"))
         //  // {
         //  //     //Creacion de rol administrador
         //  //     await _roleManager.CreateAsync(new IdentityRole("Administrador"));
         //  //
         //  //
         //  //     //Creacion de Usuarios
         //  //     await CreateUserAndAssignRole(_userManager, "Administrador", "Administrador@example.com", "Qwer@123?", "Administrador");
         //  //     await CreateUserAndAssignRole(_userManager, "Desarrollador1", "Desarrollador1@example.com", "Qwer@123?", "Administrador");
         //  //     await CreateUserAndAssignRole(_userManager, "Desarrollador2", "Desarrollador2@example.com", "Qwer@123?", "Administrador");
         //  //
         //  //
         //  // }
         //  // if (!await _roleManager.RoleExistsAsync("Desarrollador"))
         //  // {
         //  //     //Creacion de rol administrador
         //  //     await _roleManager.CreateAsync(new IdentityRole("Desarrollador"));
         //  // }
         //  // if (!await _roleManager.RoleExistsAsync("Visitante"))
         //  // {
         //  //     //Creacion de rol administrador
         //  //     await _roleManager.CreateAsync(new IdentityRole("Visitante"));
         //  // }
         //  // if (!await _roleManager.RoleExistsAsync("Registrado"))
         //  // {
         //  //     //Creacion de rol usuario registrado
         //  //     await _roleManager.CreateAsync(new IdentityRole("Registrado"));
         //  // }

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


        public IActionResult resumenTipoExamen(int? mes, int? anio)
        {
            if (!mes.HasValue)
            {
                mes = DateTime.Now.Month;
            }
            // Verificar si el anio es nulo y asignar el valor del anio actual
            if (!anio.HasValue)
            {
                anio = DateTime.Now.Year;
            }
            var listaTipoExamenDelMes = ObtenerListaTipoExamenDelMes(mes.ToString(), anio.ToString());

            if (listaTipoExamenDelMes.Count == 0)
            {
                var ListadoTipoRegistro = new List<ChartDonut> { new ChartDonut { tipoExamen = "Sin Data", nroTipo = 0 } };
                return StatusCode(StatusCodes.Status200OK, ListadoTipoRegistro);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, listaTipoExamenDelMes);
            }

        }

        private List<ChartDonut> ObtenerListaTipoExamenDelMes(string? mes, string? anio)
        {
            // Consulta LINQ para obtener la lista deseada
            var resultado = from seguimiento in _dbContext.SeguimientoMedicos
                            join tipoExamen in _dbContext.TipoExamenes on seguimiento.ID_TIPO_EXAMEN equals tipoExamen.ID_TIPO
                            where (string.IsNullOrEmpty(mes) || seguimiento.MES == mes) && seguimiento.ANHO == anio
                            group new { tipoExamen } by new { tipoExamen.COD, tipoExamen.DESCRIPCION } into g
                            select new ChartDonut
                            {
                                tipoExamen = g.Key.COD,
                                nroTipo = g.Count() // Cambiado a Count() ya que no proporcionaste un campo específico para sumar
                            };

            return resultado.ToList();
        }


        public IActionResult resumenSexo(int? mes, int? anio)
        {
            if (!mes.HasValue)
            {
                mes = DateTime.Now.Month;
            }
            // Verificar si el anio es nulo y asignar el valor del anio actual
            if (!anio.HasValue)
            {
                anio = DateTime.Now.Year;
            }
            var listaSexoDelMes = ObtenerListaSexoDelMes(mes.ToString(), anio.ToString());

            if (listaSexoDelMes.Count == 0)
            {
                var ListadoTipoRegistroSexo = new List<ChartDonut> { new ChartDonut { tipoExamen = "Sin Data", nroTipo = 0 } };
                return StatusCode(StatusCodes.Status200OK, ListadoTipoRegistroSexo);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, listaSexoDelMes);
            }

        }

        private List<ChartDonut> ObtenerListaSexoDelMes(string? mes, string? anio)
        {
            // Consulta LINQ para obtener la lista deseada
            var resultado = from seguimiento in _dbContext.SeguimientoMedicos
                            join tipoExamen in _dbContext.TipoExamenes on seguimiento.ID_TIPO_EXAMEN equals tipoExamen.ID_TIPO
                            where (string.IsNullOrEmpty(mes) || seguimiento.MES == mes) && seguimiento.ANHO == anio
                            group new { tipoExamen } by new { tipoExamen.COD, tipoExamen.DESCRIPCION } into g
                            select new ChartDonut
                            {
                                tipoExamen = g.Key.COD,
                                nroTipo = g.Count() // Cambiado a Count() ya que no proporcionaste un campo específico para sumar
                            };

            return resultado.ToList();
        }
    }
}