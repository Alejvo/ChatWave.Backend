using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User?> GetByUserName(string username);
        Task<bool> IsEmailUnique(string email);
        Task<bool> IsUserNameUnique(string username);
        Task<User> LoginUser(string email,string password);
    }
}
