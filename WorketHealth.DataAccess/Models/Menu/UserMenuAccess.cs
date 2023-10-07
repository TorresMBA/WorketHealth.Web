using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorketHealth.Domain.Entities;
using WorketHealth.Web.Models;

namespace WorketHealth.DataAccess.Models
{
    public class UserMenuAccess 
    {
        public string UserId { get; set; }
        public int MenuId { get; set; }

        public AppUsuario User { get; set; }
        public Menu Menu { get; set; }
    }
}
