using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelSuitcase.Domain.Entities;

namespace TravelSuitcase.Infrastructure.Persistence.Configuration
{
    public class SuitCaseConfig : IEntityTypeConfiguration<SuitCase>
    {
        public void Configure(EntityTypeBuilder<SuitCase> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).IsRequired().HasMaxLength(32);
            builder.HasOne(s => s.User).WithMany(u => u.SuitCases).HasForeignKey(s => s.UserId);
            builder.HasOne(s => s.Localization).WithMany(c => c.SuitCases).HasForeignKey(s => s.LocalizationId);
            builder.HasData(
                new SuitCase(1, "Pierwsza", 1, 1),
                new SuitCase(2, "Druga", 2, 2)
                );
        }
    }
}