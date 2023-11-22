using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WorketHealth.DataAccess.Models.Archivos
{
    public class ArchivoModel
    {
        public string Ruc { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
