using Engenharia.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Engenharia.Domain.Interfaces.Repository
{
    public interface IRoleRepository<TContext> : IRepositoryBase<Role, TContext> where TContext : DbContext
    {
    }
}
