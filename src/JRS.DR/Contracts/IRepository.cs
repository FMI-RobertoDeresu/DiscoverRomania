using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JRS.DR.Models;

namespace JRS.DR.Contracts
{
    public interface IRepository<in TKey, TEntity>
        where TKey : IComparable
        where TEntity : EntityBase<TKey>
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);

        TEntity Get(TKey key);
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAll();

        void SaveChanges();
    }
}