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
    public class AirCoverConfiguration : IEntityTypeConfiguration<AirCover>
    {
        public void Configure(EntityTypeBuilder<AirCover> builder)
        {
            builder.Property(x => x.Logo).HasMaxLength(120).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.FullDaySafetyLine).HasMaxLength(250).IsRequired();
            builder.Property(x => x.BookingProtectionGuarantee).HasMaxLength(250).IsRequired();
            builder.Property(x => x.CheckInGuarantee).HasMaxLength(250).IsRequired();
            builder.Property(x => x.GetWhatYouBookedGuarantee).HasMaxLength(250).IsRequired();
            builder.Property(x => x.FindMore).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(true);

        }
    }
}
