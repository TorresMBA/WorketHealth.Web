using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorketHealth.DataAccess.Models.Registros
{
    public class EnfermedadComun
    {
        [Key]
        [NotMapped]
        public int Id_Enfermedad { get; set; }
        [NotMapped]
        public string? Cod { get; set; }
        [NotMapped]
        public string? Descripcion { get; set; }
    }
}
