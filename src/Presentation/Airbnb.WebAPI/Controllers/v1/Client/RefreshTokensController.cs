using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Parameters;
using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Responses;
using Airbnb.Application.Contracts.v1.Client.Authentication.Responses;
using Airbnb.Application.Features.Admin.RefreshTokens.Queries.GetAll;
using Airbnb.Application.Features.Admin.RefreshTokens.Queries.GetById;
using Airbnb.Application.Features.Client.Authentication.Commands.GenerateRefreshToken;
using Airbnb.Application.Features.Client.Authentication.Commands.RevokeRefreshToken;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    public class RefreshTokensController : BaseController
    {
        private readonly ISender _mediatr;

        public RefreshTokensController(ISender mediatr)
        {
            _mediatr = mediatr;
        }
        /// <summary>
        /// Gets all the refresh tokens
        /// </summary>
        /// <param name="parameters"></param>
        /// <response code="200">Gets all the refresh tokens</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllRefreshTokens([FromQuery] RefreshTokenParameters parameters)
        {
            List<RefreshTokenResponse> result = await _mediatr.Send(new GetAllRefreshTokensQuery(parameters));
            return Ok(result);
        }
        /// <summary>
        /// Gets the refresh token by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetRefreshTokenById([FromRoute] Guid id)
        {
            RefreshTokenResponse result = await _mediatr.Send(new GetRefreshTokenByIdQuery(id));
            return Ok(result);
        }
    
        /// <summary>
        /// Revokes a refresh token
        /// </summary>
        /// <param name="id">Id of the refresh token</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> RevokeRefreshToken([FromRoute] Guid id)
        {
            await _mediatr.Send(new RevokeRefreshTokenCommand(id));
            return NoContent();
        }
    }
}
