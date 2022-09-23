using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.PropertyReviews.Queries.GetAll
{
    public class GetAllPropertyReviewsQuery:IRequest<List<PropertyReviewResponse>>
    {
        public Expression<Func<PropertyReview, bool>> Expression { get; set; }
        public GetAllPropertyReviewsQuery(Expression<Func<PropertyReview, bool>> expression = null)
        {
            Expression = expression;
        }
    }
}
