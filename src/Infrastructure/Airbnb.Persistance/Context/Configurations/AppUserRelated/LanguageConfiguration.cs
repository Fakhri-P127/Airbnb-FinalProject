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
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(
                 new Language()
                 {
                     Id = Guid.Parse("e5505dc9-69a2-4d83-a062-6581810a3d17"),
                     Name = "Azerbaijani",
                     IsDisplayed = true,
                     CreatedAt = DateTime.Now,
                     ModifiedAt = DateTime.Now
                 },
                  new Language()
                  {
                      Id = Guid.Parse("9e83464f-5b90-47f7-bf7e-674413c26c5c"),
                      Name = "English",
                      IsDisplayed = true,
                      CreatedAt = DateTime.Now,
                      ModifiedAt = DateTime.Now,
                  }, new Language()
                  {
                      Id = Guid.Parse("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"),
                      Name = "Japanese",
                      IsDisplayed = true,
                      CreatedAt = DateTime.Now,
                      ModifiedAt = DateTime.Now,
                  }, new Language()
                  {
                      Id = Guid.Parse("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"),
                      Name = "Russian",
                      IsDisplayed = true,
                      CreatedAt = DateTime.Now,
                      ModifiedAt = DateTime.Now,
                  }, new Language()
                  {
                      Id = Guid.Parse("066ecfc5-af54-41f4-82e4-5239d9c4109c"),
                      Name = "Turkish",
                      IsDisplayed = true,
                      CreatedAt = DateTime.Now,
                      ModifiedAt = DateTime.Now,
                  }
                );
        }
    }
}
