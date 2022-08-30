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
    public class PropertyGroupConfiguration : IEntityTypeConfiguration<PropertyGroup>
    {
        public void Configure(EntityTypeBuilder<PropertyGroup> builder)
        {

            builder.Property(x => x.Status).HasDefaultValue(true);
            builder.Property(x => x.Image).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasIndex(x => x.Image).IsUnique();
            builder.HasIndex(x => x.Name).IsUnique();
    
        }
    }
}
