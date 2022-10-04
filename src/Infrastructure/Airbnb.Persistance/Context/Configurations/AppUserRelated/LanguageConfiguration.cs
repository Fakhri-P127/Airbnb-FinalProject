using Airbnb.Domain.Entities.AppUserRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.AppUserRelated
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        private readonly AirbnbDbContext _context;

        public LanguageConfiguration(AirbnbDbContext context)
        {
            _context = context;
        }
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.LanguageCode).HasMaxLength(10);

            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.LanguageCode).IsUnique();

            if (!_context.Languages.Any())
            {
                builder.HasData(
                   new Language()
                   {
                       Id = Guid.Parse("e5505dc9-69a2-4d83-a062-6581810a3d17"),
                       Name = "Azerbaijani",
                       LanguageCode = "az",
                       IsDisplayed = true,
                       CreatedAt = DateTime.Now,
                       ModifiedAt = DateTime.Now
                   },
                    new Language()
                    {
                        Id = Guid.Parse("9e83464f-5b90-47f7-bf7e-674413c26c5c"),
                        Name = "English",
                        LanguageCode = "en",
                        IsDisplayed = true,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now,
                    }, new Language()
                    {
                        Id = Guid.Parse("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"),
                        Name = "Japanese",
                        LanguageCode = "jpn",
                        IsDisplayed = true,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now,
                    }, new Language()
                    {
                        Id = Guid.Parse("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"),
                        Name = "Russian",
                        LanguageCode = "ru",
                        IsDisplayed = true,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now,
                    }, new Language()
                    {
                        Id = Guid.Parse("066ecfc5-af54-41f4-82e4-5239d9c4109c"),
                        Name = "Turkish",
                        LanguageCode = "tr",
                        IsDisplayed = true,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now,
                    }
                  );
            }
           
        }
    }
}
