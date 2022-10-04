using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Latitude).HasColumnType("decimal(10,8)").IsRequired();
            builder.Property(x => x.Longitude).HasColumnType("decimal(11,8)").IsRequired();
            builder.Property(x => x.CheckInTime).IsRequired();
            builder.Property(x => x.CheckOutTime).IsRequired();
            builder.Property(x => x.MaxNightCount).IsRequired();
            builder.Property(x => x.MinNightCount).IsRequired();
            builder.Property(x => x.MaxGuestCount).IsRequired();
            builder.Property(x => x.BathroomCount).IsRequired();
            builder.Property(x => x.BedroomCount).IsRequired();
            builder.Property(x => x.BedCount).IsRequired();
            builder.Property(x => x.HostId).IsRequired();
            builder.Property(x => x.StateId).IsRequired();
            builder.Property(x => x.PrivacyTypeId).IsRequired();
            builder.Property(x => x.PropertyGroupId).IsRequired();
            builder.Property(x => x.PropertyTypeId).IsRequired();
            builder.Property(x => x.CancellationPolicyId).IsRequired();
            builder.Property(x => x.IsPetAllowed).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();
           
            
        }
    }
}
