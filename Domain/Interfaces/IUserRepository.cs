using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User?> GetByUserName(string storedProcedure, object param);
        Task<bool> IsEmailUnique(string email);
        Task<bool> IsUserNameUnique(string username);
    }
}
