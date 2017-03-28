using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts.ModelsConfigs
{
    public static class ObjectiveTypeConfig
    {
        public static void ConfigureObjectiveType(this ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<ObjectiveType>();
            entityBuilder.ToTable("ObjectiveType");
        }
    }
}