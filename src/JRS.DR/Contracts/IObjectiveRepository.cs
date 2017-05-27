using System.Collections.Generic;
using JRS.DR.Models.Common;

namespace JRS.DR.Contracts
{
    public interface IObjectiveRepository : IRepository<int, Objective>
    {
        int CountAll();
        IEnumerable<ObjectiveLight> GetPaged(int page, int pageSize);
    }
}