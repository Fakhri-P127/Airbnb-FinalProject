using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using MediatR;

namespace Airbnb.Application.Features.Admin.Countries.Commands.Create
{
    public class CreateCountryCommand : IRequest<CountryResponse>
    {
        public string Name { get; set; }
        public Guid RegionId { get; set; }
    }
}
