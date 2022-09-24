using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQuery:IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public LoginQuery(string email,string password, bool rememberMe)
        {
            Email = email;
            Password = password;
            RememberMe = rememberMe;
        }

    }
}
