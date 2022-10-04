using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.PropertyRelated
{
    public class PropertyGroupConfiguration : IEntityTypeConfiguration<PropertyGroup>
    {
        private readonly AirbnbDbContext _context;

        public PropertyGroupConfiguration(AirbnbDbContext context)
        {
            _context = context;
        }
        public void Configure(EntityTypeBuilder<PropertyGroup> builder)
        {

            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.Property(x => x.Image).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            if (!_context.PropertyGroups.Any())
            {
                builder.HasData(
              new PropertyGroup()
              {
                  Id = Guid.Parse("9138421c-0e9f-4d23-85dd-f2fdf3a4854c"),
                  IsDisplayed = true,
                  CreatedAt = DateTime.Now,
                  ModifiedAt = DateTime.Now,
                  Name = "Apartment",
                  Image = "54874e8e-2699-4ff7-adab-875f528dee59.jpg",
                  Properties = new(),
                  PropertyTypes = new()
              },
                new PropertyGroup()
                {
                    Id = Guid.NewGuid(),
                    IsDisplayed = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Name = "House",
                    Image = "520f85dc-c9a8-45c6-b2fc-179150d10285.jpg",
                    Properties = new(),
                    PropertyTypes = new()
                }
              );
            }
        }
    }
}
