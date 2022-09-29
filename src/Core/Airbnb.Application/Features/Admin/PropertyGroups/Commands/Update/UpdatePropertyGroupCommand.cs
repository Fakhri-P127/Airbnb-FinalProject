using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Commands.Update
{
    public class UpdatePropertyGroupCommand:IRequest<PostPropertyGroupResponse>
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
    }
}
