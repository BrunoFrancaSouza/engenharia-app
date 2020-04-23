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
    //public EngenhariaContext(DbContextOptions<EngenhariaContext> options) : base(options)
    //{

    //}
    private IConfiguration Configuration { get; }

    public AppContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySql(Configuration.GetConnectionString("ApplicationDB"));
    }

    //public DbSet<User> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Aplica alterações
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>(new RoleMap().Configure);
        modelBuilder.Entity<UserRole>(new UserRoleMap().Configure);

    }

}
