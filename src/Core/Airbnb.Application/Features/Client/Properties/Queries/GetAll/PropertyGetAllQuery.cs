using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using MediatR;

namespace Airbnb.Application.Features.Client.Properties.Queries.GetAll
{
    public class PropertyGetAllQuery:IRequest<List<GetPropertyResponse>>
    {

    }
}
