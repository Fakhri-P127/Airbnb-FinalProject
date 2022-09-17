using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.PropertyReviews.Queries.GetById
{
    public class GetPropertyReviewByIdQuery:IRequest<PropertyReviewResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<PropertyReview, bool>> Expression { get; set; }
        public GetPropertyReviewByIdQuery(Guid id, Expression<Func<PropertyReview, bool>> expression = null)
        {
            Id = id;
            Expression = expression;
        }
    }
}
