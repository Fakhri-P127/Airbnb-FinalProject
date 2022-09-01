using Airbnb.Application.Contracts.v1.Property.Responses;
using MediatR;

namespace Airbnb.Application.Features.Properties.Queries.GetAll
{
    public class PropertyGetAllQuery:IRequest<List<PropertyResponse>>
    {

    }
}
