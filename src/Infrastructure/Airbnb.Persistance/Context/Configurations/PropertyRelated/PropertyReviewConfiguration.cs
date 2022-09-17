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
    public class PropertyReviewConfiguration : IEntityTypeConfiguration<PropertyReview>
    {
        public void Configure(EntityTypeBuilder<PropertyReview> builder)
        {
            builder.Property(x => x.Text).HasMaxLength(350).IsRequired();
            builder.Property(x => x.OverallScore).IsRequired();
            builder.Property(x => x.AccuracyScore).IsRequired();
            builder.Property(x => x.CheckInScore).IsRequired();
            builder.Property(x => x.LocationScore).IsRequired();
            builder.Property(x => x.CleanlinessScore).IsRequired();
            builder.Property(x => x.CommunicationScore).IsRequired();
            builder.Property(x => x.ValueScore).IsRequired();
            //builder.Property(x => x.PropertyId).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.ReservationId).IsRequired();
            builder.Property(x => x.HostId).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
        }
    }
}
