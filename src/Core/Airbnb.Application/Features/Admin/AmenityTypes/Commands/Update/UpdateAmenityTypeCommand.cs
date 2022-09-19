using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Commands.Update
{
    public class UpdateAmenityTypeCommand:IRequest<AmenityTypeResponse>
    {
        public string Name { get; set; }
    }
}
