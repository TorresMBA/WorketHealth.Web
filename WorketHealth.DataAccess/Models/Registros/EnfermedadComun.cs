using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorketHealth.DataAccess.Models.Registros
{
    public class EnfermedadComun
    {
        [Key]
        public int ID_ENFERMEDAD { get; set; }
        public string? COD { get; set; }
        public string? DESCRIPCION { get; set; }

        public ICollection<SeguimientoEnfermedad>? SeguimientoMedicos { get; set; }
    }
}
