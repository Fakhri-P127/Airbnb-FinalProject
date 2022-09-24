using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Exceptions.AuthenticationExceptions;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.User.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _accessor;

        public DeleteUserCommandHandler(IWebHostEnvironment env, IHttpContextAccessor accessor, CustomUserManager<AppUser> userManager)
        {
            _env = env;
            _accessor = accessor;
            _userManager = userManager;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.Users.GetUserByIdAsync(request.Id,cancellationToken,
                AppUserHelper.AllUserIncludes());
            if (user is null) throw new UserIdNotFoundException();
            if (user.Id.ToString() != _accessor.HttpContext.User.GetUserIdFromClaim())
                throw new Authentication_UserIdNotSameWithAuthenticatedUserId();

            if (string.IsNullOrWhiteSpace(user.ProfilPicture))
                FileHelpers.FileDelete(_env.WebRootPath, "assets/images/UserProfilePictures", user.ProfilPicture);
            await _userManager.DeleteAsync(user);
            return await Task.FromResult(Unit.Value);
        }
    }
}
