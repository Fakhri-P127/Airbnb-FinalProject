using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Helpers
{
    public static class GuestReviewHelper
    {
        public async static Task<GuestReviewResponse> ReturnResponse(GuestReview guestReview, IUnitOfWork _unit, IMapper _mapper)
        {
            guestReview = await _unit.GuestReviewRepository.GetByIdAsync(guestReview.Id, null,
                AllGuestReviewIncludes());
            GuestReviewResponse response = _mapper.Map<GuestReviewResponse>(guestReview);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }

        public static string[] AllGuestReviewIncludes()
        {
            string[] includes = new[] { "Host", "Reservation", "AppUser" };
            return includes;
        }
    }
}
