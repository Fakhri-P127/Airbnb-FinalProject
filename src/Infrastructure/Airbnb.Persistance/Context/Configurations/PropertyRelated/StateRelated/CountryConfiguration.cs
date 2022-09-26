using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated.StateRelated
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.Property(x => x.Name).HasMaxLength(25).IsRequired();
            builder.Property(x => x.RegionId).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
