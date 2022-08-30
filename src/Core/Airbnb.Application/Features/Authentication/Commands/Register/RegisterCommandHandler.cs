using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Features.Authentication.Common;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;

namespace Airbnb.Application.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _env;

        public RegisterCommandHandler(UserManager<AppUser> userManager,IMapper mapper,IUnitOfWork unit,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unit = unit;
            _env = env;
        }
        public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            //if (user is not null) throw new DuplicateEmailValidationException();
            // 
            List<AppUser> users = await _unit.UserRepository.GetAllAsync(x => x.PhoneNumber == request.PhoneNumber);
            if (users.Any())
                throw new DuplicatePhoneNumberException();

            user = _mapper.Map<AppUser>(request);
            user.CreatedAt = DateTime.UtcNow;
            await ImageCheck(request, user);
            IdentityResult createdUser = await _userManager.CreateAsync(user, request.Password);
            if (!createdUser.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                if(!string.IsNullOrWhiteSpace(user.ProfilPicture))
                    FileHelpers.FileDelete(_env.WebRootPath, "assets/images/UserProfilePictures", user.ProfilPicture);
                
                
                throw new UserValidationException()
                {
                    ErrorMessage = createdUser.Errors.FirstOrDefault().Description
                };
            }
            // if image exists it checks image size and sets the image

        // register deki tokeni silmek olar
            string token = await _unit.JwtTokenGenerator.GenerateTokenAsync(user);
            var authResult = _mapper.Map<AuthenticationResult>(user);
            if (user.EmailConfirmed) authResult.Verifications.Add("Email verified");
            if (user.PhoneNumberConfirmed) authResult.Verifications.Add("Phone number verified");
            authResult.Token = token;
            
            return authResult;
        }

        private async Task ImageCheck(RegisterCommand request, AppUser user)
        {
            if (request.ProfilPicture is not null)
            {
                if (!request.ProfilPicture.IsImageOkay(2))
                {
                    throw new UserValidationException { ErrorMessage = "Image size too big" };
                }

                user.ProfilPicture = await request.ProfilPicture
                    .FileCreate(_env.WebRootPath, "assets/images/UserProfilePictures");
            }


        }
    }
}
