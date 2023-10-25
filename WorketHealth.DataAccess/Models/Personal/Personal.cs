using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorketHealth.DataAccess.Models.Personal
{
    public class Personal
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string? Primer_Nombre { get; set; }
        public string? Segundo_Nombre { get; set; }
        public string? Primer_Apellido { get; set; }
        public string? Segundo_Apellido { get; set; }
        public DateTime Fec_Nacimiento { get; set; }
        public string? Sexo { get; set; }
    }
}
