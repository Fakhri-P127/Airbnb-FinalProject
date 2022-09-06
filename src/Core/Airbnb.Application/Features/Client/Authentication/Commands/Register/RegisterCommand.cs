using Airbnb.Application.Features.Client.Authentication.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.Authentication.Commands.Register
{
    public class RegisterCommand:IRequest<AuthenticationResult>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        //not requireds
        public string PhoneNumber { get; set; }
        public IFormFile ProfilPicture { get; set; }

    }
}
