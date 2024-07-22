using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        string GetByUserName(string username);
        bool IsEmailUnique(string email);
        bool IsUserNameUnique(string username);
    }
}
