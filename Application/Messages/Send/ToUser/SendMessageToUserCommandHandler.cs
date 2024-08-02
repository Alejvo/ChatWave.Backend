using Domain.Interfaces;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send.ToUser
{
    public class SendMessageToUserCommandHandler : IRequestHandler<SendMessageToUserCommand,ErrorOr<Unit>>
    {
        private readonly IMessageRepository _repository;

        public SendMessageToUserCommandHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Unit>> Handle(SendMessageToUserCommand request, CancellationToken cancellationToken)
        {
            var param = new
            {
                MessageId = Guid.NewGuid().ToString(),
                request.Text,
                request.SenderId,
                request.ReceiverId
            };
            await _repository.SendToUser(param);
            return Unit.Value;
        }
    }
}
