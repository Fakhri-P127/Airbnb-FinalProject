using Airbnb.Application.Features.Client.Authentication.Commands.Register;
using Airbnb.Application.Features.Client.Authentication.Queries.Login;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.WebAPI.Controllers.v1.Base;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    public class AuthenticationController : BaseController
    {
        private readonly ISender _mediatr;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationController(ISender mediatr, UserManager<AppUser> userManager)
        {
            _mediatr = mediatr;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterCommand command)
        {
            var result = await _mediatr.Send(command);
            // email verify

            //var code = _userManager.GenerateEmailConfirmationTokenAsync(user);

            //var link = Url.Action()
            if (result is null) throw new Exception();

            return Ok(result);
        }

        [HttpGet("login")]
        // object i string email,string password kimi gondersen swaggerde de ishleyecek.
        public async Task<IActionResult> Login( LoginQuery query)
        {
            var result = await _mediatr.Send(query);
            if (result is null) throw new Exception();
            return Ok(result); ;
        }
    }
}
