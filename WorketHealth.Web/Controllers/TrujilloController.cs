using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorketHealth.DataAccess;

namespace WorketHealth.Web.Controllers
{
    public class TrujilloController : Controller
    {
        private readonly WorketHealthContext _dbContext;

        public TrujilloController(WorketHealthContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Obtiene la fecha y hora actual.
            DateTime fechaActual = DateTime.Now;

            // Obtiene el mes actual en formato de 2 dígitos (por ejemplo, "01" para enero, "02" para febrero, etc.).
            string mesActual = fechaActual.ToString("MM");

            // Obtiene el año actual en formato de 4 dígitos (por ejemplo, "2023").
            string añoActual = fechaActual.ToString("yyyy");

            // Puedes pasar los valores del mes y el año a la vista si es necesario.
            ViewBag.MesActual = mesActual;
            ViewBag.AñoActual = añoActual;

            // Llama al método ListadoDeMes para obtener la lista de meses.
            List<SelectListItem> listaMes = ListadoDeMes();

            List<SelectListItem> listaAnhos = _dbContext.Anho
                .Select(a => new SelectListItem { Value = a.Year, Text = a.Year })
                    .ToList();


            Models.ProyectoViewModel registroVM = new Models.ProyectoViewModel()
            {
                ListaMes = listaMes,
                ListaAnho = listaAnhos
            };            

            return View(registroVM);
        }

        private List<SelectListItem> ListadoDeMes()
        {
            List<SelectListItem> listaMes = new List<SelectListItem>();
            listaMes.Add(new SelectListItem()
            {
                Value = "01",
                Text = "Enero"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "02",
                Text = "Febrero"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "03",
                Text = "Marzo"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "04",
                Text = "Abril"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "05",
                Text = "Mayo"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "06",
                Text = "Junio"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "07",
                Text = "Julio"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "08",
                Text = "Agosto"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "09",
                Text = "Septiembre"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "10",
                Text = "Octubre"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "11",
                Text = "Noviembre"
            });

            listaMes.Add(new SelectListItem()
            {
                Value = "12",
                Text = "Diciembre"
            });
            // Agrega el resto de los meses aquí
            return listaMes;
        }
    }
}
