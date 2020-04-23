using Engenharia.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engenharia.Domain.Interfaces.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity Insert<V>(TEntity obj) where V : AbstractValidator<TEntity>;

        TEntity Update<V>(TEntity obj) where V : AbstractValidator<TEntity>;

        void Delete(int id);

        TEntity GetById(int id);

        //IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetAll();
    }
}
