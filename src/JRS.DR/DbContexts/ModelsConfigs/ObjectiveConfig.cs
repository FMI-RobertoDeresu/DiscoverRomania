using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts.ModelsConfigs
{
    public static class ObjectiveConfig
    {
        public static void ConfigureObjective(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Objective>().ToTable("Objective").HasKey(x => x.Id);
            modelBuilder.Entity<Objective>().Property("_status").HasColumnName("Status");
            modelBuilder.Entity<Objective>().HasOne(x => x.Type).WithOne().HasForeignKey<Objective>(x => x.ObjectiveTypeId);
            modelBuilder.Entity<Objective>().Ignore("Status");
        }
    }
}