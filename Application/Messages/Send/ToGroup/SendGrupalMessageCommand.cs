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
    public record SendGrupalMessageCommand(
            string Text,
            string SenderId,
            string GroupId,
            DateTime SentAt
        ) : IRequest<ErrorOr<MessageRequest>>;

}
