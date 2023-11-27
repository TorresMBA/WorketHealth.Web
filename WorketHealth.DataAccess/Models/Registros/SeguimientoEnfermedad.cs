using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WorketHealth.DataAccess.Models.Registros
{
    public class SeguimientoEnfermedad
    {
        public int SeguimientoMedicoId { get; set; }
        public SeguimientoMedico SeguimientoMedico { get; set; }
        public int EnfermedadComunId { get; set; }
        public EnfermedadComun EnfermedadComun { get; set; }

    }
}
