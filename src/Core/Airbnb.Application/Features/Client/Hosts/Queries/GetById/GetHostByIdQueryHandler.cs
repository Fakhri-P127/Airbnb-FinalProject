using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.Hosts.Queries.GetById
{
    public class GetHostByIdQueryHandler : IRequestHandler<GetHostByIdQuery, GetHostResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetHostByIdQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<GetHostResponse> Handle(GetHostByIdQuery request, CancellationToken cancellationToken)
        {
            Host host = await _unit.HostRepository.GetByIdAsync(request.Id,request.Expression,false,
             HostHelper.AllHostIncludes());
            if (host is null) throw new HostNotFoundException(request.Id);
            GetHostResponse response = _mapper.Map<GetHostResponse>(host);
            if (response is null) throw new Exception("Internal server error");
            return response;

        }
    }
}
