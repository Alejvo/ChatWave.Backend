using Domain.Interfaces;
using Domain.Models;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Get
{
    public class GetUserMessagesQueryHandler : IRequestHandler<GetUserMessagesQuery, ErrorOr<IReadOnlyList<MessageResponse>>>
    {
        private readonly IMessageRepository _repository;

        public GetUserMessagesQueryHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<IReadOnlyList<MessageResponse>>>Handle(GetUserMessagesQuery request, CancellationToken cancellationToken)
        {
            var userMessages = await _repository.GetUserMessages(request.UserId);
            var response = userMessages.ToList();
            return response;
        }
    }
}
