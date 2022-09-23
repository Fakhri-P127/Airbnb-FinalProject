using Airbnb.Application.Features.Client.Authentication.Commands.ForgotPassword;
using Airbnb.Application.Features.Client.Authentication.Commands.Register;
using Airbnb.Application.Features.Client.Authentication.Commands.ResetPassword;
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
            return Ok(result);
        }

        [HttpGet("login")]
        // object i string email,string password kimi gondersen swaggerde de ishleyecek.
        public async Task<IActionResult> Login(string email, string password, bool rememberme)
        {
            var result = await _mediatr.Send(new LoginQuery(email, password, rememberme));
            return Ok(result); ;
        }
        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return Ok("Successfuly logged out");
        //}
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _mediatr.Send(command);
            return NoContent();
        }
        [HttpGet("resetPassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            return Ok(new { token, email });
        }
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            await _mediatr.Send(command);
            return NoContent();
        }
    }
}
