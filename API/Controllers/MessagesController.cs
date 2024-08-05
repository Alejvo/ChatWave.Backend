using Application.Messages.Get;
using Application.Messages.Send.ToGroup;
using Application.Messages.Send.ToUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace API.Controllers
{
    [Route("api/message")]
    //[Authorize]
    public class MessagesController : ApiController
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("send/group")]
        public async Task<IActionResult> SendToGroup(SendGrupalMessageCommand command)
        {
            await _mediator.Send(command);
            return Ok("Send");
        }

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
        
        [HttpPost]
        [Route("send/user")]
        public async Task<IActionResult> SendToUser(SendMessageToUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Send");
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetUserMessages([FromQuery] string receiver, [FromQuery] string sender)
        {
            var userMessages = await _mediator.Send(new GetUserMessagesQuery(receiver,sender));
            return userMessages.Match(
                res=>Ok(res),
                errors=>Problem(errors)
                );
        }
        
    }
}
