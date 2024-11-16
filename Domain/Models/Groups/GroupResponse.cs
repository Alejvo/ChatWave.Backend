using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Groups
{
    public record GroupResponse(
        string Id,
         string Name,
         string Description,
         int Members,
         string Image
        )
    {
        public static GroupResponse ToGroupResponse(Group group)
        {
            if (group == null) return default;
            return new GroupResponse
            (
               group.Id,
               group.Name,
               group.Description,
               group.Users.Count,
               group.Image != null ? Convert.ToBase64String(group.Image) : null
            );
        }
    }
}
