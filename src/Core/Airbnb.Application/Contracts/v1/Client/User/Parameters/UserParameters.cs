using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Client.User.Parameters
{
    public class UserParameters:BaseQueryStringParameters
    {
        public override int PageSize { get; set; } = 4;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? MinDateOfBirth { get; set; }
        public DateTime? MaxDateOfBirth { get; set; }

        //optionals
        public bool? HasProfilPicture { get; set; }
        public Guid? GenderId { get; set; }
        /// <summary>
        /// Languages codes that are avaliable: [en,az,ru,tr,jpn]
        /// </summary>
        public List<string> LanguageCodes { get; set; }
        public int? MinCountForReviewsByYou{ get; set; }
        public int? MaxCountForReviewsByYou { get; set; }
        public int? MinCountForReviewsAboutYou { get; set; }
        public int? MaxCountForReviewsAboutYou { get; set; }
        public int? MinCountForReservationsYouMade { get; set; }
        public int? MaxCountForReservationsYouMade { get; set; }

    }
}
