using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using MediatR;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Update
{
    public class UpdateAmenityCommand:IRequest<PostAmenityResponse>
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? AmenityTypeId { get; set; }

    }
}
