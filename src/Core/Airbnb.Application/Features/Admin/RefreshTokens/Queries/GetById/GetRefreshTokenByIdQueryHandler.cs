using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Responses;
using Airbnb.Application.Exceptions.PropertyTypes;
using Airbnb.Application.Exceptions.RefreshTokens;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.RefreshTokens.Queries.GetById
{
    public class GetRefreshTokenByIdQueryHandler : IRequestHandler<GetRefreshTokenByIdQuery, RefreshTokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public GetRefreshTokenByIdQueryHandler(IMapper mapper, IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<RefreshTokenResponse> Handle(GetRefreshTokenByIdQuery request, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = await _unit.RefreshTokenRepository
               .GetByIdAsync(request.Id, request.Expression);
            if (refreshToken is null) throw new RefreshTokenNotFoundException();
            RefreshTokenResponse response = _mapper.Map<RefreshTokenResponse>(refreshToken);
            return response;
        }
    }
}
