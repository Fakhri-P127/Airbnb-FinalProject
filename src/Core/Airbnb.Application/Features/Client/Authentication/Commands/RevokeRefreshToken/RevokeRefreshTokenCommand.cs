using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand:IRequest
    {
        public Guid Id { get; set; }
        public RevokeRefreshTokenCommand(Guid id)
        {
            Id = id;
        }
    }
}
