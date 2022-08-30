using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Features.Authentication.Common;
using Airbnb.Domain.Entities.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly UserManager<AppUser> _userManager;
  
private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUnitOfWork unit,IMapper mapper,UserManager<AppUser> userManager)
        {
           _userManager = userManager;
           _unit = unit;
            _mapper = mapper;
        }
        public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundValidationException();

            bool result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result) throw new UserNotFoundValidationException();

            string token = await _unit.JwtTokenGenerator.GenerateTokenAsync(user);
            var authResult = _mapper.Map<AuthenticationResult>(user);
            authResult.Token = token;
            return authResult;
        }
    }
}
