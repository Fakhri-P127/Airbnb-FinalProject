using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.Application.Features.Client.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _env;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterCommandHandler(UserManager<AppUser> userManager,IMapper mapper,IUnitOfWork unit,
            IWebHostEnvironment env,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unit = unit;
            _env = env;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is not null) throw new DuplicateEmailValidationException();
            // 
            List<AppUser> users = await _unit.UserRepository.GetAllAsync(x => x.PhoneNumber == request.PhoneNumber);
            if (users.Any())
                throw new DuplicatePhoneNumberException();

            user = _mapper.Map<AppUser>(request);
            user.CreatedAt = DateTime.UtcNow;
            user.ModifiedAt = DateTime.UtcNow;
            user.PhoneNumberConfirmed = true;
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
            string token = await _jwtTokenGenerator.GenerateTokenAsync(user);
            var authResult = _mapper.Map<AuthenticationResponse>(user);
           
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
