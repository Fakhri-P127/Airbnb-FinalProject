using Airbnb.Application.Contracts.v1.Property.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Properties.Queries.GetById
{
    public class PropertyGetByIdQuery:IRequest<PropertyResponse>
    {
        public Guid Id { get; set; }
        public PropertyGetByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
