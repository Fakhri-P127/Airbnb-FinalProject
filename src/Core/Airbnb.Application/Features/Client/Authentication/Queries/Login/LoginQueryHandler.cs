using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Authentication.CustomFrameworkClasses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResponse>
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IMapper mapper,CustomUserManager<AppUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator,SignInManager<AppUser> signInManager)
        {
           _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<AuthenticationResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundValidationException();
            //if (!user.EmailConfirmed) throw new User_EmailNotConfirmedException();

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            await CheckIfSignInHappenedSuccessfully(user, result);

            var authResult = _mapper.Map<AuthenticationResponse>(user);
            if (authResult is null) throw new Exception("Internal server error");
            authResult.Token = await _jwtTokenGenerator.GenerateTokenAsync(user);

            //var sa = _signInManager.Context.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier);
            //foreach (var claim in _signInManager.Context.User.Claims)
            //{
            //    authResult.Verifications.Add($"Type: {claim.Type} Value: {claim.Value}");
            //}
            return authResult;
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
            if (!result.Succeeded) throw new UserNotFoundValidationException();
        }
    }
}
