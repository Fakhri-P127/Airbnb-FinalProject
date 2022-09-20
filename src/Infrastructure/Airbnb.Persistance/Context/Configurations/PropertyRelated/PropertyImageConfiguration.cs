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
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            //builder.Property(x => x.IsMain);
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            //builder.Property(x => x.Alternative).HasMaxLength(25);
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);

            //maybe isunique name ele
        }
    }
}
