using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Admin.Amenities.Queries.GetAll
{
    public class GetAllAmenityQueryHandler : IRequestHandler<GetAllAmenityQuery, List<GetAmenityResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllAmenityQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<GetAmenityResponse>> Handle(GetAllAmenityQuery request, CancellationToken cancellationToken)
        {
            //ExpressionStarter<Amenity> predicate = FilterRequest(request);
            List<Amenity> amenities = await _unit.AmenityRepository
                .GetAllAsync(FilterRequest(request),request.Parameters, false, "AmenityType",
                "PropertyAmenities");
            List<GetAmenityResponse> responses = _mapper.Map<List<GetAmenityResponse>>(amenities);
            //if (!responses.Any()) throw new Exception("Internal server error");
            return responses;
        }

        private static ExpressionStarter<Amenity> FilterRequest(GetAllAmenityQuery request)
        {
            ExpressionStarter<Amenity> predicate = PredicateBuilder.New<Amenity>(true);
            if (request.Parameters.AmenityTypeId.HasValue) predicate = predicate
                    .And(x => x.AmenityTypeId == request.Parameters.AmenityTypeId);
            if (!string.IsNullOrWhiteSpace(request.Parameters.Name)) predicate = predicate
                    .And(x => x.Name.Contains(request.Parameters.Name));
            if (!string.IsNullOrWhiteSpace(request.Parameters.Description)) predicate = predicate
                    .And(x => x.Description.Contains(request.Parameters.Description));
            if (request.Expression != null) predicate = predicate.And(request.Expression);
            // burda bildiririk ki, eger predicate deyeri f=>true dusa demeli
            // filter uchun hech ne gonderilmeyib. onda null a beraber edirik ki
            // getAll da expressionsiz AsQueryable() edek.
            return ExpressionHelpers<Amenity>.FilteredPredicateOrIfNoFilterReturnNull(predicate);
        }
    }
}

