using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics;
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
            ExpressionStarter<Property> filters = FilterRequest(request);
            List<Property> properties = await _unit.PropertyRepository
                .GetAllAsync(filters,request.Parameters, false, PropertyHelper.AllPropertyIncludes());

            List<GetPropertyResponse> response = _mapper.Map<List<GetPropertyResponse>>(properties);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
        private static ExpressionStarter<Property> FilterRequest(PropertyGetAllQuery request)
        {
            ExpressionStarter<Property> filters = PredicateBuilder.New<Property>(true);
            if (!string.IsNullOrWhiteSpace(request.Parameters.Title)) filters = filters
                    .And(x => x.Title.Contains(request.Parameters.Title));

            filters = MinMaxFilters(request, filters);
            filters = IdFilters(request, filters);

            if (request.Parameters.IsPetAllowed.HasValue) filters = filters
                        .And(x => x.IsPetAllowed == request.Parameters.IsPetAllowed);

            if (request.Parameters.Amenities.Any())
            {
                foreach (Guid amenityId in request.Parameters.Amenities)
                {
                    filters = filters
                   .And(pa => pa.PropertyAmenities
                   .Any(x => x.AmenityId == amenityId));
                }
            }
            return ExpressionHelpers<Property>.FilteredPredicateOrIfNoFilterReturnNull(filters);
        }

        private static ExpressionStarter<Property> IdFilters(PropertyGetAllQuery request, ExpressionStarter<Property> filters)
        {
            if (request.Parameters.HostId.HasValue) filters = filters
                    .And(x => x.HostId == request.Parameters.HostId);
            if (request.Parameters.RegionId.HasValue) filters = filters
                .And(x => x.State.RegionId == request.Parameters.RegionId);
            if (request.Parameters.CountryId.HasValue) filters = filters
                   .And(x => x.State.CountryId == request.Parameters.CountryId);
            if (request.Parameters.CityId.HasValue) filters = filters
                   .And(x => x.State.CityId == request.Parameters.CityId);
            if (request.Parameters.AirCoverId.HasValue) filters = filters
                    .And(x => x.AirCoverId == request.Parameters.AirCoverId);
            if (request.Parameters.CancellationPolicyId.HasValue) filters = filters
                    .And(x => x.CancellationPolicyId == request.Parameters.CancellationPolicyId);
            if (request.Parameters.PrivacyTypeId.HasValue) filters = filters
                    .And(x => x.PrivacyTypeId == request.Parameters.PrivacyTypeId);
            if (request.Parameters.PropertyGroupId.HasValue) filters = filters
                    .And(x => x.PropertyGroupId == request.Parameters.PropertyGroupId);
            if (request.Parameters.PropertyTypeId.HasValue) filters = filters
                    .And(x => x.PropertyTypeId == request.Parameters.PropertyTypeId);
            return filters;
        }

        private static ExpressionStarter<Property> MinMaxFilters(PropertyGetAllQuery request, ExpressionStarter<Property> filters)
        {
            if (request.Parameters.MinPrice.HasValue) filters = filters
                    .And(x => x.Price >= request.Parameters.MinPrice);
            if (request.Parameters.MaxPrice.HasValue) filters = filters
                    .And(x => x.Price <= request.Parameters.MaxPrice);

            if (request.Parameters.MinGuestCountLimit.HasValue) filters = filters
                    .And(x => x.MaxGuestCount >= request.Parameters.MinGuestCountLimit);
            if (request.Parameters.MaxGuestCountLimit.HasValue) filters = filters
                    .And(x => x.MaxGuestCount <= request.Parameters.MaxGuestCountLimit);

            if (request.Parameters.MinMinimumNightCount.HasValue) filters = filters
                    .And(x => x.MinNightCount >= request.Parameters.MinMinimumNightCount);
            if (request.Parameters.MaxMinimumNightCount.HasValue) filters = filters
                   .And(x => x.MinNightCount <= request.Parameters.MaxMinimumNightCount);
            if (request.Parameters.MinMaximumNightCount.HasValue) filters = filters
                    .And(x => x.MaxNightCount >= request.Parameters.MinMaximumNightCount);
            if (request.Parameters.MaxMaximumNightCount.HasValue) filters = filters
                   .And(x => x.MaxNightCount <= request.Parameters.MaxMaximumNightCount);


            if (request.Parameters.MinCheckInTime.HasValue) filters = filters
                    .And(x => x.CheckInTime >= request.Parameters.MinCheckInTime);
            if (request.Parameters.MaxCheckInTime.HasValue) filters = filters
                   .And(x => x.CheckInTime <= request.Parameters.MaxCheckInTime);

            if (request.Parameters.MinCheckOutTime.HasValue) filters = filters
                   .And(x => x.CheckOutTime >= request.Parameters.MinCheckOutTime);
            if (request.Parameters.MaxCheckOutTime.HasValue) filters = filters
                   .And(x => x.CheckOutTime <= request.Parameters.MaxCheckOutTime);

            if (request.Parameters.MinOverallScore.HasValue) filters = filters
                    .And(x => x.Reservations
                    .Any(x => x.PropertyReview.OverallScore >= request.Parameters.MinOverallScore));
            if (request.Parameters.MaxOverallScore.HasValue) filters = filters
                   .And(x => x.Reservations
                   .Any(x => x.PropertyReview.OverallScore <= request.Parameters.MaxOverallScore));

            if (request.Parameters.MinBathroomCount.HasValue) filters = filters
                    .And(x => x.BathroomCount >= request.Parameters.MinBathroomCount);
            if (request.Parameters.MaxBathroomCount.HasValue) filters = filters
                   .And(x => x.BathroomCount <= request.Parameters.MaxBathroomCount);
            if (request.Parameters.MinBedCount.HasValue) filters = filters
                   .And(x => x.BedCount >= request.Parameters.MinBedCount);
            if (request.Parameters.MaxBedCount.HasValue) filters = filters
                  .And(x => x.BedCount <= request.Parameters.MaxBedCount);
            if (request.Parameters.MinBedroomCount.HasValue) filters = filters
                  .And(x => x.BedroomCount >= request.Parameters.MinBedroomCount);
            if (request.Parameters.MaxBedroomCount.HasValue) filters = filters
                  .And(x => x.BedroomCount <= request.Parameters.MaxBedroomCount);
            return filters;
        }
    }
}
