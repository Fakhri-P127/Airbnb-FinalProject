using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.GuestReviews.Queries.GetAll
{
    public class GetAllGuestReviewsQuery:IRequest<List<GuestReviewResponse>>
    {
        public Expression<Func<GuestReview, bool>> Expression { get; set; }
        public GetAllGuestReviewsQuery( Expression<Func<GuestReview, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
