using Airbnb.Application.Features.Client.Authentication.Commands.Register;
using Airbnb.Application.Features.Client.Authentication.Queries.Login;
using Airbnb.Application.Filters.ActionFilters;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    [SkipMyGlobalResourceFilter]
    public class AuthenticationController : BaseController
    {
        private readonly ISender _mediatr;
      

        public AuthenticationController(ISender mediatr)
        {
            _mediatr = mediatr;
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
