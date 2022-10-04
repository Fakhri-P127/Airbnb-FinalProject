using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Admin.Regions.Commands.Update
{
    public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, RegionResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdateRegionCommandHandler(IUnitOfWork unit,IMapper mapper,IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<RegionResponse> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            Region region = await CheckExceptionsThenReturnRegion(request);
            _unit.RegionRepository.Update(region, false);
            region.Name = request.Name;
            await _unit.SaveChangesAsync();
            return await RegionHelper.ReturnResponse(region, _unit, _mapper);
        }

        private async Task<Region> CheckExceptionsThenReturnRegion(UpdateRegionCommand request)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            Region region = await _unit.RegionRepository.GetByIdAsync(Id, null, true);
            if (region is null) throw new RegionNotFoundException();
            if (await _unit.RegionRepository.GetSingleAsync(x => x.Name == request.Name) is not null)
                throw new Region_DuplicateNameException(request.Name);
            return region;
        }
    }
}
