using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Application.Features.Client.Authentication.Commands.Register
{
    /// <summary>
    /// Mesaji telefona da gondermek olardi amma emaile gondermeyi tercih etdim. Ilk defe oyrenmek uchun 
    /// daha mentiqli geldi. Amma telefon uchunde Twilio dan istifade ederek SMS gondermek olardi.
    /// </summary>
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly LinkGenerator _generator;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _accessor;

        public RegisterCommandHandler(CustomUserManager<AppUser> userManager, IMapper mapper,
            IWebHostEnvironment env, LinkGenerator generator, IEmailSender emailSender
            , IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _env = env;
            _generator = generator;
            _emailSender = emailSender;
            _accessor = accessor;
        }
        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await CheckAppUserErrors(request);
            AppUser user = _mapper.Map<AppUser>(request);
            user.PhoneNumberConfirmed = true; // login olmaq uchun nomre verification true olmalidi demishik.
            await ImageCheck(request, user);
            IdentityResult createdUserResult = await _userManager.CreateAsync(user, request.Password);
            await CheckIfResultIsSuccessful(user, createdUserResult, "creating User.");

            await SendConfirmationEmailToUser(request, user);
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "Guest");
            await CheckIfResultIsSuccessful(user, roleResult, "adding Role to User.");
            // register deki tokeni silmek olar

            RegisterResponse response = _mapper.Map<RegisterResponse>(user);
            //if (response is null) throw new Exception("Internal server error");

            response.Verifications.Add("Phone number verified");
            return response;
        }

        private async Task SendConfirmationEmailToUser(RegisterCommand request, AppUser user)
        {
            FormFileCollection files = new();
            if (request.ProfilPicture is not null) files.Add(request.ProfilPicture);
            await EmailSenderHelpers.SendConfirmationEmail(user, files, _userManager, _generator, _accessor, _emailSender);
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

            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
            if (user is not null)
                throw new DuplicatePhoneNumberException();

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
