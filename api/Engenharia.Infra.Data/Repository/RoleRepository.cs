//using Engenharia.Domain.Identity;
using Engenharia.Domain.Entities.Identity;
using Engenharia.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Engenharia.Infra.Data.Repository
{
    public class RoleRepository<TContext> : RepositoryBase<Role, TContext>, IRoleRepository<TContext> where TContext : DbContext
    {

    }
}
