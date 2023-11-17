using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorketHealth.Domain.Interfaces.Fecha
{
    public interface IMesService
    {
        List<SelectListItem> GetMeses();
    }
}
