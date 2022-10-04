using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Settings.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Common;
using AutoMapper;
using LinqKit;
using MediatR;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Queries.GetAll
{
    public class GetAllSettingsQueryHandler : IRequestHandler<GetAllSettingsQuery, List<SettingsResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllSettingsQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<SettingsResponse>> Handle(GetAllSettingsQuery request, CancellationToken cancellationToken)
        {
            ExpressionStarter<Settings> filters = FilterRequest(request);
            List<Settings> settings = await _unit.SettingsRepository
                 .GetAllAsync(filters, request.Parameters, false);

            List<SettingsResponse> responses = _mapper.Map<List<SettingsResponse>>(settings);
            return responses;
        }

        private static ExpressionStarter<Settings> FilterRequest(GetAllSettingsQuery request)
        {
            ExpressionStarter<Settings> filters = PredicateBuilder.New<Settings>(true);
            if (!string.IsNullOrWhiteSpace(request.Parameters.Key)) filters = filters
                    .And(x => x.Key.Contains(request.Parameters.Key));
            if (!string.IsNullOrWhiteSpace(request.Parameters.Value)) filters = filters
                   .And(x => x.Value.Contains(request.Parameters.Value));
            if (request.Expression is not null) filters = filters.And(request.Expression);
            ExpressionHelpers<Settings>.FilteredPredicateOrIfNoFilterReturnNull(filters);
            return filters;
        }
    }
}
