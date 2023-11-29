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

        [Authorize]
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
            return View();
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

        // ---------------- Datos de Grafico ------------------
        public IActionResult resumenTipoExamen(string? mes, string? anio)
        {
            var listaTipoExamenDelMes = ObtenerListaTipoExamenDelMes(mes, anio);

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
                            where (string.IsNullOrEmpty(mes) || seguimiento.MES == mes) && (anio == null || seguimiento.ANHO == anio)
                            group new { tipoExamen } by new { tipoExamen.COD, tipoExamen.DESCRIPCION } into g
                            select new ChartTipoExamen
                            {
                                tipoExamen = g.Key.COD,
                                nroTipo = g.Count() // Cambiado a Count() ya que no proporcionaste un campo específico para sumar
                            };

            return resultado.ToList();
        }

        public IActionResult resumenSexo(string? mes, string? anio)
        {           
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

        public IActionResult resumenoF_SEG_19(string? mes, string? anio)
        {
            var listaF_SEG_19DelMes = ObtenerListaF_SEG_19DelMes(mes, anio);

            if (listaF_SEG_19DelMes.Count == null)
            {
                var nuevoElemento = new SeguimientoMedico { MES = mes, ANHO = anio, Cantidad = 1 };
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

        public IActionResult resumenAptitud(string? mes, string? anio)
        {
            var listaTipoExamenDelMes = ObtenerListaAptitudDelMes(mes, anio);
            if (listaTipoExamenDelMes.Count == 0)
            {
                var ListadoTipoRegistro = new List<ChartAptitud> { new ChartAptitud { codAptitud = "Sin Data", cantidad = 0 } };
                return StatusCode(StatusCodes.Status200OK, ListadoTipoRegistro);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, listaTipoExamenDelMes);
            }
        }
        private List<ChartAptitud> ObtenerListaAptitudDelMes(string? mes, string? anio)
        {
            var resultado = from seguimiento in _dbContext.SeguimientoMedicos
                            join tipoExamen in _dbContext.Aptitudes on seguimiento.ID_TIPO_EXAMEN equals tipoExamen.ID_APTITUD
                            where (string.IsNullOrEmpty(mes) || seguimiento.MES == mes) && (anio == null || seguimiento.ANHO == anio)
                            group new { tipoExamen } by new { tipoExamen.COD, tipoExamen.DESCRIPCION } into g
                            select new ChartAptitud
                            {
                                codAptitud = g.Key.COD,
                                cantidad = g.Count()
                            };
            return resultado.ToList();
        }

        public IActionResult nro10EnfermedadComun(string? mes, string? anio)
        {
            var listaTipoExamenDelMes = ObtenerLista10ECDelMes(mes, anio);
            if (listaTipoExamenDelMes.Count == 0)
            {
                var ListadoTipoRegistro = new List<ChartEC10> { new ChartEC10 { codEC = "Sin Data", descEC = "Sin Data", cantidad = 0 } };
                return StatusCode(StatusCodes.Status200OK, ListadoTipoRegistro);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, listaTipoExamenDelMes);
            }
        }
        private List<ChartEC10> ObtenerLista10ECDelMes(string? mes, string? anio)
        {
            var resultado = _dbContext.SeguimientoMedicos
                            .Where(r => (string.IsNullOrEmpty(mes) || r.MES == mes) && (string.IsNullOrEmpty(anio) || r.ANHO == anio))
                            .SelectMany(r => r.Enfermedades)
                            .GroupBy(ec => new { ec.EnfermedadComun.COD, ec.EnfermedadComun.DESCRIPCION })
                            .Select(group => new ChartEC10
                            {
                                codEC = group.Key.COD,
                                descEC = group.Key.DESCRIPCION,
                                cantidad = group.Count()
                            })
                            .OrderByDescending(chartEC10 => chartEC10.cantidad)
                            .Take(10)
                            .ToList();

            // Asignar la cantidad al objeto SeguimientoMedico
            foreach (var item in resultado)
            {
                // Encuentra el SeguimientoMedico correspondiente y asigna la cantidad
                var seguimientoMedico = _dbContext.SeguimientoMedicos
                .FirstOrDefault(r => r.Enfermedades.Any(ec => ec.EnfermedadComun.COD == item.codEC));


                if (seguimientoMedico != null)
                {
                    seguimientoMedico.Cantidad = item.cantidad;
                }
            }
            return resultado.ToList();
        }

        public IActionResult ObtenerListadoEdades(string? mes, string? anio)
        {
            var query = _dbContext.Personal.AsQueryable();
            string ruc = null;
            DateTime? fechaFin = null;

            if (int.TryParse(mes, out int mesNumerico) && int.TryParse(anio, out int añoNumerico))
            {
                // Crear un objeto DateTime con el día siempre igual a 1
                fechaFin = new DateTime(añoNumerico, mesNumerico, 1);
            }

            // Aplicar filtros solo si se proporcionan valores no nulos
            if (!string.IsNullOrEmpty(ruc))
            {
                query = query.Where(p => p.Ruc == ruc);
            }

            if (fechaFin.HasValue)
            {
                query = query.Where(p => p.Fec_Nacimiento <= fechaFin.Value);
            }

            var personalFiltrado = query.ToList();

            var edades = personalFiltrado
                .Select(p => CalcularEdad(p.Fec_Nacimiento, fechaFin ?? DateTime.Now))
                .ToList();

            var resultado = new List<ChartsEdades>
            {
                new ChartsEdades { rangoEdad = "Edad 18-25 ", cantidad = edades.Count(e => e >= 18 && e <= 25) },
                new ChartsEdades { rangoEdad = "Edad 26-35 ", cantidad = edades.Count(e => e >= 26 && e <= 35) },
                new ChartsEdades { rangoEdad = "Edad 36-45 ", cantidad = edades.Count(e => e >= 36 && e <= 45) },
                new ChartsEdades { rangoEdad = "Edad 46-55 ", cantidad = edades.Count(e => e >= 46 && e <= 55) },
                new ChartsEdades { rangoEdad = "Edad 56-99 ", cantidad = edades.Count(e => e >= 56 && e <= 100) }
            };
            return StatusCode(StatusCodes.Status200OK, resultado);

        }
        private int CalcularEdad(DateTime fechaNacimiento, DateTime fechaFin)
        {
            int edad = fechaFin.Year - fechaNacimiento.Year;

            if (fechaFin.Month < fechaNacimiento.Month || (fechaFin.Month == fechaNacimiento.Month && fechaFin.Day < fechaNacimiento.Day))
            {
                edad--;
            }

            return edad;
        }
    }
}