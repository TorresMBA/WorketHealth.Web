using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WorketHealth.DataAccess.Models.Registros
{
    public class Seguimiento_EC
    {
        [Key]
        [NotMapped]
        public int ID { get; set; }
        [NotMapped]
        public int Id_Seg { get; set; }
        [NotMapped]
        public SeguimientoMedico SeguimientoMedico { get; set; }
        [NotMapped]
        public int Id_Enfermedad { get; set; }
        [NotMapped]
        public EnfermedadComun EnfermedadComun { get; set; }
    }
}
