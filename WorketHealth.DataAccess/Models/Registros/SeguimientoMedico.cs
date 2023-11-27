using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorketHealth.DataAccess.Models.Registros
{
    public class SeguimientoMedico
    {
        [Key]
        public int ID_SEG { get; set; }
        public string DNI { get; set; }
        public int ID_TIPO_EXAMEN { get; set; }
        public TipoExamen TipoExamen { get; set; }
        public DateTime FECHA_EXAM { get; set; }
        public string? AREA { get; set; }
        public string? PUESTO_DE_TRABAJO { get; set; }
        public int ID_SEG_APT { get; set; }
        public Aptitud Aptitud { get; set; }
        public string? RESTRICIONES { get; set; }
        public string RUC { get; set; }
        public string MES { get; set; }
        public string ANHO { get; set; }        
        public ICollection<SeguimientoEnfermedad>? Enfermedades { get; set; } // Relación muchos a muchos con la tabla EnfermedadesComunes
        public ICollection<SeguimientoEnfermedadTrabajo>? EnfermedadesTrabajo { get; set; } // Relación muchos a muchos con la tabla EnfermedadesRelacionadaTrabajo
        public ICollection<SeguimientoEnfermedadProfesional>? EnfermedadesProfesionales { get; set; } // Relación muchos a muchos con la tabla EnfermedadProfecional

        [NotMapped]
        public int Cantidad { get; set; }
    }
}
