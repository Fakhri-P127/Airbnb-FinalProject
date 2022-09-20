using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Features.Client.Authentication.Commands.Register;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class AuthenticationMappings:Profile
    {
        //private readonly IWebHostEnvironment _env;

        public AuthenticationMappings()
        {
            //_env = env;
            CreateMap<RegisterCommand, AppUser>()
                .ForMember(x => x.ProfilPicture, d => d.Ignore())

                //.MapFrom(r => r.ProfilPicture.FileCreate(_env.WebRootPath,"assets/images/UserProfilePictures")))
                .ForMember(x => x.UserName, d => d
                .MapFrom(r => $"{r.Firstname}{r.Lastname}"));

                //.MapFrom(r => $"{r.Firstname.Substring(0, 3)}{r.Lastname.Substring(r.Lastname.Length - 3)}"));

                //.ForMember(x => x.Status, d => d
                //.MapFrom(r => r.Status == true ? "active" : r.Status == false ? "banned" : "suspended"));
                
                //.ForMember(x=>x.DateOfBirth.Date,d=>d.MapFrom(r=>r.DateOfBirth));

            CreateMap<AppUser, AuthenticationResponse>()
                .ForMember(x=>x.Id,d=>d.MapFrom(r=> r.Id));
            ;
        }
    }
}
