using System.Collections.Generic;
using System.Linq;
using JRS.DR.Contracts;
using JRS.DR.DbContexts;
using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.Repositories
{
    public class ObjectiveRepository : RepositoryBase<int, Objective>, IObjectiveRepository
    {
        public ObjectiveRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public override Objective Get(int id)
        {
            var result = DbSet
                .Include(x => x.Location)
                .Include(x => x.Type)
                .FirstOrDefault(x => x.Id == id);

            if (result?.IsDeleted ?? false)
                return null;

            return result;
        }

        public override IEnumerable<Objective> GetAll()
        {
            var result = DbSet
                .Include(x => x.Location)
                .Include(x => x.Type)
                .ToList();

            result = result.Where(x => !(x?.IsDeleted ?? false)).ToList();

            return result;
        }
    }
}