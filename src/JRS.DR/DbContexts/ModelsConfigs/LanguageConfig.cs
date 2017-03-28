using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts.ModelsConfigs
{
    public static class LanguageConfig
    {
        public static void ConfigureLanguage(this ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<Language>();
            entityBuilder.ToTable("Language");
            entityBuilder.Ignore("Key").Property("_key").HasColumnName("Key");
        }
    }
}