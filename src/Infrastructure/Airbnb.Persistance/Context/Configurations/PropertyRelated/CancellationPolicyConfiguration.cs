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
    public class CancellationPolicyConfiguration : IEntityTypeConfiguration<CancellationPolicy>
    {
        public void Configure(EntityTypeBuilder<CancellationPolicy> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.FullRefund).HasMaxLength(100).IsRequired();
            builder.Property(x => x.PartialRefund).HasMaxLength(100);
            builder.Property(x => x.NoRefund).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(true);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
