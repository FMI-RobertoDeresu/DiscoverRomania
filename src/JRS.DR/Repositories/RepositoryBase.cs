using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JRS.DR.Contracts;
using JRS.DR.DbContexts;
using JRS.DR.Models;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.Repositories
{
    public class RepositoryBase<TKey, TEntity> : IRepository<TKey, TEntity>
        where TKey : IComparable
        where TEntity : EntityBase<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        protected DbSet<TEntity> DbSet;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            (entity as EntityComplexBase<TKey>)?.MarkAsDeleted();
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            var objects = DbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
            {
                if (obj is EntityComplexBase<TKey>)
                {
                    (obj as EntityComplexBase<TKey>).MarkAsDeleted();
                    _dbContext.Entry(obj).State = EntityState.Modified;
                }
                else
                    DbSet.Remove(obj);
            }
        }

        public virtual TEntity Get(TKey id)
        {
            var result = DbSet.Find(id);

            if ((result as EntityComplexBase<TKey>)?.IsDeleted ?? false)
                return null;

            return result;
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            var result = DbSet.FirstOrDefault(where);

            if ((result as EntityComplexBase<TKey>)?.IsDeleted ?? false)
                return null;

            return result;
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            var result = DbSet.Where(where).ToList();

            if (result.OfType<EntityComplexBase<TKey>>().Any())
                result = result.Where(x => !((x as EntityComplexBase<TKey>)?.IsDeleted ?? false)).ToList();

            return result;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var result = DbSet.ToList();

            if (result.OfType<EntityComplexBase<TKey>>().Any())
                result = result.Where(x => !((x as EntityComplexBase<TKey>)?.IsDeleted ?? false)).ToList();

            return result;
        }
    }
}