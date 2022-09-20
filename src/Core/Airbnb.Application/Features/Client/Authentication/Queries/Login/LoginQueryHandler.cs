using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IMapper mapper,UserManager<AppUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
           _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
          
            _mapper = mapper;
        }
        public async Task<AuthenticationResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundValidationException();

            bool result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result) throw new UserNotFoundValidationException();

            string token = await _jwtTokenGenerator.GenerateTokenAsync(user);
            var authResult = _mapper.Map<AuthenticationResponse>(user);
            authResult.Token = token;
            return authResult;
        }
    }
}
