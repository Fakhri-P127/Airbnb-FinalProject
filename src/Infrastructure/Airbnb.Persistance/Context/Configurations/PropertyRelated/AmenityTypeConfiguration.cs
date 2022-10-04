using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated
{
    public class AmenityTypeConfiguration : IEntityTypeConfiguration<AmenityType>
    {
        public void Configure(EntityTypeBuilder<AmenityType> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
