using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.Properties.Queries.GetAll
{
    public class PropertyGetAllQueryHandler : IRequestHandler<PropertyGetAllQuery, List<GetPropertyResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public PropertyGetAllQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
       
        public async Task<List<GetPropertyResponse>> Handle(PropertyGetAllQuery request, CancellationToken cancellationToken)
        {
            List<Property> properties = await _unit.PropertyRepository
                .GetAllAsync(null, PropertyHelper.AllPropertyIncludes());
            
            List<GetPropertyResponse> response = _mapper.Map<List<GetPropertyResponse>>(properties);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
