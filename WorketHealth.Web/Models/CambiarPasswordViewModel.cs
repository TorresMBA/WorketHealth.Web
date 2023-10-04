using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WorketHealth.Web.Models
{
    public class CambiarPasswordViewModel
    {
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(50, ErrorMessage = "El {0} debe estar enrte al menos {2} caracteres de longitud", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La Confirmacion de contraseña es obligatoria")]
        [Compare("Password", ErrorMessage = "La contraseña y confirmacion de contraseña no coinciden")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
