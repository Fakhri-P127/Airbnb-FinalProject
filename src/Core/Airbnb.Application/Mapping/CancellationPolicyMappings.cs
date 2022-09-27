using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Create;
using Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Update;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class CancellationPolicyMappings:Profile
    {

        public CancellationPolicyMappings()
        {
            CreateMap<CancellationPolicy, CancellationPolicyResponse>()
                .ForMember(dest => dest.PropertyCount, opt => {
                    opt.PreCondition(src => src.Properties != null);
                    opt.MapFrom(src => src.Properties.Count);
                    });

            CreateMap<CreateCancellationPolicyCommand, CancellationPolicy>();
            CreateMap<UpdateCancellationPolicyCommand, CancellationPolicy>();
        }
    }
}
