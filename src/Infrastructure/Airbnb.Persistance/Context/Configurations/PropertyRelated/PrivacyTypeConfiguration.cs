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
    public class PrivacyTypeConfiguration : IEntityTypeConfiguration<PrivacyType>
    {
        public void Configure(EntityTypeBuilder<PrivacyType> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(40).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
