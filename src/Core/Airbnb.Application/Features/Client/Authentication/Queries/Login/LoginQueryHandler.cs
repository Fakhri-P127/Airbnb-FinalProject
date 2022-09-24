using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
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
        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundValidationException();
            //if (!user.EmailConfirmed) throw new User_EmailNotConfirmedException();

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            await CheckIfSignInHappenedSuccessfully(user, result);

            LoginResponse response = _mapper.Map<LoginResponse>(user);
            if (response is null) throw new Exception("Internal server error");
            response.Token = await _jwtTokenGenerator.GenerateTokenAsync(user);

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
            if (!result.Succeeded) throw new UserNotFoundValidationException();
        }
    }
}
