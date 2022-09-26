using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Cities.Commands.Create
{
    public class CreateCityCommandHandler:IRequestHandler<CreateCityCommand,CityResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateCityCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<CityResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            City city = _mapper.Map<City>(request);
            await _unit.CityRepository.AddAsync(city);
            return await CityHelper.ReturnResponse(city, _unit, _mapper);
        }
    }
}
