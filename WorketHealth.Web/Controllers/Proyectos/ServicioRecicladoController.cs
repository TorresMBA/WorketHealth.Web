using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models.Charts;
using WorketHealth.DataAccess.Models.Fecha;
using WorketHealth.DataAccess.Models.Registros;
using WorketHealth.DataAccess.Models.Tablas;
using WorketHealth.DataAccess.Models.Test;
using WorketHealth.Domain.Interfaces.Fecha;

namespace WorketHealth.Web.Controllers.Proyectos
{
    [Authorize]
    public class ServicioRecicladoController : Controller
    {
        private readonly WorketHealthContext _dbContext;
        private readonly IMesService _mesService;
        private readonly IAnhoService _anhoService;

        public ServicioRecicladoController(WorketHealthContext dbContext, IMesService mesService, IAnhoService anhoService)
        {
            _dbContext = dbContext;
            _mesService = mesService;
            _anhoService = anhoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Anhos = _anhoService.GetAnhos();
            ViewBag.Meses = _mesService.GetMeses();
            return View();
        }

        public IActionResult resumenNroRegistro(int? mes, int? anio)
        {
            int mesActual = mes ?? DateTime.Now.Month;
            int anhoActual = anio ?? DateTime.Now.Year;

            List<DateTime> ultimos6Meses = Enumerable.Range(0, 6)
                .Select(i => new DateTime(anhoActual, mesActual, 1).AddMonths(-i))
                .ToList();

            var resultado = ultimos6Meses
                .Select(fecha =>
                    new registrosF_SEG_19
                    {
                        mes = fecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES")),
                        cantidad = _dbContext.SeguimientoMedicos.Count(x =>
                            x.MES == fecha.Month.ToString() &&
                            x.ANHO == fecha.Year.ToString() &&
                            x.RUC == "3")
                    }).Reverse()
                .ToList();
            return StatusCode(StatusCodes.Status200OK, resultado);
        }

        public IActionResult resumenTipoExamen(string? mes, string? anio)
        {
            var listaTipoExamenDelMes = ObtenerListaTipoExamenDelMes(mes, anio);
            if (listaTipoExamenDelMes.Count == 0)
            {
                var ListadoTipoRegistro = new List<ChartTipoExamen> { new ChartTipoExamen { tipoExamen = "Sin Data", nroTipo = 0 } };
                return StatusCode(StatusCodes.Status200OK, ListadoTipoRegistro);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, listaTipoExamenDelMes);
            }
        }
        private List<ChartTipoExamen> ObtenerListaTipoExamenDelMes(string? mes, string? anio)
        {
            var resultado = from seguimiento in _dbContext.SeguimientoMedicos
                            join tipoExamen in _dbContext.TipoExamenes on seguimiento.ID_TIPO_EXAMEN equals tipoExamen.ID_TIPO
                            where (string.IsNullOrEmpty(mes) || seguimiento.MES == mes) && (anio == null || seguimiento.ANHO == anio) && seguimiento.RUC == "3"
                            group new { tipoExamen } by new { tipoExamen.COD, tipoExamen.DESCRIPCION } into g
                            select new ChartTipoExamen
                            {
                                tipoExamen = g.Key.COD,
                                nroTipo = g.Count()
                            };
            return resultado.ToList();
        }

        public IActionResult resumenAptitud(int? mes, int? anio)
        {
            var listaTipoExamenDelMes = ObtenerListaAptitudDelMes(mes.ToString(), anio.ToString());
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
                            where (string.IsNullOrEmpty(mes) || seguimiento.MES == mes) && (anio == null || seguimiento.ANHO == anio) && seguimiento.RUC == "3"
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
                var ListadoTipoRegistro = new List<ChartEC10> { new ChartEC10 { codEC = "Sin Data", descEC = "Sin Data",  cantidad = 0 } };
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
                            .Where(r => (mes == null || r.MES == mes) && (anio == null || r.ANHO == anio) && r.RUC == "3")
                            .SelectMany(r => r.Enfermedades)
                            .GroupBy(EnfermedadComun => new { EnfermedadComun.EnfermedadComun.COD, EnfermedadComun.EnfermedadComun.DESCRIPCION })
                            .Select(group => new ChartEC10
                            {
                                codEC = group.Key.COD,
                                descEC = group.Key.DESCRIPCION,
                                cantidad = group.Count()
                            })
                            .OrderByDescending(ChartEC10 => ChartEC10.cantidad)
                            .Take(10)
                            .ToList();
            return resultado.ToList();
        }

        public IActionResult resumenoF_SEG_19(string? mes, string? anio)
        {
            var listaF_SEG_19DelMes = ObtenerListaF_SEG_19DelMes(mes, anio);

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
                            .Where(e => (mes == null || e.MES == mes) && (anio == null || e.ANHO == anio) && e.RUC == "3")
                            .GroupBy(e => new { MES = e.MES, ANHO = e.ANHO })
                            .Where(group => group.Count() > 1)
                            .Select(group => new SeguimientoMedico { MES = group.Key.MES, ANHO = group.Key.ANHO, Cantidad = group.Count() })
                            .ToList();

            return resultado.ToList();
        }

        public IActionResult ObtenerListadoEdades(string? mes, string? anio)
        {
            var query = _dbContext.Personal.AsQueryable();
            string ruc = "3";
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
                new ChartsEdades { rangoEdad = "Edad 56-100 ", cantidad = edades.Count(e => e >= 56 && e <= 100) }
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
