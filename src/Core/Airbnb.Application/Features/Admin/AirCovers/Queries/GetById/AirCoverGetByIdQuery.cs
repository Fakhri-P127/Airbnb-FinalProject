using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AirCovers.Queries.GetById
{
    public class AirCoverGetByIdQuery:IRequest<AirCoverResponse>
    {
        public Expression<Func<AirCover, bool>> Expression { get; set; }
        public Guid Id { get; set; }
        public AirCoverGetByIdQuery(Guid id, Expression<Func<AirCover, bool>> expression=null)
        {
            Id = id;
            Expression = expression;
        }
    }
}
