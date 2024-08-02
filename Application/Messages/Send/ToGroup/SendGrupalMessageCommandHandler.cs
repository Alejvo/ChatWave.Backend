using Domain.Interfaces;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send.ToGroup
{
    public class SendGrupalMessageCommandHandler : IRequestHandler<SendGrupalMessageCommand,ErrorOr<Unit>>
    {
        private readonly IMessageRepository _repository;

        public SendGrupalMessageCommandHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Unit>> Handle(SendGrupalMessageCommand request, CancellationToken cancellationToken)
        {
            var param = new
            {
                MessageId = Guid.NewGuid().ToString(),
                request.Text,
                request.SenderId,
                request.GroupId
            };
            await _repository.SendToGroup(param);
            return Unit.Value;
        }
    }
}
