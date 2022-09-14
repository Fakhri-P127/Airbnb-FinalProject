using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.GuestReviews.Queries.GetAll
{
    public class GetAllGuestReviewsQueryHandler : IRequestHandler<GetAllGuestReviewsQuery, List<GuestReviewResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllGuestReviewsQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<GuestReviewResponse>> Handle(GetAllGuestReviewsQuery request, CancellationToken cancellationToken)
        {
            List<GuestReview> guestReviews = await _unit.GuestReviewRepository
              .GetAllAsync(request.Expression, GuestReviewHelper.AllGuestReviewIncludes());
            List<GuestReviewResponse> responses = _mapper.Map<List<GuestReviewResponse>>(guestReviews);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
