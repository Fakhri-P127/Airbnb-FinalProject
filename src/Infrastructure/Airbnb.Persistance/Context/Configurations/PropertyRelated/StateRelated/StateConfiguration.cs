using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated.StateRelated
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.Property(x => x.Street).HasMaxLength(60).IsRequired();
            builder.Property(x => x.RegionId).IsRequired();
            builder.Property(x => x.CityId).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
        
        }
    }
}
