using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send.ToUser
{
    public record SendMessageToUserCommand(
            string Text,
            string SenderId,
            string ReceiverId,
        DateTime SentAt
        ) : IRequest<ErrorOr<Unit>>;
}
