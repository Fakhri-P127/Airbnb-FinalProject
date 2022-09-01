using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.User.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Features.Authentication.Commands.Register;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.User.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public UpdateUserCommandHandler(IUnitOfWork unit, IMapper mapper, IWebHostEnvironment env)
        {
            _unit = unit;
            _mapper = mapper;
            _env = env;
        }
        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.RouteId,null,"Gender");
            if (user is null) throw new UserNotFoundValidationException() { ErrorMessage = "User with this Id doesn't exist." };
            _unit.UserRepository.Update(user);
            _mapper.Map(request, user);
            //user.ModifiedAt = DateTime.UtcNow;
            await ImageCheck(request, user);
            await _unit.SaveChangesAsync();
            UserResponse response = _mapper.Map<UserResponse>(user);
            // birinci gender verence deyer null olur, gel repo ile genderi tap ve responsedaki gendere beraber et eger nulldisa
            response.Verifications = new();
            if (user.EmailConfirmed) response.Verifications.Add("Email verified");
            if (user.PhoneNumberConfirmed) response.Verifications.Add("Phone number verified");


            return response;

        }

        private async Task ImageCheck(UpdateUserCommand request, AppUser user)
        {
            if (request.ProfilPicture is not null)
            {
                if (!request.ProfilPicture.IsImageOkay(2))
                {
                    throw new UserValidationException { ErrorMessage = "Image size too big" };
                }
                if (!string.IsNullOrWhiteSpace(user.ProfilPicture))
                    FileHelpers.FileDelete(_env.WebRootPath, "assets/images/UserProfilePictures", user.ProfilPicture);
                user.ProfilPicture = await request.ProfilPicture
                    .FileCreate(_env.WebRootPath, "assets/images/UserProfilePictures");
            }


        }
    }
}
