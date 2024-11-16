using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Update
{
    public record UpdateUserCommand
        (
            string Id,
            string FirstName,
            string LastName,
            string Email,
            string Password,
            string UserName,
            DateTime Birthday,
            IFormFile ProfileImage

        ) : IRequest<ErrorOr<Unit>>;
}
