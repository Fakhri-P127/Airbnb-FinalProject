using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Commands.Update
{
    public class UpdatePropertyTypeCommand:IRequest<PostPropertyTypeResponse>
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
