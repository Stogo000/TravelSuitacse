using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelSuitcase.Domain.ValueObjects;

namespace TravelSuitcase.Infrastructure.Persistence.Configuration
{
    internal class LocalizationConfig : IEntityTypeConfiguration<Localization>
    {
        public void Configure(EntityTypeBuilder<Localization> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.City).IsRequired().HasMaxLength(32);
            builder.Property(s => s.Country).IsRequired().HasMaxLength(32);
            builder.HasData(
                new Localization(1, "Warszawa", "Polska"),
                new Localization(2, "Gdansk", "Polska")
            );
        }
    }
}