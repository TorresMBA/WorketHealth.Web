using WorketHealth.Domain.Entities;

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
