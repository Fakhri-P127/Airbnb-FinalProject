﻿using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.User.Queries.GetAll
{
    public class UserGetAllQuery : IRequest<List<UserResponse>>
    {
        public Expression<Func<AppUser, bool>> Expression { get; set; } = null;
    }
}