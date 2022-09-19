using Airbnb.Application.Contracts.v1.Client.Authentication;
using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQuery:IRequest<AuthenticationResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
