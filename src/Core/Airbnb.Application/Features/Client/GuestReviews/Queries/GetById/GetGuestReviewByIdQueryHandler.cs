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

namespace Airbnb.Application.Features.Client.GuestReviews.Queries.GetById
{
    public class GetGuestReviewByIdQueryHandler : IRequestHandler<GetGuestReviewByIdQuery, GuestReviewResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetGuestReviewByIdQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<GuestReviewResponse> Handle(GetGuestReviewByIdQuery request, CancellationToken cancellationToken)
        {
             GuestReview guestReview = await _unit.GuestReviewRepository
                .GetByIdAsync(request.Id, request.Expression,false, GuestReviewHelper.AllGuestReviewIncludes());
            if (guestReview is null) throw new GuestReviewNotFoundException(request.Id);
            GuestReviewResponse response = _mapper.Map<GuestReviewResponse>(guestReview);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
