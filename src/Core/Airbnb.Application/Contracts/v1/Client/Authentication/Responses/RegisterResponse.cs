using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Client.Authentication.Responses
{
    public class RegisterResponse:BaseResponse
    {
        public RegisterResponse()
        {
            Verifications = new();
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilPicture { get; set; }
        public List<string> Verifications { get; set; }
    }
}
