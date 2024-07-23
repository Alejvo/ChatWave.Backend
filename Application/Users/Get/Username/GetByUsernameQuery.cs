using Application.Users.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.GetBy.Username
{
    public record GetByUsernameQuery(string Username):IRequest<UserResponse>;

}
