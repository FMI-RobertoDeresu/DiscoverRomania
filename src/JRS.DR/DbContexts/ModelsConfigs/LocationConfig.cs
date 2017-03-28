using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts.ModelsConfigs
{
    public static class LocationConfig
    {
        public static void ConfigureLocation(this ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<Location>();
            entityBuilder.ToTable("Location");
        }
    }
}