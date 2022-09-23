using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Features.Client.User.Commands.Delete;
using Airbnb.Application.Features.Client.User.Commands.Update;
using Airbnb.Application.Features.Client.User.Queries.GetAll;
using Airbnb.Application.Features.Client.User.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
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
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediatr.Send(new UserGetAllQuery());
            return Ok(result);
        }
        [HttpGet("usersWithoutProfilePicture")]// bunu filterlemek lazimdi, bele sehvdi
        public async Task<IActionResult> GetUsersWithoutProfilePicture()
        {
            var query = new UserGetAllQuery
            {
                Expression = x => x.ProfilPicture == null
            };
            List<UserResponse> result = await _mediatr.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]

        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new UserGetByIdQuery(id));
            if (result is null) throw new Exception("Internal server error");
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand command)
        {
            var result = await _mediatr.Send(command);
            if (result is null) throw new Exception("Internal server error");
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}
