namespace Airbnb.Application.Contracts.v1.Client.Authentication.Responses
{
    public class AuthSuccessResponse
    {
        //public string Firstname { get; set; }
        //public string Lastname { get; set; }
        //public string Username { get; set; }
        //public string Email { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string PhoneNumber { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
