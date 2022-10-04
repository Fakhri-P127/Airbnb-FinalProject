using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.RefreshTokens;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;

namespace Airbnb.Application.Features.Client.Authentication.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler:IRequestHandler<RevokeRefreshTokenCommand>
    {
        private readonly IUnitOfWork _unit;

        public RevokeRefreshTokenCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = await _unit.RefreshTokenRepository.GetByIdAsync(request.Id, null, true);
            if (refreshToken is null) throw new RefreshTokenNotFoundException();
            _unit.RefreshTokenRepository.Update(refreshToken, false);
            refreshToken.IsRevoked = true;
            await _unit.SaveChangesAsync();
            return await Task.FromResult(Unit.Value);
        }
    }
}
