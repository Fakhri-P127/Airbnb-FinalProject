using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.Application.Features.Client.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly CustomUserManager<AppUser> _userManager;

        public ResetPasswordCommandHandler(CustomUserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {   
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)  throw new UserNotFoundValidationException() 
            {
                ErrorMessage = "User with this email doesn't exist"
            };
        
            IdentityResult resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!resetPassResult.Succeeded)
            {
                throw new UserValidationException()
                {
                    ErrorMessage = "One or more validation errors occured while changing the password.",
                    ErrorMessages = resetPassResult.Errors
                };
            }
            return await Task.FromResult(Unit.Value);
        }
    }
}
