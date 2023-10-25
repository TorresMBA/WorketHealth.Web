using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorketHealth.DataAccess.Models.Registros
{
    public class TipoExamen
    {
        [Key]
        public int ID_TIPO { get; set; }
        public string? COD { get; set; }
        public string? DESCRIPCION { get; set; }
    }
}
