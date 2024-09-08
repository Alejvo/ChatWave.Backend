using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Friends
{
    public record AddFriendCommand(string userId,string friendId):IRequest<ErrorOr<Unit>>;

}
