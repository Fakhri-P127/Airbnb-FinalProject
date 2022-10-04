using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Application.Contracts.v1.Admin.Settings.Responses;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Exceptions.Settings;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Common;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Queries.GetById
{
    public class GetSettingsByIdQueryHandler : IRequestHandler<GetSettingByIdQuery, SettingsResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetSettingsByIdQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<SettingsResponse> Handle(GetSettingByIdQuery request, CancellationToken cancellationToken)
        {
            Settings settings = await _unit.SettingsRepository
            .GetByIdAsync(request.Id, request.Expression);
            if (settings is null) throw new SettingsNotFoundException();
            SettingsResponse response = _mapper.Map<SettingsResponse>(settings);
            return response;
        }
    }
}
