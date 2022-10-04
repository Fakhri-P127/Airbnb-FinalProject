using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Parameters;
using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Responses;
using Airbnb.Application.Features.Admin.RefreshTokens.Queries.GetAll;
using Airbnb.Application.Features.Admin.RefreshTokens.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    public class RefreshTokensController : BaseController
    {
        private readonly ISender _mediatr;

        public RefreshTokensController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllRefreshTokens([FromQuery] RefreshTokenParameters parameters)
        {
            List<RefreshTokenResponse> result = await _mediatr.Send(new GetAllRefreshTokensQuery(parameters));
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetRefreshTokenById([FromRoute] Guid id)
        {
            RefreshTokenResponse result = await _mediatr.Send(new GetRefreshTokenByIdQuery(id));
            return Ok(result);
        }
    }
}
