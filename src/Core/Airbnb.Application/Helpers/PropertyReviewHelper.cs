using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
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
    public static class PropertyReviewHelper
    {
        public async static Task<PropertyReviewResponse> ReturnResponse(PropertyReview propertyReview,
            IUnitOfWork _unit, IMapper _mapper)
        {
            propertyReview = await _unit.PropertyReviewRepository.GetByIdAsync(propertyReview.Id, null,false,
                AllPropertyReviewIncludes());
            PropertyReviewResponse response = _mapper.Map<PropertyReviewResponse>(propertyReview);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
        public static string[] AllPropertyReviewIncludes()
        {
            string[] includes = new[] { "Reservation", "AppUser" };
            return includes;
        }
    }
}
