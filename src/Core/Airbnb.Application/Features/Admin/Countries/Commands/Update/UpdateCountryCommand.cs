using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using MediatR;

namespace Airbnb.Application.Features.Admin.Countries.Commands.Update
{
    public class UpdateCountryCommand:IRequest<CountryResponse>
    {
        public string Name { get; set; }
        public Guid? RegionId { get; set; }
    }
}
