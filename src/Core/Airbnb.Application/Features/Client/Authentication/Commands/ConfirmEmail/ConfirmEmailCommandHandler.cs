using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.Application.Features.Client.Authentication.Queries.ConfirmEmail
{
    public class ConfirmEmailCommandHandler:IRequestHandler<ConfirmEmailCommand>
    {
        private readonly CustomUserManager<AppUser> _userManager;

        public ConfirmEmailCommandHandler(CustomUserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundValidationException()
            { ErrorMessage = "User with this email doesn't exist" };
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                throw new UserValidationException()
                {
                    ErrorMessage = "One or more validation errors occured while confirming email.",
                    ErrorMessages = result.Errors
                };
            }
            return await Task.FromResult(Unit.Value);
        }
    }
}
