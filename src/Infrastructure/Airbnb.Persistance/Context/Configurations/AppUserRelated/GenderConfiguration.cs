using Airbnb.Domain.Entities.AppUserRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.AppUserRelated
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(
                new Gender()
                {
                    Id = Guid.NewGuid(),
                    Name= "Male",
                    IsDisplayed=true,
                    CreatedAt=DateTime.Now,
                    ModifiedAt=DateTime.Now,
                    AppUsers= new()
                },
                new Gender()
                {
                    Id = Guid.NewGuid(),
                    Name = "Female",
                    IsDisplayed = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    AppUsers = new()
                },
                new Gender()
                {
                    Id = Guid.NewGuid(),
                    Name = "Other",
                    IsDisplayed = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    AppUsers = new()
                }
                );
        }
    }
}
