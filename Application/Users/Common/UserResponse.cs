using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Common
{
    public record UserResponse
    (
            string Id,
            string FullName,
            string UserName,
            int Age,
            List<UserGroup> Groups,
            List<Friend> Friends
    )
    {
        public static UserResponse ToUserResponse(User user)
        {
            if (user == null) return default;
            return new UserResponse
            (
               user.Id,
               $"{user.FirstName} {user.LastName}",
               user.UserName,
               GetAge(user.Birthday),
               user.Groups,
               user.Friends
            );
        }

        private static int GetAge(DateTime birthday)
        {
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;

            if(birthday.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }

}
