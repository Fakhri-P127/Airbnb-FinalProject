using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using LinqKit;
using MediatR;

namespace Airbnb.Application.Features.Admin.RefreshTokens.Queries.GetAll
{
    public class GetAllRefreshTokensQueryHandler:IRequestHandler<GetAllRefreshTokensQuery,List<RefreshTokenResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public GetAllRefreshTokensQueryHandler(IMapper mapper, IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<List<RefreshTokenResponse>> Handle(GetAllRefreshTokensQuery request, CancellationToken cancellationToken)
        {
            ExpressionStarter<RefreshToken> filters = FilterRequest(request);
            List<RefreshToken> refreshTokens = await _unit.RefreshTokenRepository
              .GetAllAsync(filters, request.Parameters, false);

            List<RefreshTokenResponse> responses = _mapper.Map<List<RefreshTokenResponse>>(refreshTokens);
            return responses;
        }

        private static ExpressionStarter<RefreshToken> FilterRequest(GetAllRefreshTokensQuery request)
        {
            ExpressionStarter<RefreshToken> filters = PredicateBuilder.New<RefreshToken>(true);
            if (request.Parameters.MinExpiryDate.HasValue) filters = filters
                    .And(x => x.ExpiryDate >= request.Parameters.MinExpiryDate.Value);
            if (request.Parameters.MaxExpiryDate.HasValue) filters = filters
                    .And(x => x.ExpiryDate <= request.Parameters.MaxExpiryDate.Value);
            if (request.Parameters.HasBeenUsed.HasValue) filters = filters
                    .And(x => x.HasBeenUsed == request.Parameters.HasBeenUsed.Value);
            if (request.Parameters.IsRevoked.HasValue) filters = filters
                 .And(x => x.IsRevoked == request.Parameters.IsRevoked.Value);
            if (request.Parameters.AppUserId.HasValue) filters = filters
                    .And(x => x.AppUserId == request.Parameters.AppUserId.Value);
            if (request.Expression is not null) filters = filters
                    .And(request.Expression);
            ExpressionHelpers<RefreshToken>.FilteredPredicateOrIfNoFilterReturnNull(filters);
            return filters;
        }
    }
}
