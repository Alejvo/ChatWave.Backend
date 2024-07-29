using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Get
{
    public record GetUserMessagesQuery(string UserId): IRequest<IEnumerable<MessageResponse>>;

}
