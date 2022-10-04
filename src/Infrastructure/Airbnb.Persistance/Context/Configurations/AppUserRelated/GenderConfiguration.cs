using Airbnb.Domain.Entities.AppUserRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.AppUserRelated
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        private readonly AirbnbDbContext _context;

        public GenderConfiguration(AirbnbDbContext context)
        {
            _context = context;
        }
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            if (!_context.Genders.Any())
            {
                builder.HasData(
                new Gender()
                {
                    Id = Guid.NewGuid(),
                    Name = "Male",
                    IsDisplayed = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    AppUsers = new()
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
}
