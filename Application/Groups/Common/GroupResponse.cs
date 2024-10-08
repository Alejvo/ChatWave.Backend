﻿using Application.Users.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Common;

public record GroupResponse(
    string Id,
     string Name,
     string Description,
     IEnumerable<UserResponse?> Users
    )
{
    public static GroupResponse ToGroupResponse(Group group)
    {
        if(group.Users == null)
        {
            group.Users = [];
        }
        return new GroupResponse
        (
            group.Id,
           group.Name,
           group.Description,
          group.Users.Select(user => UserResponse.ToUserResponse(user))
        );
    }
}

