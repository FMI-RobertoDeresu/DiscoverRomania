using System;
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

        public override IEnumerable<Objective> GetMany(Expression<Func<Objective, bool>> where)
        {
            var result = QueryAll()
                .Include(x => x.Location)
                .Include(x => x.Type)
                .Where(where)
                .ToList();

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

        public IEnumerable<Objective> GetPaged(int page, int pageSize)
        {
            var result = QueryAll()
                .OrderBy(x => x.Name)
                .Include(x => x.Location)
                .Include(x => x.Type)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return result;
        }
    }
}