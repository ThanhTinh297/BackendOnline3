using Microsoft.EntityFrameworkCore;
using Backend_Online_3.Models;

namespace Backend_Online_3.Data
{
    public class BackendOnline3DbContext : DbContext
    {
        public BackendOnline3DbContext(DbContextOptions<BackendOnline3DbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<AllowAccess> AllowAccess { get; set; }
        public DbSet<Intern> Intern { get; set; }
    }
}
