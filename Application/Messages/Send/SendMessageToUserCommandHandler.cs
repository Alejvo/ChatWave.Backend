using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send
{
    public class SendMessageToUserCommandHandler : IRequestHandler<SendMessageToUserCommand>
    {
        private readonly IMessageRepository _repository;

        public SendMessageToUserCommandHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SendMessageToUserCommand request, CancellationToken cancellationToken)
        {
            var param = new { 
                MessageId = Guid.NewGuid().ToString(),
                Text = request.Text,
                SenderId=request.SenderId,
                ReceiverId = request.ReceiverId
            };
            await _repository.SendToUser(param);
        }
    }
}
