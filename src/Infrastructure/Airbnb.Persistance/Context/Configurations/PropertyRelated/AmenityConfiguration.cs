using Airbnb.Domain.Entities.Property;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.Property(x => x.AmenityType).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Icon).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);//not required
            builder.Property(x => x.Status).HasDefaultValue(true);

            builder.HasIndex(x => x.Icon).IsUnique();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.Description).IsUnique();
        }
    }
}
