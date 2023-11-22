using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorketHealth.DataAccess.Models.Archivos;
using WorketHealth.DataAccess.Models.Registros;
using WorketHealth.DataAccess.Models.Tablas;

namespace WorketHealth.DataAccess.Models.ViewModels
{
    public class ViewF_SEG_19
    {
        public List<F_SEG_19> F_SEG_19 { get; set; }
        public List<Ruc> RUC { get; set; }
        public ArchivoModel ArchivoModel { get; set; }
    }
}
