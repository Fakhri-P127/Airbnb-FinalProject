using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.Application.Features.Client.User.Commands.Update
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
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.RouteId,null,FileHelpers.AllUserRelationIncludes());
            if (user is null) throw new UserNotFoundValidationException() { ErrorMessage = "User with this Id doesn't exist." };
            _unit.UserRepository.Update(user);
            _mapper.Map(request, user);
            //user.ModifiedAt = DateTime.UtcNow;
            await ImageCheck(request, user);
            CheckRemoveLanguages(request, user);
            CheckAddLanguage(request, user);
            await _unit.SaveChangesAsync();
            UserResponse response = _mapper.Map<UserResponse>(user);
            // birinci gender verence deyer null olur, gel repo ile genderi tap ve responsedaki gendere beraber et eger nulldisa
            
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
        private void CheckAddLanguage(UpdateUserCommand request, AppUser user)
        {
            if (request.AppUserLanguages != null && request.AppUserLanguages.Count != 0)
            {
                foreach (Guid languageId in request.AppUserLanguages)
                {
                    // Amenity amenity = _unit.AmenityRepoGetById edib add etmek olar
                    AppUserLanguage appUserLanguage = new()
                    {
                        LanguageId = languageId,
                        AppUser = user,
                    };
                    user.AppUserLanguages.Add(appUserLanguage);
                }
            }
            if (!user.AppUserLanguages.Any())
            {
                throw new UserLanguageValidationException()
                { ErrorMessage = "You must have at least 1 language" };
            }

        }
        private void CheckRemoveLanguages(UpdateUserCommand request, AppUser user)
        {
            if (request.DeletedAppUserLanguages != null && request.DeletedAppUserLanguages.Count != 0)
            {
                List<Guid> removableAppUserLanguageIds = new();
                request.DeletedAppUserLanguages.ForEach(languageId =>
                {
                    var appUserLanguage = user.AppUserLanguages.FirstOrDefault(x => x.Id == languageId);
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
