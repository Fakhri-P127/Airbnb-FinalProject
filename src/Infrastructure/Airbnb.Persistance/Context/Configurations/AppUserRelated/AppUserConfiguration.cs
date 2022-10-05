using Airbnb.Domain.Entities.AppUserRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Persistance.Context.Configurations.AppUserRelated
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        private readonly AirbnbDbContext _context;

        public AppUserConfiguration(AirbnbDbContext context)
        {
            _context = context;
        }
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.IsDisplayed).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Firstname).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Lastname).HasMaxLength(15).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Work).HasMaxLength(50);
            builder.Property(x => x.About).HasMaxLength(250);
            builder.Property(x => x.ProfilPicture).HasMaxLength(120);
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(30).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(15);

            if (!_context.AppUsers.Any())
            {
                builder.HasData(
              new AppUser()
              {
                  Id = Guid.Parse("f7375a39-5d8b-4a87-be3e-f337b17351f8"),
                  Firstname = "Fexri",
                  Lastname = "Efendiyev",
                  UserName = "fexri2000",
                  DateOfBirth = new DateTime(2000, 05, 15),
                  Email = "seedEmail1@gmail.com",
                  IsDisplayed = true,
                  CreatedAt = DateTime.Now,
                  ModifiedAt = DateTime.Now,
                  EmailConfirmed = true,
                  PhoneNumberConfirmed = true,
                  PhoneNumber = "+994503661012"
              },
               new AppUser()
               {
                   Id = Guid.NewGuid(),
                   Firstname = "Eli",
                   Lastname = "Efendiyev",
                   UserName = "eli1999",
                   DateOfBirth = new DateTime(1999, 01, 26),
                   Email = "seedEmail2@gmail.com",
                   IsDisplayed = true,
                   CreatedAt = DateTime.Now,
                   ModifiedAt = DateTime.Now,
                   EmailConfirmed = true,
                   PhoneNumberConfirmed = true,
                   PhoneNumber = "+994503660012"
               }
              );
            }
        }
    }
}
