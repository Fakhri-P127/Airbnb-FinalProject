using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses.NestedResponses;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Create;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Update;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class PropertyGroupMappings:Profile
    {
        public PropertyGroupMappings()
        {
            CreateMap<PropertyGroup, GetPropertyGroupResponse>()
                .ForMember(dest => dest.PropertyCount, opt => opt.MapFrom(src => src.Properties.Count));
            CreateMap<PropertyGroup, PostPropertyGroupResponse>();
            CreateMap<PropertyGroupType, PropertyGroupTypeInPropertyGroup>();
            CreateMap<PropertyType, PropertyTypeInPropertyGroupType>();

            CreateMap<CreatePropertyGroupCommand, PropertyGroup>()
                .ForMember(x => x.Image, opt => opt.Ignore());
            CreateMap<UpdatePropertyGroupCommand, PropertyGroup>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
