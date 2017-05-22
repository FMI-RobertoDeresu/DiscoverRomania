using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts.ModelsConfigs
{
    public static class LanguageConfig
    {
        public static void ConfigureLanguage(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().ToTable("Language").HasKey(x => x.Id);
            modelBuilder.Entity<Language>().Property("_key").HasColumnName("Key");
            modelBuilder.Entity<Language>().Ignore("Key");
        }
    }
}