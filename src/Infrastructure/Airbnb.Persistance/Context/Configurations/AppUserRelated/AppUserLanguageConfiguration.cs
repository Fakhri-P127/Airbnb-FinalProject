using Airbnb.Domain.Entities.AppUserRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.AppUserRelated
{
    public class AppUserLanguageConfiguration : IEntityTypeConfiguration<AppUserLanguage>
    {
        private readonly AirbnbDbContext _context;

        public AppUserLanguageConfiguration(AirbnbDbContext context)
        {
            _context = context;
        }
        public void Configure(EntityTypeBuilder<AppUserLanguage> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();

            builder.Property(x => x.LanguageId).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();

            if (!_context.AppUserLanguages.Any())
            {
                builder.HasData(
              new AppUserLanguage()
              {
                  Id = Guid.NewGuid(),
                  AppUserId = Guid.Parse("f7375a39-5d8b-4a87-be3e-f337b17351f8"),
                  LanguageId = Guid.Parse("e5505dc9-69a2-4d83-a062-6581810a3d17"),//azerbaycanca
                  IsDisplayed = true,
                  CreatedAt = DateTime.Now,
                  ModifiedAt = DateTime.Now,
              },
                new AppUserLanguage()
                {
                    Id = Guid.NewGuid(),
                    AppUserId = Guid.Parse("f7375a39-5d8b-4a87-be3e-f337b17351f8"),
                    LanguageId = Guid.Parse("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"),//yaponca
                    IsDisplayed = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                },
                new AppUserLanguage()
                {
                    Id = Guid.NewGuid(),
                    AppUserId = Guid.Parse("f7375a39-5d8b-4a87-be3e-f337b17351f8"),
                    LanguageId = Guid.Parse("9e83464f-5b90-47f7-bf7e-674413c26c5c"),//ingilisce
                    IsDisplayed = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                },
                new AppUserLanguage()
                {
                    Id = Guid.NewGuid(),
                    AppUserId = Guid.Parse("f7375a39-5d8b-4a87-be3e-f337b17351f8"),
                    LanguageId = Guid.Parse("066ecfc5-af54-41f4-82e4-5239d9c4109c"),//turkce
                    IsDisplayed = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                }, new AppUserLanguage()
                {
                    Id = Guid.NewGuid(),
                    AppUserId = Guid.Parse("f7375a39-5d8b-4a87-be3e-f337b17351f8"),
                    LanguageId = Guid.Parse("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"),//rusca
                    IsDisplayed = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                }
              );
            }
        }
    }
}
