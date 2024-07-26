using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITokenRepository
    {
        Task<bool> IsRefreshTokenValid(string refreshToken);
        Task SaveToken(string userId,string refreshToken);
    }
}
