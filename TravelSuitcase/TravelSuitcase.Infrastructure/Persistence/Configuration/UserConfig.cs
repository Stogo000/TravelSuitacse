using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelSuitcase.Domain.Common.Constants.Entities;
using TravelSuitcase.Domain.Entities;

namespace TravelSuitcase.Infrastructure.Persistence.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Login).IsRequired().HasMaxLength(LoginConstants.Max);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(EmailConstants.Max);
            builder.Property(s => s.Password).IsRequired().HasMaxLength(PasswordConstants.MaxLength);

            builder.HasData(
                new User(1, "dsada@cos.pl", "alaZielona", "123adaAS2@"),
                new User(2, "dxxxx@cos.pl", "piotr", "123adaAS2!")
                );
        }
    }
}