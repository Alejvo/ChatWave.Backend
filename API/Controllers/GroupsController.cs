using Application.Groups.Create;
using Application.Groups.Delete;
using Application.Groups.Get.All;
using Application.Groups.Get.Id;
using Application.Groups.Update;
using Application.Groups.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _mediator.Send(new GetGroupsQuery());
            return Ok(groups);
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetGroupById(string id)
        {
            var group = await _mediator.Send(new GetGroupByIdQuery(id));
            return Ok(group);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            await _mediator.Send(new DeleteGroupCommand(id));
            return Ok("Deleted");
        }

        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> AddUserToGroup([FromBody] AddUserToGroupCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("remove-user")]
        public async Task<IActionResult> RemoveUserFromGroup([FromBody] RemoveUserFromGroupCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
