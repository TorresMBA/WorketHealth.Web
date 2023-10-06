using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorketHealth.Web.Models
{
    public class AppUsuario: IdentityUser
    {
        //Nuevas propiedades para usar role y asignación de un rol a un usuario
        [NotMapped]
        [Display(Name ="Rol para el usuario")]
        public string ?IdRol { get; set; }
        [NotMapped]
        public string ?Rol { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> ?ListaRoles { get; set; }
    }
}
