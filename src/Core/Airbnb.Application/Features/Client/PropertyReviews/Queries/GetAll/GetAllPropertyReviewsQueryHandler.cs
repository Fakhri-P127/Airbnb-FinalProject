using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
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
            if(request.Expression != null)
                await BaseHelper.GetIdFromExpression((BinaryExpression)request.Expression.Body,_unit,_userManager);
            
            List<PropertyReview> propertyReviews = await _unit.PropertyReviewRepository
                .GetAllAsync(request.Expression,request.Parameters, false, PropertyReviewHelper.AllPropertyReviewIncludes());
            List<PropertyReviewResponse> responses = _mapper.Map<List<PropertyReviewResponse>>(propertyReviews);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }

        
    }
}
