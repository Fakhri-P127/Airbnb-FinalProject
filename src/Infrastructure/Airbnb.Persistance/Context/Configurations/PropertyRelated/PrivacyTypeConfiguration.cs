using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated
{
    public class PrivacyTypeConfiguration : IEntityTypeConfiguration<PrivacyType>
    {
        private readonly AirbnbDbContext _context;

        public PrivacyTypeConfiguration(AirbnbDbContext context)
        {
            _context = context;
        }
        public void Configure(EntityTypeBuilder<PrivacyType> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(40).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);

            builder.HasIndex(x => x.Name).IsUnique();

            if (!_context.PrivacyTypes.Any())
            {
                builder.HasData(
               new PrivacyType()
               {
                   Id = Guid.NewGuid(),
                   Name = "Full apartment",
                   CreatedAt = DateTime.Now,
                   ModifiedAt = DateTime.Now,
                   IsDisplayed = true,
                   Properties = new()
               },
                 new PrivacyType()
                 {
                     Id = Guid.NewGuid(),
                     Name = "Shared house",
                     CreatedAt = DateTime.Now,
                     ModifiedAt = DateTime.Now,
                     IsDisplayed = true,
                     Properties = new()
                 },
                   new PrivacyType()
                   {
                       Id = Guid.NewGuid(),
                       Name = "Private room",
                       CreatedAt = DateTime.Now,
                       ModifiedAt = DateTime.Now,
                       IsDisplayed = true,
                       Properties = new()
                   }
               );
            }
        }
    }
}
