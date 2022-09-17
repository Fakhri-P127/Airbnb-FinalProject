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
    public class AppUserLanguageConfiguration : IEntityTypeConfiguration<AppUserLanguage>
    {
        public void Configure(EntityTypeBuilder<AppUserLanguage> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();

            builder.Property(x => x.LanguageId).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();

        }
    }
}
