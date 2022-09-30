using Airbnb.Application.Contracts.v1.Client.User.Parameters;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Features.Client.User.Commands.Delete;
using Airbnb.Application.Features.Client.User.Commands.Update;
using Airbnb.Application.Features.Client.User.Queries.GetAll;
using Airbnb.Application.Features.Client.User.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    //[SkipMyGlobalResourceFilter]
    public class UsersController : BaseController
    {
        private readonly ISender _mediatr;

        public UsersController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserParameters parameters)
        {
            List<UserResponse> result = await _mediatr.Send(new UserGetAllQuery(parameters));
            return Ok(result);
        }
        //[HttpGet("usersWithoutProfilePicture")]// bunu filterlemek lazimdi, bele sehvdi
        //[ResponseCache(Duration = 30)]
        //public async Task<IActionResult> GetUsersWithoutProfilePicture([FromQuery] UserParameters)
        //{
        //    var query = new UserGetAllQuery
        //    {

        //        Expression = x => x.ProfilPicture == null
        //    };
        //    List<UserResponse> result = await _mediatr.Send(query);
        //    return Ok(result);
        //}
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            UserResponse result = await _mediatr.Send(new UserGetByIdQuery(id));
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand command)
        {
            UserResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Guest,Moderator,Admin")]

        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}
