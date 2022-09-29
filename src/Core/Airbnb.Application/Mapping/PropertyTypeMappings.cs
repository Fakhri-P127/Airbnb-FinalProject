using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
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
            
            CreateMap<PropertyGroup, PropertyGroupInPropertyType>();

            CreateMap<CreatePropertyTypeCommand, PropertyType>();
            CreateMap<UpdatePropertyTypeCommand, PropertyType>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
