using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Exceptions.AuthenticationExceptions;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Airbnb.Application.Features.Client.User.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _accessor;
        private readonly CustomUserManager<AppUser> _userManager;

        public UpdateUserCommandHandler(IMapper mapper, IWebHostEnvironment env
            ,IHttpContextAccessor accessor,CustomUserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _env = env;
            _accessor = accessor;
            _userManager = userManager;
        }
        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            AppUser user = await _userManager.Users.GetUserByIdAsync(Id,cancellationToken,AppUserHelper.AllUserIncludes());
            if (user is null) throw new UserIdNotFoundException();
            if (user.Id.ToString() != _accessor.HttpContext.User.GetUserIdFromClaim())
                throw new Authentication_UserIdNotSameWithAuthenticatedUserId();
           
            _mapper.Map(request, user);
            await ImageCheck(request, user);
            CheckRemoveLanguages(request, user);
            CheckAddLanguage(request, user);
            await _userManager.UpdateAsync(user);
            UserResponse response = _mapper.Map<UserResponse>(user);
           
            if (user.EmailConfirmed) response.Verifications.Add("Email verified");
            if (user.PhoneNumberConfirmed) response.Verifications.Add("Phone number verified");

            return response;

        }

        private async Task ImageCheck(UpdateUserCommand request, AppUser user)
        {
            if (request.ProfilPicture is not null)
            {
                if (!request.ProfilPicture.IsImageOkay(2))
                    throw new UserValidationException { ErrorMessage = "Image size too big" };
                if (!string.IsNullOrWhiteSpace(user.ProfilPicture))
                    FileHelpers.FileDelete(_env.WebRootPath, "assets/images/UserProfilePictures", user.ProfilPicture);
                user.ProfilPicture = await request.ProfilPicture
                    .FileCreate(_env.WebRootPath, "assets/images/UserProfilePictures");
            }
        }
        private static void CheckAddLanguage(UpdateUserCommand request, AppUser user)
        {
            if (request.AppUserLanguages != null && request.AppUserLanguages.Count != 0)
            {
                foreach (Guid languageId in request.AppUserLanguages)
                {
                    AppUserLanguage appUserLanguage = new()
                    {
                        LanguageId = languageId,
                        AppUser = user,
                    };
                    user.AppUserLanguages.Add(appUserLanguage);
                }
            }
            //if (!user.AppUserLanguages.Any())
            //{
            //    throw new UserLanguageValidationException()
            //    { ErrorMessage = "You must have at least 1 language" };
            //}

        }
        private static void CheckRemoveLanguages(UpdateUserCommand request, AppUser user)
        {
            if (request.DeletedAppUserLanguages != null && request.DeletedAppUserLanguages.Count != 0
                && user.AppUserLanguages != null && user.AppUserLanguages.Count != 0)
            {
                List<Guid> removableAppUserLanguageIds = new();
                request.DeletedAppUserLanguages.ForEach(languageId =>
                {
                    AppUserLanguage appUserLanguage = user.AppUserLanguages.FirstOrDefault(x => x.Id == languageId);
                    if (appUserLanguage is null) throw new UserLanguageValidationException
                    { ErrorMessage = $"Language with this Id({languageId}) doesn't exist" };
                    removableAppUserLanguageIds.Add(languageId);
                });
                user.AppUserLanguages.RemoveAll(appUserLanguage => removableAppUserLanguageIds
                .Any(remLanguageId => appUserLanguage.Id == remLanguageId));
            }
        }
    }
}
