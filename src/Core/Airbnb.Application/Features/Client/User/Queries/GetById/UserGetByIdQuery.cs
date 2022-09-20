﻿using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.User.Queries.GetById
{
    public class UserGetByIdQuery:IRequest<UserResponse>
    {
        public Expression<Func<AppUser, bool>> Expression { get; set; }
        public string Id { get; set; }
        public UserGetByIdQuery(string id, Expression<Func<AppUser, bool>> expression = null)
        {
            Id = id;
            Expression = expression;

        }
    }
}
