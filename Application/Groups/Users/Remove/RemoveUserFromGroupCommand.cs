using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Users.Remove
{
    public record RemoveUserFromGroupCommand(string groupId, string userId) : IRequest<ErrorOr<Unit>>;
}
