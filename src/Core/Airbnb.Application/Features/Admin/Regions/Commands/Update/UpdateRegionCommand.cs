using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using MediatR;

namespace Airbnb.Application.Features.Admin.Regions.Commands.Update
{
    public class UpdateRegionCommand:IRequest<RegionResponse>
    {
        public string Name { get; set; }
    }
}
