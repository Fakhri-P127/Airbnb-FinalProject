using Airbnb.Domain.Entities.AppUserRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Context.Configurations.AppUserRelated
{
    public class GuestReviewConfiguration : IEntityTypeConfiguration<GuestReview>
    {
        public void Configure(EntityTypeBuilder<GuestReview> builder)
        {
            builder.Property(x => x.Text).HasMaxLength(350).IsRequired();
            builder.Property(x => x.GuestScore).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.HostId).IsRequired();
            builder.Property(x => x.ReservationId).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();

        }
    }
}
