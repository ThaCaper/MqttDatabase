using Flespi.Entity;
using Microsoft.EntityFrameworkCore;

namespace Flespi.Infrastructure.SQL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<User> Users { get; set; }
    }
}