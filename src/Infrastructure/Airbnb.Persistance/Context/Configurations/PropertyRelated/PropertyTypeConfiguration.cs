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
    public class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true);
            builder.Property(x => x.Icon).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300);//not required
            builder.Property(x => x.PropertyGroupId).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(
              new PropertyType()
              {
                  Id = Guid.NewGuid(),
                  IsDisplayed = true,
                  CreatedAt = DateTime.Now,
                  ModifiedAt = DateTime.Now,
                  Name = "Condo",
                  Properties = new(),
                  Icon = $"<i class=\"fa-solid fa-apartment\"></i>",
                  PropertyGroupId = Guid.Parse("9138421c-0e9f-4d23-85dd-f2fdf3a4854c"),
                  Description = "A place within a multi-unit building or complex owned by the residents."
              },
                new PropertyType()
                {
                    Id = Guid.NewGuid(),
                    IsDisplayed = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Name = "Vacation House",
                    Properties = new(),
                    Icon = $"<i class='fa-solid fa-apartment'></i>",
                    PropertyGroupId = Guid.Parse("9138421c-0e9f-4d23-85dd-f2fdf3a4854c"),
                    Description = "A furnished rental property that includes a kitchen and bathroom and may offer some guest services, like a reception desk."
                }
              );
        }
    }
}
