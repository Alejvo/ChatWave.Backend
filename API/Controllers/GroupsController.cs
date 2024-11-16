
using Application.Groups.Create;
using Application.Groups.Delete;
using Application.Groups.Get.All;
using Application.Groups.Update;
using Application.Groups.Users.Add;
using Application.Groups.Users.Remove;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Groups.Get.Id;
using Application.Groups.Get.Name;

namespace API.Controllers
{
    [Route("api/groups")]
   [Authorize]
    public class GroupsController : ApiController
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
            return groups.Match(
                res=>Ok(res),
                errors=>Problem(errors)
                );
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetGroupById(string id)
        {
            var group = await _mediator.Send(new GetGroupByIdQuery(id));
            return group.Match(
                res => Ok(res),
                errors => Problem(errors)
                );
        }
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetGroupByName(string name)
        {
            var group = await _mediator.Send(new GetGroupsByNameQuery(name));
            return group.Match(
                res => Ok(res),
                errors => Problem(errors)
                );
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateGroup([FromForm] CreateGroupCommand command)
        {
            var createdGroup = await _mediator.Send(command);
            return createdGroup.Match(
                res => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateGroup([FromForm] UpdateGroupCommand command)
        {
            var updatedGroup= await _mediator.Send(command);
            return updatedGroup.Match(
                res=>NoContent(),
                errors=>Problem(errors)
                );
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            var deletedGroup = await _mediator.Send(new DeleteGroupCommand(id));
            return deletedGroup.Match(
                res => NoContent(),
                errors => Problem(errors)
                );
        }
        
        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> AddUserToGroup([FromBody] AddUserToGroupCommand command)
        {
            var user = await _mediator.Send(command);
            return user.Match(
                res => NoContent(),
                errors => Problem(errors)
                );
        }
        
        [HttpDelete]
        [Route("remove-user")]
        public async Task<IActionResult> RemoveUserFromGroup([FromBody] RemoveUserFromGroupCommand command)
        {
            var user = await _mediator.Send(command);
            return user.Match(
                res => NoContent(),
                errors => Problem(errors)
                );
        }
        
    }
}
