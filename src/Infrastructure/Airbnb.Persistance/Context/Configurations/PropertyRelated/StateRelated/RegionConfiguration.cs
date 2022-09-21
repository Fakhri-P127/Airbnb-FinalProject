using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated.StateRelated
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.Property(x => x.Name).HasMaxLength(25).IsRequired();
        }
    }
}
