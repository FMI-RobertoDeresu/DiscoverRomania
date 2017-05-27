using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts.ModelsConfigs
{
    public static class LocationConfig
    {
        public static void ConfigureLocation(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().ToTable("Location").HasKey(x => x.Id);
        }
    }
}