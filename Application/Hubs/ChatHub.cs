using Application.Messages.Send.ToGroup;
using Application.Messages.Send.ToUser;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessageToUser(string connectionId,string sender, string receiver, string message)
        {
            await _mediator.Send(new SendMessageToUserCommand(message,sender,receiver));
            await Clients.Client(connectionId).SendAsync("ReceiveMessage",sender,message);
        }
        public async Task SendMessageToGroup(string group, string sender, string message)
        {
            await _mediator.Send(new SendGrupalMessageCommand(message,sender,group));
            await Clients.Group(group).SendAsync("ReceiveMessage",sender,message);
        }

        public async Task AddToGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,group);
        }

        public async Task RemoveFromGroup(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId,group);
        }
    }
}
