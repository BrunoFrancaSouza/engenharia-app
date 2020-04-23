using Engenharia.Domain.Interfaces.Service;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Engenharia.Service.Services
{
    public class BaseService<TEntity, TContext> : IBaseService<TEntity> where TEntity : class where TContext : DbContext
    {
        private RepositoryBase<TEntity, TContext> repository = new RepositoryBase<TEntity, TContext>();

        public TEntity Insert<V>(TEntity obj) where V : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<V>());

            repository.Insert(obj);
            return obj;
        }

        public TEntity Update<V>(TEntity obj) where V : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<V>());

            repository.Update(obj);
            return obj;
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            repository.Delete(id);
        }

        //public IEnumerable<TEntity> GetAll()
        public IQueryable<TEntity> GetAll()
        {
            return repository.GetAll();
        }

        public TEntity GetById(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            return repository.GetById(id);
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
