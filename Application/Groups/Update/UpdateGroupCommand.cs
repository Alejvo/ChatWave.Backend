using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Update
{
    public record UpdateGroupCommand(
            string Id,
            string Name,
            string Description,
            IFormFile Image
        ) :IRequest<ErrorOr<Unit>>;
}
