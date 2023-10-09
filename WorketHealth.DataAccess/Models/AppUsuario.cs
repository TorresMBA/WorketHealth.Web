using Microsoft.AspNetCore.Identity;

namespace WorketHealth.DataAccess.Models {
    public class AppUsuario: IdentityUser
    {
        public IEnumerable<UserMenuAccess> UserMenuAccess { get; set; }
    }
}
