using Airbnb.Domain.Entities.AppUserRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.AppUserRelated
{
    public class RefreshTokenConfiguration:IEntityTypeConfiguration<RefreshToken>
    {

        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id).HasName("Token");
            builder.Property(x => x.JwtId).IsRequired();
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.Property(x => x.HasBeenUsed).IsRequired();
            builder.Property(x => x.ExpiryDate).IsRequired();
            builder.Property(x => x.IsRevoked).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();
        }
    }
}
