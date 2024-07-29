using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send
{
    public class SendGrupalMessageCommandHandler:IRequestHandler<SendGrupalMessageCommand>
    {
        private readonly IMessageRepository _repository;

        public SendGrupalMessageCommandHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SendGrupalMessageCommand request, CancellationToken cancellationToken)
        {
            var param = new { 
                MessageId = Guid.NewGuid().ToString(),
                Text =request.Text,
                SenderId = request.SenderId,
                GroupId=request.GroupId};
            await _repository.SendToGroup(param);
        }
    }
}
