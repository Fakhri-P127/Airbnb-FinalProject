using Airbnb.Application.Contracts.v1.Client.GuestReviews.Parameters;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.GuestReviews.Queries.GetAll
{
    public class GetAllGuestReviewsQuery:IRequest<List<GuestReviewResponse>>
    {
        public GuestReviewParameters Parameters { get; set; }
        public Expression<Func<GuestReview, bool>> Expression { get; set; }
        public GetAllGuestReviewsQuery(GuestReviewParameters parameters, Expression<Func<GuestReview, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
