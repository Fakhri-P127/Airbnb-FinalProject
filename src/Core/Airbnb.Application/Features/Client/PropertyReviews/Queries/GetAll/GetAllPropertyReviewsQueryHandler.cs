using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.PropertyReviews.Queries.GetAll
{
    public class GetAllPropertyReviewsQueryHandler : IRequestHandler<GetAllPropertyReviewsQuery, List<PropertyReviewResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public GetAllPropertyReviewsQueryHandler(IUnitOfWork unit, IMapper mapper,CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<List<PropertyReviewResponse>> Handle(GetAllPropertyReviewsQuery request, CancellationToken cancellationToken)
        {
            // basehelper le de Id ni goturmek olardi amma maraqli olsun deye ferqli yollada etdim
            if (request.Expression != null)
                await BaseHelper.GetIdFromExpression((BinaryExpression)request.Expression.Body, _unit, _userManager);
           ExpressionStarter<PropertyReview> filters =  FilterRequest(request);
            List<PropertyReview> propertyReviews = await _unit.PropertyReviewRepository
                .GetAllAsync(filters, request.Parameters, false, PropertyReviewHelper.AllPropertyReviewIncludes());
            List<PropertyReviewResponse> responses = _mapper.Map<List<PropertyReviewResponse>>(propertyReviews);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }

        private static ExpressionStarter<PropertyReview> FilterRequest(GetAllPropertyReviewsQuery request)
        {
            ExpressionStarter<PropertyReview> filters = PredicateBuilder.New<PropertyReview>(true);
            if (request.Parameters.AppUserId.HasValue) filters = filters
                    .And(x => x.AppUserId == request.Parameters.AppUserId);
            if (request.Parameters.ReservationId.HasValue) filters = filters
                    .And(x => x.ReservationId == request.Parameters.ReservationId);
            if (request.Parameters.HostId.HasValue) filters = filters
                    .And(x => x.HostId == request.Parameters.HostId);
            if (request.Parameters.PropertyId.HasValue) filters = filters
                    .And(x => x.Reservation.PropertyId == request.Parameters.PropertyId);
            return ExpressionHelpers<PropertyReview>.FilteredPredicateOrIfNoFilterReturnNull(filters);
        }

    }
}
