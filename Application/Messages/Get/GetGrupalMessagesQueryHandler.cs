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
    public class GetGrupalMessagesQueryHandler : IRequestHandler<GetGrupalMessagesQuery, ErrorOr<IReadOnlyList<MessagesByGroup>>>
    {
        private readonly IMessageRepository _repository;

        public GetGrupalMessagesQueryHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<IReadOnlyList<MessagesByGroup>>> Handle(GetGrupalMessagesQuery request, CancellationToken cancellationToken)
        {
            var grupalMessage = await _repository.GetGroupMessages(request.GroupId);
            var response = grupalMessage.ToList();
            return response;
        }
    }
}
