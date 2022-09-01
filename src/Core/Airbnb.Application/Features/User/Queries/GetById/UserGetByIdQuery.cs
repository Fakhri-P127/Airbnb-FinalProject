using Airbnb.Application.Contracts.v1.User.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.User.Queries.GetById
{
    public class UserGetByIdQuery:IRequest<UserResponse>
    {
        public string Id { get; set; }
        public UserGetByIdQuery(string id)
        {
            Id = id;
        }
    }
}
