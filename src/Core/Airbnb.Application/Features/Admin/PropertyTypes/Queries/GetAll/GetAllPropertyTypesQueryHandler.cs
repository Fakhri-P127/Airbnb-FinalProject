using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Queries.GetAll
{
    public class GetAllPropertyTypesQueryHandler : IRequestHandler<GetAllPropertyTypesQuery, List<GetPropertyTypeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public GetAllPropertyTypesQueryHandler(IMapper mapper, IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<List<GetPropertyTypeResponse>> Handle(GetAllPropertyTypesQuery request, CancellationToken cancellationToken)
        {
            List<PropertyType> propertyTypes = await _unit.PropertyTypeRepository
                .GetAllAsync(request.Expression,request.Parameters,
                false,PropertyTypeHelper.AllPropertyTypeIncludes());

            List<GetPropertyTypeResponse> responses = _mapper.Map<List<GetPropertyTypeResponse>>(propertyTypes);
            //if (!responses.Any()) throw new Exception("Internal server error");
            return responses;
        }
    }
}
