using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelSuitcase.Domain.ValueObjects;

namespace TravelSuitcase.Infrastructure.Persistence.Configuration
{
    public class SuitCaseItemConfig : IEntityTypeConfiguration<SuitCaseItem>
    {
        public void Configure(EntityTypeBuilder<SuitCaseItem> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.HasOne(s => s.SuitCase).WithMany(u => u.Items).HasForeignKey(s => s.SuitCaseId);
            builder.HasOne(s => s.Item).WithMany(c => c.SuitCase).HasForeignKey(s => s.ItemId);
            builder.HasData(
                new SuitCaseItem { Id = 1, SuitCaseId = 1, ItemId = 1 },
                new SuitCaseItem { Id = 2, SuitCaseId = 2, ItemId = 2 }
                );
        }
    }
}