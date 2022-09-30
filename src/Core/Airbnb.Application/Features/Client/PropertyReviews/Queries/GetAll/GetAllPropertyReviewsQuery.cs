using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Parameters;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Parameters;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.PropertyReviews.Queries.GetAll
{
    public class GetAllPropertyReviewsQuery:IRequest<List<PropertyReviewResponse>>
    {
        public PropertyReviewParameters Parameters { get; set; }
        public Expression<Func<PropertyReview, bool>> Expression { get; set; }
        public GetAllPropertyReviewsQuery(PropertyReviewParameters parameters,Expression<Func<PropertyReview, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
