using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorketHealth.DataAccess.Models.Registros
{
    public class SeguimientoMedico
    {
        [Key]
        [NotMapped]
        public int Id_Seg { get; set; }
        [NotMapped]
        public string DNI { get; set; }
        [NotMapped]
        public string? Id_Tipo_Examen { get; set; }
        [NotMapped]
        public DateTime? Fecha_Exam { get; set; }
        [NotMapped]
        public string? Area { get; set; }
        [NotMapped]
        public string? Puesto_Trabajo { get; set; }
        [NotMapped]
        public int? Id_Seg_Apt { get; set; }
        [NotMapped]
        public string? Restrinccion { get; set; }
        [NotMapped]
        public int? Id_EC { get; set; }
        [NotMapped]
        public int? Id_ERT { get; set; }
        [NotMapped]
        public int? Id_EP { get; set; }
        [NotMapped]
        public string? Ruc { get; set; }
        [NotMapped]
        public string? Mes { get; set; }
        [NotMapped]
        public string? Anho { get; set; }
        [NotMapped]
        public ICollection<Seguimiento_EC>? Seguimiento_EC { get; set; } // Relación muchos a muchos con la tabla EnfermedadesComunes
    }
}
