using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Commands.Update
{
    public class UpdatePropertyGroupCommand:IRequest<PostPropertyGroupResponse>
    {
        public Guid Id { get; set; }
        public IFormFile Image { get; set; }
        public string Name { get; set; }
    }
}
