using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorketHealth.DataAccess.Models.Registros
{
    public class SeguimientoEnfermedadProfesional
    {
        public int SeguimientoMedicoId { get; set; }
        public SeguimientoMedico SeguimientoMedico { get; set; }
        public int EnfermedadProfesionalId { get; set; }
        public EnfermedadProfesional EnfermedadProfesional { get; set; }
    }
}
