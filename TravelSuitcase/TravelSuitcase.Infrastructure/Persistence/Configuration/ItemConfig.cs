using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelSuitcase.Domain.ValueObjects;

namespace TravelSuitcase.Infrastructure.Persistence.Configuration
{
    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).IsRequired().HasMaxLength(32);
            builder.Property(s => s.Quantity).IsRequired();
            builder.Property(s => s.IsPacked).IsRequired();
            builder.HasData(
                new Item(1, "T-shirt", 2),
                new Item(2, "Jeans", 1)
                );
        }
    }
}