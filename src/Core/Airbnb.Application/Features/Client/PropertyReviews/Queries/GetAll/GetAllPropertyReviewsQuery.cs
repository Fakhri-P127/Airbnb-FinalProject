﻿using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
