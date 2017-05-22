using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts.ModelsConfigs
{
    public static class ObjectiveTypeConfig
    {
        public static void ConfigureObjectiveType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjectiveType>().ToTable("ObjectiveType").HasKey(x => x.Id);
        }
    }
}