using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorketHealth.DataAccess.Models.Tablas
{
    public class Ruc
    {
        [Key]
        public int ID_RUC { get; set; }
        public int COD_RUC { get; set; }
        public string? NOM_RUC { get; set; }
        public string? DESCRIPCION_RUC { get; set; }
        
    }
}
