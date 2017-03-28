using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts.ModelsConfigs
{
    public static class ObjectiveConfig
    {
        public static void ConfigureObjective(this ModelBuilder modelBuilder)
        {
            var objectiveBuilder = modelBuilder.Entity<Objective>();
            objectiveBuilder.ToTable("Objective");
            objectiveBuilder.Ignore("Status").Property("_status").HasColumnName("Status");
            objectiveBuilder.HasOne(x => x.Type).WithOne().HasForeignKey("Objective", "ObjectiveTypeId");
        }
    }
}