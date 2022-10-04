using Airbnb.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.Common
{
    public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.HasIndex(x => x.Key).IsUnique();
        }
    }
}
