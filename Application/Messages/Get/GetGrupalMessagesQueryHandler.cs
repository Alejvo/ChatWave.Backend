using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Get
{
    public class GetGrupalMessagesQueryHandler : IRequestHandler<GetGrupalMessagesQuery, IEnumerable<MessageResponse>>
    {
        private readonly IMessageRepository _repository;

        public GetGrupalMessagesQueryHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MessageResponse>> Handle(GetGrupalMessagesQuery request, CancellationToken cancellationToken)
        {
            var grupalMessage = await _repository.GetGroupMessages(request.GroupId);
            return grupalMessage;
        }
    }
}
