using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorketHealth.Domain.Interfaces.Fecha;

namespace WorketHealth.Domain.Entities.Fecha
{
    public class AnhoService:IAnhoService
    {
        public List<SelectListItem> GetAnhos()
        {
            var anhos = new List<SelectListItem>();
            int anioInicial = 2020;
            int anioActual = DateTime.Now.Year;
            for (int i = anioInicial; i <= anioActual; i++)
            {
                anhos.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            return anhos;
        }
    }
}
