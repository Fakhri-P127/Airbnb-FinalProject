using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated
{
    public class CancellationPolicyConfiguration : IEntityTypeConfiguration<CancellationPolicy>
    {
        public void Configure(EntityTypeBuilder<CancellationPolicy> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.FullRefund).HasMaxLength(500).IsRequired();
            builder.Property(x => x.PartialRefund).HasMaxLength(500);
            builder.Property(x => x.NoRefund).HasMaxLength(500).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
