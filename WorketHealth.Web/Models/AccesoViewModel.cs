using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WorketHealth.Web.Models
{
    public class AccesoViewModel
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordar Datos")]
        public bool RememberMe { get; set; }

    }
}
