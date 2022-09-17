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
    public class PropertyGroupTypeConfiguration : IEntityTypeConfiguration<PropertyGroupType>
    {
        public void Configure(EntityTypeBuilder<PropertyGroupType> builder)
        {
            builder.Property(x => x.PropertyGroupId).IsRequired();
            builder.Property(x => x.PropertyTypeId).IsRequired();

            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
        }
    }
}
