using Airbnb.Application.Contracts.v1.Client.User.Parameters;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Features.Client.User.Commands.Delete;
using Airbnb.Application.Features.Client.User.Commands.Update;
using Airbnb.Application.Features.Client.User.Queries.GetAll;
using Airbnb.Application.Features.Client.User.Queries.GetById;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    public class UsersController : BaseController
    {
        private readonly ISender _mediatr;

        public UsersController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Gets all the users. You can filter it to find the data you want. 
        /// Languages codes that are avaliable: [en,az,ru,tr,jpn]. 
        /// Writing anything other than these will be automatically ignored.
        /// </summary>
        /// <param name="parameters"></param>
        /// <response code="200">List of users returned successfuly</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <returns>List of users</returns>
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserParameters parameters)
        {
            List<UserResponse> result = await _mediatr.Send(new UserGetAllQuery(parameters));
            Log.Information($"Returned user with count:{parameters.PageSize}");
            return Ok(result);
        }
        /// <summary>
        /// Gets a user by given Id
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <response code="200">User returned successfuly</response>
        /// <response code="404">User with given Id doesn't exist</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            UserResponse result = await _mediatr.Send(new UserGetByIdQuery(id));
            Log.Information($"Returned user by {id} id");
            return Ok(result);
        }
        /// <summary>
        /// Updates user
        /// </summary>
        /// <response code="200">User updated successfuly</response>
        /// <response code="400">Something went wrong with image validation</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <response code="404">User with given Id doesn't exist</response>
        /// <response code="409">Authenticated user Id is different from given route Id</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Guest")]

        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand command)
        {
            UserResponse result = await _mediatr.Send(command);
            Log.Information("Updated user successfuly");
            return Ok(result);
        }
        /// <summary>
        /// Deletes the user
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <response code="204">User deleted successfuly</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <response code="404">User with given Id doesn't exist</response>
        /// <response code="409">Authenticated user Id is different from given route Id</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Guest,Moderator,Admin")]
        [ProducesResponseType(204)]// default response status kodu 200 idi, onu deyishmek uchun yazdim.
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteUserCommand(id));
            Log.Information("Deleted user successfuly");
            return NoContent();
        }
    }
}
