using Microsoft.AspNetCore.Identity;
using WorketHealth.DataAccess.Models;

namespace WorketHealth.Web.Models
{
    public class AppUsuario: IdentityUser
    {
        public IEnumerable<UserMenuAccess> UserMenuAccess { get; set; }
    }
}
