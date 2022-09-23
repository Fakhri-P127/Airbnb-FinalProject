using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Contracts.v1.Admin.EmailRelated.Responses;
using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Authentication.CustomFrameworkClasses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Application.Features.Client.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResponse>
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly LinkGenerator _generator;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _accessor;

        public RegisterCommandHandler(CustomUserManager<AppUser> userManager, IMapper mapper,
            IWebHostEnvironment env,LinkGenerator generator,IEmailSender emailSender
            ,IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _env = env;
            _generator = generator;
            _emailSender = emailSender;
            _accessor = accessor;
        }
        public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await CheckAppUserErrors(request);
            AppUser user = _mapper.Map<AppUser>(request);
            if (request.PhoneNumber is not null) user.PhoneNumberConfirmed = true;
            //user.CreatedAt = DateTime.UtcNow;
            //user.ModifiedAt = DateTime.UtcNow;
            await ImageCheck(request, user);
            IdentityResult createdUserResult = await _userManager.CreateAsync(user, request.Password);
            await CheckIfResultIsSuccessful(user, createdUserResult, "creating User.");
            //FormFileCollection files = new();
            //files.Add(request.ProfilPicture);
            await AuthenticationHelper.SendConfirmationEmail(user,null,_userManager,_generator,_accessor,_emailSender);
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "Guest");
            await CheckIfResultIsSuccessful(user, roleResult, "adding Role to User.");
            // register deki tokeni silmek olar
            //string token = await _jwtTokenGenerator.GenerateTokenAsync(user);
            //var authResult = _mapper.Map<AuthenticationResponse>(user);
            //if (authResult is null) throw new Exception("Internal server error");
            //authResult.Token = token;
            //if (user.EmailConfirmed) authResult.Verifications.Add("Email verified");
            //if (user.PhoneNumberConfirmed) authResult.Verifications.Add("Phone number verified");
            return new AuthenticationResponse();
            //return authResult;
        }

  

        private async Task CheckIfResultIsSuccessful(AppUser user, IdentityResult result,
           string errorMessage)
        {
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                if (!string.IsNullOrWhiteSpace(user.ProfilPicture))
                    FileHelpers.FileDelete(_env.WebRootPath, "assets/images/UserProfilePictures", user.ProfilPicture);
                throw new UserValidationException()
                {
                    ErrorMessage = $"One or more validation errors occured while {errorMessage}",
                    ErrorMessages = result.Errors
                };
            }
        }

        private async Task CheckAppUserErrors(RegisterCommand request)
        {
            //AppUser user = await _userManager.FindByEmailAsync(request.Email);
            //if (user is not null) throw new DuplicateEmailValidationException();
            if (request.PhoneNumber is not null)
            {
                //AppUser user = await _unit.UserRepository.GetSingleAsync(x => x.PhoneNumber == request.PhoneNumber)
                AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
                if (user is not null )
                    throw new DuplicatePhoneNumberException();
            }
        }

        private async Task ImageCheck(RegisterCommand request, AppUser user)
        {
            if (request.ProfilPicture is not null)
            {
                if (!request.ProfilPicture.IsImageOkay(2))
                {
                    throw new UserProfilPictureException { ErrorMessage = "Image size too big" };
                }

                user.ProfilPicture = await request.ProfilPicture
                    .FileCreate(_env.WebRootPath, "assets/images/UserProfilePictures");
            }


        }
    }
}
