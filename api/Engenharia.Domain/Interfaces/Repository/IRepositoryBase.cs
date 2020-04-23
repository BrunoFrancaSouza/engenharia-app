using Engenharia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engenharia.Domain.Interfaces.Repository
{
    //public interface IRepositoryBase<T> where T : BaseEntity
    //public interface IRepositoryBase<T> where T : IBaseEntity
    public interface IRepositoryBase<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        void Insert(TEntity obj);

        void Update(TEntity obj);

        void Delete(int id);

        TEntity GetById(int id);

        //IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetAll();
    }
}
