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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CheckInDate).IsRequired();
            builder.Property(x => x.CheckOutDate).IsRequired();
            builder.Property(x => x.AdultCount).IsRequired();
            builder.Property(x => x.ChildCount).IsRequired();
            builder.Property(x => x.InfantCount).IsRequired();
            builder.Property(x => x.PetCount).IsRequired();
            builder.Property(x => x.PricePerDay).IsRequired();
            builder.Property(x => x.ServiceFee).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.PropertyId).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.HostId).IsRequired();
        }
    }
}
