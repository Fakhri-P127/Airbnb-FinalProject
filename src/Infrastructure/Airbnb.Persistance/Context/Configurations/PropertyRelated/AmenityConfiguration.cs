using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.Property(x => x.AmenityTypeId).IsRequired();
            builder.Property(x => x.Icon).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300);//not required
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
