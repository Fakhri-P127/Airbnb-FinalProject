using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated.StateRelated
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
        }
    }
}
