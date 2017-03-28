using JRS.DR.DbContexts.ModelsConfigs;
using JRS.DR.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace JRS.DR.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Language> Languages { get; protected set; }

        public DbSet<Location> Locations { get; protected set; }

        public DbSet<Objective> Objectives { get; protected set; }

        public DbSet<ObjectiveType> ObjectiveTypes { get; protected set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureLanguage();
            modelBuilder.ConfigureLocation();
            modelBuilder.ConfigureObjective();
            modelBuilder.ConfigureObjectiveType();
        }
    }
}