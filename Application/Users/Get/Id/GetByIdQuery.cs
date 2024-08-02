using Application.Users.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.GetBy.Id
{
    public record GetByIdQuery(string Id):IRequest<ErrorOr<UserResponse>>;

}
