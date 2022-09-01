using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Features.User.Commands.Delete;
using Airbnb.Application.Features.User.Commands.Update;
using Airbnb.Application.Features.User.Queries.GetAll;
using Airbnb.Application.Features.User.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1
{

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
            //if (!result.Any()) throw new Exception("Internal server error");
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var result = await _mediatr.Send(new UserGetByIdQuery(id));
            if (result is null) throw new Exception("Internal server error");
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm]UpdateUserCommand command)
        {
            var result = await _mediatr.Send(command);
            if (result is null) throw new Exception("Internal server error");
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]string id)
        {
            var result = await _mediatr.Send(new DeleteUserCommand(id));
            //if (result is null) throw new Exception("Internal server error");
            return NoContent();
        }
    }
}
