using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthSuccessResponse>
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly ITokenGeneratorService _tokenGenerator;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginQueryHandler(CustomUserManager<AppUser> userManager,
            ITokenGeneratorService tokenGenerator, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _signInManager = signInManager;
        }
        public async Task<AuthSuccessResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken);
            //AppUser user = await _userManager.Users.FindByEmailAsync(request.Email);
            if (user is null)   throw new UserNotFoundValidationException() 
                {ErrorMessage = "Phone number or password is incorrect." };
            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            await CheckIfSignInHappenedSuccessfully(user, result);

            //LoginResponse response = _mapper.Map<LoginResponse>(user);
            ////if (response is null) throw new Exception("Internal server error");
            List<Claim> claims = await _tokenGenerator.CreateClaims(user);
            AuthSuccessResponse response = new()
            {
                AccessToken = await _tokenGenerator.GenerateJwtAccessTokenAsync(claims),
                RefreshToken = await _tokenGenerator.GenerateRefreshTokenAsync(claims,user.Id)
            };
            return response;
        }

        private async Task CheckIfSignInHappenedSuccessfully(AppUser user, SignInResult result)
        {
            if (result.IsNotAllowed)
            {
                if (await _userManager.IsEmailConfirmedAsync(user) is false)
                    throw new User_EmailNotConfirmedException();
                if (await _userManager.IsPhoneNumberConfirmedAsync(user) is false)
                    throw new User_PhoneNumberNotConfirmedException();
            }
            if (result.IsLockedOut) throw new User_IsLockedOutException();
            if (!result.Succeeded)  throw new UserNotFoundValidationException() 
                { ErrorMessage = "Phone number or password is incorrect." };
        }
    }
}
