using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.GuestReviews.Queries.GetById
{
    public class GetGuestReviewByIdQuery:IRequest<GuestReviewResponse>
    {
        public Guid Id { get; set; }
      
        public Expression<Func<GuestReview, bool>> Expression { get; set; }
        public GetGuestReviewByIdQuery(Guid id,Expression<Func<GuestReview, bool>> expression = null)
        {
            Id = id;
            Expression = expression;
        }
    }
}
