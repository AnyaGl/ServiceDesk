using Microsoft.EntityFrameworkCore;
using ServiceDesk.Model;

namespace ServiceDesk
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Task> Tasks { get; set; }
    }
}
