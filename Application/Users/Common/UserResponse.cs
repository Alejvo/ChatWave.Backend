using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Common
{
    public record UserResponse
    (
            string FullName,
            string UserName,
            DateTime Birthday
    )
    {
        public static UserResponse ToUserResponse(User user)
        {
            return new UserResponse
            (
               $"{user.FirstName} {user.LastName}",
               user.UserName,
                user.Birthday
            );
        }
    }

}
