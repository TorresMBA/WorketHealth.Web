using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace WorketHealth.Web.Models
{
    public class ProyectoViewModel
    {
        //Para seleccion de Mes
        [Display(Name = "Seleccionar Mes")]
        public IEnumerable<SelectListItem>? ListaMes { get; set; }

        public string? MesSeleccionado { get; set; }
        //Para seleccion de Anho
        [Display(Name = "Seleccionar Mes")]
        public IEnumerable<SelectListItem>? ListaAnho { get; set; }

        public string? AnhoSeleccionado { get; set; }
    }
}
