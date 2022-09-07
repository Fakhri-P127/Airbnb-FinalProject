using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.Amenities.Commands.Create
{
    public class CreateAmenityCommand:IRequest<PostAmenityResponse>
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AmenityTypeId { get; set; }
    }
}
