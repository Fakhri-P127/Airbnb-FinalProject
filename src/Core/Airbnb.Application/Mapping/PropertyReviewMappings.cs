using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses.NestedResponses;
using Airbnb.Application.Features.Client.PropertyReviews.Commands.Create;
using Airbnb.Application.Features.Client.PropertyReviews.Commands.Update;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class PropertyReviewMappings : Profile
    {
        public PropertyReviewMappings()
        {
            CreateMap<CreatePropertyReviewCommand, PropertyReview>()
                .ForMember(dest => dest.OverallScore, opt => opt.MapFrom(src =>
                Math.Round(src.AverageOverallScore(), 2)));

            CreateMap<UpdatePropertyReviewCommand, PropertyReview>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OverallScore, opt => opt
                .MapFrom(src => Math.Round(src.AverageOverallScore(), 2)))
                .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<PropertyReview, PropertyReviewResponse>();
                //.ForMember(dest=>dest.Property,opt=>opt
                //.MapFrom(src=>$"https://localhost:7146/api/v1/propertyreviews/{src.PropertyId}"));
            CreateMap<AppUser, AppUserInPropertyReviewResponse>();
            CreateMap<Reservation, ReservationInPropertyReviewResponse>();
        }
    }
}
