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
    public class HostConfiguration : IEntityTypeConfiguration<Host>
    {
        public void Configure(EntityTypeBuilder<Host> builder)
        {
            builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.Property(x => x.IsSuperHost).IsRequired();

        }
    }
}
