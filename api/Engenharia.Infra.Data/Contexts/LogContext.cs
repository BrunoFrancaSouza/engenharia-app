using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Engenharia.Infra.Data.Contexts
{
    public class LogContext : DbContext
    {
        private IConfiguration Configuration { get; }

        public LogContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql(Configuration.GetConnectionString("LogDB"));
        }

        //public DbSet<User> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Role>(new RoleMap().Configure);

        }
    }
}
