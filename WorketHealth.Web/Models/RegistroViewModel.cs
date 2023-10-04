using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WorketHealth.Web.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "El campo Nombre de Usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo de Email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(50,ErrorMessage = "El {0} debe estar enrte al menos {2} caracteres de longitud",MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La Confirmacion de contraseña es obligatoria")]
        [Compare("Password", ErrorMessage = "La contraseña y confirmacion de contraseña no coinciden")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }

        //Para seleccion de roles
        [Display(Name = "Seleccionar Rol")]
        public IEnumerable<SelectListItem> ListaRoles { get; set; }
        [Display(Name = "Rol seleccionado")]
        public string RolSeleccionado { get; set; }

    }
}
