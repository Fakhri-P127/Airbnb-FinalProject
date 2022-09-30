using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.GuestReviews.Queries.GetAll
{
    public class GetAllGuestReviewsQueryHandler : IRequestHandler<GetAllGuestReviewsQuery, List<GuestReviewResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public GetAllGuestReviewsQueryHandler(IUnitOfWork unit, IMapper mapper,CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<List<GuestReviewResponse>> Handle(GetAllGuestReviewsQuery request, CancellationToken cancellationToken)
        {
            if (request.Expression != null)
                await BaseHelper.GetIdFromExpression((BinaryExpression)request.Expression.Body, _unit,_userManager);

            List<GuestReview> guestReviews = await _unit.GuestReviewRepository
              .GetAllAsync(request.Expression,request.Parameters,false, GuestReviewHelper.AllGuestReviewIncludes());
            List<GuestReviewResponse> responses = _mapper.Map<List<GuestReviewResponse>>(guestReviews);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
