using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WorketHealth.DataAccess {
    public class WorketHealthContext : IdentityDbContext{

        public WorketHealthContext(DbContextOptions<WorketHealthContext> options) : base(options)
        {
        }
    }
}