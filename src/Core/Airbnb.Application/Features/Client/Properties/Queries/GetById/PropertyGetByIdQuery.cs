using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using MediatR;

namespace Airbnb.Application.Features.Client.Properties.Queries.GetById
{
    public class PropertyGetByIdQuery:IRequest<GetPropertyResponse>
    {
        public Guid Id { get; set; }
        public PropertyGetByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
