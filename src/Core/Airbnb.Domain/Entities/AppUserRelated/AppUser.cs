using Airbnb.Domain.Entities.AppUserRelated.CustomFrameworkClasses;
using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.Domain.Entities.AppUserRelated
{
    public class AppUser : CustomIdentityUser
    {
        public AppUser()
        {
            AppUserLanguages = new();
            ReviewsByYou = new();
            ReviewsAboutYou = new();
            ReservationsYouMade = new();
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
      
        //optionals
        public string ProfilPicture { get; set; }
        public string About { get; set; }
        public Guid? GenderId { get; set; }
        public Gender Gender { get; set; }
        public string Work { get; set; }
        public Host Host { get; set; }
        public List<AppUserLanguage> AppUserLanguages { get; set; }
        public List<PropertyReview> ReviewsByYou { get; set; }
        public List<GuestReview> ReviewsAboutYou { get; set; }
        public List<Reservation> ReservationsYouMade { get; set; }
    }
}
