using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Delete
{
    public record DeleteGroupCommand(string Id):IRequest<ErrorOr<Unit>>;
}
