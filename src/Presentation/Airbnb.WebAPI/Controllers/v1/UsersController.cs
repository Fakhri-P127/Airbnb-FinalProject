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
        private readonly IUnitOfWork _unit;
        private readonly ISender _mediator;

        public UsersController(IUnitOfWork unit,ISender mediator)
        {
            _unit = unit;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new UserGetAllQuery());
            //if (!result.Any()) throw new Exception("Internal server error");
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var result = await _mediator.Send(new UserGetByIdQuery(id));
            if (result is null) throw new Exception("Internal server error");
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm]UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is null) throw new Exception("Internal server error");
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));
            //if (result is null) throw new Exception("Internal server error");
            return NoContent();
        }
    }
}
