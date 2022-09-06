using Airbnb.Application.Contracts.v1.Client.User.Responses.NestedUserResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.User.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; }
        //banned suspended or active
        //public bool Status { get; set; }
        //optionals
        public string PhoneNumber { get; set; }
        public GenderInUserResponse Gender { get; set; }
        public string ProfilPicture { get; set; }
        public string About { get; set; }
        public string Work { get; set; }
        public HostInUserResponse Host { get; set; }
        public List<string> Verifications { get; set; }
        public List<AppUserLanguageInUserResponse> AppUserLanguages { get; set; }
        public List<ReservationInUserResponse> ReservationsYouMade { get; set; }
        public List<PropertyReviewsInUserResponse> ReviewsByYou { get; set; }
        public List<GuestReviewInUserResponse> ReviewsAboutYou { get; set; }


    }
}
