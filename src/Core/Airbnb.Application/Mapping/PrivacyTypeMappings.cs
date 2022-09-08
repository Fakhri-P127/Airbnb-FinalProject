using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class PrivacyTypeMappings:Profile
    {
        public PrivacyTypeMappings()
        {
            CreateMap<PrivacyType, PrivacyTypeResponse>()
                .ForMember(dest => dest.PropertyCount, opt => {
                    opt.PreCondition(src => src.Properties != null);
                    opt.MapFrom(src => src.Properties.Count);
                });

            
        }
    }
}
