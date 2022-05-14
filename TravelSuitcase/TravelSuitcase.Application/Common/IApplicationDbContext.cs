using Microsoft.EntityFrameworkCore;
using TravelSuitcase.Domain.Entities;
using TravelSuitcase.Domain.ValueObjects;

namespace TravelSuitcase.Application.Common
{
    public interface IApplicationDbContext
    {
        public DbSet<SuitCase> SuitCases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}