﻿using Application.Users.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Get.All
{
    public record GetAllUsersQuery():IRequest<ErrorOr<IReadOnlyList<UserResponse>>>;

}
