using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models;
using WorketHealth.DataAccess.Models.Charts;
using WorketHealth.DataAccess.Models.Personal;
using WorketHealth.DataAccess.Models.Registros;
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
                var ListadoTipoRegistro = new List<ChartTipoExamen> { new ChartTipoExamen { tipoExamen = "Sin Data", nroTipo = 1 } };
                return StatusCode(StatusCodes.Status200OK, ListadoTipoRegistro);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, listaTipoExamenDelMes);
            }

        }
        private List<ChartTipoExamen> ObtenerListaTipoExamenDelMes(string? mes, string? anio)
        {
            // Consulta LINQ para obtener la lista deseada
            var resultado = from seguimiento in _dbContext.SeguimientoMedicos
                            join tipoExamen in _dbContext.TipoExamenes on seguimiento.ID_TIPO_EXAMEN equals tipoExamen.ID_TIPO
                            where (string.IsNullOrEmpty(mes) || seguimiento.MES == mes) && seguimiento.ANHO == anio
                            group new { tipoExamen } by new { tipoExamen.COD, tipoExamen.DESCRIPCION } into g
                            select new ChartTipoExamen
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
            var listaSexoDelMes = ObtenerListaSexoDelMes();

            if (listaSexoDelMes.Count == 0)
            {
                var ListadoTipoRegistroSexo = new List<ChartsPersonal> { new ChartsPersonal { genero = "Sin Data", cantidad = 0 } };
                return StatusCode(StatusCodes.Status200OK, ListadoTipoRegistroSexo);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, listaSexoDelMes);
            }

        }
        private List<ChartsPersonal> ObtenerListaSexoDelMes()
        {
            // Consulta LINQ para obtener la lista deseada
            var resultado = _dbContext.Personal.GroupBy(x => x.Sexo)
                            .Where(group => group.Count() > 1)
                            .Select(group => new ChartsPersonal { 
                                genero = group.Key == "M" ? "Masculino" : (group.Key == "F" ? "Femenino" : "Otros"), 
                                cantidad = group.Count() })
                            .ToList();

            return resultado.ToList();
        }

        public IActionResult resumenoF_SEG_19(int? mes, int? anio)
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

            var listaF_SEG_19DelMes = ObtenerListaF_SEG_19DelMes(mes.ToString(), anio.ToString());

            if (listaF_SEG_19DelMes.Count == null)
            {
                var nuevoElemento = new SeguimientoMedico { MES = mes.ToString(), ANHO = anio.ToString(), Cantidad = 1 };
                return StatusCode(StatusCodes.Status200OK, new List<SeguimientoMedico> { nuevoElemento });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, listaF_SEG_19DelMes);
            }
        }

        private List<SeguimientoMedico> ObtenerListaF_SEG_19DelMes(string? mes, string? anio)
        {
            // Consulta LINQ para obtener la lista deseada
            var resultado = _dbContext.SeguimientoMedicos
                            .Where(e => (mes == null || e.MES == mes) && (anio == null || e.ANHO == anio))
                            .GroupBy(e => new { MES = e.MES, ANHO = e.ANHO })
                            .Where(group => group.Count() > 1)
                            .Select(group => new SeguimientoMedico { MES = group.Key.MES, ANHO = group.Key.ANHO, Cantidad = group.Count() })
                            .ToList();

            return resultado.ToList();
        }

    }
}