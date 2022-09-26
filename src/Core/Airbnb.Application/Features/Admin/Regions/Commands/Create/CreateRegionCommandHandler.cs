using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Regions.Commands.Create
{
    public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, RegionResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateRegionCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<RegionResponse> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            Region region = await _unit.RegionRepository.GetSingleAsync(x => x.Name == request.Name);
            if (region is not null) throw new Region_DuplicateNameException(request.Name);
            region = new Region { Name = request.Name };
            await _unit.RegionRepository.AddAsync(region);
            return await RegionHelper.ReturnResponse(region, _unit, _mapper);
        }
    }
}
