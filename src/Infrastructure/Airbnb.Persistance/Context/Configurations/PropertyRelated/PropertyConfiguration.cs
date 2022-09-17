using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Country).HasMaxLength(50).IsRequired();
            builder.Property(x => x.City).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Street).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Latitude).HasColumnType("decimal(10,8)").IsRequired();
            builder.Property(x => x.Longitude).HasColumnType("decimal(11,8)").IsRequired();
            builder.Property(x => x.MaxGuestCount).IsRequired();
            builder.Property(x => x.PropertyGroupId).IsRequired();
            builder.Property(x => x.PropertyTypeId).IsRequired();
            builder.Property(x => x.AirCoverId).IsRequired();
            //builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.BathroomCount).IsRequired();
            builder.Property(x => x.BedroomCount).IsRequired();
            builder.Property(x => x.BedCount).IsRequired();
            builder.Property(x => x.CancellationPolicyId).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            
            //builder.Property(x => x.AdultCount).IsRequired();
            //builder.Property(x => x.ChildrenCount).IsRequired();

            builder.Property(x => x.MaxNightCount).IsRequired();

        }
    }
}
