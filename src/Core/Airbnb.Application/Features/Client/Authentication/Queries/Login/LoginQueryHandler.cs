using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Features.Client.Authentication.Common;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUnitOfWork unit,IMapper mapper,UserManager<AppUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
           _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundValidationException();

            bool result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result) throw new UserNotFoundValidationException();

            string token = await _jwtTokenGenerator.GenerateTokenAsync(user);
            var authResult = _mapper.Map<AuthenticationResult>(user);
            authResult.Token = token;
            return authResult;
        }
    }
}
