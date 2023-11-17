using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models.Fecha;
using WorketHealth.DataAccess.Models.Test;
using WorketHealth.Domain.Interfaces.Fecha;

namespace WorketHealth.Web.Controllers
{
    public class TrujilloController : Controller
    {
        private readonly WorketHealthContext _dbContext;
        private readonly IMesService _mesService;
        private readonly IAnhoService _anhoService;

        public TrujilloController(WorketHealthContext dbContext, IMesService mesService, IAnhoService anhoService)
        {
            _dbContext = dbContext;
            _mesService = mesService;
            _anhoService = anhoService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //    // Obtiene la fecha y hora actual.
            //    DateTime fechaActual = DateTime.Now;
            //
            //    // Obtiene el mes actual en formato de 2 dígitos (por ejemplo, "01" para enero, "02" para febrero, etc.).
            //    string mesActual = fechaActual.ToString("MM");
            //
            //    // Obtiene el año actual en formato de 4 dígitos (por ejemplo, "2023").
            //    string añoActual = fechaActual.ToString("yyyy");
            //
            //    // Puedes pasar los valores del mes y el año a la vista si es necesario.
            //    ViewBag.MesActual = mesActual;
            //    ViewBag.AñoActual = añoActual;
            //
            //    // Llama al método ListadoDeMes para obtener la lista de meses.
            //    List<SelectListItem> listaMes = ListadoDeMes();
            //
            //    List<SelectListItem> listaAnhos = _dbContext.Anho
            //        .Select(a => new SelectListItem { Value = a.Year, Text = a.Year })
            //            .ToList();
            //
            //
            //    Models.ProyectoViewModel registroVM = new Models.ProyectoViewModel()
            //    {
            //        ListaMes = listaMes,
            //        ListaAnho = listaAnhos
            //    };
            //    
            ViewBag.Anhos = _anhoService.GetAnhos();
            ViewBag.Meses = _mesService.GetMeses();

            // Recupera los datos de la base de datos
            // var chartData = _context.ChartData.ToList();
            // //var chartData = GenerateDummyData();
            // Convierte los datos a un formato que Morris.js pueda utilizar (JSON)
           // // var chartDataJson = JsonConvert.SerializeObject(chartData);

        

          //  ViewData["testData"] = testData;

            return View();
        }


        public IActionResult resumenNroRegistro()
        {
            var today = DateTime.Today;
            var lastSixMonths = Enumerable.Range(0, 6)
                .Select(i => today.AddMonths(-i))
                .ToList();
            var ListadoNroRegistro = new List<ChartData>
            {
                new ChartData { y = "enero" , a = 10 , b = 10 },
                new ChartData { y = "febrero" , a = 65 , b = 0 },
                new ChartData { y = "marzo" , a = 50 , b = 20 },
                new ChartData { y = "abril" , a = 75 , b = 20 },
                new ChartData { y = "mayo" , a = 50 , b = 20 },
                new ChartData { y = "junio" , a = 75 , b = 20 },
                new ChartData { y = "julio" , a = 75 , b = 30 },
                new ChartData { y = "agosto" , a = 75 , b = 40 },
                new ChartData { y = "septiembre" , a = 75 , b = 100 },
                new ChartData { y = "octubre" , a = 75 , b = 60 },
                new ChartData { y = "noviembre" , a = 75 , b = 55 },
                new ChartData { y = "diciembre" , a = 75 , b = 20 }
            };

            var ListaNroRegistro = lastSixMonths.Select((date, index) =>
            {
                var monthName = date.ToString("MMMM");
                var manualEntry = ListadoNroRegistro.FirstOrDefault(entry => entry.y == monthName);

                return new ChartData
                {
                    y = monthName,
                    a = manualEntry != null ? manualEntry.a : 0,
                    b = manualEntry != null ? manualEntry.b : 0
                };
            }).Reverse().ToList();


            return StatusCode(StatusCodes.Status200OK, ListaNroRegistro);

        }

        public IActionResult resumenTipoExamen(int? mes, int? anio) {

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
                var ListadoTipoRegistro = new List<ChartDonut> { new ChartDonut { tipoExamen = "Sin Data", nroTipo = 1 } };
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
                                //DescripcionTipoExamen = g.Key.DESCRIPCION,
                                nroTipo = g.Count() // Cambiado a Count() ya que no proporcionaste un campo específico para sumar
                            };

            return resultado.ToList();
        }

    }
}
