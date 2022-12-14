using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using MediatR;

namespace Airbnb.Application.Features.Admin.Cities.Commands.Update
{
    public class UpdateCityCommand:IRequest<CityResponse>
    {
        public string Name { get; set; }
        public Guid? CountryId { get; set; }
    }
}
