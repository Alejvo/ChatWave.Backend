using Domain.Interfaces;
using Domain.Models.Messages;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send.ToGroup
{
    public class SendGrupalMessageCommandHandler : IRequestHandler<SendGrupalMessageCommand,ErrorOr<MessageRequest>>
    {
        private readonly IMessageRepository _repository;

        public SendGrupalMessageCommandHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<MessageRequest>> Handle(SendGrupalMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new MessageRequest
            {
                MessageId = Guid.NewGuid().ToString(),
                Text= request.Text,
                SenderId=request.SenderId,
                ReceiverId= request.GroupId,
                SentAt = DateTime.UtcNow
            };
            await _repository.SendToGroup(
                new
                {
                    MessageId = message.MessageId,
                    Text = message.Text,
                    SenderId = message.SenderId,
                    GroupId = message.ReceiverId,
                    SentAt = message.SentAt
                });
            return message;
        }
    }
}
