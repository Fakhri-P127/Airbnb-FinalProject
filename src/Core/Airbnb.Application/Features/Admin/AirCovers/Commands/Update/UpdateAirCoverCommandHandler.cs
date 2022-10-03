using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Exceptions.AirCovers;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Update
{
    public class UpdateAirCoverCommandHandler : IRequestHandler<UpdateAirCoverCommand, AirCoverResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdateAirCoverCommandHandler(IUnitOfWork unit,IMapper mapper,IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<AirCoverResponse> Handle(UpdateAirCoverCommand request, CancellationToken cancellationToken)
        {
            // guid olmadan gondersem evvelceden tutacaq ve bu error hech vaxt ishlemeyecek amma yenede her ehtimala qarshi yazdim
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            AirCover airCover = await _unit.AirCoverRepository.GetByIdAsync(Id, null,true);
            if (airCover is null) throw new AirCoverNotFoundException();
            _unit.AirCoverRepository.Update(airCover);
            _mapper.Map(request, airCover);
            await _unit.SaveChangesAsync();
            airCover = await _unit.AirCoverRepository.GetByIdAsync(airCover.Id, null);
            AirCoverResponse response = _mapper.Map<AirCoverResponse>(airCover);
            //if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
