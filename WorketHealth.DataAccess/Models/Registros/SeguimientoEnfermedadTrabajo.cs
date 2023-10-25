using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorketHealth.DataAccess.Models.Registros
{
    public class SeguimientoEnfermedadTrabajo
    {
        public int SeguimientoMedicoId { get; set; }
        public SeguimientoMedico SeguimientoMedico { get; set; }
        public int EnfermedadRelacionadaTrabajoId { get; set; }
        public EnfermedadRelacionadaTrabajo EnfermedadRelacionadaTrabajo { get; set; }
    }
}
