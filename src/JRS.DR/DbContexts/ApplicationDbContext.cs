using JRS.DR.DbContexts.ModelsConfigs;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureLanguage();
            modelBuilder.ConfigureLocation();
            modelBuilder.ConfigureObjective();
            modelBuilder.ConfigureObjectiveType();
            base.OnModelCreating(modelBuilder);
        }
    }
}