using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Messages;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Get
{
    public class GetUserMessagesQueryHandler : IRequestHandler<GetUserMessagesQuery, ErrorOr<IReadOnlyList<MessagesBySender>>>
    {
        private readonly IMessageRepository _repository;

        public GetUserMessagesQueryHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<IReadOnlyList<MessagesBySender>>>Handle(GetUserMessagesQuery request, CancellationToken cancellationToken)
        {
            var userMessages = await _repository.GetUserMessages(request.ReceiverId,request.SenderId);
            var response = userMessages.ToList();
            return response;
        }
    }
}
