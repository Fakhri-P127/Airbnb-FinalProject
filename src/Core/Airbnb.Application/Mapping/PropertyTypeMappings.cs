using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Create;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Update;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses.NestedResponses;
using Airbnb.Application.Features.Admin.PropertyTypes.Commands.Create;
using Airbnb.Application.Features.Admin.PropertyTypes.Commands.Update;

namespace Airbnb.Application.Mapping
{
    public class PropertyTypeMappings:Profile
    {
        public PropertyTypeMappings()
        {
            CreateMap<PropertyType, GetPropertyTypeResponse>()
                .ForMember(dest => dest.PropertyCount, opt => opt.MapFrom(src => src.Properties.Count));
            CreateMap<PropertyType, PostPropertyTypeResponse>();
            CreateMap<PropertyGroupType, PropertyGroupTypeInPropertyType>();
            CreateMap<PropertyGroup, PropertyGroupInPropertyGroupType>();

            CreateMap<CreatePropertyTypeCommand, PropertyType>();
            CreateMap<UpdatePropertyTypeCommand, PropertyType>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
