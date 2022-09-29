using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated
{
    public class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.Property(x => x.Icon).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300);//not required
            builder.Property(x => x.PropertyGroupId).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
