using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.Properties.Queries.GetAll
{
    public class PropertyGetAllQueryHandler : IRequestHandler<PropertyGetAllQuery, List<GetPropertyResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public PropertyGetAllQueryHandler(IUnitOfWork unit, IMapper mapper, IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<List<GetPropertyResponse>> Handle(PropertyGetAllQuery request, CancellationToken cancellationToken)
        {
            if (_accessor.HttpContext.GetRouteValue("hostId") != null)
            {
                Guid hostId = BaseHelper.GetHostIdFromRoute(_accessor);
                if (await _unit.HostRepository.GetByIdAsync(hostId, null) is null)
                    throw new HostNotFoundException(hostId);
            }
            List<Property> properties = await _unit.PropertyRepository
                .GetAllAsync(request.Expression);

            List<GetPropertyResponse> response = _mapper.Map<List<GetPropertyResponse>>(properties);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
