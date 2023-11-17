using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorketHealth.Domain.Interfaces.Fecha;

namespace WorketHealth.Domain.Entities.Fecha
{
    public class MesService:IMesService
    {        
        public List<SelectListItem> GetMeses()
        {
            var meses = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                meses.Add(new SelectListItem
                {
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                    Value = i.ToString()
                });
            }
            return meses;
        }
    }
}
