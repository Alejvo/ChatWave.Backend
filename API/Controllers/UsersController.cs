using Application.Users.Common;
using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.Get.All;
using Application.Users.GetBy.Id;
using Application.Users.GetBy.Username;
using Application.Users.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById([FromRoute] string id)
        {
            var user = await _mediator.Send(new GetByIdQuery(id));
            return Ok(user);
        }

        [HttpGet]
        [Route("username/{username}")]
        public async Task<ActionResult<UserResponse>> GetUserByUsername([FromRoute] string username)
        {
            var user = await _mediator.Send(new GetByUsernameQuery(username));
            return Ok(user);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Updated");

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return Ok("Deleted");
        }
    }
}
