using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Engenharia.Infra.Data.Contexts
{
    public class LogContext : DbContext
    {
        private IConfiguration Configuration { get; }

        public LogContext(IConfiguration configuration, DbContextOptions<LogContext> options)
        : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                //optionsBuilder.UseMySql(Configuration.GetConnectionString("LogDB"));
                throw new System.ArgumentNullException(nameof(optionsBuilder));
        }

        //public DbSet<User> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Role>(new RoleMap().Configure);

        }
    }
}
