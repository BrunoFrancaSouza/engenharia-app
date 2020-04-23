using Engenharia.Domain.Entities;
using Engenharia.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

//public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    //protected EngenhariaContext context = new EngenhariaContext();
    //protected TContext context = new TContext();

    protected DbSet<TEntity> dbSet;
    protected TContext context;

    public void Insert(TEntity obj)
    {
        dbSet.Add(obj);
        context.SaveChanges();
    }

    public void Update(TEntity obj)
    {
        context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        context.Set<TEntity>().Remove(GetById(id));
        context.SaveChanges();
    }

    //public IEnumerable<TEntity> GetAll()
    public IQueryable<TEntity> GetAll()
    {
        var result = dbSet.AsQueryable();
        return result;
    }

    public TEntity GetById(int id)
    {
        return dbSet.Find(id);
    }
}

