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
    public class AirCoverConfiguration : IEntityTypeConfiguration<AirCover>
    {
        public void Configure(EntityTypeBuilder<AirCover> builder)
        {
            builder.Property(x => x.Logo).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(300).IsRequired();
            builder.Property(x => x.FullDaySafetyLine).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.BookingProtectionGuarantee).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.CheckInGuarantee).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.GetWhatYouBookedGuarantee).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.FindMore).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(true);

        }
    }
}
