using Microsoft.EntityFrameworkCore;
using TravelSuitcase.Application.Common;
using TravelSuitcase.Domain.Entities;
using TravelSuitcase.Domain.ValueObjects;
using TravelSuitcase.Infrastructure.Persistence.Configuration;

namespace TravelSuitcase.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<SuitCase> SuitCases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<Item> Items { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SuitCaseConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new LocalizationConfig());
            modelBuilder.ApplyConfiguration(new ItemConfig());
        }
    }
}