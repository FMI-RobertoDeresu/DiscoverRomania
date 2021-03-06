﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JRS.DR.Contracts;
using JRS.DR.DbContexts;
using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.Repositories
{
    public class ObjectiveRepository : RepositoryBase<int, Objective>, IObjectiveRepository
    {
        public ObjectiveRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public int CountAll()
        {
            return QueryAll().Count();
        }

        public override Objective Get(int id)
        {
            var result = QueryAll()
                .Include(x => x.Language)
                .Include(x => x.Type)
                .Include(x => x.Location)
                .Include(x => x.Type)
                .FirstOrDefault(x => x.Id == id);

            return result;
        }

        public override IEnumerable<Objective> GetMany(Expression<Func<Objective, bool>> where, params Expression<Func<Objective, object>>[] includes)
        {
            var query = QueryAll()
                .Include(x => x.Location)
                .Include(x => x.Type)
                .Where(where);
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            var result = query.ToList();

            return result;
        }

        public override IEnumerable<Objective> GetAll()
        {
            var result = QueryAll()
                .Include(x => x.Location)
                .Include(x => x.Type)
                .ToList();

            return result;
        }

        public IEnumerable<ObjectiveLight> GetPaged(int page, int pageSize)
        {
            var result = QueryAll()
                .OrderBy(x => x.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ObjectiveLight()
                {
                    Id = x.Id,
                    Name =  x.Name,
                    Description = x.Description
                })
                .ToList();

            return result;
        }
    }
}