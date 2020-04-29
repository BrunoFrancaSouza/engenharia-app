//using Engenharia.Domain.Entities;
using Engenharia.Domain.Identity;
using Engenharia.Infra.Data.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class AppContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole,
                                                   IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                                   IdentityUserToken<int>>
{
    private IConfiguration Configuration { get; }

    public AppContext(IConfiguration configuration, DbContextOptions<AppContext> options)
        : base(options)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            //optionsBuilder.UseMySql(Configuration.GetConnectionString("ApplicationDB"));
            throw new System.ArgumentNullException(nameof(optionsBuilder));
    }

    //public DbSet<User> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Aplica alterações
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

    }

}
