using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.Authentication.Commands.Register
{
    public class RegisterCommand:IRequest<AuthenticationResponse>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        //not requireds
        public string PhoneNumber { get; set; }
        public IFormFile ProfilPicture { get; set; }

    }
}
