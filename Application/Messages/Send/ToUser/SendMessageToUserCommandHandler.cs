using Application.Hubs;
using Domain.Interfaces;
using Domain.Models.Messages;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send.ToUser
{
    public class SendMessageToUserCommandHandler : IRequestHandler<SendMessageToUserCommand,ErrorOr<MessageRequest>>
    {
        private readonly IMessageRepository _repository;
        private readonly IHubContext<ChatHub> _hubContext;

        public SendMessageToUserCommandHandler(IMessageRepository repository,IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public async Task<ErrorOr<MessageRequest>> Handle(SendMessageToUserCommand request, CancellationToken cancellationToken)
        {
            var message = new MessageRequest
            {
                MessageId = Guid.NewGuid().ToString(),
                Text=request.Text,
                SenderId = request.SenderId,
                ReceiverId= request.ReceiverId,
                SentAt = DateTime.UtcNow
            };
            await _repository.SendToUser(message);

            return message;
        }
    }
}
