using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Features.Client.Authentication.Commands.ForgotPassword;
using Airbnb.Application.Features.Client.Authentication.Commands.GenerateRefreshToken;
using Airbnb.Application.Features.Client.Authentication.Commands.Register;
using Airbnb.Application.Features.Client.Authentication.Commands.ResetPassword;
using Airbnb.Application.Features.Client.Authentication.Commands.RevokeRefreshToken;
using Airbnb.Application.Features.Client.Authentication.Commands.SendConfirmationEmail;
using Airbnb.Application.Features.Client.Authentication.Queries.ConfirmEmail;
using Airbnb.Application.Features.Client.Authentication.Queries.Login;
using Airbnb.Application.Filters.ActionFilters;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    [SkipMyGlobalFilter]
    [AllowAnonymous]
    [EnsureUserIsNotAuthenticatedActionFilter]
    [Route("api/v1/[controller]/[action]")]
    public class AuthenticationController : BaseController
    {
        private readonly ISender _mediatr;

        public AuthenticationController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterCommand command)
        {
            RegisterResponse result = await _mediatr.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            AuthSuccessResponse result = await _mediatr.Send(query);
            return Ok(result); ;
        }
        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return Ok();
        //}

        /// <summary>
        /// Creates a new JWT Access token and a Refresh Token 
        /// </summary>
        /// <param name="command">Command for creating a new refresh token</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] CreateRefreshTokenCommand command)
        {
            AuthSuccessResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _mediatr.Send(command);
            return NoContent();
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            return Ok(new { token, email });
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            await _mediatr.Send(command);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand command)
        {
            await _mediatr.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> SendConfirmationEmail([FromQuery] SendConfirmationEmailQuery query)
        {
            await _mediatr.Send(query);
            return NoContent();
        }
    }
}
