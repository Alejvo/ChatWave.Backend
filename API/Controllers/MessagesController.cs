using Application.Messages.Get;
using Application.Messages.Send.ToGroup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/message")]
    [Authorize]
    public class MessagesController : ApiController
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /*
        [HttpPost]
        [Route("send/group")]
        public async Task<IActionResult> SendToGroup(SendGrupalMessageCommand command)
        {
            await _mediator.Send(command);
            return Ok("Send");
        }*/

        [HttpGet]
        [Route("get/group/{id}")]
        public async Task<IActionResult> GetGrupalMessages([FromRoute] string id)
        {
            var grupalMessages = await _mediator.Send(new GetGrupalMessagesQuery(id));
            return grupalMessages.Match(
                res => Ok(res),
                errors => Problem(errors)
                );
        }
        /*
        [HttpPost]
        [Route("send/user")]
        public async Task<IActionResult> SendToUser(SendMessageToUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Send");
        }*/

        [HttpGet]
        [Route("get/user/{id}")]
        public async Task<IActionResult> GetUserMessages([FromRoute] string id)
        {
            var userMessages = await _mediator.Send(new GetUserMessagesQuery(id));
            return userMessages.Match(
                res=>Ok(res),
                errors=>Problem(errors)
                );
        }
        
    }
}
