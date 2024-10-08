﻿using ErrorOr;
using MediatR;
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
            string Description
        ):IRequest<ErrorOr<Unit>>;
}
