using Airbnb.Application.Features.Authentication.Commands.Register;
using Airbnb.Application.Features.Authentication.Common;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Common;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class AuthenticationMapping:Profile
    {
        //private readonly IWebHostEnvironment _env;

        public AuthenticationMapping()
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

            CreateMap<AppUser, AuthenticationResult>()
                .ForMember(x=>x.Id,d=>d.MapFrom(r=> r.Id));
            ;
        }
    }
}
