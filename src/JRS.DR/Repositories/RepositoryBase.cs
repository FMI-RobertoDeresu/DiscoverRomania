using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            (entity as EntityComplexBase<TKey>)?.PreInsert();
            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (entity is EntityComplexBase<TKey>)
            {
                (entity as EntityComplexBase<TKey>).MarkAsDeleted();
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                DbSet.Remove(entity);
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            var objects = DbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
                Delete(obj);
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
            var result = QueryAll().FirstOrDefault(where);

            return result;
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = QueryAll().Where(where);
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            var result = query.ToList();

            return result;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var result = QueryAll().ToList();

            return result;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        protected IQueryable<TEntity> QueryAll()
        {
            return typeof(EntityComplexBase<TKey>).IsAssignableFrom(typeof(TEntity))
                ? DbSet.Where(x => !(x as EntityComplexBase<TKey>).IsDeleted)
                : DbSet;
        }
    }
}