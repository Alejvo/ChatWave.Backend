using Application.Messages.Get.Group;
using Application.Messages.Get.User;
using Application.Messages.Send.ToGroup;
using Application.Messages.Send.ToUser;
using Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hubs
{
    [Authorize]
    public class ChatHub:Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"ConnId:{Context.ConnectionId}, User:{Context.UserIdentifier}");
            return base.OnConnectedAsync();
        }

        public async Task SendMessageToUser(string receiver,string sender,string message)
        {
            var newMessage = await _mediator.Send(new SendMessageToUserCommand(message,sender,receiver));
            await Clients.User(receiver).SendAsync("ReceiveMessage",newMessage.Value);
            await Clients.User(sender).SendAsync("ReceiveMessage",newMessage.Value);
        }
        public async Task GetUserMessages(string receiver,string sender)
        {
            var messages = await _mediator.Send(new GetUserMessagesQuery(receiver,sender));
            await Clients.Caller.SendAsync("ReceiveMessageHistory",messages.Value);
        }

        public async Task GetGroupMessages(string group, string user)
        {
            var messages = await _mediator.Send(new GetGrupalMessagesQuery(user,group));
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.Caller.SendAsync("ReceiveMessageHistory", messages.Value);
        }
        public async Task SendMessageToGroup(string group, string sender, string message)
        {
 
                var newMessage = await _mediator.Send(new SendGrupalMessageCommand(message, sender, group, DateTime.Now));
                await Clients.Group(group).SendAsync("ReceiveMessage", newMessage.Value);
            

        }

    }
}
