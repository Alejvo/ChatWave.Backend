﻿using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Users.Add
{
    public record AddUserToGroupCommand(string groupId, string userId) : IRequest<ErrorOr<Unit>>;
}
